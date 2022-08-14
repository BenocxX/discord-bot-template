using DiscordBotTemplate.Core.Libs.CommandActions.CommandActionVariants;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace DiscordBotTemplate.Core.Libs.CommandActions.CommandActionHandlers;

public class PingCommandAction : CommandActionNoContent
{
    public override async Task<CommandActionStatus> Action(Command command, DiscordClient client, MessageCreateEventArgs @event)
    {
        try
        {
            VerifyNoContent(command, "This command takes no arguments. Try `!ping`.");
            await @event.Message.RespondAsync("pong!");
        }
        catch (Exception e)
        {
            return new CommandActionStatus(StatusCode.Error, $"Error while processing the action. {e.Message}");
        }

        return new CommandActionStatus(StatusCode.Success);
    }
}