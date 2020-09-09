using System;
using System.Collections.Generic;
using System.Linq;

namespace Sklep.Models
{
    public class RateRepository : IRateRepository
    {
        private readonly AppDbContext _appDbContext;

        public RateRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddRate(Rate rate)
        {
            _appDbContext.Rates.Add(rate);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Rate> GetRatesByPoductId(int productId)
        {
            return _appDbContext.Rates.Where(r => r.ProductId == productId);
        }

        public float GetRatingByProductId(int productId)
        {
            float value = 0;
            int counter = 0;
            foreach(Rate rate in GetRatesByPoductId(productId))
            {
                value += rate.Value;
                counter++;
            }
            return (float)Math.Round(value / counter, 1);
        }

        public bool CheckRate(string userId, int productId)
        {
            if (_appDbContext.Rates.Where(r => r.UserId == userId).Where(r => r.ProductId == productId).Count() > 0)
                return true;
            else
                return false;
        }

        public void EditRate(Rate rate)
        {
            _appDbContext.Rates.Update(rate);
            _appDbContext.SaveChanges();
        }

        public Rate GetRateByUserId(string userId, int productId)
        {
            return _appDbContext.Rates.FirstOrDefault(r => r.UserId == userId);
        }
    }
}