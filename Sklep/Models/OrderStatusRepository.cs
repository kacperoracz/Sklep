using System.Collections.Generic;
using System.Linq;

namespace Sklep.Models
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderStatusRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<OrderStatus> GetAllStatuses()
        {
            return _appDbContext.OrderStatuses;
        }

        public string GetOrderStatusById(int orderStatusId)
        {
            return _appDbContext.OrderStatuses.FirstOrDefault(s => s.Id == orderStatusId).Name;
        }
    }
}
