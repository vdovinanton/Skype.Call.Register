using System;
using SkypeWatcher.Entity.Models;

namespace SkypeWatcher.Entity.Interfaces
{
    /// <summary>
    /// Abtraction layer between functional and database context
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// <see cref="Person"/> context
        /// </summary>
        IPersonRepository UsersRepository { get; }

        /// <summary>
        /// <see cref="CallHistory"/> context
        /// </summary>
        ICallHistoryRepository History { get; }

        int Complete();
    }
}
