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
        private Product _testProductD;

        private List<Offer> _testOffersProductA;
        private List<Offer> _testOffersProductB;

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
            _testProductD = new Product { SkuCode = "D", UnitPrice = 15m };

            _mockProductRepository.Setup(p => p.GetByCode(_testProductA.SkuCode)).Returns(_testProductA);
            _mockProductRepository.Setup(p => p.GetByCode(_testProductB.SkuCode)).Returns(_testProductB);
            _mockProductRepository.Setup(p => p.GetByCode(_testProductC.SkuCode)).Returns(_testProductC);
            _mockProductRepository.Setup(p => p.GetByCode(_testProductD.SkuCode)).Returns(_testProductD);

            _testOffersProductA = new List<Offer> { new Offer { SkuCode = "A", Quantity = 3, OfferPrice = 130m } };
            _testOffersProductB = new List<Offer> { new Offer { SkuCode = "B", Quantity = 2, OfferPrice = 45m } };

            _mockOfferRepository.Setup(p => p.GetByCode(_testProductA.SkuCode)).Returns(_testOffersProductA);
            _mockOfferRepository.Setup(p => p.GetByCode(_testProductB.SkuCode)).Returns(_testOffersProductB);


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

        [Test]
        public void GivenSingleProductWithOffer_WhenPriceCalculatedOnUnderQualifiedQuantity_ThenUnitPriceIsApplied()
        {
            const int quantity = 2;
            var order = new Order();
            order.Items.Add(_testProductA.SkuCode, quantity);

            var totalPrice = _priceCalculator.Calculate(order);

            Assert.That(totalPrice, Is.EqualTo(_testProductA.UnitPrice * quantity));
        }

        [Test, Ignore("this test would fail until we implement discount logic in price calculator")]
        public void GivenSingleProductWithOffer_WhenPriceCalculatedOnExactQualifiedQuantity_ThenOfferPriceIsApplied()
        {
            var offer = _testOffersProductA.First();
            var order = new Order();
            order.Items.Add(_testProductA.SkuCode, offer.Quantity);

            var totalPrice = _priceCalculator.Calculate(order);

            Assert.That(totalPrice, Is.EqualTo(offer.OfferPrice));
        }

        [Test, Ignore("this test would fail until we implement discount logic in price calculator")]
        public void GivenSingleProductWithOffer_WhenPriceCalculatedOnOverQualifiedQuantity_ThenOfferAndUnitPricesAreAppliedAppropriately()
        {
            const int surplusQuantity = 2;
            var offer = _testOffersProductA.First();

            var order = new Order();
            order.Items.Add(_testProductA.SkuCode, offer.Quantity + surplusQuantity);

            var actualTotalPrice = _priceCalculator.Calculate(order);

            var expectedTotalPrice = offer.OfferPrice + _testProductA.UnitPrice * surplusQuantity;
            Assert.That(actualTotalPrice, Is.EqualTo(expectedTotalPrice));
        }

        [Test, Ignore("this test would fail until we implement discount logic in price calculator")]
        public void GivenMultipleProductsWithOffers_WhenPriceCalculated_ThenAllOffersAreApplied()
        {
            //to do: add test body... (it should be an extension of the previoous test to check multiple offers are applied ) 
        }
    }
}
