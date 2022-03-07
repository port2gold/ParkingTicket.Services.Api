using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTicket.Services.Data.Models
{
    public class ParkingTickets
    {
        public int Id { get; set; }
        public string EntryTime { get; set; }
        public string ExitTime { get; set; }
        public string Name { get; set; }
        public decimal AmountToPay { get; set; }
        public DateTime Date { get; set; }
        public int HoursSpent { get; set; }
    }
}
