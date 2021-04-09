using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(MWFDatabaseServicesAPI.Startup))]

namespace MWFDatabaseServicesAPI
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //  Basic HTTP client factory usage
            builder.Services.AddHttpClient();
        }
    }
}
