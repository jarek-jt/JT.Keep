using System.ComponentModel.DataAnnotations;

namespace JT.Keep.Domain
{
    public class Cooperator
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Emial { get; set; }
    }
}
