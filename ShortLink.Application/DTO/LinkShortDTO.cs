namespace ShortLink.Application.DTO
{
    /// <summary>
    /// Link short DTO
    /// </summary>
    public class LinkShortDTO
    {
        public string ShortLink { get; set; }
        public string OriginalLink { get; set; }
        public string Hash { get; set; }
    }
}
