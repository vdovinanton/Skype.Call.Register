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
    public class DialogRepository: Repository<Dialog>, IDialogRepository
    {
        private SkypeCallContext DialogContext => Context as SkypeCallContext;

        public DialogRepository(DbContext context) : base(context)
        {
        }

        public void CreateOrUpdate(Dialog dialog)
        {
            var currentClient =
                DialogContext.Dialogs.Include(q => q.Client)
                    .Select(q => q.Client)
                    .FirstOrDefault(q => q.LoginName == dialog.Client.LoginName);

            var currentPartner =
                DialogContext.Dialogs.Include(q => q.Partner)
                    .Select(q => q.Partner)
                    .FirstOrDefault(q => q.LoginName == dialog.Partner.LoginName);

            if (currentClient != null)
            {
                dialog.Client = currentClient;
            }

            if (currentPartner != null)
            {
                dialog.Partner = currentPartner;
            }

            DialogContext.Dialogs.Add(dialog);
        }
    }
}