using System.Collections.Generic;

namespace App
{
    public interface IOfferRepository
    {
        IEnumerable<Offer> GetByCode(string skuCode);
    }
}