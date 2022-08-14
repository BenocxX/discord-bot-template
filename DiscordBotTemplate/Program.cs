using DiscordBotTemplate.Core.Libs;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Debug("Program has started.");

var discord = Discord.CreateClient();

Log.Debug("Discord bot has been created.");

discord.MessageCreated += (client, @event) => {
    if (!Command.IsValid(@event.Message))
        return Task.CompletedTask;
    
    Log.Information($"@{@event.Message.Author.Username} in #{@event.Message.Channel.Name}: {@event.Message.Content}");
    
    var command = Command.Create(client, @event);
    command.Action();
    
    return Task.CompletedTask;
};

await discord.ConnectAsync();

Log.Debug("Discord bot has been connected.");

await Task.Delay(-1);

Log.Debug("Program shutting down...");
Log.CloseAndFlush();