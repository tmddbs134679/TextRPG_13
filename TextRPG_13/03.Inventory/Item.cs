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
        public int Id { get; set; }              // 메뉴 선택용 번호
        public string Name { get; set; }         // 아이템 이름
        public float ATKbonus { get; set; }        // 공격력 추가
        public float DEFbonus { get; set; }        // 방어력 추가
        public int Cost { get; }            // 골드 가격
        public string Description { get; set; }  // 아이템 설명
        public bool IsPurchased { get; set; }  // 구매 여부
        public int HealAmount { get; set; }      //회복량
        public bool IsEquipped { get; set; } = false; //
        public ITEMTYPE ItemCategory { get; set; }

        public bool IsEquipable => ItemCategory == ITEMTYPE.WEAPON || ItemCategory == ITEMTYPE.ARMOR;

        public Item() { }
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
        
        //public bool IsConsumable => Type == ITEMTYPE.POTION;

        //public Item(string name, ITEMTYPE type, int power, int defense, int purchaseprice, string description)
        //{
        //    Name = name;
        //    Type = type;
        //    Power = power;
        //    Defense = defense;
        //    PurchasePrice = purchaseprice;
        //    SellPrice = (int)((int)PurchasePrice * 0.7);
        //    Description = description;
        //}

    }
}
