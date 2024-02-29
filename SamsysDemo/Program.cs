
using Microsoft.EntityFrameworkCore;
using SamsysDemo.BLL.Services;
using SamsysDemo.DAL;
using SamsysDemo.DAL.Repositories;
using SamsysDemo.Infrastructure.Interfaces.Repositories;

namespace SamsysDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();

                //builder.WithOrigins("http://127.0.0.1:5173", "https://127.0.0.1:5173")
                //       .AllowAnyMethod()
                //       .AllowAnyHeader()
                //       .AllowCredentials();
            }));

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<ClientService>();
            builder.Services.AddTransient<IClientRepository, ClientRepository>();

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

            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                await next();
                var path = context.Request.Path.Value;

                //Se o path estiver vazio ou for para ver o index.html
                //E se não for para obter a API, o swagger ou algum outro ficheiro
                if (path == null || path == "/index.html" || (!path.StartsWith("/api") && !Path.HasExtension(path) && !path.StartsWith("/swagger") && !path.StartsWith("/notificationHub")))
                {
                    //Redirecionamos para o HTML
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}