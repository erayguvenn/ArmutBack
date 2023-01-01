using ArmutReborn.Models;

namespace ArmutReborn.DTO
{
    public class BidAllValueDTO
    {
        public uint Id { get; set; }
        public uint WorklistingId { get; set; }
        public uint WorkerId { get; set; }
        public uint UserId { get; set; }
        public uint Price { get; set; }
        public string? Message { get; set; }
        public bool Accepted { get; set; }
        public string WorkerName { get; set; } = null!;
        public string WorkerSurname { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public static BidAllValueDTO ToDTO(Bid bid)
        {
            BidAllValueDTO bidAllValueDTO = new BidAllValueDTO();

            bidAllValueDTO.Id = bid.Id;
            bidAllValueDTO.WorklistingId = bid.WorklistingId;
            bidAllValueDTO.WorkerId = bid.WorkerId;
            bidAllValueDTO.Price = bid.Price;
            bidAllValueDTO.Message = bid.Message;
            bidAllValueDTO.Accepted = bid.Accepted;
            bidAllValueDTO.UserId = bid.Worker.User.Id;
            bidAllValueDTO.WorkerName = bid.Worker.User.Name;
            bidAllValueDTO.WorkerSurname = bid.Worker.User.Surname;
            bidAllValueDTO.PhoneNumber = bid.Worker.User.PhoneNumber;
            bidAllValueDTO.Email = bid.Worker.User.Email;


            return bidAllValueDTO;

        }

    }
}
