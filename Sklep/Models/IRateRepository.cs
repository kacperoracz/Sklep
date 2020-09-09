using System.Collections.Generic;

namespace Sklep.Models
{
    public interface IRateRepository
    {
        void AddRate(Rate rate);
        IEnumerable<Rate> GetRatesByPoductId(int productId);
        float GetRatingByProductId(int rateId);
        bool CheckRate(string userId, int productId);
        void EditRate(Rate rate);
        Rate GetRateByUserId(string userId, int productId);
    }
}
