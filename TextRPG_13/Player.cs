using System;
using System.Collections;
using System.Collections.Generic;

namespace TextRPG_13
{
    public class Player
    {
        public JOBTYPE Type { get; }
        public PlayerStatement Stats { get; }
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
        }

        private static int GetRequiredExp(int level)
        {
            return (5 * level * level + 35 * level - 20) / 2; //레벨에 따른 필요 경험치 계산식
        }
        public void VictoryBattleResult(Monster target)
        {

            var (newLv, newExp, isLvUp) = GetExpAndLevel(target.Stats.Lv, Stats.Exp, Stats.Level);
            Stats.Level = newLv;
            Stats.Exp = newExp;

            if (isLvUp == true)
            {
                var (newDef, newAtk) = LvUpStat(Stats.Defensivepower, Stats.Offensivepower);
                Stats.Defensivepower = newDef;
                Stats.Offensivepower = newAtk;
            }
        }

        private static (int newLv, int newExp, bool isLvUp) GetExpAndLevel(int monsterLv, int currentExp, int currentLv)
        {
            currentExp += monsterLv ;
            int requiredExp = GetRequiredExp(currentLv);

            bool isLvUp = currentExp >= requiredExp;

            if (isLvUp)
            {
                currentLv++;
            }

            return (currentLv, currentExp, isLvUp);
        }

        private static (float newDefend, float newAttack) LvUpStat(float defend, float attack)
        {
            defend += 1f;
            attack += 0.5f;
            return (defend, attack);
        }
    }
}