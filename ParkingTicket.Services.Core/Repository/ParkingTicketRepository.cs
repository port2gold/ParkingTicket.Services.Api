using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingTicket.Services.Core.Interface;
using ParkingTicket.Services.Data.Models;

namespace ParkingTicket.Services.Core.Repository
{
    public class ParkingTicketRepository : IParkingTicket
    {

        private async Task<List<ParkingTickets>> GetParkingTickets()
        {
            var connectionString = "Server=tcp:SQL5091.site4now.net;Database=db_a483f5_usertest;User ID=db_a483f5_usertest_admin;Password=userTester.1";
            var parkingTicketList = new List<ParkingTickets>();
            using (var conn = new SqlConnection(connectionString))
            {

                await conn.OpenAsync();
                var sql = "SELECT  * FROM [db_a483f5_usertest].[dbo].[PackingTickets]";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ParkingTickets parkingTicket = new ParkingTickets();
                            parkingTicket.AmountToPay = Convert.ToDecimal(reader["AmountTopay"]);
                            parkingTicket.EntryTime = Convert.ToString(reader["EntryTime"]);
                            parkingTicket.ExitTime = Convert.ToString(reader["ExitTime"]);
                            parkingTicket.Id = Convert.ToInt32(reader["Id"]);
                            parkingTicket.HoursSpent = Convert.ToInt32(reader["HoursSpent"]);
                            parkingTicket.Date = Convert.ToDateTime(reader["Date"]);
                            parkingTicket.Name = Convert.ToString(reader["Name"]);

                            parkingTicketList.Add(parkingTicket);
                        }
                    }

                }
            }

            return parkingTicketList;

        }

        private async Task<bool> AddParkingTicketToDb(ParkingTickets parkingTicket)
        {
            var connectionString = "Server=tcp:SQL5091.site4now.net;Database=db_a483f5_usertest;User ID=db_a483f5_usertest_admin;Password=userTester.1";
            using (var conn = new SqlConnection(connectionString))
            {

                await conn.OpenAsync();
                var sql = "INSERT INTO [db_a483f5_usertest].[dbo].[PackingTickets] VALUES(@AmountToPay, @EntryTime, @ExitTIme, @HoursSpent, @Date, @Name)";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            command.Parameters.Add(new SqlParameter("AmountToPay", parkingTicket.AmountToPay));
                            command.Parameters.Add(new SqlParameter("Name", parkingTicket.Name));
                            command.Parameters.Add(new SqlParameter("EntryTime", parkingTicket.EntryTime));
                            command.Parameters.Add(new SqlParameter("ExitTime", parkingTicket.ExitTime));
                            command.Parameters.Add(new SqlParameter("HoursSpent", parkingTicket.HoursSpent));
                            command.ExecuteNonQuery();

                        }
                    }

                }
                return true;
            }


        }
        public async Task<List<ParkingTickets>> GetParkingTickets(DateTime date)
        {
            var result = await GetParkingTickets();

            if (!result.Any())
                return new List<ParkingTickets>();
            return result.Where(x => x.Date.Date == date.Date).ToList();
        }

        public  async Task<bool> AddParkingTicket(ParkingTickets parkingTicket)
        {
            var addTicket = await AddParkingTicketToDb(parkingTicket);
            return addTicket;
        }
    }
}
