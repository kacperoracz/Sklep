using System.Collections.Generic;
using System.Linq;

namespace Sklep.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _appDbContext.Products;
        }

        public Product GetProductById(int productId)
        {
            return _appDbContext.Products.FirstOrDefault(p => p.Id == productId);
        }

        public void AddProduct(Product product)
        {
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
        }

        public void EditProduct(Product product)
        {
            _appDbContext.Products.Update(product);
            _appDbContext.SaveChanges();
        }

        public void AddProductNumber(int productId, int number)
        {
            Product product = GetProductById(productId);
            product.Number += number;
            EditProduct(product);
        }

        public void RemoveProductNumber(int productId, int number)
        {
            Product product = GetProductById(productId);
            product.Number -= number;
            EditProduct(product);
        }
    }
}
