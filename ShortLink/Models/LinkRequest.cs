using System.ComponentModel.DataAnnotations;

namespace ShortLink.Models
{
    public class LinkRequest
    {
        [DataType(DataType.Url)]
        [Required]
        public string Url { get; set; }
    }
}