using System;
using System.Collections.Generic;

namespace App
{
    public class InMemoryOfferRepository : IOfferRepository
    {
        private readonly Dictionary<string,Offer> _offers = new Dictionary<string, Offer>();

       public InMemoryOfferRepository()
        {
            _offers.Add("A", new Offer { SkuCode = "A", Quantity = 3, OfferPrice = 130m });
            _offers.Add("B", new Offer { SkuCode = "B", Quantity = 2, OfferPrice = 45m });
        }

        public Offer GetByCode(string skuCode)
        {
            return _offers.ContainsKey(skuCode) ? _offers[skuCode] : null;
        }
    }
}