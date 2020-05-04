using Antlr4.Runtime;
using JetBrains.ReSharper.Plugins.Spring.Util;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.Text;

namespace JetBrains.ReSharper.Plugins.Spring
{
    public class SpringLexer : ILexer<int>
    {
        private readonly ToylangLexer _toyLangAntlrLexer;
        private IToken _token;
        private int _currentPosition;

        public SpringLexer(IBuffer buffer)
        {
            Buffer = buffer;
            _currentPosition = 0;
            _toyLangAntlrLexer = new ToylangLexer(new AntlrInputStream(buffer.GetText()));
        }

        public void Start()
        {
            Advance();
        }

        public void Advance()
        {
            _token = _toyLangAntlrLexer.NextToken();
            Logger.Default.Log(
                $"Advanced token type is {_token.Type} start {_token.StartIndex} stop {_token.StopIndex + 1}");
            _currentPosition++;
        }

        public int CurrentPosition
        {
            get => _currentPosition;
            set
            {
                _toyLangAntlrLexer.Reset();
                _currentPosition = 0;
                while (_currentPosition < value)
                {
                    _token = _toyLangAntlrLexer.NextToken();
                    _currentPosition++;
                }
            }
        }

        object ILexer.CurrentPosition
        {
            get => CurrentPosition;
            set => CurrentPosition = (int) value;
        }

        public TokenNodeType TokenType => SpringTokenType.CreateFromAntlrIndex(_token.Type);

        public int TokenStart => _token.StartIndex;
        public int TokenEnd => _token.StopIndex + 1;
        public IBuffer Buffer { get; }
    }
}