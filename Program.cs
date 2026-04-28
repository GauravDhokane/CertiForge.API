
using System.Runtime.Intrinsics.X86;
using CertiForge.Application;
using CertiForge.Application.Interfaces.Courses;
using CertiForge.Application.Services;
using CertiForge.Infrastructure;
using CertiForge.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddOpenApi();

            //builder.Services.AddAutoMapper(typeof(MappingProfile));
            // Fix for CS1503: Use the correct overload of AddAutoMapper
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();

            //need to chaneg for production
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();
            // configure the http request pipeline
            app.UseCors("default");
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
