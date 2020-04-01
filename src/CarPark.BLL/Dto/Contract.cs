using System;

namespace CarPark.BLL.Dto
{
    public class Contract
    {
        public int Id { get; set; }

        public DateTime StarTimeContract { get; set; }

        public DateTime EndTimeContract { get; set; }

        public Cars CarId { get; set; }

        public int ContractDays { get; set; }

        public decimal SummaryPrice { get; set; }
    }
}