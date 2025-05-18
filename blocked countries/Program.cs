using AutoMapper;
using blocked_countries.Helper;
using blocked_countries.Middleware;
using blockedCountries.Core.Entities;
using blockedCountries.Core.Repositories;
using blockedCountries.Services;
using Microsoft.AspNetCore.Mvc;

namespace blocked_countries
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IBlockCountries, BlockCountriesServices>();
            builder.Services.AddSingleton<IIPAddress,IPAddressServices>();
            builder.Services.AddSingleton<IAccessLog, AccessLogServices>();
            builder.Services.AddSingleton<ITemporalBlockService, TemporalBlockService>();
            builder.Services.AddHostedService<TemporalBlockCleanupService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddHttpClient();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
