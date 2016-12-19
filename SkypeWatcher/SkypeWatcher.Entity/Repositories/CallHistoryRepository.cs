using System.Data.Entity;
using SkypeWatcher.Entity.Interfaces;
using SkypeWatcher.Entity.Models;

namespace SkypeWatcher.Entity.Repositories
{
    public class CallHistoryRepository: Repository<CallHistory>, ICallHistoryRepository
    {
        private SkypeCallContext HistoryContext => Context as SkypeCallContext;
        public CallHistoryRepository(DbContext context) : base(context)
        {
        }
    }
}
