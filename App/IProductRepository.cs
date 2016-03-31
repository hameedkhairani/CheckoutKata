namespace App
{
    public interface IProductRepository
    {
        Product GetByCode(string skuCode);
    }
}