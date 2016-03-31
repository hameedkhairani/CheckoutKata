using App;
using Moq;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class CheckoutTests
    {
        private Product _testProduct;
        private Mock<IProductRepository> _mockProductRepository;
        private Checkout _checkout;

        [SetUp]
        public void SetUp()
        {
            _testProduct = new Product { SkuCode = "A", UnitPrice = 50m };
            _mockProductRepository = new Mock<IProductRepository>();
            _checkout = new Checkout(_mockProductRepository.Object);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenNoProducts_WhenCheckedOut_ThenTotalPriceIsZero(string skuCodes)
        {
            _checkout.Scan(skuCodes);
            Assert.That(_checkout.TotalPrice, Is.EqualTo(0));
        }

        [Test]
        public void GivenSingleProductWithNoOffers_WhenCheckedOut_ThenTotalPriceIsEqualToUnitPrice()
        {
            _mockProductRepository.Setup(p => p.GetByCode(_testProduct.SkuCode)).Returns(_testProduct);
            _checkout.Scan(_testProduct.SkuCode);
            Assert.That(_checkout.TotalPrice, Is.EqualTo(_testProduct.UnitPrice));
        }

    }
}
