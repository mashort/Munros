using Munros.Core.Entities;
using Munros.Infrastructure.Data;
using System.Collections.Generic;

namespace Munros.Test
{
    public static class SeedData
    {
        public static void PopulateTestData(AppDbContext dbContext)
        {
            foreach (var item in dbContext.Munros)
            {
                dbContext.Remove(item);
            }

            dbContext.SaveChanges();

            foreach (Munro item in TestData())
            {
                dbContext.Munros.Add(item);
            }

            dbContext.SaveChanges();
        }

        public static List<Munro> TestData()
        {
            List<Munro> munros = new List<Munro>()
            {
                new Munro { Id = 1, Name = "Regular Munro 1", Height = 931, Category = "MUN", GridReference = "MN112201" },
                new Munro { Id = 2, Name = "Regular Munro 2", Height = 932, Category = "MUN", GridReference = "MN112202" },
                new Munro { Id = 3, Name = "Regular Top 1", Height = 1000, Category = "TOP", GridReference = "TP112201" },
                new Munro { Id = 4, Name = "Uncategorised Munro 1", Height = 923, Category = "", GridReference = "UC112201" }
            };

            return munros;
        }
    }
}
