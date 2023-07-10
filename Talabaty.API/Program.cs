using Microsoft.EntityFrameworkCore;
using Talabaty.API.Middleware;
using Talabaty.BLL.Interfaces;
using Talabaty.BLL.Repositories;
using Talabaty.DAL.Data;

namespace Talabaty.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // To register AutoMapper in the Program

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });


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

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();


            app.MapControllers();

            // To make the app update the database automatic using this:

            using var scope = app.Services.CreateScope();
            var Servies = scope.ServiceProvider;
            var context = Servies.GetRequiredService<StoreContext>();
            var logger = Servies.GetRequiredService<ILogger<Program>>();

            try
            {
                await context.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while migrating process!");
            }


            app.Run();
        }
    }
}