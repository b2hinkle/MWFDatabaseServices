using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MWFDataLibrary.BuisnessLogic;
using System.Text.Json;
using MWFModelsLibrary.Models;
using System.Collections.Generic;
using System.Web;
using System.Collections.Specialized;

namespace MWFDatabaseServicesAPI
{
    public static class GameInstanceController
    {
        [FunctionName("CreateGameInstanceAndReturnId")]
        public static async Task<IActionResult> CreateGameInstanceAndReturnId(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await req.ReadAsStringAsync();
            JsonElement jsonBody = JsonSerializer.Deserialize<JsonElement>(requestBody);
            /*GameInstanceModel gameInstance = JsonSerializer.Deserialize<GameInstanceModel>(requestBody);*/

            // get all of the values we need for the GameInstanceProcessor (maybe deserialize json to a GameInstanceModel instead? Or maybe just pass a GameInstanceModel with a null Id to this endpoint)
            int reqGame;
            string reqPort;
            string reqArgs;
            int reqHostId;
            try
            {
                reqGame = jsonBody.GetProperty("Game").GetInt32();
                reqPort = jsonBody.GetProperty("Port").GetString();
                reqArgs = jsonBody.GetProperty("Args").GetString();
                reqHostId = jsonBody.GetProperty("HostId").GetInt32();
            }
            catch (Exception e)
            {
                log.LogError(e, e.Message);
                return new BadRequestObjectResult("Request didn't meet syntax requirements (make sure you include everything and have the correct property types)");
            }


            // call on the data processor and store the returned Id
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MWFDatabaseServicesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                int retVal = await GameInstanceProcessor.CreateGameInstanceAndReturnIdAsync(connectionString, reqGame, reqPort, reqArgs, reqHostId);
                // Returning a success code means we continue running the newly created game instance process
                return new OkObjectResult(retVal);
            }
            catch (Exception e)
            {
                log.LogError(e, e.Message);
                return new ConflictObjectResult("Conflict when inserting into the database");
            }

        }

        [FunctionName("DeleteGameInstanceById")]
        public static async Task<IActionResult> DeleteGameInstanceById(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            int gameInstanceId;
            try
            {
                NameValueCollection queryStringMap = HttpUtility.ParseQueryString(req.QueryString.Value);
                gameInstanceId = int.Parse(queryStringMap["Id"]);
            }
            catch (Exception e)
            {
                log.LogError(e, e.Message);
                return new BadRequestObjectResult("Request didn't meet syntax requirements (make sure you include everything and have the correct property types)");
            }
            
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MWFDatabaseServicesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // Maybe have the sp throw exception if id passed in doesn't exist and we can do a try catch here to give a more specific response message
            await GameInstanceProcessor.DeleteGameInstanceByIdAsync(connectionString, gameInstanceId);
            return new OkObjectResult("If correct Id was passed in, row deleted");
        }

        [FunctionName("GetGameInstances")]
        public static async Task<IActionResult> GetGameInstances(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MWFDatabaseServicesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            IEnumerable<GameInstanceModel> GameInstances = await GameInstanceProcessor.GetGameInstancesAsync(connectionString);
            // Passing an IEnumerable into the OkObjectResult will put it in the body
            return new OkObjectResult(GameInstances);
        }

        [FunctionName("GetGameInstancesWithHostIps")]
        public static async Task<IActionResult> GetGameInstancesWithHostIps(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
    ILogger log)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MWFDatabaseServicesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            IEnumerable<GameInstanceWithHostIpModel> GameInstancesWithHostIps = await GameInstanceProcessor.GetGameInstancesWithHostsAsync(connectionString);
            // Passing an IEnumerable into the OkObjectResult will put it in the body
            return new OkObjectResult(GameInstancesWithHostIps);
        }
    }
}
