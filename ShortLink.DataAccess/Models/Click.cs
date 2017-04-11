using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortLink.DataAccess.Models
{
    public class Click
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LinkId { get; set; }
        public DateTime Timestamp { get; set; }

        [ForeignKey("LinkId")]
        public virtual Link Link { get; set; }
    }
}
