using System.Collections.Generic;
using JetBrains.ReSharper.Plugins.Spring.Util;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;
using JetBrains.Util;
using JetBrains.Util.Collections;

namespace JetBrains.ReSharper.Plugins.Spring
{
    public class SpringTokenType : TokenNodeType
    {
        private static readonly HashMap<int, SpringTokenType> AntlrToSpring = new HashMap<int, SpringTokenType>();

        public static readonly SpringTokenType LEFT_CURLY_BRACKET = new SpringTokenType("{", 1);
        public static readonly SpringTokenType RIGHT_CURLY_BRACKET = new SpringTokenType("}", 2);
        public static readonly SpringTokenType LEFT_PARENTHESIS = new SpringTokenType("(", 3);
        public static readonly SpringTokenType RIGHT_PARENTHESIS = new SpringTokenType(")", 4);
        public static readonly SpringTokenType EQUALS_SIGN = new SpringTokenType("=", 5);
        public static readonly SpringTokenType COMMA = new SpringTokenType(",", 6);
        public static readonly SpringTokenType KW_RETURN = new SpringTokenType("KW_RETURN", 7);
        public static readonly SpringTokenType KW_ELSE = new SpringTokenType("KW_ELSE", 8);
        public static readonly SpringTokenType KW_IF = new SpringTokenType("KW_IF", 9);
        public static readonly SpringTokenType KW_FUN = new SpringTokenType("KW_FUN", 10);
        public static readonly SpringTokenType KW_VAR = new SpringTokenType("KW_VAR", 11);
        public static readonly SpringTokenType KW_WHILE = new SpringTokenType("KW_WHILE", 12);
        public static readonly SpringTokenType OP_LOGICAL = new SpringTokenType("OP_LOGICAL", 13);
        public static readonly SpringTokenType OP_EQ = new SpringTokenType("OP_EQ", 14);
        public static readonly SpringTokenType OP_ADDITIONAL = new SpringTokenType("OP_ADDITIONAL", 15);
        public static readonly SpringTokenType OP_COMPARE = new SpringTokenType("OP_COMPARE", 16);
        public static readonly SpringTokenType OP_MULTIPLY = new SpringTokenType("OP_MULTIPLY", 17);
        public static readonly SpringTokenType IDENTIFIER = new SpringTokenType("IDENTIFIER", 18);
        public static readonly SpringTokenType INTEGER_LITERAL = new SpringTokenType("INTEGER_LITERAL", 19);
        public static readonly SpringTokenType COMMENT = new SpringTokenType("COMMENT", 20);
        public static readonly SpringTokenType WS = new SpringTokenType("WS", 21);
        public static readonly SpringTokenType UNKNOWN = new SpringTokenType("UNKNOWN", 22);

        private SpringTokenType(string s, int index) : base(s, index)
        {
            AntlrToSpring[index] = this;
        }

        public static SpringTokenType CreateFromAntlrIndex(int index)
        {
            if (AntlrToSpring.ContainsKey(index))
            {
                return AntlrToSpring[index];
            }

            return null;
        }

        public override LeafElementBase Create(IBuffer buffer, TreeOffset startOffset, TreeOffset endOffset)
        {
            return new SpringGenericToken(buffer.GetText(new TextRange(startOffset.Offset, endOffset.Offset)), this);
        }

        public override bool IsWhitespace => this == WS;
        public override bool IsComment => this == COMMENT;
        public override bool IsStringLiteral => false;
        public override bool IsConstantLiteral => this == INTEGER_LITERAL;
        public override bool IsIdentifier => this == IDENTIFIER;

        public override bool IsKeyword => this == KW_IF || this == KW_FUN || this == KW_VAR || this == KW_ELSE ||
                                          this == KW_WHILE || this == KW_RETURN;

        public override string TokenRepresentation { get; }
    }
}