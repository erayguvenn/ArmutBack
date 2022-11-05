using System;
using System.Collections.Generic;

namespace ArmutReborn.Models
{
    public partial class WorkListing
    {
        public WorkListing()
        {
            Jobs = new HashSet<Job>();
        }

        public uint Id { get; set; }
        public uint WorkerId { get; set; }
        public uint CategoryId { get; set; }
        public string State { get; set; } = null!;
        public string RuleFill { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual Workcategory Category { get; set; } = null!;
        public virtual Worker Worker { get; set; } = null!;
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
