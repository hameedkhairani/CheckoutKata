using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class Checkout
    {
        private readonly IProductRepository _productRepository;
        private readonly IOfferRepository _offerRepository;

        private decimal _totalPrice;

        public Checkout(IProductRepository productRepository, IOfferRepository offerRepository)
        {
            _productRepository = productRepository;
            _offerRepository = offerRepository;
        }

        public void Scan(string skuCodes)
        {
            var order = BuildOrder(skuCodes);
            if (order != null)
            {
                _totalPrice = CalculateTotalPrice(order);
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

        private decimal CalculateTotalPrice(Dictionary<string, int> order)
        {
            return order.Keys.Sum(code => CalculatePrice(code, order[code]));
        }

        private decimal CalculatePrice(string skuCode, int quantity)
        {
            var product = _productRepository.GetByCode(skuCode);
            return product.UnitPrice * quantity;
        }

        public decimal TotalPrice
        {
            get { return _totalPrice; }
        }
    }
}
