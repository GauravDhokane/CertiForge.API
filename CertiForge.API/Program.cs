using CertiForge.Application;
using CertiForge.Application.DTOValidations;
using CertiForge.Application.Interfaces.Courses;
using CertiForge.Application.Services;
using CertiForge.Infrastructure;
using CertiForge.Infrastructure.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Scalar.AspNetCore;
using Serilog;

namespace CertiForge.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddApplicationInsightsTelemetry(); 

            builder.Services.AddDbContext<CertiForgeContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"),
                    providerOptions => providerOptions.EnableRetryOnFailure());//very helpfull if 1st time db connection
            }); 


            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<Filters.ValidationFilter>(); ///addding our custom validation filter to the global filter collection so that it will be applied to all the controllers 
                options.Filters.Add<Filters.GlobalExceptionFilter>();// all the controllers and actions in the application and we don't need to add it to each controller or action separately     
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddOpenApi();


            //here all configuration and additionof azure b2c is done and we are also adding some events
            //to log the errors and to log the scope claim if it is present in the token for debugging purpose
            //and we are using serilog for logging the information and errors in the console and in the file as well
            

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddMicrosoftIdentityWebApi(options =>
              {
                  builder.Configuration.Bind("AzureAdB2C", options);

                  options.Events = new JwtBearerEvents
                  {

                      OnTokenValidated = context =>
                      {
                          var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                          // Access the scope claim (scp) directly
                          var scopeClaim = context.Principal?.Claims.FirstOrDefault(c => c.Type == "scp")?.Value;

                          if (scopeClaim != null)
                          {
                              logger.LogInformation("Scope found in token: {Scope}", scopeClaim);
                          }
                          else
                          {
                              logger.LogWarning("Scope claim not found in token.");
                          }


                          return Task.CompletedTask;
                      },
                      OnAuthenticationFailed = context =>
                      {
                          var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                          logger.LogError("Authentication failed: {Message}", context.Exception.Message);
                          return Task.CompletedTask;
                      },
                      OnChallenge = context =>
                      {
                          var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                          logger.LogError("Challenge error: {ErrorDescription}", context.ErrorDescription);
                          return Task.CompletedTask;
                      }
                  };
              }, options => { builder.Configuration.Bind("AzureAdB2C", options); });


            //builder.Services.AddAutoMapper(typeof(MappingProfile));
            // Fix for CS1503: Use the correct overload of AddAutoMapper
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            builder.Services.AddValidatorsFromAssemblyContaining<CreateCourseValidator>();

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
