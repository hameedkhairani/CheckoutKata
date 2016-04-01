namespace App
{
    public interface IOrderCreator
    {
        Order Create(string skuCodes);
    }
}