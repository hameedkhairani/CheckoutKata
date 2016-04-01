using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class Checkout
    {
        private readonly IOrderCreator _orderCreator;
        private readonly IPriceCalculator _priceCalculator;

        private decimal _totalPrice;

        public Checkout(IOrderCreator orderCreator, IPriceCalculator priceCalculator)
        {
            _priceCalculator = priceCalculator;
            _orderCreator = orderCreator;
        }

        public void Scan(string skuCodes)
        {
            var order = _orderCreator.Create(skuCodes);
            if (order != null)
            {
                _totalPrice = _priceCalculator.Calculate(order);
            }
        }
        
        public decimal TotalPrice
        {
            get { return _totalPrice; }
        }
    }
}
