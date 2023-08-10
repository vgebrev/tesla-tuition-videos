namespace TTV.Web.Shared
{
    public record LookupDto
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
    }
}
