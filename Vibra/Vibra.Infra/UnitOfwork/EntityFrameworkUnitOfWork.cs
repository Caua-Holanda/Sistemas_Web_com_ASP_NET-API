using ProvaMed.DomainModel.Interfaces.UoW;
using Vibra.Infra.Data;
namespace ProvaMed.Infra.UoW
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly VibraDbContext _context;

        public EntityFrameworkUnitOfWork(VibraDbContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

      
    }
}
