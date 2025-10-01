using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace persistence.Data
{
    public class DbInitializer(StoreContext storeContext, UserManager<ApplicationUser> _users,
        RoleManager<ApplicationRole> _roles, IRabbitMQPublisherService rabbitMQPublisher) : IDbInitializer
    {
        public async Task InitailizeAsync()
        {
            if(storeContext.Database.GetPendingMigrations().Any())
                await storeContext.Database.MigrateAsync();

            if(!storeContext.ProductBrands.Any())
            {
                var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\persistence\Data\Seeding\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if(brands != null && brands.Any())
                {
                    await storeContext.AddRangeAsync(brands);
                    await storeContext.SaveChangesAsync();
                }
            }
            if (!storeContext.ProductTypes.Any())
            {
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\persistence\Data\Seeding\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (types != null && types.Any())
                {
                    await storeContext.AddRangeAsync(types);
                    await storeContext.SaveChangesAsync();
                }
            }
            if (!storeContext.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\persistence\Data\Seeding\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (products != null && products.Any())
                {
                    await storeContext.AddRangeAsync(products);
                    await storeContext.SaveChangesAsync();
                }
            }
        }

        public async Task InitailizeIdentityAsync()
        {
            // Roles -------------------------------------------------
            foreach (var role in new[] { "Admin", "User", "SuperAdmin" })
                if (!await _roles.RoleExistsAsync(role))
                    await _roles.CreateAsync(new ApplicationRole { Name = role });

            // Users -------------------------------------------------
            if (!_users.Users.Any())
            {
                var admin = new ApplicationUser
                {
                    UserName = "S-12",
                    Email = "admin@super.com",
                    DisplayName = "Admin User",
                };

                var super = new ApplicationUser
                {
                    UserName = "SA12",
                    Email = "super@super.com",
                    DisplayName = "Super Admin User"
                };

                await _users.CreateAsync(admin, "P@ssw0rd");
                await _users.CreateAsync(super, "P@ssw0rd");

                await _users.AddToRoleAsync(admin, "Admin");
                await _users.AddToRoleAsync(super, "SuperAdmin");
            }
        }
        public async Task InitializeRabbitMQAsync()
        {
            const string exchangeName = "Product";
            const string addedQueue = "Added_Products";
            const string deletedQueue = "Deleted_Products";

            // 1. Create exchange
            await rabbitMQPublisher.CreateExchange(exchangeName, "direct");

            // 2. Create queues
            await rabbitMQPublisher.CreateQueue(addedQueue);
            await rabbitMQPublisher.CreateQueue(deletedQueue);

            // 3. Bind queues
            await rabbitMQPublisher.BindQueue(addedQueue, exchangeName, "Key.Added");
            await rabbitMQPublisher.BindQueue(deletedQueue, exchangeName, "Key.Deleted");
        }
    }
}
    
