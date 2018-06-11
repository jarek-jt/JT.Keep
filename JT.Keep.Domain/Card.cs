using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace JT.Keep.Domain
{
    public class Card
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Text { get; set; }
        public Color Colour { get; set; }
        public DateTime Reminder { get; set; }
        public virtual ICollection<ToDo> ToDos { get; set; }
        public virtual ICollection<Cooperator> Cooperators { get; set; }

    }
}
