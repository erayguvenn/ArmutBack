using System;
using System.Collections.Generic;

namespace ArmutReborn.Models
{
    public partial class Rating
    {
        public uint Id { get; set; }
        public uint JobId { get; set; }
        public string Comment { get; set; } = null!;
        public sbyte Star { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Job Job { get; set; } = null!;
    }
}
