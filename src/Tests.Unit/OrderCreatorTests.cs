using App;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class OrderCreatorTests
    {
        private OrderCreator _orderCreator;

        [SetUp]
        public void SetUp()
        {
            _orderCreator = new OrderCreator();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenNoProducts_WhenOrderCreated_ThenReturnsNull(string skuCodes)
        {
            var order = _orderCreator.Create(skuCodes);

            Assert.That(order, Is.Null);
        }

        [Test]
        public void GivenListOfProducts_WhenOrderCreated_ThenReturnsProductCollectionByQuantity()
        {
            const string skuCodes = "ABCAAB";
            var order = _orderCreator.Create(skuCodes);

            Assert.That(order, Is.Not.Null);
            Assert.That(order.Items.Count, Is.EqualTo(3));
            Assert.That(order.Items["A"], Is.EqualTo(3));
            Assert.That(order.Items["B"], Is.EqualTo(2));
            Assert.That(order.Items["C"], Is.EqualTo(1));
        }

        [TestCase("ABCA")]
        [TestCase("AACB")]
        [TestCase("BACA")]
        public void GivenListOfProductsInDifferentOrder_WhenOrderCreated_ThenReturnsEquivalentProductCollection(string skuCodes)
        {
            var order = _orderCreator.Create(skuCodes);

            Assert.That(order.Items.Count, Is.EqualTo(3));
            Assert.That(order.Items["A"], Is.EqualTo(2));
            Assert.That(order.Items["B"], Is.EqualTo(1));
            Assert.That(order.Items["C"], Is.EqualTo(1));
        }

    }
}
