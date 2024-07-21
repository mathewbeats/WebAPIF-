namespace SentinelAPI

open System
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open SentinelAPI.DatabaseMigration

module Program =

    let createHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder.UseStartup<Startup>() |> ignore)

    [<EntryPoint>]
    let main args =
        // Ejecutar migraciones
        upgradeDatabase()

        createHostBuilder(args).Build().Run()
        0
