using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IRepository<User> Users { get; }
        public IRepository<Expense> Expenses { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new Repository<User>(context);
            Expenses = new Repository<Expense>(context);
        }

        public async Task<int> SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
