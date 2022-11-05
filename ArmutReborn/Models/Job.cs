using System;
using System.Collections.Generic;

namespace ArmutReborn.Models
{
    public partial class Job
    {
        public Job()
        {
            Ratings = new HashSet<Rating>();
        }

        public uint Id { get; set; }
        public uint WorkListingId { get; set; }
        public uint EmployerId { get; set; }
        public string State { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual User Employer { get; set; } = null!;
        public virtual WorkListing WorkListing { get; set; } = null!;
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
