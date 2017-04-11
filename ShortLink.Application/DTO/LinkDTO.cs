using System;

namespace ShortLink.Application.DTO
{
    public class LinkDTO
    {
        public string Id { get; set; }
        public string OriginalLink { get; set; }
        public int Count { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
