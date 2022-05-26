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
                        Id = 1,
                        FirstName = "Rui",
                        Surname = "Pereira",
                        Email = "myemail.pt@here.com",
                        Password = Guid.NewGuid().ToString(),
                    },
                    new Customer
                    {
                        Id = 2,
                        FirstName = "Rui2",
                        Surname = "Pereira",
                        Email = "myemail.pt@here.com",
                        Password = Guid.NewGuid().ToString(),
                    },
                    new Customer
                    {
                        Id = 3,
                        FirstName = "Rui3",
                        Surname = "Pereira",
                        Email = "myemail.pt@here.com",
                        Password = Guid.NewGuid().ToString(),
                    },
                    new Customer
                    {
                        Id = 4,
                        FirstName = "Rui4",
                        Surname = "Pereira",
                        Email = "myemail.pt@here.com",
                        Password = Guid.NewGuid().ToString(),
                    },
                    new Customer
                    {
                        Id = 5,
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
