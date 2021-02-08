using MWFDataLibrary.DataAccess;
using MWFModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Threading.Tasks;

namespace MWFDataLibrary.BuisnessLogic
{
    public static class GameInstanceProcessor
    {
        public static async Task<int> CreateGameInstanceAndReturnIdAsync(string connString, int game, string args, string associatedHost)
        {
            // name of stored procedure to execute
            string procedureName = "spGameInstance_CreateAndOutputId";


            // create the data table representation of the user defined game instance table
            DataTable gameInstanceTable = new DataTable("@inGameInstance");
            gameInstanceTable.Columns.Add("Game", typeof(int));
            gameInstanceTable.Columns.Add("Args");
            gameInstanceTable.Columns.Add("AssociatedHost");
            // fill in the data
            gameInstanceTable.Rows.Add(game, args, associatedHost);

            // make parameters to pass to the stored procedure
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@inGameInstance", gameInstanceTable.AsTableValuedParameter("udtGameInstance"), DbType.Object);
            parameters.Add("@outId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // execute our stored procedure with the parameters we made
            await SqlDataAccess.ModifyDataAsync(connString, procedureName, parameters);
            // return the outputed id from the stored procedure
            return parameters.Get<int>("@outId");
        }

        public static async Task<int> DeleteGameInstanceByIdAsync(string connString, int id)
        {
            string storedProcedureName = "spGameInstance_DeleteById";

            // make parameters to pass to the stored procedure
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@inId", id, dbType: DbType.Int32);

            return await SqlDataAccess.ModifyDataAsync(connString, storedProcedureName, parameters);
        }

        public static async Task<IEnumerable<GameInstanceModel>> GetGameInstancesAsync(string connString)
        {
            string storedProcedureName = "spGameInstance_SelectAll";
            return await SqlDataAccess.LoadDataAsync<GameInstanceModel>(connString, storedProcedureName);
        }
    }
}
