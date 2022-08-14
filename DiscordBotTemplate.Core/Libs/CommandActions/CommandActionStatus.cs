namespace DiscordBotTemplate.Core.Libs.CommandActions;

public class CommandActionStatus
{
    public readonly StatusCode Code;
    public readonly string Reason;
    
    public CommandActionStatus(StatusCode code, string reason = "")
    {
        Code = code;
        Reason = reason;
    }
}