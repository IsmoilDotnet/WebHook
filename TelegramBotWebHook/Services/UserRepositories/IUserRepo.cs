using TelegramBotWebHook.Models;

namespace TelegramBotWebHook.Services.UserRepositories
{
    public interface IUserRepo
    {
        public Task Add(UserModel user);
        public Task<List<UserModel>> GetAllUsers();
    }
}
