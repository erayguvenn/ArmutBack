using System;
using System.Collections.Generic;

namespace ArmutReborn.Models
{
    public partial class WorkListing
    {
        public WorkListing()
        {
            Bids = new HashSet<Bid>();
            Jobs = new HashSet<Job>();
        }

        public uint Id { get; set; }
        public uint UserId { get; set; }
        public uint CategoryId { get; set; }
        public string State { get; set; } = null!;
        public string RuleFill { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual Workcategory Category { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
