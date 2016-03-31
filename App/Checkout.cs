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
                var product = _productRepository.GetByCode(skuCodes);
                _totalPrice = product.UnitPrice;
            }
        }

        public decimal TotalPrice
        {
            get { return _totalPrice; }
        }
    }
}
