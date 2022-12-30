using System;
using System.Collections.Generic;

namespace ArmutReborn.Models
{
    public partial class Worker
    {
        public Worker()
        {
            Bids = new HashSet<Bid>();
        }

        public uint Id { get; set; }
        public string Adress { get; set; } = null!;
        public uint UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
