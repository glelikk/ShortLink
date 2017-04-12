using System;

namespace ShortLink.Application.DTO
{
    /// <summary>
    /// Link Full DTO
    /// </summary>
    public class LinkDTO : LinkShortDTO
    {
        public int Count { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
