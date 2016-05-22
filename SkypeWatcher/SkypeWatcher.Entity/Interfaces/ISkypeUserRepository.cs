using SkypeWatcher.Entity.Models;

namespace SkypeWatcher.Entity.Interfaces
{
    /// <summary>
    /// Represents <see cref="SkypeUser"/> repository
    /// </summary>
    public interface ISkypeUserRepository : IRepository<SkypeUser>
    {
       /// <summary>
       /// Create or modifier user
       /// </summary>
       /// <param name="user">Current <see cref="SkypeUser"/></param>
        void AddOrCreate(SkypeUser user);
    }
}
