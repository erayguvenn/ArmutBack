using ArmutReborn.Models;

namespace ArmutReborn.DTO
{
    public class WorkerDTO
    {
        public uint WorkerId { get; set; }
        public uint UserId { get; set; }
        public string WorkerName { get; set; } = null!;
        public string WorkerSurname { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Adress { get; set; } = null!;


        public static WorkerDTO ToDTO(Worker worker)
        {
            WorkerDTO workerDTO = new WorkerDTO();

            workerDTO.WorkerId = worker.Id;
            workerDTO.UserId = worker.UserId;
            workerDTO.Adress= worker.Adress;
            workerDTO.WorkerName = worker.User.Name;
            workerDTO.WorkerSurname = worker.User.Surname;
            workerDTO.PhoneNumber = worker.User.PhoneNumber;
            workerDTO.Email = worker.User.Email;


            return workerDTO;

        }
    }
}
