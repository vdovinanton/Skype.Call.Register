using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeWatcher.Entity.Models
{
    public class Person
    {
        [Key]
        public string Id { get; set; }
        public string LoginName { get; set; }
        public Person()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
