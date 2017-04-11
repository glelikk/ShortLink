using System;
using System.Collections.Generic;

namespace ShortLink.DataAccess.Models
{
    public class Link
    {
        public Link()
        {
            Clicks = new List<Click>();
        }
        public string Id { get; set; }
        public string OriginalLink { get; set; }
        public int ClientId { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Click> Clicks { get; set; }
    }
}
