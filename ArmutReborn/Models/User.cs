using System;
using System.Collections.Generic;

namespace ArmutReborn.Models
{
    public partial class User
    {
        public User()
        {
            Jobs = new HashSet<Job>();
            MessageAlans = new HashSet<Message>();
            MessageGonderens = new HashSet<Message>();
            WorkListings = new HashSet<WorkListing>();
            Workers = new HashSet<Worker>();
        }

        public uint Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Message> MessageAlans { get; set; }
        public virtual ICollection<Message> MessageGonderens { get; set; }
        public virtual ICollection<WorkListing> WorkListings { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
