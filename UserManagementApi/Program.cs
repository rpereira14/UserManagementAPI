using UserManagementApi.Core;
using UserManagementApi.Repositories;
using Microsoft.EntityFrameworkCore;
using UserManagementApi.Managers;

namespace UserManagementApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            // Create singleton model of the appSettings.json
            var settings = builder.Configuration.Get<Settings>();
            builder.Services.AddSingleton(s => settings);

            var connectionString = DbPreparation.BuildConnString(builder.Configuration, settings);

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerManager, CustomerManager>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //Configure the HTTP request pipeline.
            // if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}


            DbPreparation.Populate(app);
            
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}