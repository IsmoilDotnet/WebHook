using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TelegramBotWebHook.Models;

namespace TelegramBotWebHook.Persistance
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
