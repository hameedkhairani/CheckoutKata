using App.Models;

namespace App.Contracts
{
    public interface IOrderCreator
    {
        Order Create(string skuCodes);
    }
}