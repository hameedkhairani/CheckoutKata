using System;
using System.Collections.Generic;

namespace App
{
    public class PriceCalculator : IPriceCalculator
    {
        private readonly IProductRepository _productRepository;
        private readonly IOfferRepository _offerRepository;

        public PriceCalculator(IProductRepository productRepository, IOfferRepository offerRepository)
        {
            _productRepository = productRepository;
            _offerRepository = offerRepository;
        }

        public decimal Calculate(Dictionary<string, int> order)
        {
            decimal totalPrice = 0;
            foreach (var code in order.Keys)
            {
                Product product;
                if (TryGetProduct(code, out product))
                {
                    var offers = _offerRepository.GetByCode(code);
                    totalPrice += CalculateProductPrice(product, offers, order[code]);
                }
            }
            return totalPrice;
        }

        private bool TryGetProduct(string code, out Product product)
        {
            try
            {
                product = _productRepository.GetByCode(code);
                return true;
            }
            catch (ArgumentException)
            {
                product = null;
                return false;
            }
        }

        private decimal CalculateProductPrice(Product product, IEnumerable<Offer> offers, int quantity)
        {
            //to do: implement offers
            return product.UnitPrice * quantity;
        }
    }
}