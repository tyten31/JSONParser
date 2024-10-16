using System.Reflection;

namespace Challenge
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "tests");

            // Get all directories
            foreach (var directory in Directory.GetDirectories(path))
            {
                // Get all files in directory
                foreach (var file in Directory.GetFiles(directory))
                {
                    Console.WriteLine(file);

                    try
                    {
                        var lexer = new Lexer();
                        var parser = new Parser(lexer.Lex(file));
                        parser.Parse();

                        Console.WriteLine("VALID");
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine($"INVALID: {x.Message}");
                    }

                    Console.WriteLine(string.Empty);
                }
            }

            return 0;
        }
    }
}