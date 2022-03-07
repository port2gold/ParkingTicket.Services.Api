using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTicket.Services.Data.Models
{
    public class ParkingRule
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Decimal Amount { get; set; }
    }
}
