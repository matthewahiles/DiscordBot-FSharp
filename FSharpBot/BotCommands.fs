namespace FSharpBot

open System.Threading.Tasks
open DSharpPlus
open DSharpPlus.CommandsNext
open DSharpPlus.CommandsNext.Attributes
open DSharpPlus.Entities
open System

type BotCommands() =
    [<Command("hi")>]
    member public self.hi(ctx:CommandContext) = 
        async { ctx.RespondAsync "Hi there" |> ignore } |> Async.StartAsTask :> Task       

    [<Command("echo")>]
    member public self.echo(ctx:CommandContext) (message:string) =
        async { ctx.RespondAsync message |> ignore } |> Async.StartAsTask :> Task 