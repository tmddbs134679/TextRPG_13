using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace TextRPG_13
{
    public class Player
    {
        public JOBTYPE Type { get; }
        public PlayerStatement Stats { get; private set; }
        public Inventory Inven { get; }
        //public QuestManager QuestManager { get; private set; } = new QuestManager();
        public List<Skill> Skills { get; private set; }

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
                Exp = preset.Exp,
                Potion = preset.Potion
            };
            //인벤토리 인스턴스 생성 후 기본 포션 3개 추가
            Inven = new Inventory();
            Inven.AddInitialPotions();
            Inven.AddSword();
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

        public static int GetDamageWithVariance(float baseAtk)
        {
            Random rand = new Random();
            double offset = Math.Ceiling(baseAtk * 0.1);
            int critalChance = rand.Next(1, 101);
            int avoidAttack = rand.Next(1, 101);
            int finalDamage = 0;

            if (avoidAttack > 10)
            {
                finalDamage = (int)baseAtk + rand.Next(-(int)offset, (int)offset);
                if (critalChance <= 15)
                {
                    finalDamage = (int)Math.Ceiling((finalDamage * 1.5));
                }

            }

            return finalDamage;
        }
        //플레이어에서
        public void UseSkill(Player player,Skill skill, List <Monster> monsters,int index)
        {
            if (player.Stats.MP < skill.Mpcost) return;

            player.Stats.MP -= skill.Mpcost;
            if(skill.HitCount > 1)
                HitMultiEnemy(player, skill, monsters);
            else
                monsters[index].TakeSkillDamage(skill.Damage, player);

        }
        private void  HitMultiEnemy(Player player, Skill skill, List<Monster> monsters)
        {
            Random random = new Random();
            int hits = 0;
            while (skill.HitCount < hits)
            {
                int rand = random.Next(0, skill.HitCount);
                if (!monsters[hits].Stats.IsDead)
                {
                    monsters[rand].TakeSkillDamage(skill.Damage, player);
                    hits++;
                }

            }
        }
    }
}