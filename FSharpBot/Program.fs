open System
open Discord
open Discord.WebSocket
open System.Threading.Tasks
open Microsoft.Extensions.Configuration
open System.IO

let logEvent (message:LogMessage) =
        Console.WriteLine(message.ToString())
let logMessage (message: SocketMessage) =
        Console.WriteLine(message.Content.ToString())
let getConfig =
    let builder = new ConfigurationBuilder()
    do builder.SetBasePath( Directory.GetCurrentDirectory() ) |> ignore
    do builder.AddJsonFile("config.json") |> ignore
    builder.Build()

let private client = new DiscordSocketClient()
let private config = getConfig

let registerEvents =
    async {
        client.add_Log(fun message -> async {logEvent message} |> Async.StartAsTask :> _)
        client.add_MessageReceived(fun message -> async {logMessage message} |> Async.StartAsTask :> _)
    }

let mainTask =
    async {
        do! registerEvents
        do! Async.AwaitTask(client.LoginAsync(TokenType.Bot, config.["BotToken"]))
        do! Async.AwaitTask(client.StartAsync())
        do! Async.AwaitTask(Task.Delay(-1))
    }
[<EntryPoint>]
let main argv =
    Async.RunSynchronously(mainTask)
    0
