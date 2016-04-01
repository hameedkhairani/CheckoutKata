using System.Collections.Generic;
using App.Models;

namespace App.Contracts
{
    public interface IOfferRepository
    {
        IEnumerable<Offer> GetByCode(string skuCode);
    }
}