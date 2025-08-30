using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using persistence.Data;
using persistence.Repositories;
using AutoMapper;
//using AutoMapper.Extensions.Microsoft.DependencyInjection;
using System.Runtime.InteropServices;
using Services.Abstraction;
using Services;
using Services.MappingProfiles;
using Microsoft.AspNetCore.Identity;


namespace ECommerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(opts =>
            {
                opts.Password.RequiredLength = 3;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireDigit = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireLowercase = false;
            })
            .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
                builder.Configuration.GetConnectionString("MongoIdentityConnection"),
                databaseName: "ECommerceAppIdentity")
            .AddDefaultTokenProviders();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            await InitializeDatabaseAsync(app);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            async Task InitializeDatabaseAsync(WebApplication App)
            {
                using var scope = App.Services.CreateScope();
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                await dbInitializer.InitailizeAsync();
                await dbInitializer.InitailizeIdentityAsync();
            }
        }


    }
}
