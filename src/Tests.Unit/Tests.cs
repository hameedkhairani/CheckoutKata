using App;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class Tests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenEmptyBasket_WhenCheckedOut_ThenTotalPriceIsZero(string skuCodes)
        {
            var co = new Checkout();
            co.Scan(skuCodes);
            Assert.That(co.TotalPrice, Is.EqualTo(0));
        }
    }
}
