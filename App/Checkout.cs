namespace App
{
    public class Checkout
    {
        private readonly IProductRepository _productRepository;
        private decimal _totalPrice;

        public Checkout(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Scan(string skuCodes)
        {
            if (string.IsNullOrWhiteSpace(skuCodes))
            {
                _totalPrice = 0;
            }
            else
            {
                _totalPrice = AddPrices(skuCodes);
            }
        }

        private decimal AddPrices(string skuCodes)
        {
            var totalPrice = 0m;
            foreach (var code in skuCodes)
            {
                var item = _productRepository.GetByCode(code.ToString());
                totalPrice += item.UnitPrice;
            }
            return totalPrice;
        }

        public decimal TotalPrice
        {
            get { return _totalPrice; }
        }
    }
}
