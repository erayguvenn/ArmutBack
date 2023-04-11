using ArmutReborn.Models;

namespace ArmutReborn.DTO
{
    public class MessageDTO
    {
        public ulong Id { get; set; }
        public uint BidId { get; set; }
        public uint GonderenId { get; set; }
        public uint AlanId { get; set; }
        public string Content { get; set; } = null!;

        public static Message MessageConverter(MessageDTO messagedto)
        {
            Message message = new Message();

            message.Id = messagedto.Id;
            message.BidId = messagedto.BidId;
            message.GonderenId = messagedto.GonderenId;
            message.AlanId = messagedto.AlanId;
            message.Content = messagedto.Content;

            return message;

        }
    }
   
}
