using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace UsedCar.Backend.Infrastructures.EntityFrameworkCore
{
    public static class DbContextFactory
    {
        public static UsedCarDBContext Create()
        {
            SqliteConnection connection = new("Filename=:memory:");

            connection.Open();

            var options = new DbContextOptionsBuilder<UsedCarDBContext>()
                .UseSqlite(connection)
                .Options;

            var context = new UsedCarDBContext(options);

            context.Database.EnsureCreated();

            return new UsedCarDBContext(options);
        }

    }
}
