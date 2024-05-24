using Telegram.Bot;
using TelegramBotWebHook.Models;
using TelegramBotWebHook.Services.UserRepositories;

namespace TelegramBotWebHook.Services.BacgroundServices
{
    public class QalesBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ITelegramBotClient _client;

        public QalesBackgroundService(IServiceScopeFactory serviceScopeFactory, ITelegramBotClient client)
        {
            _scopeFactory = serviceScopeFactory;
            _client = client;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepo>();
                    var users = await userRepository.GetAllUsers();

                    foreach (var user in users)
                    {
                        await SendNotification(user, stoppingToken);
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private Task SendNotification(UserModel user, CancellationToken token)
        {
            try
            {
                return _client.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "Qales",
                    cancellationToken: token);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                return Task.CompletedTask;
            }
        }
    }
}
