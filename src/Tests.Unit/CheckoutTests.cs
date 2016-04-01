using App;
using App.Contracts;
using App.Domain;
using App.Models;
using Moq;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class CheckoutTests
    {
        private Mock<IOrderCreator> _mockOrderCreator;
        private Mock<IPriceCalculator> _mockPriceCalculator;
        private Checkout _checkout;
        private string _testSkuCodes;
        private Order _testOrder;
        private decimal _testTotalPrice;

        [SetUp]
        public void SetUp()
        {
            _mockOrderCreator = new Mock<IOrderCreator>();
            _mockPriceCalculator = new Mock<IPriceCalculator>();
            _checkout = new Checkout(_mockOrderCreator.Object, _mockPriceCalculator.Object);
            _testSkuCodes = "ABCA";
            _testOrder = new Order();
            _testTotalPrice = 100m;
        }

        [Test]
        public void GivenValidOrder_WhenItemsScanned_ThenCalculatesTotalPriceOnOrder()
        {
            _mockOrderCreator.Setup(p => p.Create(_testSkuCodes)).Returns(_testOrder);
            _mockPriceCalculator.Setup(p => p.Calculate(_testOrder)).Returns(_testTotalPrice);

            _checkout.Scan(_testSkuCodes);

            _mockOrderCreator.Verify(p => p.Create(_testSkuCodes));
            _mockPriceCalculator.Verify(p => p.Calculate(_testOrder));
            Assert.That(_checkout.TotalPrice, Is.EqualTo(_testTotalPrice));
        }

        [Test]
        public void GivenInvalidOrder_WhenItemsScanned_ThenDoesNotCalculateTotalPrice()
        {
            _mockOrderCreator.Setup(p => p.Create(_testSkuCodes)).Returns((Order) null);
            _checkout.Scan(_testSkuCodes);

            _mockOrderCreator.Verify(p => p.Create(_testSkuCodes));
            _mockPriceCalculator.Verify(p => p.Calculate(It.IsAny<Order>()), Times.Never);
        }

    }
}
