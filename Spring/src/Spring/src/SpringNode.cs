using Antlr4.Runtime;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;

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

        public RuleContext Context { get; set; }

        public override NodeType NodeType { get; }
        public override PsiLanguageType Language => SpringLanguage.Instance;
    }
}