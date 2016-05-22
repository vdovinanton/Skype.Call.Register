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
        [Key]
        public string Id { get; set; }
        public string LoginName { get; set; }
        public List<CallHistory> CallHistory { get; set; }

        public SkypeUser()
        {
            Id = Guid.NewGuid().ToString();
            CallHistory = new List<CallHistory>();
        }
    }
}
