namespace App
{
    public class Checkout
    {
        private readonly IProductRepository _productRepository;
        private readonly IOfferRepository _offerRepository;

        private decimal _totalPrice;

        public Checkout(IProductRepository productRepository, IOfferRepository offerRepository)
        {
            _productRepository = productRepository;
            _offerRepository = offerRepository;
        }

        public void Scan(string skuCodes)
        {
            _totalPrice = string.IsNullOrWhiteSpace(skuCodes) ? 0 : AddPrices(skuCodes);
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
