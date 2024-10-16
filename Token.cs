namespace Challenge
{
    internal enum TokenType
    {
        Boolean,
        Colon,
        Comma,     
        LeftBrace,
        LeftBracket,
        Null,
        Number,
        RightBrace,
        RightBracket,
        String,
    }

    internal class Token
    {
        public TokenType Type { get; set; }

        public string Value { get; set; }
    }
}