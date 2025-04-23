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
                id:1000,
                name: "체력 포션",
                itemCategory: ITEMTYPE.POTION,
                atk: 0,
                def: 0,
                cost: 30,
                desc: "체력을 30 회복하는 포션입니다."
            );
        }
    }
}
