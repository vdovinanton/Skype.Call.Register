using SkypeWatcher.Entity.Interfaces;
using SkypeWatcher.Entity.Repositories;

namespace SkypeWatcher.Entity
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly SkypeCallContext _context;
        public UnitOfWork(SkypeCallContext context)
        {
            _context = context;
            UsersRepository = new SkypeUserRepository(_context);
            History = new CallHistoryRepository(_context);
        }

        public ISkypeUserRepository UsersRepository { get; }
        public ICallHistoryRepository History { get; }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
