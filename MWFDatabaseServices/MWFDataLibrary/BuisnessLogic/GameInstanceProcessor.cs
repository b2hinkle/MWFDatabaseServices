using MWFDataLibrary.DataAccess;
using MWFModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace MWFDataLibrary.BuisnessLogic
{
    public static class GameInstanceProcessor
    {
        public static int CreateGameInstanceAndReturnId(string connString, int game, string args, string associatedHost)
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
            SqlDataAccess.ModifyData(connString, procedureName, parameters);
            // return the outputed id from the stored procedure
            return parameters.Get<int>("@outId");
        }

        public static int DeleteGameInstanceById(string connString, int id)
        {
            string storedProcedureName = "spGameInstance_DeleteById";

            // make parameters to pass to the stored procedure
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@inId", id, dbType: DbType.Int32);

            return SqlDataAccess.ModifyData(connString, storedProcedureName, parameters);
        }

        public static IEnumerable<GameInstanceModel> GetGameInstances(string connString)
        {
            // New way (using stored procedure)
            string storedProcedureName = "spGameInstance_SelectAll";
            return SqlDataAccess.LoadData<GameInstanceModel>(connString, storedProcedureName);
        }
    }
}
