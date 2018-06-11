using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
