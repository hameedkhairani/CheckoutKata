using System.Collections.Generic;

namespace App
{
    public class Order
    {
        public Order()
        {
            Items = new Dictionary<string, int>();
        }

        public Dictionary<string, int> Items { get; set; }
    }
}