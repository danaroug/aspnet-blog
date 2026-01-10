using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WarbandOfTheSpiritborn.Areas.Identity.IdentityHostingStartup))]

namespace WarbandOfTheSpiritborn.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}