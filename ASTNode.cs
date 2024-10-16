namespace Challenge
{
    internal enum ASTType
    {
        Array,
        Boolean,
        Null,
        Number,
        Object,
        String,
    }

    internal class ASTNode
    {
        public ASTType Type { get; set; }

        public object? Value { get; set; }
    }
}