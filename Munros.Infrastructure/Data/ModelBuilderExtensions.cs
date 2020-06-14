using Microsoft.EntityFrameworkCore;
using Munros.Core.Entities;

namespace Munros.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Munro>().HasData(
                new Munro { Id = 1, Name = "Ben Chonzie", Height = 931, Category = "MUN", GridReference = "NN773308" },
                new Munro { Id = 2, Name = "Ben Vorlich", Height = 985, Category = "MUN", GridReference = "NN629189" },
                new Munro { Id = 7, Name = "Stob Binnein - Stob Coire an Lochain", Height = 1068, Category = "TOP", GridReference = "NN438220" },
                new Munro { Id = 12, Name = "Cruach Ardrain - Stob Garbh SE Top", Height = 923.4, Category = "", GridReference = "NN412217" },
                new Munro { Id = 28, Name = "Ben Vane", Height = 915.76, Category = "MUN", GridReference = "NN277098" }
            );
        }
    }
}
