using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public static class ItemDatabase
    {
        public static List<Item> Items = new List<Item>
        {
            new Item(1, ITEMTYPE.ARMOR,"수련자 갑옷", 0, 5, 1000, "수련에 도움을 주는 갑옷입니다."),//방어구1
            new Item(2, ITEMTYPE.ARMOR, "무쇠갑옷",   0, 9, 2000, "무쇠로 만들어져 튼튼한 갑옷입니다."),//방어구2
            new Item(3, ITEMTYPE.ARMOR, "스파르타의 갑옷", 0, 15, 3500, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."),//방어구3
            new Item(4, ITEMTYPE.WEAPON, "낡은 검", 2, 0, 600, "쉽게 볼 수 있는 낡은 검 입니다."),//무기1
            new Item(5, ITEMTYPE.WEAPON, "청동 도끼", 5, 0, 1500, "어디선가 사용됐던것 같은 도끼입니다."),//무기2
            new Item(6, ITEMTYPE.WEAPON, "스파르타의 창", 7, 0, 5000, "스파르타의 전사들이 사용했다는 전설의 창입니다."),//무기3

            new Item(100, ITEMTYPE.POTION, "소형물약", 0, 0, 100, "체력을 조금 회복시켜준다.", 30),//더 추가 가능
            new Item(101, ITEMTYPE.POTION, "중형물약", 0, 0, 200, "체력을 어느정도 회복시켜준다.", 50),
            new Item(103, ITEMTYPE.POTION, "마나물약", 0, 0, 200, "마나를 어느정도 회복시켜준다.", 30)
        };
    }
}