using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class ItemStack
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public ItemStack() {}
        public ItemStack(Item item, int quantity = 1)
        {
            Item = item;
            Quantity = quantity;
        }

        public void Add(int amount)
        {
            Quantity += amount;
        }

        public void Remove(int amount)
        {
            Quantity -= amount;
            if (Quantity < 0) Quantity = 0;
        }
    }
}

