using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;

namespace SkypeWatcher.Entity.Models
{
    public enum UserStatus
    {
        NotFound = 1,
        EmptyAccountBalance = 2,
        // ReSharper disable once InconsistentNaming
        OK = 3
    }
    public class ViewModel
    {
        public string Name { get; set; }
        public TCallStatus Status { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
