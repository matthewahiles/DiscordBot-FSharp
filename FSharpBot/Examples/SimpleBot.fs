module FSharpBot.Examples

open System
open Discord
open Discord.WebSocket
open System.Threading.Tasks

let logEvent (message:LogMessage) =
        Console.WriteLine(message.ToString())
let logMessage (message: SocketMessage) =
        Console.WriteLine(message.Content.ToString())

let mainTask =
    async {
        let client = new DiscordSocketClient();
        client.add_Log(fun message -> async {logEvent message} |> Async.StartAsTask :> _)
        client.add_MessageReceived(fun message -> async {logMessage message} |> Async.StartAsTask :> _)
        let token = "NDY1NzY5OTMyNDAwMDk5MzI5.DipcxQ.FYI4zQTNOUraH9vtHIeU4HQ-vuI"
        do! Async.AwaitTask(client.LoginAsync(TokenType.Bot, token))
        do! Async.AwaitTask(client.StartAsync())
        do! Async.AwaitTask(Task.Delay(-1))
    }