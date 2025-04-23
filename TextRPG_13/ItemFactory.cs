using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    internal class ItemFactory
    {
        public static Item CreateHealthPotion()
        {
            return new Item(
                name: "체력 포션",
                type: ITEMTYPE.POTION,
                power: 0,
                defense: 0,
                purchaseprice: 30,
                description: "체력을 30 회복하는 포션입니다."
            );
        }
    }
}
