using DiscordBotTemplate.Core.Libs.CommandActions;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Serilog;

namespace DiscordBotTemplate.Core.Libs;

public class Command
{
    private readonly DiscordClient _client;
    private readonly MessageCreateEventArgs _event;
    private readonly ICommandAction? _commandAction;
    public readonly bool HasContent;
    public readonly char Prefix;
    public readonly string Keyword;
    public readonly string Content;

    protected Command(DiscordClient client, MessageCreateEventArgs @event, ICommandAction? commandAction)
    {
        _client = client;
        _event = @event;
        _commandAction = commandAction;
        HasContent = HasContentAfterKeyword(_event.Message);
        Prefix = GetCommandPrefix(_event.Message);
        Keyword = GetCommandKeyword(_event.Message);
        Content = GetCommandContent(_event.Message);
    }

    public static bool IsValid(DiscordMessage message)
    {
        var commandKey = GetCommandKeyword(message);
        return HasPrefix(message) && CommandRepository.Instance.HasCommand(commandKey);
    }
    
    public static Command Create(DiscordClient client, MessageCreateEventArgs @event)
    {
        var commandKeyword = GetCommandKeyword(@event.Message);
        var commandAction = CommandRepository.Instance.GetCommandAction(commandKeyword);
        return new Command(client, @event, commandAction);
    }
    
    public void Action()
    {
        if (_commandAction == null)
        {
            Log.Error("Command Action not found.");
            return;
        }
        
        var status = _commandAction.Action(this, _client, _event).Result;
        Log.Information($"Command Action: [{status.Code} ({(int) status.Code})] {status.Reason}");
    }
    
    private static bool HasPrefix(DiscordMessage message)
    {
        return message.Content.First() == '!';
    }
    
    private static bool HasContentAfterKeyword(DiscordMessage message)
    {
        var command = GetCommandWithoutPrefix(message);
        var words = command.Split(' ');
        return words.Length > 1;
    }
    
    private static string GetCommandWithoutPrefix(DiscordMessage message)
    {
        return HasPrefix(message) ? message.Content[1..] : message.Content;
    }

    private static char GetCommandPrefix(DiscordMessage message)
    {
        return message.Content.First();
    }

    private static string GetCommandKeyword(DiscordMessage message)
    {
        var contentWithoutPrefix = GetCommandWithoutPrefix(message);
        return contentWithoutPrefix.Split(' ').First();
    }
    
    private static string GetCommandContent(DiscordMessage message)
    {
        if (!HasContentAfterKeyword(message))
            return "";
        
        var command = GetCommandWithoutPrefix(message);
        return command[(command.Split()[0].Length + 1)..];
    }
}