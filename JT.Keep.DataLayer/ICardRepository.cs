using JT.Keep.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace JT.Keep.DataLayer
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<IEnumerable<ToDo>> GetTodosByIdAsync(int id);
        IEnumerable<Card> GetCardsByColour(Color colour);
        IEnumerable<Card> GetCardsByReminderDate(DateTime date);
        Task<IEnumerable<ToDo>> GetCardsTodosAsync(bool? done);
    }
}
