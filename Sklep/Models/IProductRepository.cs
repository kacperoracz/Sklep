using System.Collections.Generic;

namespace Sklep.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int productId);
        void AddProduct(Product product);
        void EditProduct(Product product);
        void AddProductNumber(int productId, int number);
        void RemoveProductNumber(int productId, int number);
    }
}
