using Microsoft.EntityFrameworkCore;

namespace SemanticKernel
{
    internal class Seeder
    {
        public static void Seed()
        {
            // Database initialization
            using (var context = new Context())
            {
                //context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
        }
    }
}
