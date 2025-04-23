using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Item
    {
        //public string Name { get; set; }
        //public ITEMTYPE Type { get; set; }
        public int Power { get; set; }

        public int Defense { get; set; }
        public int PurchasePrice { get; set; }
        public int SellPrice { get; set; }
        //public string Description { get; set; }
        public bool IsEquipped { get; set; } = false;

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

        //나연 추가
        public int Id { get; }              // 메뉴 선택용 번호
        public string Name { get; }         // 아이템 이름
        public int ATKbonus { get; }        // 공격력 추가
        public int DEFbonus { get; }        // 방어력 추가
        public int Cost { get; }            // 골드 가격
        public string Description { get; }  // 아이템 설명
        public bool IsPurchased { get; set; }  // 구매 여부
        public int HealAmount { get; }      //회복량

        public Item(int id, string name, int atk, int def, int cost, string desc, int healAmount = 0)
        {
            Id = id;
            Name = name;
            ATKbonus = atk;
            DEFbonus = def;
            Cost = cost;
            Description = desc;
            HealAmount = healAmount;
        }

        public List<Item> ItemDatabase { get; } = new List<Item>
        {
                           //  이름    공격력 방어력 가격      설명
                new Item(1, "수련자 갑옷", 0, 5, 1000, "수련에 도움을 주는 갑옷입니다."),//방어구1
                new Item(2, "무쇠갑옷",   0, 9, 2000, "무쇠로 만들어져 튼튼한 갑옷입니다."),//방어구2
                new Item(3, "스파르타의 갑옷", 0, 15, 3500, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."),//방어구3
                new Item(4, "낡은 검", 2, 0, 600, "쉽게 볼 수 있는 낡은 검 입니다."),//무기1
                new Item(5, "청동 도끼", 5, 0, 1500, "어디선가 사용됐던것 같은 도끼입니다."),//무기2
                new Item(6, "스파르타의 창", 7, 0, 5000, "스파르타의 전사들이 사용했다는 전설의 창입니다."),//무기3

                new Item(100, "소형물약", 0, 0, 100, "Hp 20 회복", 20)

        };
    }
}
