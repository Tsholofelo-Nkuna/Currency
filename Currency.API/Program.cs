using Currency.Service;
using Currency.SQLServer.DAL;
using Currency.SQLServer.DAL.Repositories;

namespace Currency.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCurrencyServices(builder.Configuration);
            builder.Services.AddCors(config =>
            {
                config.AddDefaultPolicy(policyConfig =>
                {
                    policyConfig.AllowAnyOrigin();
                    policyConfig.AllowAnyMethod();
                    policyConfig.AllowAnyHeader();
                });
            });
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
            app.UseCors();
           // app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
