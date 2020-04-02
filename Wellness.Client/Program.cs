using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Skclusive.Material.Component;

namespace Wellness.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton<AppState>();
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();

            builder.Services.TryAddMaterialServices(new MaterialConfigBuilder().Build());


            await builder.Build().RunAsync();
        }
    }
}
