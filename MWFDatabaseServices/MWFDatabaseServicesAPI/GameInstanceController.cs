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

namespace MWFDatabaseServicesAPI
{
    public static class GameInstanceController
    {
        [FunctionName("CreateGameInstanceAndReturnId")]
        public static async Task<IActionResult> CreateGameInstanceAndReturnId(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("processing CreateGameInstanceAndReturnId endpoint");

            // get the body in json format (there may be a better way to do this)
            string requestBody = await req.ReadAsStringAsync();
            JsonElement jsonBody = JsonSerializer.Deserialize<JsonElement>(requestBody);
            /*GameInstanceModel gameInstance = JsonSerializer.Deserialize<GameInstanceModel>(requestBody);*/

            // get all of the values we need for the GameInstanceProcessor (maybe deserialize json to a GameInstanceModel instead? Or maybe just pass a GameInstanceModel with a null Id to this endpoint)
            int reqGame = jsonBody.GetProperty("Game").GetInt32();
            string reqPort = jsonBody.GetProperty("Port").GetString();
            string reqArgs = jsonBody.GetProperty("Args").GetString();
            int reqHostId = jsonBody.GetProperty("HostId").GetInt32();

            // hard coded connection string for now
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MWFDatabaseServicesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            // call on the data processor and store the returned Id
            int retVal = await GameInstanceProcessor.CreateGameInstanceAndReturnIdAsync(connectionString, reqGame, reqPort, reqArgs, reqHostId);

            // Remember, returning a good result means the game instance is currentlly running
            return new OkObjectResult(retVal);
        }

        [FunctionName("DeleteGameInstanceById")]
        public static async Task<IActionResult> DeleteGameInstanceById(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("gameInstanceID = " /*+ gameinstanceID.ToString()*/);


            // get the body in json format (there may be a better way to do this)
            string requestBody = await req.ReadAsStringAsync();
            int gameInstanceId = int.Parse(requestBody);

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MWFDatabaseServicesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            int rowsDeleted = await GameInstanceProcessor.DeleteGameInstanceByIdAsync(connectionString, gameInstanceId);

            // Passing an int into the OkObjectResult will put the int in the body
            return new OkObjectResult(rowsDeleted);
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

        [FunctionName("GetSomething")]
        public static async Task<IActionResult> GetSomething(
[HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
ILogger log)
        {
            return new OkObjectResult(425);
        }
    }
}
