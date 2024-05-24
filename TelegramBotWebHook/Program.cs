
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramBotWebHook.Models;
using TelegramBotWebHook.Persistance;
using TelegramBotWebHook.Services.BacgroundServices;
using TelegramBotWebHook.Services.Handlers;
using TelegramBotWebHook.Services.UserRepositories;

namespace TelegramBotWebHook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<HostOptions>(options =>
            {
                options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
            });

            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddDbContext<BotDbContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=WebHookDb;User Id=postgres;Password=psqlDB;");
            });
            builder.Services.AddScoped(p => new TelegramBotClient("7161612621:AAFta4VzSXhHbrDJx5NT6ep16PmoDp9eFTw"));

            var botConfig = builder.Configuration.GetSection("BotConfiguration")
           .Get<BotConfiguration>();

            builder.Services.AddHttpClient("webhook")
                .AddTypedClient<ITelegramBotClient>(httpClient
                    => new TelegramBotClient(botConfig.Token, httpClient));

            builder.Services.AddHostedService<ConfigureWebhook>();


            builder.Services.AddHostedService<QalesBackgroundService>();

            var app = builder.Build();



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
