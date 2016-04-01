using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class Checkout
    {
        private readonly IPriceCalculator _priceCalculator;
        private decimal _totalPrice;

        public Checkout(IPriceCalculator priceCalculator)
        {
            _priceCalculator = priceCalculator;
        }

        public void Scan(string skuCodes)
        {
            var order = BuildOrder(skuCodes);
            if (order != null)
            {
                _totalPrice = _priceCalculator.Calculate(order);
            }
        }

        private Dictionary<string, int> BuildOrder(string skuCodes)
        {
            if (!string.IsNullOrWhiteSpace(skuCodes))
            {
                return skuCodes.ToCharArray()
                               .GroupBy(p => p)
                               .ToDictionary(p => p.Key.ToString(), p => p.Count());
            }
            return null;
        }

        public decimal TotalPrice
        {
            get { return _totalPrice; }
        }
    }
}
