using System;
using App;
using Moq;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private IProductRepository _productRepository;

        [SetUp]
        public void SetUp()
        {
            _productRepository = new InMemoryProductRepository();
        }

        [Test]
        public void GivenInvalidSkuCode_WhenRepositoryQueried_ThenArgumentExceptionThrown()
        {
            const string testSkuCode = "some_invalid_Code";
            var exception = Assert.Throws<ArgumentException>(() => _productRepository.GetByCode(testSkuCode));
            Assert.That(exception.Message, Is.EqualTo(string.Format("Invalid Sku Code {0}", testSkuCode))); 
        }

        [Test]
        public void GivenValidSkuCode_WhenRepositoryQueried_ThenReturnsProduct()
        {
            const string testSkuCode = "A";
            var product = _productRepository.GetByCode(testSkuCode);
            Assert.That(product, Is.Not.Null);
            Assert.That(product.SkuCode, Is.EqualTo(testSkuCode));
        }

    }
}
