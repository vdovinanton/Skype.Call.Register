using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
