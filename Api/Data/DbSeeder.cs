using Api.Models;

namespace Api.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Items.Any())
        {
            context.Items.AddRange(
                new Item { Title = "Demo Item 1", Description = "From seed" },
                new Item { Title = "Demo Item 2", Description = "From seed" },
                new Item { Title = "Demo Item 3", Description = "From seed" }
            );

            context.SaveChanges();
        }
    }
}
