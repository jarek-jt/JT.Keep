using JT.Keep.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JT.Keep.DataLayer
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        Task<T> GetByIdAsync(int id);

        Task<int> Insert(T entity);

        Task<DBStatusEnum> Update(T entity);

        Task<DBStatusEnum> Delete(int id);
    }
}
