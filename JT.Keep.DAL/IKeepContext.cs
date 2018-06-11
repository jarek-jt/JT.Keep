using JT.Keep.Domain;
using Microsoft.EntityFrameworkCore;

namespace JT.Keep.DataLayer
{
    public interface IKeepContext
    {
        DbSet<Card> Cards { get; set; }
        DbSet<Cooperator> Cooperators { get; set; }
    }
}