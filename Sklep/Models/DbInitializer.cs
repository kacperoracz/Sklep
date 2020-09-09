using System.Linq;

namespace Sklep.Models
{
    public class DbInitializer
    {
        private readonly AppDbContext _appDbContext;

        public DbInitializer(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public static void Seed(AppDbContext context)
        {
            if (!context.Products.Any())
            {
                context.AddRange(
                    new Product { Name = "Latarka", Number = 15, Price = 35, Descryption = "Latarka co bardzo mocno świeci"},
                    new Product { Name = "Wędka", Number = 5, Price = 100, Descryption = "Wędka do łapania dużych ryb" },
                    new Product { Name = "Czapka", Number = 50, Price = 20, Descryption = "Czapka co osłoni Cię przed słońcem" },
                    new Product { Name = "Kalosze", Number = 32, Price = 150, Descryption = "Kalosze do ochrony skarpetek przed wodą" }
                    );
            }
            context.SaveChanges();

            if (!context.OrderStatuses.Any())
            {
                context.AddRange(
                    new OrderStatus { Name = "Oczekiwanie na płatność" }
                    );
                context.SaveChanges();
                context.AddRange(
                    new OrderStatus { Name = "Płatność przyjęta" }
                    );
                context.SaveChanges();
                context.AddRange(
                    new OrderStatus { Name = "Wysłane" }
                    );
                context.SaveChanges();
                context.AddRange(
                    new OrderStatus { Name = "Dostarczone" }
                    );
                context.SaveChanges();
                context.AddRange(
                    new OrderStatus { Name = "Anulowane" }
                    );
                context.SaveChanges();
            }
        }
    }
}
