using System.ComponentModel.DataAnnotations;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace DiscordBotTemplate.Core.Libs.CommandActions.CommandActionVariants;

public abstract class CommandActionNoContent : ICommandAction
{
    protected void VerifyNoContent(Command command, string errorMessage = "")
    {
        if (command.HasContent)
            throw new ValidationException(errorMessage);
    }

    public abstract Task<CommandActionStatus> Action(Command command, DiscordClient client, MessageCreateEventArgs @event);
}