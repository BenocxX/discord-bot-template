using DSharpPlus;
using Serilog;

namespace DiscordBotTemplate.Core.Libs;

public static class Discord
{
    public static DiscordClient CreateClient()
    {
        var token = Environment.GetEnvironmentVariable("FAMIGLIO_DISCORD_TOKEN");

        if (string.IsNullOrEmpty(token))
        {
            Log.Error("Discord bot token is null or empt.");
            throw new NullReferenceException();
        }

        return new DiscordClient(new DiscordConfiguration
        {
            Token = token,
            TokenType = TokenType.Bot, 
            Intents = DiscordIntents.AllUnprivileged
        });
    }
}