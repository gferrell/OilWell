using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OilWellToolService.Data;
using OilWellToolService.Controllers;

namespace OilWellToolService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<OilWellToolServiceContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("OilWellToolServiceContext") ?? throw new InvalidOperationException("Connection string 'OilWellToolServiceContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapOilWellToolEndpoints();

            app.Run();
        }
    }
}