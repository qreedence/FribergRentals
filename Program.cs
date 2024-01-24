using FribergRentals.Data;
using FribergRentals.Data.Interfaces;
using FribergRentals.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using FribergRentals.Utilities;

namespace FribergRentals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddTransient<ICar, CarRepo>();
            builder.Services.AddTransient<IAppUser, AppUserRepo>();
            builder.Services.AddTransient<IOrder, OrderRepo>();
            builder.Services.AddTransient<IAdmin, AdminRepo>();
            builder.Services.AddTransient<IUser, UserRepo>();
            builder.Services.AddScoped<SessionUtility>();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();
            app.MapRazorPages();

            app.Run();
        }
    }
}
