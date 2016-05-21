using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeWatcher.Entity.Models
{
    public class SkypeUser
    {
        public string Id { get; set; }
        public string LoginName { get; set; }
        public IList<CallHistory> CallHistory { get; set; }

        public SkypeUser()
        {
            Id = Guid.NewGuid().ToString();
            CallHistory = new List<CallHistory>();
        }
    }
}
