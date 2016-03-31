using System;
using App;
using Moq;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class OfferRepositoryTests
    {
        private IOfferRepository _offerRepository;

        [SetUp]
        public void SetUp()
        {
            _offerRepository = new InMemoryOfferRepository();
        }

        [Test]
        public void GivenOfferIsUnavailableForProduct_WhenRepositoryQueried_ThenReturnsNull()
        {
            const string testSkuCode = "some_code_without_offer";
            var offer = _offerRepository.GetByCode(testSkuCode);
            Assert.That(offer, Is.Null);
        }

        [Test]
        public void GivenOfferIsAvailableForProduct_WhenRepositoryQueried_ThenReturnsOffer()
        {
            const string testSkuCode = "A";
            var offer = _offerRepository.GetByCode(testSkuCode);
            Assert.That(offer, Is.Not.Null);
            Assert.That(offer.SkuCode, Is.EqualTo(testSkuCode));
        }

    }
}
