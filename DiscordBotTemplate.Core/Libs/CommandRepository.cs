using DiscordBotTemplate.Core.Libs.CommandActions;
using DiscordBotTemplate.Core.Libs.CommandActions.CommandActionHandlers;

namespace DiscordBotTemplate.Core.Libs;

public class CommandRepository
{
    private static CommandRepository? _instance;
    private static readonly object Padlock = new ();
    
    private readonly Dictionary<string, ICommandAction> _commands;
    
    private CommandRepository ()
    {
        _commands = new Dictionary<string, ICommandAction>
        {
            { "ping", new PingCommandAction() }
        };
    }

    public static CommandRepository Instance
    {
        get
        {
            lock (Padlock)
            {
                _instance ??= new CommandRepository();
                
                return _instance;
            }
        }
    }

    public bool HasCommand(string message)
    {
        return _commands.ContainsKey(message);
    }

    public ICommandAction GetCommandAction(string key)
    {
        return _commands[key];
    }
}