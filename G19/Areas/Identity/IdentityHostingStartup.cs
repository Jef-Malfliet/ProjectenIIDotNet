using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(G19.Areas.Identity.IdentityHostingStartup))]
namespace G19.Areas.Identity {
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}