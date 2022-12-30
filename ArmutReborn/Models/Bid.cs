using System;
using System.Collections.Generic;

namespace ArmutReborn.Models
{
    public partial class Bid
    {
        public uint Id { get; set; }
        public uint WorklistingId { get; set; }
        public uint WorkerId { get; set; }
        public uint Price { get; set; }
        public string? Message { get; set; }
        public bool Accepted { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual Worker Worker { get; set; } = null!;
        public virtual WorkListing Worklisting { get; set; } = null!;
    }
}
