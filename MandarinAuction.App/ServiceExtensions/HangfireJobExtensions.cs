using MandarinAuction.App.Schedulers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace MandarinAuction.App.ServiceExtensions
{
    public static class HangfireJobExtensions
    {
        public static IApplicationBuilder UseHangfireJobs(this IApplicationBuilder app, IConfiguration configuration)
        {
            JobsScheduler.Start(configuration); 

            return app;
        }
    }
}