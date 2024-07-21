namespace SentinelAPI

module CrudOperations =

    open System
    open Microsoft.EntityFrameworkCore
    open FSharp.Control.Tasks.V2.ContextInsensitive
    open SentinelAPI.SentinelDomain
    open SentinelAPI.SentinelPersistence
    open Newtonsoft.Json

    let createSentinel (dbContext: SentinelDataContext) (newSentinel: Sentinel) =
        task {
            dbContext.Sentinels.Add(newSentinel) |> ignore
            let! result = dbContext.SaveChangesAsync()
            return newSentinel
        }

    let getSentinel (dbContext: SentinelDataContext) id =
        task {
            let! sentinel = dbContext.Sentinels.FirstOrDefaultAsync(fun s -> s.id = id)
            return if isNull (box sentinel) then None else Some sentinel
        }

    let getAllSentinels (dbContext: SentinelDataContext) =
        task {
            let! sentinels = dbContext.Sentinels.ToListAsync()
            return sentinels |> List.ofSeq
        }

    let updateSentinel (dbContext: SentinelDataContext) (updatedSentinel: Sentinel) =
        task {
            let! existingSentinel = dbContext.Sentinels.FirstOrDefaultAsync(fun s -> s.id = updatedSentinel.id)
            match existingSentinel |> box |> isNull with
            | true -> return None
            | false ->
                dbContext.Sentinels.Update(updatedSentinel) |> ignore
                let! result = dbContext.SaveChangesAsync()
                return Some updatedSentinel
        }

    let deleteSentinel (dbContext: SentinelDataContext) id =
        task {
            let! existingSentinel = dbContext.Sentinels.FirstOrDefaultAsync(fun s -> s.id = id)
            match existingSentinel |> box |> isNull with
            | true -> return false
            | false ->
                dbContext.Sentinels.Remove(existingSentinel) |> ignore
                let! result = dbContext.SaveChangesAsync()
                return true
        }
