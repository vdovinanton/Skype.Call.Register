using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeWatcher.Entity.Models
{
    public class CallHistory
    {
        [Key]
        public string Id { get; set; }
        public string SkypeUserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public CallHistory()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
