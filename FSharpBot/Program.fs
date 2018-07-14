namespace FSharpBot

module core =

    open System
    open DSharpPlus
    open DSharpPlus.CommandsNext
    open System.Threading.Tasks
    open Microsoft.Extensions.Configuration
    open System.IO

    let getConfig =
        let builder = new ConfigurationBuilder()
        do builder.SetBasePath( Directory.GetCurrentDirectory() ) |> ignore
        do builder.AddJsonFile("config.json") |> ignore
        builder.Build()

    let private config = getConfig

    let getDiscordConfig =
        let conf = new DiscordConfiguration()
        conf.set_Token config.["BotToken"]
        conf.set_TokenType TokenType.Bot
        conf.set_UseInternalLogHandler true
        conf.set_LogLevel LogLevel.Debug
        conf

    let getCommandsConfig =
        let conf = new CommandsNextConfiguration()
        conf.set_StringPrefix "!"
        conf

    let client =  new DiscordClient(getDiscordConfig)
    let commands = client.UseCommandsNext(getCommandsConfig)

    let mainTask =
        async {
            client.add_MessageCreated(fun e -> async { Console.WriteLine e.Message.Content } |> Async.StartAsTask :> _)
            commands.RegisterCommands<BotCommands>()
            client.ConnectAsync() |> Async.AwaitTask |> Async.RunSynchronously
            do! Async.AwaitTask(Task.Delay(-1))
        }
    [<EntryPoint>]
    let main argv =
        Async.RunSynchronously(mainTask)
        0
