using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SkypeWatcher.Entity.Models;

namespace SkypeWatcher.Entity
{
    public class SkypeCallContext : DbContext 
    {
        public SkypeCallContext(): base("SkypeRegister")
        {
        }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<CallHistory> History { get; set; }
        public virtual DbSet<Dialog> Dialogs { get; set; }
        public virtual  DbSet<Person> Users { get; set; }
    }

}
