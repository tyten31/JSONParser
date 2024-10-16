namespace Challenge
{
    internal class Parser
    {
        private readonly List<Token> _tokens;
        private int _position;

        public Parser(List<Token> tokens)
        {
            _position = 0;
            _tokens = tokens;
        }

        public ASTNode Parse()
        {
            if (_tokens == null || _tokens.Count == 0)
            {
                throw new Exception("Nothing to parse");
            }

            switch (_tokens[_position].Type)
            {
                case TokenType.Boolean:
                    return new ASTNode { Type = ASTType.Boolean, Value = _tokens[_position].Value };

                case TokenType.String:
                    return new ASTNode { Type = ASTType.String, Value = _tokens[_position].Value };

                case TokenType.Number:
                    return new ASTNode { Type = ASTType.Number, Value = _tokens[_position].Value };

                case TokenType.Null:
                    return new ASTNode { Type = ASTType.Null, Value = null };

                case TokenType.LeftBrace:
                    return ParseObject();

                case TokenType.LeftBracket:
                    return ParseArray();

                default:
                    throw new Exception($"Unexpected token type: {_tokens[_position].Type}");
            }
        }

        private Token Advance()
        {
            return _tokens[++_position];
        }

        private ASTNode ParseArray()
        {
            var astNode = new ASTNode { Type = ASTType.Array, Value = new List<object>() };
            var token = Advance();

            while (token.Type != TokenType.RightBracket)
            {
                ((List<object>)astNode.Value).Add(Parse()?.Value);

                token = Advance();

                if (token.Type == TokenType.Comma)
                {
                    token = Advance();
                }
            }

            return astNode;
        }

        private ASTNode ParseObject()
        {
            var astNode = new ASTNode { Type = ASTType.Object, Value = new Dictionary<string, object>() };
            var token = Advance();

            while (token.Type != TokenType.RightBrace)
            {
                if (token.Type == TokenType.Comma)
                {
                    token = Advance();
                }

                if (token.Type == TokenType.String)
                {
                    var key = token.Value;

                    token = Advance();

                    if (token.Type != TokenType.Colon)
                    {
                        throw new Exception($"Expected colon, got {token.Value}");
                    }

                    token = Advance();

                    ((Dictionary<string, object>)astNode.Value).Add(key, Parse()?.Value);
                }
                else
                {
                    throw new Exception($"Expected string key, got {token.Value}");
                }

                token = Advance();
            }

            return astNode;
        }
    }
}