using Microsoft.EntityFrameworkCore;
using TelegramBotWebHook.Models;
using TelegramBotWebHook.Persistance;

namespace TelegramBotWebHook.Services.UserRepositories
{
    public class UserRepo : IUserRepo
    {
        private readonly BotDbContext _botDbContext;

        public UserRepo(BotDbContext botDbContext)
        {
            _botDbContext = botDbContext;
        }

        public async Task Add(UserModel user)
        {
            var res = await _botDbContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (res != null)
            {
                return;
            }
            await _botDbContext.Users.AddAsync(user);
            await _botDbContext.SaveChangesAsync();
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _botDbContext.Users.ToListAsync();
        }
    }
}
