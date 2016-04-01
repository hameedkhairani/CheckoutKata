using App.Domain;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class Tests
    {
        private Checkout _checkout;

        [SetUp]
        public void SetUp()
        {
            var orderCreator = new OrderCreator();
            var productRepository = new InMemoryProductRepository();
            var offerRepository = new InMemoryOfferRepository();
            var priceCalculator = new PriceCalculator(productRepository, offerRepository);
            _checkout = new Checkout(orderCreator, priceCalculator);
        }

        [TestCase("", 0)]
        [TestCase("A", 50)]
        [TestCase("AB", 80)]
        [TestCase("CDBA", 115)]
        [TestCase("AA", 100)]
        //[TestCase("AAA", 130)] //"this test case would fail until we implement discount logic in price calculator"
        //[TestCase("AAABB", 175)] //"this test case would fail until we implement discount logic in price calculator"
        public void GivenProducts_WhenScanned_ThenCalculatesTotalPrice(string skuCodes, decimal expectedPrice)
        {
            _checkout.Scan(skuCodes);
            Assert.That(_checkout.TotalPrice, Is.EqualTo(expectedPrice));
        }
    }
}
