using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JT.Keep.Domain;
using Microsoft.EntityFrameworkCore;


namespace JT.Keep.DataLayer
{
    public class CooperatorsRepository : IRepository<Cooperator>, IDisposable
    {
        private KeepContext _db;

        public CooperatorsRepository()
        {
            _db = new KeepContext(
                    new DbContextOptionsBuilder<KeepContext>()
                        .UseInMemoryDatabase("keepDB")
                        .Options);
            
            InitializeData();
        }

        public CooperatorsRepository(IKeepContext db)
        {
            _db = (KeepContext) db;
        }

        #region CRUD

        public IEnumerable<Cooperator> GetAll()
        {
            return _db.Cooperators;
        }

        public async Task<Cooperator> GetByIdAsync(int id)
        {
            return await _db.Cooperators.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> Insert(Cooperator cooperator)
        {
            _db.Add(cooperator);
            await _db.SaveChangesAsync();

            return cooperator.Id;
        }

        public async Task<DBStatusEnum> Update(Cooperator cooperator)
        {
            _db.Entry(cooperator).State = EntityState.Modified;

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
            var cooperator = _db.Cooperators.Find(id);

            if (cooperator == null)
            {
                return DBStatusEnum.Error;
            }

            _db.Cooperators.Remove(cooperator);
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
            List<Cooperator> cooperators = new List<Cooperator>()
            {
                new Cooperator{Id=1, Name="Jarek", Emial="jarek@gmail.com"},
                new Cooperator{Id=2, Name="Krzysiek", Emial="krzysiek@gmail.com"},
                new Cooperator{Id=3, Name="Adam", Emial="adam@gmail.com"}
            };

            _db.AddRange(cooperators);
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
