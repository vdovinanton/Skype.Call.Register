using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkypeWatcher.Entity.Models
{
    /// <summary>
    /// Represents <see cref="SkypeUser"/> entity
    /// </summary>
    public class SkypeUser
    {
        /// <summary>
        /// Gets or sets <see cref="SkypeUser"/> identifier
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets <see cref="SkypeUser"/> login name
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// Gets or sets <see cref="SkypeUser"/> call history
        /// </summary>
        public List<CallHistory> CallHistory { get; set; }

        public SkypeUser()
        {
            Id = Guid.NewGuid().ToString();
            CallHistory = new List<CallHistory>();
        }
    }
}
