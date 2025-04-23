using System;
using System.Collections;
using System.Collections.Generic;

namespace TextRPG_13
{
    public class Player
    {
        public JOBTYPE Type { get; }
        public PlayerStatement Stats { get; }
        public Inventory Inven { get; }
        public QuestManager QuestManager { get; private set; } = new QuestManager();

        // 생성자: 직업을 받아서 해당 프리셋 적용
        public Player(JOBTYPE job)
        {
            Type = job;

            // 프리셋에서 가져와 복사
            var preset = PlayerStatement.GetPreset(job);
            
            Stats = new PlayerStatement
            {
                Name = preset.Name,
                Job = preset.Job,
                Offensivepower = preset.Offensivepower,
                Defensivepower = preset.Defensivepower,
                Max_HP = preset.Max_HP,
                HP = preset.HP,
                Gold = preset.Gold,
                Max_MP = preset.Max_MP,
                MP = preset.MP,
                Exp = preset.Exp,
                Potion = preset.Potion
            };

            //인벤토리 인스턴스 생성 후 기본 포션 3개 추가
            Inven = new Inventory();
            for (int i = 0; i < 3; i++)
            {
                Inventory.AddItem(ItemFactory.CreateHealthPotion(), ITEMTYPE.POTION);
            }
        }
    }
}