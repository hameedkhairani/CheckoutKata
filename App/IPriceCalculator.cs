using System.Collections.Generic;

namespace App
{
    public interface IPriceCalculator
    {
        decimal Calculate(Order order);
    }
}