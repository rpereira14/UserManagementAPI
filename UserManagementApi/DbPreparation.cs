using UserManagementApi.Repositories;
using Microsoft.EntityFrameworkCore;
using UserManagementApi.Models;

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

        public static string BuildConnString(ConfigurationManager configuration)
        {
            var server = configuration["DbServer"] ?? "ms-sql-server";
            var port = configuration["DbPort"] ?? "1433";
            var catalog = configuration["DbCatalog"] ?? "Customers";
            var user = configuration["DbUser"] ?? "SA";
            var password = configuration["DbPassword"] ?? "PaSSw0rd2022";

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
