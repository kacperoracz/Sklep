using System.Collections.Generic;

namespace Sklep.Models
{
    public interface IOrderStatusRepository
    {
        string GetOrderStatusById(int orderStatusId);
        IEnumerable<OrderStatus> GetAllStatuses();
    }
}
