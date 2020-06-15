using Microsoft.EntityFrameworkCore;
using Munros.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace Munros.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Munro>().HasData(CreateMunroObjectsFromCsv());
        }

        public static List<Munro> CreateMunroObjectsFromCsv()
        {
            List<Munro> munros = new List<Munro>();

            try
            {
                string[] csvLines = File.ReadAllLines("munro-data.csv");
                string[] values;

                for (int i = 0; i < csvLines.Length; i++)
                {
                    if (i > 0)
                    {
                        values = csvLines[i].Split(',');

                        munros.Add(
                            new Munro
                            {
                                Id = int.Parse(values[0]),
                                Name = values[6],
                                Height = double.Parse(values[10]),
                                Category = values[28],
                                GridReference = values[14]
                            }
                        );
                    }
                }
            }
            catch (Exception)
            {
                // Do something meaningful here e.g. wire-up of logging...
                throw;
            }

            return munros;
        }
    }
}
