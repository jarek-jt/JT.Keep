using JT.Keep.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace JT.Keep.DataLayer
{
    public class KeepContext : DbContext, IKeepContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Cooperator> Cooperators { get; set; }

        public KeepContext(DbContextOptions<KeepContext> options) : base(options)
        {
        
        }
    }
}
