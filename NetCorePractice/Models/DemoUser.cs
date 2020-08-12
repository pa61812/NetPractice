using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePractice.Models
{
    public class DemoUser
    {
        public string Name { get; set; }

        public PhoneNumber PhoneNumber;
        public string Address { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public bool IsActive { get; set; }
    }
}
