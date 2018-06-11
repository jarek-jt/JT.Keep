using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JT.Keep.API.DTO
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}
