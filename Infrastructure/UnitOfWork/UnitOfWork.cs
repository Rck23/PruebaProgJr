using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FailsContext _context;
        private IFailRepository _fails;

        public UnitOfWork(FailsContext context)
        {
            _context = context;
        }

        public IFailRepository fails
        {
            get
            {
                if (_fails == null)
                {
                    _fails = new FailRepository(_context);
                }
                return _fails;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
