using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JT.Keep.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;


namespace JT.Keep.DataLayer
{
    public class CardsRepository : ICardRepository, IDisposable
    {
        private KeepContext _db;

        public CardsRepository()
        {
            _db = new KeepContext(
                    new DbContextOptionsBuilder<KeepContext>()
                        .UseInMemoryDatabase("keepDB")
                        .Options);
            
            InitializeData();
        }

        public CardsRepository(IKeepContext db)
        {
            _db = (KeepContext) db;
        }

        #region CRUD

        public IEnumerable<Card> GetAll()
        {
            return _db.Cards;
        }

        public IEnumerable<Card> GetCardsByColour(Color colour)
        {
            return _db.Cards.Where(x=>x.Colour==colour).AsEnumerable<Card>();
        }

        public IEnumerable<Card> GetCardsByReminderDate(DateTime date)
        {
            return _db.Cards.Where(x => x.Reminder == date).AsEnumerable<Card>();
        }

        public async Task<IEnumerable<ToDo>> GetCardsTodosAsync(bool? done)
        {
            List<ToDo> todos = new List<ToDo>();

            if(done.HasValue)
                await _db.Cards.ForEachAsync(x => todos.AddRange(x.ToDos.Where(y => y.Checked == done)));
            else
                await _db.Cards.ForEachAsync(x => todos.AddRange(x.ToDos));

            return todos;
        }

        public async Task<Card> GetByIdAsync(int id)
        {
            return await _db.Cards.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ToDo>> GetTodosByIdAsync(int id)
        {
            var card = await _db.Cards.FirstOrDefaultAsync(p => p.Id == id);
            return card.ToDos;
        }

        public async Task<int> Insert(Card card)
        {
            _db.Add(card);
            await _db.SaveChangesAsync();

            return card.Id;
        }

        public async Task<DBStatusEnum> Update(Card card)
        {
            _db.Entry(card).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
                return DBStatusEnum.Ok;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<DBStatusEnum> Delete(int id)
        {
            var card = _db.Cards.Find(id);

            if (card == null)
            {
                return DBStatusEnum.Error;
            }

            _db.Cards.Remove(card);
            await _db.SaveChangesAsync();

            return DBStatusEnum.Ok;
        }

        #endregion

        #region private
        private bool CardExists(int id)
        {
            return _db.Cards.Any(e => e.Id == id);
        }

        private void InitializeData()
        {
            var cards = new
                List<Card>() { new Card { Id = 1, Text = "Things to buy", Colour=Color.DarkGoldenrod, Title="Shopping", ToDos= new List<ToDo>()
                {
                    new ToDo(){Id=1,Text="Milk"},
                    new ToDo(){Id=2,Text="Apple"}
                },
                    Cooperators = null

                },
             new Card { Id = 2, Text = "Remember on interview", Colour=Color.Cyan, Title="Work", ToDos= new List<ToDo>()
                {
                    new ToDo(){Id=3,Text="Slides", Checked=true},
                    new ToDo(){Id=4,Text="Application", Checked=true}
                } }};

            _db.AddRange(cards);
            _db.SaveChanges();
        }
        #endregion 

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_db != null)
                    {
                        _db.Dispose();
                        _db = null;
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
