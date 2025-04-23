using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Item
    {
        //나연 추가
        public int Id { get; }              // 메뉴 선택용 번호
        public string Name { get; }         // 아이템 이름
        public int ATKbonus { get; }        // 공격력 추가
        public int DEFbonus { get; }        // 방어력 추가
        public int Cost { get; }            // 골드 가격
        public string Description { get; }  // 아이템 설명
        public bool IsPurchased { get; set; }  // 구매 여부
        public int HealAmount { get; }      //회복량
        public bool IsEquipped { get; set; } = false; //

        public ITEMTYPE ItemCategory { get; }

        public Item(int id, ITEMTYPE itemCategory, string name, int atk, int def, int cost, string desc, int healAmount = 0)
        {
            Id = id;
            ItemCategory = itemCategory;
            Name = name;
            ATKbonus = atk;
            DEFbonus = def;
            Cost = cost;
            Description = desc;
            HealAmount = healAmount;
        }
        //public bool IsEquipable => Type == ITEMTYPE.WEAPON || Type == ITEMTYPE.ARMOR;
        //public bool IsConsumable => Type == ITEMTYPE.POTION;

        //public Item(string name, ITEMTYPE type, int power, int defense, int purchaseprice, string description)
        //{
        //    Name = name;
        //    //Type = type;
        //    Power = power;
        //    Defense = defense;
        //    PurchasePrice = purchaseprice;
        //    SellPrice = (int)((int)PurchasePrice * 0.7);
        //    Description = description;
        //}

    }
}
