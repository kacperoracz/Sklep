using System.Collections.Generic;
using System.Linq;

namespace Sklep.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICartRepository _cartRepository;

        public OrderRepository(AppDbContext appDbContext, ICartRepository cartRepository)
        {
            _appDbContext = appDbContext;
            _cartRepository = cartRepository;
        }

        public void AddOrder(Order order, int cartId)
        {
            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();
            _cartRepository.RemoveProductFromCart(_cartRepository.GetCartById(cartId));
        }

        public void AddOrder(List<Order> orders, List<Cart> carts)
        {
            _appDbContext.Orders.AddRange(orders);
            _appDbContext.Carts.RemoveRange(carts);
            _appDbContext.SaveChanges();
        }

        public Order GetOrderById(int orderId)
        {
            return _appDbContext.Orders.FirstOrDefault(o => o.Id == orderId);
        }

        public void ChangeOrderStatus(int orderId, int newStatusId)
        {
            Order order = GetOrderById(orderId);
            order.StatusId = newStatusId;
            _appDbContext.Orders.Update(order);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            return _appDbContext.Orders.Where(o => o.UserId == userId);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _appDbContext.Orders;
        }
    }
}
