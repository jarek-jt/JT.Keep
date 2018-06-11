using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
