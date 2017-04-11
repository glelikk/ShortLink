using System.ComponentModel.DataAnnotations.Schema;

namespace ShortLink.DataAccess.Models
{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClientKey { get; set; }
    }
}
