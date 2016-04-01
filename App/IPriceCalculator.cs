using System.Collections.Generic;

namespace App
{
    public interface IPriceCalculator
    {
        decimal Calculate(Dictionary<string, int> order);
    }
}