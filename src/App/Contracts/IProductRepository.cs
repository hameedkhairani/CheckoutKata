using App.Models;

namespace App.Contracts
{
    public interface IProductRepository
    {
        Product GetByCode(string skuCode);
    }
}
