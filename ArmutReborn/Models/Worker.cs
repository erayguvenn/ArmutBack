using System;
using System.Collections.Generic;

namespace ArmutReborn.Models
{
    public partial class Worker
    {
        public Worker()
        {
            WorkListings = new HashSet<WorkListing>();
        }

        public uint Id { get; set; }
        public string Adress { get; set; } = null!;
        public uint UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<WorkListing> WorkListings { get; set; }
    }
}
