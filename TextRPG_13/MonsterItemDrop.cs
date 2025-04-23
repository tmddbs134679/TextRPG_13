using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class MonsterItemDrop
    {
        //드랍 결과 담기
        public class DropResult
        {
            public List<(string name, int count)> PotionDrops { get; }
            public List<(string name, int count)> EquipDrops { get; }

            public DropResult(
                List<(string Name, int Count)> potionDrops,
                List<(string Name, int Count)> equipDrops)
            {
                PotionDrops = potionDrops;
                EquipDrops = equipDrops;
            }
        }
        //랜덤
        private static readonly Random random = new Random();

        public DropResult MonsterDrops(int monsterLv)
        {
            var drops = new List<Item>();
            var allItems = ItemDatabase.Items;

            int potionMin, potionMax;
            switch(monsterLv)
            {
                case 1: potionMin = potionMax = 1; break; //레벨1 : 포션 1개 고정 드랍
                case 2: potionMin = 1; potionMax = 2; break; //레벨2 : 포션 1~2개 드랍
                case 3: potionMin = 1; potionMax = 3; break; //레벨3 : 포션 1~3개 드랍
                case 4: potionMin = 2; potionMax = 4; break; //레벨4 : 포션 2~4개 드랍
                case 5: potionMin = 3; potionMax = 5; break; //레벨5 : 포션 3~5개 드랍
                default: potionMin = potionMax = 1; break;
            }
            int potionCount = random.Next(potionMin, potionMax+1);
            var potion = allItems.First(i => i.Id == 100);

            for (int i = 0; i < potionCount; i++)
                drops.Add(potion);

            int armorId, weaponId;
            switch (monsterLv)
            {
                case 1:
                case 2: armorId = 1; weaponId = 4; break;  // 방어구1, 무기1
                case 3:
                case 4: armorId = 2; weaponId = 5; break;  // 방어구2, 무기2
                case 5: armorId = 3; weaponId = 6; break;  // 방어구3, 무기3
                default: armorId = 1; weaponId = 4; break;
            }
            var armor = allItems.First(i => i.Id == armorId);
            var weapon = allItems.First(i => i.Id == weaponId);

            // 레벨 1,3은 “무조건 하나만”
            // 레벨 2,4,5는 “50% 확률로 둘 다, 아니면 하나만”
            if (monsterLv == 1 || monsterLv == 3)
            {
                drops.Add(random.NextDouble() < 0.5 ? armor : weapon);
            }
            else
            {
                if (random.NextDouble() < 0.5)
                {
                    drops.Add(armor);
                    drops.Add(weapon);
                }
                else
                {
                    drops.Add(random.NextDouble() < 0.5 ? armor : weapon);
                }
            }

            //포션
            var potionGroups = drops
                .Where(i => i.HealAmount > 0)        
                .GroupBy(i => i.Name)                
                .Select(g => (Name: g.Key, Count: g.Count()))
                .ToList();

            //장비
            var equipGroups = drops
                .Where(i => i.ATKbonus > 0 || i.DEFbonus > 0)
                .GroupBy(i => i.Name)
                .Select(g => (Name: g.Key, Count: g.Count()))
                .ToList();

            //포션, 장비 반환
            return new DropResult(potionGroups, equipGroups);
        }
    }
}
