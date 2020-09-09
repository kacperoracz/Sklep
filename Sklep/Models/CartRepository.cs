using System.Collections.Generic;
using System.Linq;

namespace Sklep.Models
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IProductRepository _productRepository;

        public CartRepository(AppDbContext appDbContext, IProductRepository productRepository)
        {
            _appDbContext = appDbContext;
            _productRepository = productRepository;
        }

        public void AddProductToCart(Cart cart)
        {
            _appDbContext.Carts.Add(cart);
            _appDbContext.SaveChanges();
        }

        public void RemoveProductFromCart(Cart cart)
        {
            _appDbContext.Carts.Remove(cart);
            _appDbContext.SaveChanges();
        }
        public IEnumerable<Cart> GetCartsByUserId(string userId)
        {
            return _appDbContext.Carts.Where(c => c.UserId == userId);
        }

        public Cart GetCartById(int cartId)
        {
            return _appDbContext.Carts.FirstOrDefault(c => c.Id == cartId);
        }
    }
}
