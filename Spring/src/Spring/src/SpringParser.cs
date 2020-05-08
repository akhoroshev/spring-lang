using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
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
using JetBrains.ReSharper.Psi.Resolve;
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
                parser.AddErrorListener(new SpringErrorListener(builder));
                var visitor = new NodeVisitor(builder);

                // Begin Top level File
                var fileBeginMark = builder.Mark();

                // Inner structure
                visitor.Visit(parser.file());

                // End Top level File
                builder.ResetCurrentLexeme(visitor.MaxTokenIndexConsumed, visitor.MaxTokenIndexConsumed);
                builder.Done(fileBeginMark, SpringFileNodeType.Instance, null);

                var compositeElement = builder.BuildTree();
                var file = (IFile) compositeElement;
                return file;
            }
        }


        private class NodeVisitor : ToylangBaseVisitor<Unit>
        {
            private readonly PsiBuilder _psiBuilder;

            public NodeVisitor(PsiBuilder psiBuilder)
            {
                MaxTokenIndexConsumed = 0;
                _psiBuilder = psiBuilder;
            }

            private int BeginCreatingNode(int begin)
            {
                _psiBuilder.ResetCurrentLexeme(begin, begin);
                return _psiBuilder.Mark();
            }

            private void EndCreatingNode(int mark, int end, CompositeNodeType nodeType, object data)
            {
                _psiBuilder.ResetCurrentLexeme(end, end);
                _psiBuilder.Done(mark, nodeType, data);
            }

            private void CreateNode(ParserRuleContext context, int begin, int end, CompositeNodeType nodeType,
                object data)
            {
                var mark = BeginCreatingNode(begin);
                base.VisitChildren(context);
                EndCreatingNode(mark, end, nodeType, data);
            }

            public override Unit VisitStmtVariable(ToylangParser.StmtVariableContext context)
            {
                if (context.exception != null)
                    return Unit.Instance;

                var interval = context.SourceInterval;
                CreateNode(context, interval.a, interval.b + 1, SpringCompositeNodeType.NodeTypeIdentifierDeclaration,
                    context);
                return Unit.Instance;
            }

            public override Unit VisitStmtFunction(ToylangParser.StmtFunctionContext context)
            {
                if (context.exception != null)
                    return Unit.Instance;

                var interval = context.SourceInterval;
                CreateNode(context, interval.a, interval.b + 1, SpringCompositeNodeType.NodeTypeIdentifierDeclaration,
                    context);
                return Unit.Instance;
            }

            public override Unit VisitFunctionParameter(ToylangParser.FunctionParameterContext context)
            {
                var interval = context.SourceInterval;
                CreateNode(context, interval.a, interval.b + 1, SpringCompositeNodeType.NodeTypeIdentifierDeclaration,
                    context);
                return Unit.Instance;
            }

            public override Unit VisitIdentifier(ToylangParser.IdentifierContext context)
            {
                if (context.GetText().Equals(String.Empty))
                    return Unit.Instance;

                var interval = context.SourceInterval;
                CreateNode(context, interval.a, interval.b + 1, SpringCompositeNodeType.NodeTypeIdentifier, context);
                return Unit.Instance;
            }

            public override Unit VisitBlock(ToylangParser.BlockContext context)
            {
                var interval = context.SourceInterval;
                CreateNode(context, interval.a, interval.b + 1, SpringCompositeNodeType.NodeTypeBlock, context);
                return Unit.Instance;
            }

            public override Unit VisitTerminal(ITerminalNode node)
            {
                MaxTokenIndexConsumed = Math.Max(node.Symbol.TokenIndex, MaxTokenIndexConsumed);
                return base.VisitTerminal(node);
            }

            public int MaxTokenIndexConsumed { get; private set; }
        }
    }

    class SpringErrorListener : BaseErrorListener
    {
        private readonly PsiBuilder _builder;

        public SpringErrorListener(PsiBuilder builder)
        {
            _builder = builder;
        }

        public override void SyntaxError(
            TextWriter output, IRecognizer recognizer, IToken offendingSymbol,
            int line, int charPositionInLine, string msg, RecognitionException e
        )
        {
            _builder.ResetCurrentLexeme(offendingSymbol.TokenIndex, offendingSymbol.TokenIndex);
            var mark = _builder.Mark();
            var length = offendingSymbol.StopIndex - offendingSymbol.StartIndex + 1;
            _builder.Done(mark, SpringErrorNodeType.Instance, new SpringErrorNodeType.Message(msg, length));
        }
    }

    [DaemonStage]
    class SpringDaemonStage : DaemonStageBase<SpringNodeFile>
    {
        protected override IDaemonStageProcess CreateDaemonProcess(IDaemonProcess process,
            DaemonProcessKind processKind, SpringNodeFile file,
            IContextBoundSettingsStore settingsStore)
        {
            return new SpringDaemonProcess(process, file);
        }

        private class SpringDaemonProcess : IDaemonStageProcess
        {
            private readonly SpringNodeFile _file;
            private readonly SpringReferenceFactory _referenceFactory;

            public SpringDaemonProcess(IDaemonProcess process, SpringNodeFile file)
            {
                _file = file;
                DaemonProcess = process;
                _referenceFactory = new SpringReferenceFactory();
            }

            public void Execute(Action<DaemonStageResult> committer)
            {
                var highlightings = new List<HighlightingInfo>();

                foreach (var treeNode in _file.Descendants())
                {
                    if (treeNode is SpringNodeError error)
                    {
                        var range = error.GetDocumentRange().ExtendRight(error.Length);
                        highlightings.Add(new HighlightingInfo(range,
                            new CSharpSyntaxError(error.ErrorDescription, range)));
                    }

                    var references = _referenceFactory.GetReferences(treeNode, ReferenceCollection.Empty);
                    if (references.Any(it => it.Resolve().Info.ResolveErrorType != ResolveErrorType.OK))
                    {
                        var range = references.First().GetDocumentRange();
                        highlightings.Add(new HighlightingInfo(range,
                            new CSharpSyntaxError("Undefined symbol", range)));
                    }
                }

                var result = new DaemonStageResult(highlightings);
                committer(result);
            }

            public IDaemonProcess DaemonProcess { get; }
        }

        protected override IEnumerable<SpringNodeFile> GetPsiFiles(IPsiSourceFile sourceFile)
        {
            yield return (SpringNodeFile) sourceFile.GetDominantPsiFile<SpringLanguage>();
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
            var file = (SpringNodeFile) sourceFile.GetDominantPsiFile<SpringLanguage>();
            var node = file.FindNodeAt(documentRange);
            return new SpringTreeNodeSelection(file, node);
        }

        public class SpringTreeNodeSelection : TreeNodeSelection<SpringNodeFile>
        {
            public SpringTreeNodeSelection(SpringNodeFile fileNode, ITreeNode node) : base(fileNode, node)
            {
            }

            public override ISelectedRange Parent => new SpringTreeNodeSelection(FileNode, TreeNode.Parent);
        }
    }
}