namespace SentinelAPI

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.EntityFrameworkCore
open Giraffe
open SentinelAPI.SentinelPersistence
open SentinelAPI.Handlers

type public Startup(configuration: IConfiguration) =
    member _.Configuration = configuration

    member _.ConfigureServices(services: IServiceCollection) =
        services.AddSingleton<IConfiguration>(configuration) |> ignore

        services.AddDbContext<SentinelDataContext>(fun options ->
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            |> ignore)
        |> ignore

        services.AddGiraffe() |> ignore

    static member WebApp =
        choose
            [ route "/" >=> text "Hello World"
              route "/sentinels"
              >=> choose
                      [ GET >=> Handlers.getAllSentinelsHandler
                        POST >=> Handlers.createSentinelHandler ]
              routef "/sentinels/%s" Handlers.getSentinelHandler
              PUT >=> routef "/sentinels/%s" Handlers.updateSentinelHandler
              DELETE >=> routef "/sentinels/%s" Handlers.deleteSentinelHandler ]

    member _.Configure (app: IApplicationBuilder) (env: IHostEnvironment) : unit =
        if env.IsDevelopment() then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseGiraffe(Startup.WebApp)
