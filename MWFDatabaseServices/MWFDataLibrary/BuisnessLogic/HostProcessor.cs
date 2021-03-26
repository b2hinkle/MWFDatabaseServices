using MWFDataLibrary.DataAccess;
using MWFModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWFDataLibrary.BuisnessLogic
{
    public class HostProcessor
    {
        public static int CreateHost(string hostIp, string hostServicesAPISocketAddress, bool isActive, string connString)
        {
            var parameters = new
            {
                HostIp = hostIp,
                HostServicesAPISocketAddress = hostServicesAPISocketAddress,
                IsActive = isActive
            };

            //  Old way (not using stored procedures and using older function that took in a string sql)
            /*string sql = @"insert into dbo.ServerTable (ServerIP, GameInstancesManagementApiIp, GameInstancesManagementApiPort, IsActive)
                         values (@ServerIP, @GameInstancesManagementApiIp, @GameInstancesManagementApiPort, @IsActive);";
            return SqlDataAccess.ModifyDatabase(connString, sql, parameters);*/

            throw new NotImplementedException();
        }

        public static int RemoveHost(string serverIP, string connString)
        {
            //  Old way (not using stored procedures and using older function that took in a string sql)
            /*string sql = @"DELETE FROM dbo.ServerTable WHERE ServerIP=@ServerIP;";
            return SqlDataAccess.ModifyDatabase(sql, connString, new { ServerIP = serverIP });*/

            throw new NotImplementedException();
        }

        public static IEnumerable<HostModel> LoadHosts(string connString)
        {
            //  Old way (not using stored procedures and using older function that took in a string sql)
            /*string sql = @"select * from dbo.ServerTable;";
            return SqlDataAccess.LoadData<HostModel>(sql, connString);*/

            throw new NotImplementedException();
        }
    }
}
