namespace SentinelAPI


module SentinelDomain = 

    [<CLIMutable>]
    type SentinelData = {
        name: string
    }

    [<CLIMutable>]
    type Sentinel = {
        id: string
        dataJson: string
    }

namespace SentinelAPI

open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Configuration
open SentinelAPI.SentinelDomain

module SentinelPersistence = 

    type SentinelDataContext(configuration : IConfiguration) = 
        inherit DbContext()

        [<DefaultValue>]
        val mutable sentinels : DbSet<Sentinel>

        member public this.Sentinels
            with get() = this.sentinels 
            and set s = this.sentinels <- s

        override __.OnConfiguring(optionsBuilder : DbContextOptionsBuilder) = 
            let connectionString = configuration.GetConnectionString("DefaultConnection")
            optionsBuilder.UseSqlServer(connectionString) |> ignore

        override __.OnModelCreating(modelBuilder : ModelBuilder) = 

            modelBuilder.Entity<Sentinel>()
                .ToTable("Sentinels")
                |> ignore

            modelBuilder.Entity<Sentinel>()
                .HasKey(fun s -> s.id :> obj)
                |> ignore

            modelBuilder.Entity<Sentinel>()
                .Property(fun s -> s.id)
                .HasColumnName("id")
                |> ignore

            modelBuilder.Entity<Sentinel>()
                .Property(fun s -> s.dataJson)
                .HasColumnName("data")
                |> ignore
