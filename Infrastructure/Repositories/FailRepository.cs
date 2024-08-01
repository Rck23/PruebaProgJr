using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FailRepository : GenericRepository<Fail>, IFailRepository
    {
        public FailRepository(FailsContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Fail>> GetFirstFails() =>
            await _context.Fails
                .OrderBy(p => p.DateRegistered).ToListAsync();

        public override async Task<Fail> GetByIdAsync(int id)
        {
            return await _context.Fails.FirstOrDefaultAsync(p => p.Id == id);
        }

        // SOBREESCRIBIENDO EL METODO DE GENERIC REPOSITORY
        public override async Task<IEnumerable<Fail>> GetAllAsync()
        {
            return await _context.Fails
                .ToListAsync();
        }
    }
}
