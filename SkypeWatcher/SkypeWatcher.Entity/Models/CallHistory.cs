using System;
using System.ComponentModel.DataAnnotations;

namespace SkypeWatcher.Entity.Models
{
    /// <summary>
    /// Represents <see cref="CallHistory"/> entity
    /// </summary>
    public class CallHistory
    {
        /// <summary>
        /// Gets or sets call identifier
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets skype user id
        /// </summary>
        public string SkypeUserId { get; set; }

        /// <summary>
        /// Gets or sets call datetime start
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets call datetime end
        /// </summary>
        public DateTime End { get; set; }

        public CallHistory()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
