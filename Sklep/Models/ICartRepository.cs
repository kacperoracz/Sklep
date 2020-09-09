using System.Collections.Generic;

namespace Sklep.Models
{
    public interface ICartRepository
    {
        void AddProductToCart(Cart cart);
        void RemoveProductFromCart(Cart cart);
        IEnumerable<Cart> GetCartsByUserId(string userId);
        Cart GetCartById(int cartId);
    }
}
