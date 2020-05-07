using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using ICSharpCode.NRefactory.CSharp;
using JetBrains.Application.Settings;
using JetBrains.Core;
using JetBrains.Diagnostics;
using JetBrains.DocumentModel;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon.CSharp.Errors;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Feature.Services.SelectEmbracingConstruct;
using JetBrains.ReSharper.I18n.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.TreeBuilder;
using JetBrains.Text;
using JetBrains.Util.Logging;
using Logger = JetBrains.ReSharper.Plugins.Spring.Util.Logger;

namespace JetBrains.ReSharper.Plugins.Spring
{
    internal class SpringParser : IParser
    {
        private readonly ILexer _lexer;

        public SpringParser(ILexer lexer)
        {
            _lexer = lexer;
        }

        public IFile ParseFile()
        {
            using (var def = Lifetime.Define())
            {
                var builder = new PsiBuilder(_lexer, SpringFileNodeType.Instance, new TokenFactory(), def.Lifetime);

                var lexer = new ToylangLexer(new AntlrInputStream(_lexer.Buffer.GetText()));
                var parser = new ToylangParser(new CommonTokenStream(lexer));

                new MyVisitor(builder).Visit(parser.file());

                var file = (IFile) builder.BuildTree();
                return file;
            }
        }


        private class MyVisitor : ToylangBaseVisitor<Unit>
        {
            private readonly PsiBuilder _psiBuilder;

            public MyVisitor(PsiBuilder psiBuilder)
            {
                _psiBuilder = psiBuilder;
            }

            private int BeginCreatingNode(int begin)
            {
                _psiBuilder.ResetCurrentLexeme(begin, begin);
                return _psiBuilder.Mark();
            }

            private void EndCreatingNode(int mark, int end, CompositeNodeType nodeType)
            {
                _psiBuilder.ResetCurrentLexeme(end, end);
                _psiBuilder.Done(mark, nodeType, null);
            }

            private void CreateNode(ParserRuleContext context, int begin, int end, CompositeNodeType nodeType)
            {
                var mark = BeginCreatingNode(begin);

                base.VisitChildren(context);

                EndCreatingNode(mark, end, nodeType);
            }

            private static Interval GetInterval(ParserRuleContext context)
            {
                Assertion.Assert(context.Start != null, "start token must be not null");
                return context.Stop == null
                    ? new Interval(context.Start.TokenIndex, context.Start.TokenIndex)
                    : context.SourceInterval;
            }


            public override Unit VisitFile(ToylangParser.FileContext context)
            {
                var interval = GetInterval(context);
                // start file from begin of the file
                CreateNode(context, 0, interval.b, SpringFileNodeType.Instance);
                return Unit.Instance;
            }


            public override Unit VisitStmtFunction(ToylangParser.StmtFunctionContext context)
            {
                var interval = GetInterval(context);
                CreateNode(context, interval.a, interval.b, SpringCompositeNodeType.BLOCK);
                return Unit.Instance;
            }
        }
    }

    [DaemonStage]
    class SpringDaemonStage : DaemonStageBase<SpringFile>
    {
        protected override IDaemonStageProcess CreateDaemonProcess(IDaemonProcess process,
            DaemonProcessKind processKind, SpringFile file,
            IContextBoundSettingsStore settingsStore)
        {
            return new SpringDaemonProcess(process, file);
        }

        internal class SpringDaemonProcess : IDaemonStageProcess
        {
            private readonly SpringFile myFile;
            public SpringDaemonProcess(IDaemonProcess process, SpringFile file)
            {
                myFile = file;
                DaemonProcess = process;
            }

            public void Execute(Action<DaemonStageResult> committer)
            {
                var highlightings = new List<HighlightingInfo>();
                foreach (var treeNode in myFile.Descendants())
                {
                    if (treeNode is PsiBuilderErrorElement error)
                    {
                        var range = error.GetDocumentRange();
                        highlightings.Add(new HighlightingInfo(range,
                            new CSharpSyntaxError(error.ErrorDescription, range)));
                    }
                }

                var result = new DaemonStageResult(highlightings);
                committer(result);
            }

            public IDaemonProcess DaemonProcess { get; }
        }

        protected override IEnumerable<SpringFile> GetPsiFiles(IPsiSourceFile sourceFile)
        {
            yield return (SpringFile) sourceFile.GetDominantPsiFile<SpringLanguage>();
        }
    }

    internal class TokenFactory : IPsiBuilderTokenFactory
    {
        public LeafElementBase CreateToken(TokenNodeType tokenNodeType, IBuffer buffer, int startOffset, int endOffset)
        {
            return tokenNodeType.Create(buffer, new TreeOffset(startOffset), new TreeOffset(endOffset));
        }
    }

    [ProjectFileType(typeof(SpringProjectFileType))]
    public class SelectEmbracingConstructProvider : ISelectEmbracingConstructProvider
    {
        public bool IsAvailable(IPsiSourceFile sourceFile)
        {
            return sourceFile.LanguageType.Is<SpringProjectFileType>();
        }

        public ISelectedRange GetSelectedRange(IPsiSourceFile sourceFile, DocumentRange documentRange)
        {
            var file = (SpringFile) sourceFile.GetDominantPsiFile<SpringLanguage>();
            var node = file.FindNodeAt(documentRange);
            return new SpringTreeNodeSelection(file, node);
        }

        public class SpringTreeNodeSelection : TreeNodeSelection<SpringFile>
        {
            public SpringTreeNodeSelection(SpringFile fileNode, ITreeNode node) : base(fileNode, node)
            {
            }

            public override ISelectedRange Parent => new SpringTreeNodeSelection(FileNode, TreeNode.Parent);
        }
    }
}