#region usings

using MandarinAuction.App.Middlewares;
using MandarinAuction.App.ServiceExtensions;
using MandarinAuction.Infrastructure.Data;
using MandarinAuction.UIModels.Mappings.DI;
using Microsoft.EntityFrameworkCore;

#endregion

namespace MandarinAuction.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddJwtAuthentication(builder.Configuration);

        builder.Services.AddDbContext<MsDbContext>(
            opt => opt.UseSqlServer(
                builder.Configuration.GetConnectionString("MsDbConnect")));

        builder.Services.AddRepositories();
        builder.Services.AddBusinessServices();
        builder.Services.AddUtilities();

        builder.Services.AddHangfireStorage(builder.Configuration);

        builder.Services.AddMapper();
        builder.Services.AddUiMapper();

        builder.Services.AddControllersWithViews();
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseExceptionsHandler();
        app.UseHangfireJobs(app.Configuration);

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            "default",
            "{controller=Auctions}/{action=Index}/{id?}");

        app.Run();
    }
}