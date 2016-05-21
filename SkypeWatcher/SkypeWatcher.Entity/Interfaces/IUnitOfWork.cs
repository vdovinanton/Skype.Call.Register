using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkypeWatcher.Entity.Models;

namespace SkypeWatcher.Entity.Interfaces
{
    /// <summary>
    /// Abtraction layer between functional and database context
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// <see cref="SkypeUser"/> context
        /// </summary>
        ISkypeUserRepository UsersRepository { get; }

        /// <summary>
        /// <see cref="CallHistory"/> context
        /// </summary>
        ICallHistoryRepository History { get; }

        int Complete();
    }
}
