using System;
using System.Collections.Generic;

namespace App
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly Dictionary<string,Product> _products = new Dictionary<string, Product>();

        public InMemoryProductRepository()
        {
            _products.Add("A", new Product { SkuCode = "A", UnitPrice = 50m });
            _products.Add("B", new Product { SkuCode = "B", UnitPrice = 30m });
            _products.Add("C", new Product { SkuCode = "C", UnitPrice = 20m });
            _products.Add("D", new Product { SkuCode = "D", UnitPrice = 15m });
        }

        public Product GetByCode(string skuCode)
        {
            if (!_products.ContainsKey(skuCode))
            {
                throw new ArgumentException(string.Format("Invalid Sku Code {0}", skuCode));
            }
            return _products[skuCode];
        }
    }
}