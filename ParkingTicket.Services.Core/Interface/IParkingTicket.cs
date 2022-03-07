using ParkingTicket.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTicket.Services.Core.Interface
{
    public interface IParkingTicket
    {
        Task<List<ParkingTickets>> GetParkingTickets(DateTime date);

        Task<bool> AddParkingTicket(ParkingTickets parkingTicket);



    }
}
