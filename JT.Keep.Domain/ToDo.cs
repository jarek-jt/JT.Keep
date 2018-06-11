using System.ComponentModel.DataAnnotations;

namespace JT.Keep.Domain
{
    public  class ToDo
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}
