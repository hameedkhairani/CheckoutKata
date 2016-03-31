using System;

namespace App
{
    public class Checkout
    {
        private decimal _totalPrice;

        public void Scan(string skuCodes)
        {
            if (string.IsNullOrWhiteSpace(skuCodes))
            {
                _totalPrice = 0;
            }
        }

        public decimal TotalPrice
        {
            get { return _totalPrice; }
        }
    }
}
