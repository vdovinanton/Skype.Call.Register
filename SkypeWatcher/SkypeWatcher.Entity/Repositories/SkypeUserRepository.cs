using System.Data.Entity;
using System.Linq;
using SkypeWatcher.Entity.Interfaces;
using SkypeWatcher.Entity.Models;

namespace SkypeWatcher.Entity.Repositories
{
    public class SkypeUserRepository: Repository<SkypeUser>, ISkypeUserRepository
    {
        private SkypeCallContext UserContext => Context as SkypeCallContext;
        public SkypeUserRepository(DbContext context) : base(context)
        {
        }

        public void AddOrCreate(SkypeUser user)
        {
            var currentUser = UserContext.Users
                .FirstOrDefault(u => u.LoginName == user.LoginName);

            if (currentUser != null)
                currentUser.CallHistory.Add(user.CallHistory.Last());
            else
                UserContext.Users.Add(user);
        }
    }
}
