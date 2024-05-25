using Microsoft.EntityFrameworkCore;
using ProductStore.Api.Extensions;
using ProductStore.Api.Midllewares;
using ProductStore.Data.DbContexts;
using ProductStore.Service.Meppers;
using Serilog;

namespace ProductStore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Set Database Configuration
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            builder.Services.AddCustomServices();
            builder.Services.AddHttpContextAccessor();

            // Logger
            var logger = new LoggerConfiguration()
              .ReadFrom.Configuration(builder.Configuration)
              .Enrich.FromLogContext()
              .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddMemoryCache();


            // swagger set up
            builder.Services.AddSwaggerService();

            // JWT service
            builder.Services.AddJwtService(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAll");

            // Init accessor
            app.InitAccessor();

            //app.InitAccessor();
            app.UseMiddleware<ExceptionHandlerMiddleWare>();


            app.MapControllers();

            app.Run();
        }
    }
}