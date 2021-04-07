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
    public static class HostController
    {
        [FunctionName("CreateHostAndReturnId")]
        public static async Task<IActionResult> CreateHostAndReturnId(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("processing CreateHostAndReturnId endpoint");

            // get the body in json format
            string requestBody = await req.ReadAsStringAsync();
            JsonElement jsonBody = JsonSerializer.Deserialize<JsonElement>(requestBody);

            // get all of the values we need for the HostProcessor (maybe deserialize json to a HostModel instead? Or maybe just pass a HostModel with a null Id to this endpoint)
            string reqHostIp;
            string reqHostServicesAPISocketAddress;
            bool reqIsActive;
            try
            {
                reqHostIp = jsonBody.GetProperty("HostIp").GetString();
                reqHostServicesAPISocketAddress = jsonBody.GetProperty("HostServicesAPISocketAddress").GetString();
                reqIsActive = jsonBody.GetProperty("IsActive").GetBoolean();
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
                int retVal = await HostProcessor.CreateHostAndReturnIdAsync(connectionString, reqHostIp, reqHostServicesAPISocketAddress, reqIsActive);
                return new OkObjectResult(retVal);
            }
            catch (Exception e)
            {
                log.LogError(e, e.Message);
                return new ConflictObjectResult("Conflict when inserting into the database");
            }
            
            
        }

        [FunctionName("DeleteHostById")]
        public static async Task<IActionResult> DeleteHostById(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // get the body in json format
            string requestBody = await req.ReadAsStringAsync();
            int hostId = int.Parse(requestBody);

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MWFDatabaseServicesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            int rowsDeleted = await HostProcessor.DeleteHostByIdAsync(connectionString, hostId);

            // Passing an int into the OkObjectResult will put the int in the body
            return new OkObjectResult(rowsDeleted);
        }

        [FunctionName("GetHosts")]
        public static async Task<IActionResult> GetHosts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MWFDatabaseServicesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            IEnumerable<HostModel> Hosts = await HostProcessor.GetHostsAsync(connectionString);
            // Passing an IEnumerable into the OkObjectResult will put it in the body
            return new OkObjectResult(Hosts);
        }

    }
}
