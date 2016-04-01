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

        public decimal Calculate(Order order)
        {
            decimal totalPrice = 0;
            if (order != null)
            {
                foreach (var code in order.Items.Keys)
                {
                    Product product;
                    if (TryGetProduct(code, out product))
                    {
                        var offers = _offerRepository.GetByCode(code);
                        totalPrice += CalculateProductPrice(product, offers, order.Items[code]);
                    }
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
            //to do: implement offer discounts

            return product.UnitPrice * quantity;
        }
    }
}