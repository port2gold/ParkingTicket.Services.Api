using ParkingTicket.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTicket.Services.Core.Interface
{
    public interface IParkingTicketMain
    {
        Task<bool> ComputesAndSaveTicket(AddTicketDto request);


    }
}
