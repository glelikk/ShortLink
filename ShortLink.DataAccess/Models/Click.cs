using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortLink.DataAccess.Models
{
    public class Click
    {
        public string LinkId { get; set; }
        public DateTime Timestamp { get; set; }

        [ForeignKey("LinkId")]
        public virtual Link Link { get; set; }
    }
}
