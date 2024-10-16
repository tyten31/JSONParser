using System.Text;

namespace Challenge
{
    internal class Lexer
    {
        public List<Token> Lex(string path)
        {
            var tokens = new List<Token>();

            if (File.Exists(path))
            {
                using StreamReader read = new(path);
                string? line;

                while ((line = read.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var current = 0;

                        while (current < line.Length)
                        {
                            var character = line[current];

                            // Left Brace {
                            if (character.Equals('{'))
                            {
                                tokens.Add(new Token { Type = TokenType.LeftBrace, Value = character.ToString() });
                                current++;
                                continue;
                            }

                            // Right Brace }
                            if (character.Equals('}'))
                            {
                                tokens.Add(new Token { Type = TokenType.RightBrace, Value = character.ToString() });
                                current++;
                                continue;
                            }

                            // Left Bracket [
                            if (character.Equals('['))
                            {
                                tokens.Add(new Token { Type = TokenType.LeftBracket, Value = character.ToString() });
                                current++;
                                continue;
                            }

                            // Right Bracket ]
                            if (character.Equals(']'))
                            {
                                tokens.Add(new Token { Type = TokenType.RightBracket, Value = character.ToString() });
                                current++;
                                continue;
                            }

                            // Colon :
                            if (character.Equals(':'))
                            {
                                tokens.Add(new Token { Type = TokenType.Colon, Value = character.ToString() });
                                current++;
                                continue;
                            }

                            // Comma ,
                            if (character.Equals(','))
                            {
                                tokens.Add(new Token { Type = TokenType.Comma, Value = character.ToString() });
                                current++;
                                continue;
                            }

                            // Whitespace
                            if (char.IsWhiteSpace(character))
                            {
                                current++;
                                continue;
                            }

                            // Number (decimal or int)
                            if (char.IsNumber(character))
                            {
                                var number = new StringBuilder();

                                while (current < line.Length && char.IsNumber(character))
                                {
                                    character = line[current];
                                    number.Append(character);
                                    current++;
                                }

                                tokens.Add(new Token { Type = TokenType.Number, Value = number.ToString() });

                                continue;
                            }

                            // String
                            if (character.Equals('"'))
                            {
                                var value = new StringBuilder();

                                character = line[++current];

                                while (current < line.Length && !character.Equals('"'))
                                {
                                    value.Append(character);
                                    character = line[++current];
                                }

                                tokens.Add(new Token { Type = TokenType.String, Value = value.ToString() });

                                current++;
                                continue;
                            }

                            // false
                            if (character.Equals('f') && line[current + 1].Equals('a') && line[current + 2].Equals('l') && line[current + 3].Equals('s') && line[current + 4].Equals('e'))
                            {
                                tokens.Add(new Token { Type = TokenType.Boolean, Value = "false" });

                                current += 5;
                                continue;
                            }

                            // true
                            if (character.Equals('t') && line[current + 1].Equals('r') && line[current + 2].Equals('u') && line[current + 3].Equals('e'))
                            {
                                tokens.Add(new Token { Type = TokenType.Boolean, Value = "true" });

                                current += 4;
                                continue;
                            }

                            // null
                            if (character.Equals('n') && line[current + 1].Equals('u') && line[current + 2].Equals('l') && line[current + 3].Equals('l'))
                            {
                                tokens.Add(new Token { Type = TokenType.Boolean, Value = null });

                                current += 4;
                                continue;
                            }

                            throw new Exception($"Invalid character: {character} in line {line}");
                        }
                    }
                }
            }

            return tokens;
        }
    }
}