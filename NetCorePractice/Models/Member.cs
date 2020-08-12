using System;
using System.Collections.Generic;

namespace NetCorePractice.Models
{
    public partial class Member
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Tel { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
