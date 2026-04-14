
using System.Runtime.Intrinsics.X86;
using CertiForge.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace CertiForge.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<CertiForgeContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"),
                    providerOptions => providerOptions.EnableRetryOnFailure());//very helpfull if 1st time db connection
            });


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                //using scalarapiReference it is like bruno addition we can test api here 
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("My app");
                    options.WithTheme(ScalarTheme.Saturn);
                    options.WithSidebar(false);
                });
                
                //nswag swagger implementation addition we can test api here 

                app.UseSwaggerUi(options => 
                {
                    options.DocumentPath = "openapi/v1.json";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
