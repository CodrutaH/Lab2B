﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2B.Models
{
    public class MoviesDbSeeder
    {
        public static void Initialize(MoviesDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any flowers.
            if (context.Movies.Any())
            {
                return;   // DB has been seeded
            }

            context.Movies.AddRange(
                  new Movie
                  {
                      Title = "telefon",
                      Description = "White, Yellow",
                     Genre = Genre.action,
                      Director = "Ana",
                      Duration = 100,
                      Year = 1999,
                      Date = DateTime.Now,
                      Rating = 9,
                      Watched = "yes"
                  },
                new Movie
                {
                    Title = "Titanic",
                    Description = "a big boat",
                     Genre = Genre.comedy,
                    Director = "Ana",
                    Duration = 100,
                    Year = 2001,
                    Date = DateTime.Now,
                    Rating = 10,
                    Watched = "no"
                },
                new Movie
                {
                    Title = "Vals",
                    Description = "a big boat",
                     Genre = Genre.comedy,
                    Director = "Ana",
                    Duration = 100,
                    Year = 2001,
                    Date = DateTime.Now,
                    Rating = 10,
                    Watched = "no"
                }
            );

            context.SaveChanges();
        }
    }
}
