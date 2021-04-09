using MWFDataLibrary.DataAccess;
using MWFModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace MWFDataLibrary.BuisnessLogic
{
    public class HostProcessor
    {
        public static async Task<int> CreateHostAndReturnIdAsync(string connString, string hostIp, string hostServicesAPISocketAddress, bool isActive)
        {
            // name of stored procedure to execute
            string procedureName = "spHost_CreateAndOutputId";


            // create the data table representation of the user defined host table
            DataTable hostTable = new DataTable("@inHost");
            hostTable.Columns.Add("HostIp");
            hostTable.Columns.Add("HostServicesAPISocketAddress");
            hostTable.Columns.Add("IsActive", typeof(bool));
            // fill in the data
            hostTable.Rows.Add(hostIp, hostServicesAPISocketAddress, isActive);

            // make parameters to pass to the stored procedure
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@inHost", hostTable.AsTableValuedParameter("udtHost"), DbType.Object);
            parameters.Add("@outId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // execute our stored procedure with the parameters we made
            await SqlDataAccess.ModifyDataAsync(connString, procedureName, parameters);
            // return the outputed id from the stored procedure
            return parameters.Get<int>("@outId");
        }

        public static async Task<int> DeleteHostByIdAsync(string connString, int id)
        {
            string storedProcedureName = "spHost_DeleteById";

            // make parameters to pass to the stored procedure
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@inId", id, dbType: DbType.Int32);

            return await SqlDataAccess.ModifyDataAsync(connString, storedProcedureName, parameters);
        }

        public static async Task<IEnumerable<HostModel>> GetHostsAsync(string connString)
        {
            string storedProcedureName = "spHost_SelectAll";
            return await SqlDataAccess.LoadDataAsync<HostModel>(connString, storedProcedureName);
        }
    }
}
