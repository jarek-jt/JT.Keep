using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace JT.Keep.API.DTO
{
    public class Card
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Color Colour { get; set; }
        public DateTime Reminder { get; set; }
        public ICollection<ToDo> ToDos { get; set; }
        public ICollection<Cooperator> Cooperator { get; set; }
    }
}
