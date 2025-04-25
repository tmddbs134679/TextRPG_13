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
            public (int potionId, int minCount, int maxCount)[] PotionRule { get; }
            //그 레벨에서 가능한 방어구와 무기 id
            public int[] ArmorIds { get; }
            public int[] WeaponIds { get; }
            public bool AlwaysOne { get; }   // true무조건 하나 false 50% 확률로 둘 다

            public DropConfig(
                (int potionId, int minCount, int maxCount)[] potionRule,
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
            new DropConfig(new[] { (100,1,1), (103,1,1) }, new[]{1},new[]{4},true),
            new DropConfig(new[] { (100,1,2), (103,1,2) }, new[]{1},new[]{4},false),
            new DropConfig(new[] { (100,1,3), (103,1,3) }, new[]{2},new[]{5},true),
            new DropConfig(new[] { (100,3,3), (103,3,3) }, new[]{2},new[]{5},false),
            new DropConfig(new[] { (101,2,3), (103,2,3) }, new[]{3},new[]{6},false), 
            // true: 무조건 하나만 / false: 50% 확률로 둘 다
            };
        }


        
        public class DropResult
        {
            public List<(string name, int count)> PotionDrops { get; }
            public List<(string name, int count)> EquipDrops { get; }
            public List<Item> DroppedItems { get; }
            public DropResult(
                List<(string Name, int Count)> potionDrops,
                List<(string Name, int Count)> equipDrops,
                List<Item> droppedItems)
            {
                PotionDrops = potionDrops;
                EquipDrops = equipDrops;
                DroppedItems = droppedItems;
            }
        }

        //랜덤
        private static readonly Random random = new Random();
        //실제 드랍을 수행할 MOnsterDrops메서드
        public DropResult MonsterDrops(int monsterLv)
        {
            var drops = new List<Item>();
            var allItems = ItemDatabase.Items;
            var cfg = MonsterDropTable.Configs[monsterLv];

            //포션 드랍

            foreach (var rule in cfg.PotionRule)
            {
                int pid = rule.potionId;
                int pmin = rule.minCount;
                int pmax = rule.maxCount;

                // Lv.4 몬스터 특수 조건: 중형포션 vs 소형포션
                // 중형 포션 드랍 조건(소형 3개 또는 중형 1 ~ 2개 확률 50퍼)
                if (monsterLv == 4 && pid == 100 && random.NextDouble() < 0.5)
                {
                    // 중형 포션 1~2개
                    pid = 101;
                    pmin = 1;
                    pmax = 2;
                }

                int pcount = random.Next(pmin, pmax + 1);
                //id가 pid와 같은 첫 번째 아이템 객체를 찾아 potion 변수에 담기
                var potion = allItems.First(i => i.Id == pid);

                for (int i = 0; i < pcount; i++)
                    drops.Add(potion);//같은 potion객체를 리스트에 차례로 추가
            }

            //장비 드랍
            //아이템 리스트에서 id가 같은 첫번째 객체를 꺼내라
            var armors = cfg.ArmorIds.Select(id => allItems.First(i => i.Id == id)).ToList();//지정된 방어구 id에 대응하는 객체 리스트
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

            return new DropResult(potionGroups, equipGroups, drops);
        }
    }
}
