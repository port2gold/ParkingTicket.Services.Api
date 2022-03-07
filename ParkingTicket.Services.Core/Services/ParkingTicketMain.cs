using ParkingTicket.Services.Core.Interface;
using ParkingTicket.Services.Data;
using ParkingTicket.Services.Data.Dto;
using ParkingTicket.Services.Data.Models;


namespace ParkingTicket.Services.Core
{
    public class ParkingTicketMain : IParkingTicketMain
    {
        private readonly IparkingRule _parkingRule;
        private readonly IParkingTicket _parkingTicket;

        public ParkingTicketMain(IparkingRule parkingRule, IParkingTicket parkingTicket)
        {
            _parkingRule = parkingRule;
            _parkingTicket = parkingTicket;
        }

        private async Task<CalculateParkingAmountRespDto> CalculateParkingAMount(string EntryTime, string ExitTime)
        {

            var result = new CalculateParkingAmountRespDto();
            //Get Parking Rules
            var parkingRules = await _parkingRule.GetParkingRule();

            var entranceFee = parkingRules.FirstOrDefault(x => x.Description == "Entrance fee").Amount;
            var firstHour = parkingRules.FirstOrDefault(x => x.Description == "First Full or partial hour").Amount;
            var successiveHour = parkingRules.FirstOrDefault(x => x.Description == "Each successive full or partial hour").Amount;
            
            

            var amount = entranceFee;
            var entryTime = DateTime.Parse(EntryTime);
            var exitTime = DateTime.Parse(ExitTime);

            var timeSpan = exitTime.Subtract(entryTime);
            result.HoursSpent = timeSpan.Hours;
            result.Amount = amount;
            if (entryTime == exitTime) return result;


            amount += firstHour;
            while(entryTime >= exitTime)
            {
                amount += successiveHour;

                entryTime.AddHours(1);
            }
            result.Amount = amount;
            return result;
        }
        


        public async Task<bool> ComputesAndSaveTicket(AddTicketDto request)
        {
            var parkingTicket = new ParkingTickets
            {
                EntryTime = request.EntryTime,
                ExitTime = request.ExitTime,
                Date = DateTime.UtcNow,
                Name = "Parking Ticket",

            };

            var response = await CalculateParkingAMount(request.EntryTime, request.EntryTime);

            parkingTicket.HoursSpent = response.HoursSpent;
            parkingTicket.AmountToPay = response.Amount;

            await _parkingTicket.AddParkingTicket(parkingTicket);

            return true;


        }
    }
}
