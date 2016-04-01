using System;
using System.Linq;
using App;
using App.Contracts;
using App.Domain;
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
        public void GivenOffersAreUnavailableForProduct_WhenRepositoryQueried_ThenReturnsNull()
        {
            const string testSkuCode = "some_code_without_offers";
            var offer = _offerRepository.GetByCode(testSkuCode);
            Assert.That(offer, Is.Empty);
        }

        [Test]
        public void GivenOfferAreAvailableForProduct_WhenRepositoryQueried_ThenReturnsOffers()
        {
            const string testSkuCode = "A";
            var offers = _offerRepository.GetByCode(testSkuCode);
            Assert.That(offers, Is.Not.Null);
            Assert.That(offers.First().SkuCode, Is.EqualTo(testSkuCode));
        }

    }
}
