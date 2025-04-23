using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class MonsterItemDrop
    {
        public class DropConfig
        {
            //드랍할 포션 아이템 id랑 최소 최대값 받아서 몇 개 드랍할지 범위
            public (int potionId, int minCount, int maxCount) PotionRule { get; }
            public int[] ArmorIds { get; }
            public int[] WeaponIds { get; }
            public bool AlwaysOne { get; }   // true무조건 하나 false 50% 확률로 둘 다

            public DropConfig(
                (int potionId, int minCount, int maxCount) potionRule,
                int[] armorIds,
                int[] weaponIds,
                bool alwaysOne
            )
            {
                PotionRule = potionRule;
                ArmorIds = armorIds;
                WeaponIds = weaponIds;
                AlwaysOne = alwaysOne;
            }
        }

        public static class MonsterDropTable
        {
            /* 기획
             * 레벨 1 몬스터 : 소형포션 1개 고정드랍 / 방어구(id : 1), 무기(id : 4) 중에서 하나 드랍  
             * 레벨 2 몬스터 : 소형포션 1~2개 드랍 / 방어구(id : 1), 무기(id : 4) 중에서 하나만 줄 확률 50퍼, 둘 다 줄 확률 50퍼
             * 레벨 3 몬스터 : 소형포션 1~3개 드랍 / 방어구(id : 2), 무기(id : 5) 중에서 하나 드랍
             * 레벨 4 몬스터 : 소형포션 3개 드랍 또는 중형포션 1~2개 드랍 / 방어구(id : 2), 무기(id : 5) 중에서 하나만 줄 확률 50퍼, 둘 다 줄 확률 50퍼
             * 레벨 5 몬스터 : 중형포션 2~3개 드랍 / 방어구(id : 3), 무기(id : 6) 중에서 하나만 줄 확률 50퍼, 둘 다 줄 확률 50퍼
             */
            // 인덱스: monsterLv 1~5 
            public static readonly DropConfig[] Configs = {
            null,//0번은 쓰지 않음
            new DropConfig((100,1,1), new[]{1},      new[]{4},     true),
            new DropConfig((100,1,2), new[]{1},      new[]{4},     false),
            new DropConfig((100,1,3), new[]{2},      new[]{5},     true),
            new DropConfig((100,3,3), new[]{2},      new[]{5},     false),
            new DropConfig((101,2,3), new[]{3},      new[]{6},     false), 
            // true: 무조건 하나만 / false: 50% 확률로 둘 다
            };
        }
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
            var cfg = MonsterDropTable.Configs[monsterLv];

            //포션 드랍
            int pid = cfg.PotionRule.potionId;
            int pmin = cfg.PotionRule.minCount;
            int pmax = cfg.PotionRule.maxCount;
            int pcount = random.Next(pmin, pmax + 1);

            // 중형 포션 드랍 조건(3 vs 1~2)
            if (monsterLv == 4 && random.NextDouble() < 0.5)
            {
                // 절반확률로 중형 포션 1~2개
                pid = 101;
                pmin = 1;
                pmax = 2;
                pcount = random.Next(pmin, pmax + 1);
            }

            var potion = allItems.First(i => i.Id == pid);
            for (int i = 0; i < pcount; i++)
                drops.Add(potion);

            //장비 드랍
            var armors = cfg.ArmorIds.Select(id => allItems.First(i => i.Id == id)).ToList();
            var weaps = cfg.WeaponIds.Select(id => allItems.First(i => i.Id == id)).ToList();

            bool oneOnly = cfg.AlwaysOne;
            if (oneOnly)
            {
                // armor 또는 weapon 중 하나만 랜덤
                var choice = random.NextDouble() < 0.5 ? armors[0] : weaps[0];
                drops.Add(choice);
            }
            else
            {
                // 50% 확률로 둘 다, 아니면 하나만
                if (random.NextDouble() < 0.5)
                {
                    drops.AddRange(armors);
                    drops.AddRange(weaps);
                }
                else
                {
                    drops.Add(random.NextDouble() < 0.5 ? armors[0] : weaps[0]);
                }
            }

            //그룹핑(UIManager에서 사용해야 함)
            var potionGroups = drops
                .Where(i => i.ItemCategory == ITEMTYPE.POTION)
                .GroupBy(i => i.Name)
                .Select(g => (Name: g.Key, Count: g.Count()))
                .ToList();

            var equipGroups = drops
                .Where(i => i.ItemCategory != ITEMTYPE.POTION)
                .GroupBy(i => i.Name)
                .Select(g => (Name: g.Key, Count: g.Count()))
                .ToList();

            return new DropResult(potionGroups, equipGroups);
        }
        /*
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
            var potion = allItems.First(i => i.ItemCategory == ITEMCATEGORY.POTION);

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
            var armor = allItems.First(i => i.ItemCategory == ITEMCATEGORY.ARMOR);
            var weapon = allItems.First(i => i.ItemCategory == ITEMCATEGORY.WEAPON);

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
        */
    }
}
