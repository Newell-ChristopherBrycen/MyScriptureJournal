using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MyScriptureJournal.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Data.MyScriptureContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<Data.MyScriptureContext>>()))
            {
                // Look for any movies.
                if (context.Scripture.Any())
                {
                    return;   // DB has been seeded
                }

                context.Scripture.AddRange(
                    new Scripture
                    {
                        EntryDate = DateTime.Parse("2020-10-5"),
                        Book = "John",
                        Chapter = 3,
                        Verse = 16,
                        JournalEntry = "Oldy, but a Goody"
                    },

                    new Scripture
                    {
                        EntryDate = DateTime.Parse("2020-10-27"),
                        Book = "Isaiah",
                        Chapter = 41,
                        Verse = 10,
                        JournalEntry = "No reason to fear, God is our strength and purpose"
                    },

                    new Scripture
                    {
                        EntryDate = DateTime.Parse("2020-10-20"),
                        Book = "Philippians",
                        Chapter = 4,
                        Verse = 13,
                        JournalEntry = "I can do all things, even .NET programming..."
                    }
                    
                );
                context.SaveChanges();
            }
        }
    }
}
