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
            UsersRepository = new PersonRepository(_context);
            History = new CallHistoryRepository(_context);
            Dialog = new DialogRepository(_context);
        }

        public IPersonRepository UsersRepository { get; }
        public ICallHistoryRepository History { get; }
        public IDialogRepository Dialog { get; set; }
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
