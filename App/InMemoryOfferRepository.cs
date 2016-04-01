using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class InMemoryOfferRepository : IOfferRepository
    {
        private readonly Dictionary<string,List<Offer>> _offers = new Dictionary<string, List<Offer>>();

       public InMemoryOfferRepository()
        {
            _offers.Add("A", new List<Offer> { new Offer { SkuCode = "A", Quantity = 3, OfferPrice = 130m } });
            _offers.Add("B", new List<Offer> { new Offer { SkuCode = "B", Quantity = 2, OfferPrice = 45 } });
        }

        public IEnumerable<Offer> GetByCode(string skuCode)
        {
            return _offers.ContainsKey(skuCode) ? _offers[skuCode] : Enumerable.Empty<Offer>();
        }
    }
}