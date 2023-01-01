namespace ArmutReborn.Models
{
    public class BidDTO
    {
        public uint Id { get; set; }
        public uint WorklistingId { get; set; }
        public uint WorkerId { get; set; }
        public uint Price { get; set; }
        public string? Message { get; set; }
        public bool Accepted { get; set; }

        public static Bid BidConverter(BidDTO biddto)
        {
            Bid bid = new Bid();

            bid.Id = biddto.Id;
            bid.WorklistingId = biddto.WorklistingId;
            bid.WorkerId = biddto.WorkerId;
            bid.Price = biddto.Price;
            bid.Message = biddto.Message;

            return bid;

        }
    }
}
