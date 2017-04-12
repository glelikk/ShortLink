using System;

namespace ShortLink.Application.DTO
{
    public class LinkDTO : LinkShortDTO
    {
        public int Count { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
