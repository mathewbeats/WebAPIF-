namespace SentinelAPI

open System.IO
open DbUp
open System
open System.Reflection
open Microsoft.Extensions.Configuration

module DatabaseMigration =

    let getConfiguration () =
        ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional = false, reloadOnChange = true)
            .Build()

    let upgradeDatabase () =
        let configuration = getConfiguration()
        let connectionString = configuration.GetConnectionString("DefaultConnection")
        
        let scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts")

        let upgrader = DeployChanges.To
                            .SqlDatabase(connectionString)
                            .WithScriptsFromFileSystem(scriptPath)
                            .LogToConsole()
                            .Build()

        let result = upgrader.PerformUpgrade()

        if result.Successful then
            printfn "Migration successful"
        else
            printfn "Migration failed: %s" result.Error.Message
