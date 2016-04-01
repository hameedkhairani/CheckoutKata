using System.Collections.Generic;
using System.Linq;
using App;
using Moq;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class PriceCalculatorTests
    {
        private Product _testProductA;
        private Product _testProductB;
        private Product _testProductC;

        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IOfferRepository> _mockOfferRepository;

        private PriceCalculator _priceCalculator;

        [SetUp]
        public void SetUp()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockOfferRepository = new Mock<IOfferRepository>();

            _priceCalculator = new PriceCalculator(_mockProductRepository.Object, _mockOfferRepository.Object);

            _testProductA = new Product { SkuCode = "A", UnitPrice = 50m };
            _testProductB = new Product { SkuCode = "B", UnitPrice = 30m };
            _testProductC = new Product { SkuCode = "C", UnitPrice = 20m };

            _mockProductRepository.Setup(p => p.GetByCode(_testProductA.SkuCode)).Returns(_testProductA);
            _mockProductRepository.Setup(p => p.GetByCode(_testProductB.SkuCode)).Returns(_testProductB);
            _mockProductRepository.Setup(p => p.GetByCode(_testProductC.SkuCode)).Returns(_testProductC);
        }

        [Test]
        public void GivenEmptyOrder_WhenPriceCalculated_ThenTotalPriceIsZero()
        {
            Assert.That(_priceCalculator.Calculate(null), Is.EqualTo(0));
            Assert.That(_priceCalculator.Calculate(new Order()), Is.EqualTo(0));
        }

        [Test]
        public void GivenSingleProductWithNoOffers_WhenPriceCalculated_ThenTotalPriceIsEqualToUnitPrice()
        {
            var order = new Order();
            order.Items.Add(_testProductA.SkuCode, 1);
            
            var totalPrice =_priceCalculator.Calculate(order);

            Assert.That(totalPrice, Is.EqualTo(_testProductA.UnitPrice));
        }

        [Test]
        public void GivenMultipleProductsWithNoOffers_WhenPriceCalculated_ThenTotalPriceIsEqualToSumOfUnitPrices()
        {
            var order = new Order();
            order.Items.Add(_testProductA.SkuCode, 1);
            order.Items.Add(_testProductB.SkuCode, 1);

            var totalPrice = _priceCalculator.Calculate(order);

            Assert.That(totalPrice, Is.EqualTo(_testProductA.UnitPrice + _testProductB.UnitPrice));
        }

        [Test]
        public void GivenSameProductMultipleTimesWithNoOffers_WhenPriceCalculated_ThenTotalPriceIsEqualToUnitPriceTimesQuantity()
        {
            const int quantity = 3;
            var order = new Order();
            order.Items.Add(_testProductA.SkuCode, quantity);

            var totalPrice = _priceCalculator.Calculate(order);

            Assert.That(totalPrice, Is.EqualTo(_testProductA.UnitPrice * quantity));
        }
    }
}
