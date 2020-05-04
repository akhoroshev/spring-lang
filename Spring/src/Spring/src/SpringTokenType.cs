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

        private static readonly SpringTokenType PRAGMA = new SpringTokenType("pragma", 1);
        private static readonly SpringTokenType SEMICOLON = new SpringTokenType(";", 2);
        private static readonly SpringTokenType CIRCUMFLEX_ACCENT = new SpringTokenType("^", 3);
        private static readonly SpringTokenType TILDE = new SpringTokenType("~", 4);
        private static readonly SpringTokenType GREATER_THAN_SIGNEQUALS_SIGN = new SpringTokenType(">=", 5);
        private static readonly SpringTokenType GREATER_THAN_SIGN = new SpringTokenType(">", 6);
        private static readonly SpringTokenType LESS_THAN_SIGN = new SpringTokenType("<", 7);
        private static readonly SpringTokenType LESS_THAN_SIGNEQUALS_SIGN = new SpringTokenType("<=", 8);
        private static readonly SpringTokenType EQUALS_SIGN = new SpringTokenType("=", 9);
        private static readonly SpringTokenType IMPORT = new SpringTokenType("import", 10);
        private static readonly SpringTokenType AS = new SpringTokenType("as", 11);
        private static readonly SpringTokenType ASTERISK = new SpringTokenType("*", 12);
        private static readonly SpringTokenType FROM = new SpringTokenType("from", 13);
        public static readonly SpringTokenType LEFT_CURLY_BRACKET = new SpringTokenType("{", 14);
        private static readonly SpringTokenType COMMA = new SpringTokenType(",", 15);
        public static readonly SpringTokenType RIGHT_CURLY_BRACKET = new SpringTokenType("}", 16);
        private static readonly SpringTokenType ABSTRACT = new SpringTokenType("abstract", 17);
        private static readonly SpringTokenType CONTRACT = new SpringTokenType("contract", 18);
        private static readonly SpringTokenType INTERFACE = new SpringTokenType("interface", 19);
        private static readonly SpringTokenType LIBRARY = new SpringTokenType("library", 20);
        private static readonly SpringTokenType IS = new SpringTokenType("is", 21);
        private static readonly SpringTokenType LEFT_PARENTHESIS = new SpringTokenType("(", 22);
        private static readonly SpringTokenType RIGHT_PARENTHESIS = new SpringTokenType(")", 23);
        private static readonly SpringTokenType OVERRIDE = new SpringTokenType("override", 24);
        private static readonly SpringTokenType USING = new SpringTokenType("using", 25);
        private static readonly SpringTokenType FOR = new SpringTokenType("for", 26);
        private static readonly SpringTokenType STRUCT = new SpringTokenType("struct", 27);
        private static readonly SpringTokenType MODIFIER = new SpringTokenType("modifier", 28);
        private static readonly SpringTokenType FUNCTION = new SpringTokenType("function", 29);
        private static readonly SpringTokenType RETURNS = new SpringTokenType("returns", 30);
        private static readonly SpringTokenType EVENT = new SpringTokenType("event", 31);
        private static readonly SpringTokenType ENUM = new SpringTokenType("enum", 32);
        private static readonly SpringTokenType LEFT_SQUARE_BRACKET = new SpringTokenType("[", 33);
        private static readonly SpringTokenType RIGHT_SQUARE_BRACKET = new SpringTokenType("]", 34);
        private static readonly SpringTokenType FULL_STOP = new SpringTokenType(".", 35);
        private static readonly SpringTokenType MAPPING = new SpringTokenType("mapping", 36);
        private static readonly SpringTokenType EQUALS_SIGNGREATER_THAN_SIGN = new SpringTokenType("=>", 37);
        private static readonly SpringTokenType MEMORY = new SpringTokenType("memory", 38);
        private static readonly SpringTokenType STORAGE = new SpringTokenType("storage", 39);
        private static readonly SpringTokenType CALLDATA = new SpringTokenType("calldata", 40);
        private static readonly SpringTokenType IF = new SpringTokenType("if", 41);
        private static readonly SpringTokenType ELSE = new SpringTokenType("else", 42);
        private static readonly SpringTokenType TRY = new SpringTokenType("try", 43);
        private static readonly SpringTokenType CATCH = new SpringTokenType("catch", 44);
        private static readonly SpringTokenType WHILE = new SpringTokenType("while", 45);
        private static readonly SpringTokenType ASSEMBLY = new SpringTokenType("assembly", 46);
        private static readonly SpringTokenType DO = new SpringTokenType("do", 47);
        private static readonly SpringTokenType RETURN = new SpringTokenType("return", 48);
        private static readonly SpringTokenType THROW = new SpringTokenType("throw", 49);
        private static readonly SpringTokenType EMIT = new SpringTokenType("emit", 50);
        private static readonly SpringTokenType VAR = new SpringTokenType("var", 51);
        private static readonly SpringTokenType ADDRESS = new SpringTokenType("address", 52);
        private static readonly SpringTokenType BOOL = new SpringTokenType("bool", 53);
        private static readonly SpringTokenType STRING = new SpringTokenType("string", 54);
        private static readonly SpringTokenType BYTE_SMALL = new SpringTokenType("byte", 55);
        private static readonly SpringTokenType PLUS_SIGNPLUS_SIGN = new SpringTokenType("++", 56);
        private static readonly SpringTokenType HYPHEN_MINUSHYPHEN_MINUS = new SpringTokenType("--", 57);
        private static readonly SpringTokenType NEW = new SpringTokenType("new", 58);
        private static readonly SpringTokenType COLON = new SpringTokenType(":", 59);
        private static readonly SpringTokenType PLUS_SIGN = new SpringTokenType("+", 60);
        private static readonly SpringTokenType HYPHEN_MINUS = new SpringTokenType("-", 61);
        private static readonly SpringTokenType AFTER = new SpringTokenType("after", 62);
        private static readonly SpringTokenType DELETE = new SpringTokenType("delete", 63);
        private static readonly SpringTokenType EXCLAMATION_MARK = new SpringTokenType("!", 64);
        private static readonly SpringTokenType ASTERISKASTERISK = new SpringTokenType("**", 65);
        private static readonly SpringTokenType SOLIDUS = new SpringTokenType("/", 66);
        private static readonly SpringTokenType PERCENT_SIGN = new SpringTokenType("%", 67);
        private static readonly SpringTokenType LESS_THAN_SIGNLESS_THAN_SIGN = new SpringTokenType("<<", 68);
        private static readonly SpringTokenType GREATER_THAN_SIGNGREATER_THAN_SIGN = new SpringTokenType(">>", 69);
        private static readonly SpringTokenType AMPERSAND = new SpringTokenType("&", 70);
        private static readonly SpringTokenType VERTICAL_LINE = new SpringTokenType("|", 71);
        private static readonly SpringTokenType EQUALS_SIGNEQUALS_SIGN = new SpringTokenType("==", 72);
        private static readonly SpringTokenType EXCLAMATION_MARKEQUALS_SIGN = new SpringTokenType("!=", 73);
        private static readonly SpringTokenType AMPERSANDAMPERSAND = new SpringTokenType("&&", 74);
        private static readonly SpringTokenType VERTICAL_LINEVERTICAL_LINE = new SpringTokenType("||", 75);
        private static readonly SpringTokenType QUESTION_MARK = new SpringTokenType("?", 76);
        private static readonly SpringTokenType VERTICAL_LINEEQUALS_SIGN = new SpringTokenType("|=", 77);
        private static readonly SpringTokenType CIRCUMFLEX_ACCENTEQUALS_SIGN = new SpringTokenType("^=", 78);
        private static readonly SpringTokenType AMPERSANDEQUALS_SIGN = new SpringTokenType("&=", 79);

        private static readonly SpringTokenType
            LESS_THAN_SIGNLESS_THAN_SIGNEQUALS_SIGN = new SpringTokenType("<<=", 80);

        private static readonly SpringTokenType GREATER_THAN_SIGNGREATER_THAN_SIGNEQUALS_SIGN =
            new SpringTokenType(">>=", 81);

        private static readonly SpringTokenType PLUS_SIGNEQUALS_SIGN = new SpringTokenType("+=", 82);
        private static readonly SpringTokenType HYPHEN_MINUSEQUALS_SIGN = new SpringTokenType("-=", 83);
        private static readonly SpringTokenType ASTERISKEQUALS_SIGN = new SpringTokenType("*=", 84);
        private static readonly SpringTokenType SOLIDUSEQUALS_SIGN = new SpringTokenType("/=", 85);
        private static readonly SpringTokenType PERCENT_SIGNEQUALS_SIGN = new SpringTokenType("%=", 86);
        private static readonly SpringTokenType LET = new SpringTokenType("let", 87);
        private static readonly SpringTokenType COLONEQUALS_SIGN = new SpringTokenType(":=", 88);
        private static readonly SpringTokenType EQUALS_SIGNCOLON = new SpringTokenType("=:", 89);
        private static readonly SpringTokenType SWITCH = new SpringTokenType("switch", 90);
        private static readonly SpringTokenType CASE = new SpringTokenType("case", 91);
        private static readonly SpringTokenType DEFAULT = new SpringTokenType("default", 92);
        private static readonly SpringTokenType INT = new SpringTokenType("Int", 93);
        private static readonly SpringTokenType UINT = new SpringTokenType("Uint", 94);
        private static readonly SpringTokenType BYTE = new SpringTokenType("Byte", 95);
        private static readonly SpringTokenType FIXED = new SpringTokenType("Fixed", 96);
        private static readonly SpringTokenType UFIXED = new SpringTokenType("Ufixed", 97);
        private static readonly SpringTokenType BOOLEANLITERAL = new SpringTokenType("BooleanLiteral", 98);
        private static readonly SpringTokenType DECIMALNUMBER = new SpringTokenType("DecimalNumber", 99);
        private static readonly SpringTokenType HEXNUMBER = new SpringTokenType("HexNumber", 100);
        private static readonly SpringTokenType NUMBERUNIT = new SpringTokenType("NumberUnit", 101);
        private static readonly SpringTokenType HEXLITERALFRAGMENT = new SpringTokenType("HexLiteralFragment", 102);
        private static readonly SpringTokenType RESERVEDKEYWORD = new SpringTokenType("ReservedKeyword", 103);
        private static readonly SpringTokenType ANONYMOUSKEYWORD = new SpringTokenType("AnonymousKeyword", 104);
        private static readonly SpringTokenType BREAKKEYWORD = new SpringTokenType("BreakKeyword", 105);
        private static readonly SpringTokenType CONSTANTKEYWORD = new SpringTokenType("ConstantKeyword", 106);
        private static readonly SpringTokenType IMMUTABLEKEYWORD = new SpringTokenType("ImmutableKeyword", 107);
        private static readonly SpringTokenType CONTINUEKEYWORD = new SpringTokenType("ContinueKeyword", 108);
        private static readonly SpringTokenType LEAVEKEYWORD = new SpringTokenType("LeaveKeyword", 109);
        private static readonly SpringTokenType EXTERNALKEYWORD = new SpringTokenType("ExternalKeyword", 110);
        private static readonly SpringTokenType INDEXEDKEYWORD = new SpringTokenType("IndexedKeyword", 111);
        private static readonly SpringTokenType INTERNALKEYWORD = new SpringTokenType("InternalKeyword", 112);
        private static readonly SpringTokenType PAYABLEKEYWORD = new SpringTokenType("PayableKeyword", 113);
        private static readonly SpringTokenType PRIVATEKEYWORD = new SpringTokenType("PrivateKeyword", 114);
        private static readonly SpringTokenType PUBLICKEYWORD = new SpringTokenType("PublicKeyword", 115);
        private static readonly SpringTokenType VIRTUALKEYWORD = new SpringTokenType("VirtualKeyword", 116);
        private static readonly SpringTokenType PUREKEYWORD = new SpringTokenType("PureKeyword", 117);
        private static readonly SpringTokenType TYPEKEYWORD = new SpringTokenType("TypeKeyword", 118);
        private static readonly SpringTokenType VIEWKEYWORD = new SpringTokenType("ViewKeyword", 119);
        private static readonly SpringTokenType CONSTRUCTORKEYWORD = new SpringTokenType("ConstructorKeyword", 120);
        private static readonly SpringTokenType FALLBACKKEYWORD = new SpringTokenType("FallbackKeyword", 121);
        private static readonly SpringTokenType RECEIVEKEYWORD = new SpringTokenType("ReceiveKeyword", 122);
        private static readonly SpringTokenType IDENTIFIER = new SpringTokenType("Identifier", 123);

        private static readonly SpringTokenType
            STRINGLITERALFRAGMENT = new SpringTokenType("StringLiteralFragment", 124);

        private static readonly SpringTokenType VERSIONLITERAL = new SpringTokenType("VersionLiteral", 125);
        private static readonly SpringTokenType WS = new SpringTokenType("WS", 126);
        private static readonly SpringTokenType COMMENT = new SpringTokenType("COMMENT", 127);

        private static readonly SpringTokenType UNKNOWN = new SpringTokenType("UNKNOWN", 129);


        private SpringTokenType(string s, int index) : base(s, index)
        {
            Logger.Default.Log($"New SpringTokenType {s} {index}");
            AntlrToSpring[index] = this;
        }

        public static SpringTokenType CreateFromAntlrIndex(int index)
        {
            Logger.Default.Log($"Lookup index {index}");
            if (AntlrToSpring.ContainsKey(index))
            {
                Logger.Default.Log($"Index found {index}");
                return AntlrToSpring[index];
            }

            Logger.Default.Log($"Index not found {index}");
            return null;
        }

        public override LeafElementBase Create(IBuffer buffer, TreeOffset startOffset, TreeOffset endOffset)
        {
            return new SpringGenericToken(buffer.GetText(new TextRange(startOffset.Offset, endOffset.Offset)), this);
        }

        public override bool IsWhitespace => this == WS;
        public override bool IsComment => this == COMMENT;
        public override bool IsStringLiteral => this == STRINGLITERALFRAGMENT;
        public override bool IsConstantLiteral => this == DECIMALNUMBER;
        public override bool IsIdentifier => this == IDENTIFIER;

        public override bool IsKeyword => this == FOR || this == IF || this == RETURN || this == PRAGMA ||
                                          this == ELSE || this == CONTRACT || this == FUNCTION || this == LIBRARY;

        public override string TokenRepresentation { get; }
    }
}