using UserManagementApi.Repositories;
using Microsoft.EntityFrameworkCore;
using UserManagementApi.Models;
using UserManagementApi.Core;

namespace UserManagementApi
{
    public static class DbPreparation
    {
        public static void Populate(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DataContext>();
                if(context != null) FeedDB(context);
            }
        }

        public static string BuildConnString(ConfigurationManager configuration, Settings settings)
        {
            var server = configuration["DbServer"] ?? settings.DbServer;
            var port = configuration["DbPort"] ?? settings.DbPort;
            var catalog = configuration["DbCatalog"] ?? settings.DbCatalog;
            var user = configuration["DbUser"] ?? settings.DbUser;
            var password = configuration["DbPassword"] ?? settings.DbPassword;

            return $"Server={server},{port}; Initial Catalog={catalog};User ID = {user}; Password={password}";
        }

        public static void FeedDB(DataContext context)
        {
            System.Console.WriteLine("Starting migrations...");
            context.Database.Migrate();
            if (!context.Customers.Any())
            {
                Console.WriteLine("Inserting data...");
                context.Customers.AddRange(
                    new Customer
                    {
                        FirstName = "Rui",
                        Surname = "Pereira",
                        Email = "myemail.pt@here.com",
                        Password = Guid.NewGuid().ToString(),
                    },
                    new Customer
                    {
                        FirstName = "Rui2",
                        Surname = "Pereira",
                        Email = "myemail.pt@here.com",
                        Password = Guid.NewGuid().ToString(),
                    },
                    new Customer
                    {
                        FirstName = "Rui3",
                        Surname = "Pereira",
                        Email = "myemail.pt@here.com",
                        Password = Guid.NewGuid().ToString(),
                    },
                    new Customer
                    {
                        FirstName = "Rui4",
                        Surname = "Pereira",
                        Email = "myemail.pt@here.com",
                        Password = Guid.NewGuid().ToString(),
                    },
                    new Customer
                    {
                        FirstName = "Rui5",
                        Surname = "Pereira",
                        Email = "myemail.pt@here.com",
                        Password = Guid.NewGuid().ToString(),
                    });
                context.SaveChanges();
            }
            else
                Console.WriteLine("Already have data...");

        }
    }
}
