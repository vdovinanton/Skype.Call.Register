using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkypeWatcher.Entity.Models;

namespace SkypeWatcher.Entity.Interfaces
{
    public interface IDialogRepository: IRepository<Dialog>
    {
        void CreateOrUpdate(Dialog dialog);
    }
}
