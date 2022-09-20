namespace DynamicFilter.Sql.Parser
{
    public class Error
    {
        public string? Message { get; set; }
        public int LineNumber { get; set; }
        public int Position { get; set; }

        public override string ToString()
        {
            return $"Ln {LineNumber}, Pos {Position}: {Message}";
        }
    }
}
