using System.Collections.Generic;

namespace Sklep.Models
{
    public interface IOrderRepository
    {
        void AddOrder(Order order, int cartId);
        void AddOrder(List<Order> orders, List<Cart> carts);
        void ChangeOrderStatus(int orderId, int newStatusId);
        Order GetOrderById(int orderId);
        IEnumerable<Order> GetOrdersByUserId(string userId);
        IEnumerable<Order> GetAllOrders();
    }
}
