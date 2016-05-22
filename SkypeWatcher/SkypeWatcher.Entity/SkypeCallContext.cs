using System.Data.Entity;
using SkypeWatcher.Entity.Models;

namespace SkypeWatcher.Entity
{
    public class SkypeCallContext : DbContext 
    {
        public SkypeCallContext(): base("SkypeCall")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual  DbSet<SkypeUser> Users { get; set; }
        public virtual DbSet<CallHistory> History { get; set; }
    }
}
