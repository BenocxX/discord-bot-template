using DSharpPlus;
using DSharpPlus.EventArgs;

namespace DiscordBotTemplate.Core.Libs.CommandActions;

public interface ICommandAction
{
    public Task<CommandActionStatus> Action(Command command, DiscordClient client, MessageCreateEventArgs @event);
}