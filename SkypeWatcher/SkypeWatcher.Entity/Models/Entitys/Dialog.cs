using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeWatcher.Entity.Models
{
    public class Dialog
    {
        [Key]
        public string Id { get; set; }
        public string PartnerId { get; set; }
        public Person Partner { get; set; }
        public string ClientId { get; set; }
        public Person Client { get; set; }

        public List<CallHistory> CallHistory { get; set; }
        public Dialog()
        {
            Id = Guid.NewGuid().ToString();
            CallHistory = new List<CallHistory>();
        }
    }
}

