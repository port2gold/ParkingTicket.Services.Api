using Microsoft.AspNetCore.Mvc;
using ParkingTicket.Services.Core.Interface;
using ParkingTicket.Services.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingTicket.Services.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ParkingTicketController : ControllerBase
    {
        private readonly IParkingTicket _parkingTicket;
        private readonly IParkingTicketMain _mainService;

        public ParkingTicketController(IParkingTicket parkingTicket, IParkingTicketMain mainService)
        {
            _parkingTicket = parkingTicket;
            _mainService = mainService;
        }
        // GET: api/<ParkingTicketController>
        [HttpGet("GetParkingTicket")]
        public async Task<IActionResult> GetListParkingTicket([FromQuery] DateTime dateTime)
        {
            try
            {
                var result = await _parkingTicket.GetParkingTickets(dateTime);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

      

        // POST api/<ParkingTicketController>
        [HttpPost("ComputesAddParkingTicket")]
        public async Task<IActionResult >AddParkingTicket([FromBody] AddTicketDto request)
        {
            try
            {
                var result =await _mainService.ComputesAndSaveTicket(request);
                return Ok(result); 

            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
