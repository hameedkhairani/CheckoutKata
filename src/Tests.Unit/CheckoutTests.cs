using System.Linq;
using App;
using Moq;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class CheckoutTests
    {
        private Product _testProductA;
        private Product _testProductB;
        private Product _testProductC;

        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IOfferRepository> _mockOfferRepository;

        private Checkout _checkout;
        private PriceCalculator _priceCalculator;

        [SetUp]
        public void SetUp()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockOfferRepository = new Mock<IOfferRepository>();

            _priceCalculator = new PriceCalculator(_mockProductRepository.Object, _mockOfferRepository.Object);
            _checkout = new Checkout(_priceCalculator);

            _testProductA = new Product { SkuCode = "A", UnitPrice = 50m };
            _testProductB = new Product { SkuCode = "B", UnitPrice = 30m };
            _testProductC = new Product { SkuCode = "C", UnitPrice = 20m };

            _mockProductRepository.Setup(p => p.GetByCode(_testProductA.SkuCode)).Returns(_testProductA);
            _mockProductRepository.Setup(p => p.GetByCode(_testProductB.SkuCode)).Returns(_testProductB);
            _mockProductRepository.Setup(p => p.GetByCode(_testProductC.SkuCode)).Returns(_testProductC);
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
            _checkout.Scan(_testProductA.SkuCode);
            Assert.That(_checkout.TotalPrice, Is.EqualTo(_testProductA.UnitPrice));
        }

        [Test]
        public void GivenMultipleProductsWithNoOffers_WhenCheckedOut_ThenTotalPriceIsEqualToSumOfUnitPrices()
        {
            _checkout.Scan(_testProductA.SkuCode + _testProductB.SkuCode);
            Assert.That(_checkout.TotalPrice, Is.EqualTo(_testProductA.UnitPrice + _testProductB.UnitPrice));
        }

        [TestCase(2)]
        [TestCase(3)]
        public void GivenSameProductMultipleTimesWithNoOffers_WhenCheckedOut_ThenTotalPriceIsEqualToUnitPriceTimesQuantity(int quantity)
        {
            var skuCodes = string.Concat(Enumerable.Repeat(_testProductA.SkuCode, quantity));
            _checkout.Scan(skuCodes);
            Assert.That(_checkout.TotalPrice, Is.EqualTo(_testProductA.UnitPrice * quantity));
        }

        [TestCase("ABC")]
        [TestCase("ACB")]
        [TestCase("BCA")]
        public void GivenMultipleProductsInDifferentOrderWithNoOffers_WhenCheckedOut_ThenTotalPriceIsTheSame(string skuCodes)
        {
            var expectedTotalPrice = _testProductA.UnitPrice + _testProductB.UnitPrice + _testProductC.UnitPrice;
            _checkout.Scan(skuCodes);
            Assert.That(_checkout.TotalPrice, Is.EqualTo(expectedTotalPrice));
        }

    }
}
