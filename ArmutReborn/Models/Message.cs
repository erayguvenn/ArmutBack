using System;
using System.Collections.Generic;

namespace ArmutReborn.Models
{
    public partial class Message
    {
        public ulong Id { get; set; }
        public uint BidId { get; set; }
        public uint GonderenId { get; set; }
        public uint AlanId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SentDate { get; set; }

        public virtual User Alan { get; set; } = null!;
        public virtual Bid Bid { get; set; } = null!;
        public virtual User Gonderen { get; set; } = null!;
    }
}
