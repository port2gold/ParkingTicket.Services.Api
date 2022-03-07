using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTicket.Services.Data.Dto
{
    public class CalculateParkingAmountRespDto
    {
        public int HoursSpent { get; set; }
        public decimal Amount { get; set; }
    }
}
