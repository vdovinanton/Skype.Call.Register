using System.Data.Entity;
using System.Linq;
using SkypeWatcher.Entity.Interfaces;
using SkypeWatcher.Entity.Models;

namespace SkypeWatcher.Entity.Repositories
{
    public class PersonRepository: Repository<Person>, IPersonRepository
    {
        private SkypeCallContext UserContext => Context as SkypeCallContext;
        public PersonRepository(DbContext context) : base(context)
        {
        }
    }
}
