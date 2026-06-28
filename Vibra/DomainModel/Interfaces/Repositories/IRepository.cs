using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vibra.DomainModel.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Read(Guid id);
        Task<IEnumerable<TEntity>> ReadAll();
        void Create(TEntity entity);
        void Update(TEntity entity);
        Task Delete(Guid id);

        void Delete(TEntity entity);

    }
}
