using MWFDataLibrary.DataAccess;
using MWFModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MWFDataLibrary.BuisnessLogic
{
    public class HostProcessor
    {
        public static async Task<int> CreateHostAndReturnIdAsync(string connectionString, string reqHostIp, string reqHostServicesAPISocketAddress, bool reqIsActive)
        {
            

            throw new NotImplementedException();
        }

        public static int RemoveHost(string serverIP, string connString)
        {
            //  Old way (not using stored procedures and using older function that took in a string sql)
            /*string sql = @"DELETE FROM dbo.ServerTable WHERE ServerIP=@ServerIP;";
            return SqlDataAccess.ModifyDatabase(sql, connString, new { ServerIP = serverIP });*/

            throw new NotImplementedException();
        }

        public static async Task<IEnumerable<HostModel>> GetHostsAsync(string connString)
        {
            string storedProcedureName = "spHost_SelectAll";
            return await SqlDataAccess.LoadDataAsync<HostModel>(connString, storedProcedureName);
        }
    }
}
