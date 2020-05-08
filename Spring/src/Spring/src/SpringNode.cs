using System.Linq;
using System.Xml;
using Antlr4.Runtime;
using ICSharpCode.NRefactory.CSharp;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using NodeType = JetBrains.ReSharper.Psi.ExtensionsAPI.Tree.NodeType;

namespace JetBrains.ReSharper.Plugins.Spring
{
    public class SpringNodeFile : FileElementBase
    {
        public override NodeType NodeType => SpringFileNodeType.Instance;

        public override PsiLanguageType Language => SpringLanguage.Instance;
    }

    public class SpringNodeError : CompositeElement, IErrorElement
    {
        public override NodeType NodeType => SpringErrorNodeType.Instance;

        public override PsiLanguageType Language => SpringLanguage.Instance;

        public SpringNodeError(string text, int length)
        {
            ErrorDescription = text;
            Length = length;
        }

        public int Length { get; }
        public string ErrorDescription { get; }
    }

    public class SpringNodeCompositeAntlr : CompositeElement
    {
        public SpringNodeCompositeAntlr(RuleContext context, NodeType nodeType)
        {
            NodeType = nodeType;
            Context = context;
        }

        protected RuleContext Context { get; }

        public override NodeType NodeType { get; }
        public override PsiLanguageType Language => SpringLanguage.Instance;
    }

    public class SpringNodeIdentifier : SpringNodeCompositeAntlr
    {
        public SpringNodeIdentifier(RuleContext context, NodeType nodeType) : base(context, nodeType)
        {
        }

        public string Name => (Context as ToylangParser.IdentifierContext)?.IDENTIFIER().GetText();
    }

    public class SpringNodeIdentifierDeclaration : SpringNodeCompositeAntlr, IDeclaration
    {
        public SpringNodeIdentifierDeclaration(RuleContext context, NodeType nodeType) : base(context, nodeType)
        {
            DeclaredElement = new SpringIdentifierDeclared(this);
        }

        private SpringNodeIdentifier Identifier =>
            this.Children().First(it => it is SpringNodeIdentifier) as SpringNodeIdentifier;

        public XmlNode GetXMLDoc(bool inherit) => null;

        public void SetName(string name)
        {
        }

        public TreeTextRange GetNameRange() => Identifier.GetTreeTextRange();

        public bool IsSynthetic() => false;

        public IDeclaredElement DeclaredElement { get; }
        public string DeclaredName => Identifier.Name;
    }
}