namespace ArmutReborn.Models
{
    public class WorkListingDTOcs
    {

        public uint CategoryId { get; set; }
        public string State { get; set; } = null!;
        public string RuleFill { get; set; } = null!;
        public uint UserId { get; set; }

        public static WorkListing WorkListingConverter(WorkListingDTOcs worklistingdto)
        {
            WorkListing work = new WorkListing();

            work.CategoryId = worklistingdto.CategoryId;
            work.State = worklistingdto.State;
            work.RuleFill = worklistingdto.RuleFill;
            work.UserId = worklistingdto.UserId;
            return work;

        }
    }
}
