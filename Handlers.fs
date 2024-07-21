namespace SentinelAPI

open Microsoft.AspNetCore.Http
open Giraffe
open FSharp.Control.Tasks.V2.ContextInsensitive
open SentinelAPI.SentinelPersistence
open SentinelAPI.SentinelDomain
open SentinelAPI.CrudOperations
open Newtonsoft.Json

module Handlers =

    let createSentinelHandler : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let dbContext = ctx.GetService<SentinelDataContext>()
                let! newSentinel = ctx.BindJsonAsync<Sentinel>()
                let! createdSentinel = createSentinel dbContext newSentinel
                return! json createdSentinel next ctx
            }

    let getSentinelHandler (id : string) : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let dbContext = ctx.GetService<SentinelDataContext>()
                let! sentinelOption = getSentinel dbContext id
                match sentinelOption with
                | Some sentinel ->
                    let data = JsonConvert.DeserializeObject<SentinelData>(sentinel.dataJson)
                    let result = { id = sentinel.id; dataJson = JsonConvert.SerializeObject(data) }
                    return! json result next ctx
                | None -> return! RequestErrors.NOT_FOUND "Sentinel not found" next ctx
            }
            
    let getAllSentinelsHandler : HttpHandler =
        fun (next: HttpFunc) (ctx : HttpContext) ->
            task {
                let dbContext = ctx.GetService<SentinelDataContext>()
                let! sentinels = getAllSentinels dbContext
                return! json sentinels next ctx
            }

    let updateSentinelHandler (id : string) : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let dbContext = ctx.GetService<SentinelDataContext>()
                let! updatedData = ctx.BindJsonAsync<SentinelData>()
                let! existingSentinel = getSentinel dbContext id
                match existingSentinel with
                | Some sentinel ->
                    let updatedSentinel = { id = sentinel.id; dataJson = JsonConvert.SerializeObject(updatedData) }
                    let! result = updateSentinel dbContext updatedSentinel
                    match result with
                    | Some updated -> return! json updated next ctx
                    | None -> return! RequestErrors.NOT_FOUND "Failed to update sentinel" next ctx
                | None -> return! RequestErrors.NOT_FOUND "Sentinel not found" next ctx
            }

    let deleteSentinelHandler (id : string) : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let dbContext = ctx.GetService<SentinelDataContext>()
                let! result = deleteSentinel dbContext id
                if result then return! Successful.OK "Sentinel deleted" next ctx
                else return! RequestErrors.NOT_FOUND "Sentinel not found" next ctx
            }
