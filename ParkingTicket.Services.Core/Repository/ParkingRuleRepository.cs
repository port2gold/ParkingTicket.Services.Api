using ParkingTicket.Services.Core.Interface;
using ParkingTicket.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTicket.Services.Core.Repository
{
    public class ParkingRuleRepository :IparkingRule
    {
 
        private async Task<List<ParkingRule>> GetParkingRules()
        {
            
            var connectionString = "Server=tcp:SQL5091.site4now.net;Database=db_a483f5_usertest;User ID=db_a483f5_usertest_admin;Password=userTester.1";
            var parkingRulesList = new List<ParkingRule>();
            using (var conn = new SqlConnection(connectionString))
            {

                await conn.OpenAsync();
                var sql = "SELECT  * FROM [db_a483f5_usertest].[dbo].[PackingRules]";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ParkingRule parkingRule = new ParkingRule();
                            parkingRule.Description = Convert.ToString(reader["RuleDescription"]);
                            parkingRule.Amount = Convert.ToDecimal(reader["Amount"]);
                            parkingRule.Id = Convert.ToInt32(reader["Id"]);
                            parkingRulesList.Add(parkingRule);
                        }
                    }

                }
            }


            return parkingRulesList;
        }
        public async Task<List<ParkingRule>> GetParkingRule()
        {
            var result = await GetParkingRules();

            if (!result.Any())
                return new List<ParkingRule>();

            return result; 
        }
    }
}
