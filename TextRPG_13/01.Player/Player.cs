﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace TextRPG_13
{
    public class Player
    {
        public JOBTYPE Type { get; }
        public PlayerStatement Stats { get;  set; }
        public Inventory Inven { get; set; }
        //public QuestManager QuestManager { get; private set; } = new QuestManager();
        public List<Skill> Skills { get; private set; }

        public Player() { }
        // 생성자: 직업을 받아서 해당 프리셋 적용
        public Player(JOBTYPE job)
        {
            Type = job;

            // 프리셋에서 가져와 복사
            var preset = PlayerStatement.GetPreset(job);

            // 직업에 따른 스킬 세팅
            Skills = SkillsForEachJob.GetSkills(job);

            Stats = new PlayerStatement
            {
                Name = preset.Name,
                Job = preset.Job,
                Level = preset.Level,
                baseATK = preset.baseATK,
                baseDEF = preset.baseDEF,
                Max_HP = preset.Max_HP,
                HP = preset.HP,
                Max_MP = preset.Max_MP,
                MP = preset.MP,
                Gold = preset.Gold,
                Exp = preset.Exp
            };
            //인벤토리 인스턴스 생성 후 기본 포션 3개 추가
            Inven = new Inventory(GameManager.CurrentPlayer);
            Inven.AddInitialPotions();
            Inven.AddSword();
        }
        private static Random random = new Random();

        public static int GetDamageWithVariance(float baseAtk)
        {
           
            double offset = Math.Ceiling(baseAtk * 0.1);
            int critalChance = random.Next(1, 101);
            int avoidAttack = random.Next(1, 101);
            int finalDamage = 0;

            if (avoidAttack > 10)
            {
                finalDamage = (int)baseAtk + random.Next(-(int)offset, (int)offset);
                if (critalChance <= 15)
                {
                    finalDamage = (int)Math.Ceiling((finalDamage * 1.5));
                }

            }

            return finalDamage;
        }
        private static int GetRequiredExp(int level)
        {
            return (5 * level * level + 35 * level - 20) / 2; //레벨에 따른 필요 경험치 계산식
        }
        public void VictoryBattleResult(Monster target, Player player)
        {

            var (newLv, newExp, isLvUp) = GetExpAndLevel(target.Stats.Lv, player.Stats.Exp, player.Stats.Level);
            player.Stats.Level = newLv;
            player.Stats.Exp = newExp;

            if (isLvUp == true)
            {
                var (newDef, newAtk) = LvUpStat(player.Stats.baseDEF, player.Stats.baseATK);
                player.Stats.baseDEF = newDef;
                player.Stats.baseATK = newAtk;
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
        
    
        public void RestoreReferences()
        {
            if (Stats != null)
                Stats.SetOwner(this);

            if (Inven != null)
                Inven.SetOwner(this);

            if (Skills == null || Skills.Count == 0)
            {
                Skills = SkillsForEachJob.GetSkills(Stats.Job);
            }

            Stats.bonusATK = 0;
            Stats.bonusDEF = 0;

            foreach (var item in Inven.GetEquippedItems())
            {
                Stats.bonusATK += item.ATKbonus;
                Stats.bonusDEF += item.DEFbonus;
            }

        }
      

    }
}