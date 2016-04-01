using System.Linq;
using App.Contracts;
using App.Models;

namespace App.Domain
{
    public class OrderCreator : IOrderCreator
    {
        public Order Create(string skuCodes)
        {
            if (!string.IsNullOrWhiteSpace(skuCodes))
            {
                var items = skuCodes.ToCharArray()
                    .GroupBy(p => p)
                    .ToDictionary(p => p.Key.ToString(), p => p.Count());
                
                return new Order {Items = items};
            }
            return null;
        }
    }
}