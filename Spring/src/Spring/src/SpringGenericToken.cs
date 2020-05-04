using System.Text;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Text;

namespace JetBrains.ReSharper.Plugins.Spring
{
    public class SpringGenericToken : LeafElementBase, ITokenNode
    {
        private readonly string _text;
        private readonly SpringTokenType _type;

        public SpringGenericToken(string text, SpringTokenType type)
        {
            _text = text;
            _type = type;
        }
            
        public override int GetTextLength()
        {
            return _text.Length;
        }

        public override string GetText()
        {
            return _text;
        }

        public override StringBuilder GetText(StringBuilder to)
        {
            to.Append(GetText());
            return to;
        }

        public override IBuffer GetTextAsBuffer()
        {
            return new StringBuffer(GetText());
        }
            
        public override string ToString()
        {
            return base.ToString() + "(type:" + _type + ", text:" + _text + ")";
        }

        public override NodeType NodeType => _type;
        public override PsiLanguageType Language => SpringLanguage.Instance;
        public TokenNodeType GetTokenType()
        {
            return _type;
        }
    }
}