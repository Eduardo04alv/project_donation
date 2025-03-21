using project_donation.context.beneficiaries;
using project_donation.context.donor;
using project_donation.services.donor;
using Microsoft.EntityFrameworkCore;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<donorContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

        builder.Services.AddDbContext<beneficiariesContex>(_options =>
            _options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

        builder.Services.AddTransient<DonationAdoService>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}