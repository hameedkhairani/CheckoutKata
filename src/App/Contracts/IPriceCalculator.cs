using App.Models;

namespace App.Contracts
{
    public interface IPriceCalculator
    {
        decimal Calculate(Order order);
    }
}