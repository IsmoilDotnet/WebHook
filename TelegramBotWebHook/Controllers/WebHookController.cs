using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Telegram.Bot.Types;
using TelegramBotWebHook.Services.Handlers;


namespace TelegramBotWebHook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHookController : ControllerBase
    {
        private readonly BotUpdateHandler _botUpdateHandler;

        public WebHookController(BotUpdateHandler botUpdateHandler)
        {
            _botUpdateHandler = botUpdateHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Connector([FromBody] Update update, CancellationToken cancellation)
        {
            await _botUpdateHandler.HandleUpdateAsync(update, cancellation);

            return Ok();
        }
    }
}
