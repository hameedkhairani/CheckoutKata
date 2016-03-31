namespace App
{
    public interface IOfferRepository
    {
        Offer GetByCode(string skuCode);
    }
}