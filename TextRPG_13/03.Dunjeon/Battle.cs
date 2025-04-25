using System;
using System.Collections.Generic;
using System.Linq;
using TextRPG_13;
using static System.Net.Mime.MediaTypeNames;

namespace TextRPG_13
{
    public class Battle
    {
        private Player player;
        private List<Monster> monsters;
        private StageManager stageManager;
        bool stageAdvanced = false;
        public Battle()
        {
            stageManager = GameManager.Stage;
        }

        public void BattleSequence()
        {
            player = GameManager.CurrentPlayer;
            int beforeLv = player.Stats.Level;
            int beforeExp = player.Stats.Exp;
            int beforeMP = player.Stats.MP;
            int beforeHP = player.Stats.HP;
            int deathCount = 0;
            int rewardsGold = 0;
            stageAdvanced = false;
            List<Item> droppedItems = new();

            monsters = stageManager.SpawnWave();

            bool isPlayerTurn = true;

            while (player.Stats.HP > 0 && monsters.Any(m => !m.Stats.IsDead))
            {
                if (isPlayerTurn)
                {
                    isPlayerTurn = ProcessPlayerTurn(ref deathCount, beforeLv, beforeExp, beforeHP, beforeMP, ref rewardsGold, droppedItems);
                }
                else
                {
                    ProcessMonsterTurn(beforeHP, beforeMP, ref rewardsGold, droppedItems);
                    isPlayerTurn = true;
                }
            }

        }

        private bool ProcessPlayerTurn(ref int deathCount, int beforeLv, int beforeExp, int beforeHP, int beforeMP, ref int rewardsGold, List<Item> droppedItems)
        {
            UIManager.BattleStart(player, monsters);
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadKey();
                return true;
            }

            return choice switch
            {
                1 => HandleBasicAttack(ref deathCount, beforeLv, beforeExp, beforeHP, beforeMP, ref rewardsGold, droppedItems),
                2 => HandleSkillAttack(ref deathCount, beforeLv, beforeExp, beforeHP, beforeMP, ref rewardsGold, droppedItems),
                _ => InvalidChoice()
            };
        }

        private bool HandleBasicAttack(ref int deathCount, int beforeLv, int beforeExp, int beforePlayerHP, int beforeMP, ref int rewardsGold, List<Item> droppedItems)
        {
            int targetIndex = GetTargetIndex();
            if (targetIndex == -1) return true;

            Monster target = monsters[targetIndex];
            if (target.Stats.IsDead)
            {
                Console.WriteLine("\n이미 죽은 몬스터입니다.");
                Console.ReadKey();
                return true;
            }

            int beforeHP = target.Stats.monsterHP;
            int damage = (int)Player.GetDamageWithVariance(player.Stats.Offensivepower);
            ApplyDamage(target, damage);

            if (target.Stats.IsDead)
                HandleMonsterDefeat(target, ref deathCount, ref rewardsGold, droppedItems);

            PostAttackProcess(target, beforeLv);
            ShowAttackResult(target, damage, beforeHP);

            if (deathCount == monsters.Count)
            {
                ShowVictoryResult(deathCount, beforeLv, beforeExp, beforePlayerHP, beforeMP, rewardsGold, droppedItems);
                if (!stageAdvanced)
                {
                    stageManager.NextStage();
                    stageAdvanced = true;
                }
                return false;
            }

            return false;
        }

        private bool HandleSkillAttack(ref int deathCount, int beforeLv, int beforeExp, int beforeHP, int beforeMP, ref int rewardsGold, List<Item> droppedItems)
        {
            UIManager.PrintSkills(player);
            if (!int.TryParse(Console.ReadLine(), out int choice)) return InvalidChoice();

            if (choice < 1 || choice > player.Skills.Count) return InvalidChoice();

            Skill skill = player.Skills[choice - 1];
            if (player.Stats.MP < skill.Mpcost)
            {
                Console.WriteLine("\nMP가 부족합니다.");
                Console.ReadKey();
                return true;
            }

            player.Stats.MP -= skill.Mpcost;

            if (skill.HitCount == 1)
            {
                return HandleSingleTargetSkill(skill, ref deathCount, beforeLv, beforeExp, beforeHP, beforeMP, ref rewardsGold, droppedItems);
            }
            else
            {
                return HandleMultiTargetSkill(skill, ref deathCount, beforeLv, beforeExp, beforeHP, beforeMP, ref rewardsGold, droppedItems);
            }
        }

        private bool HandleSingleTargetSkill(Skill skill, ref int deathCount, int beforeLv, int beforeExp, int beforePlayerHP, int beforeMP, ref int rewardsGold, List<Item> droppedItems)
        {
            int targetIndex = GetTargetIndex();
            if (targetIndex == -1) return true;

            Monster target = monsters[targetIndex];
            if (target.Stats.IsDead)
            {
                Console.WriteLine("\n이미 죽은 몬스터입니다.");
                Console.ReadKey();
                return true;
            }

            int beforeHP = target.Stats.monsterHP;
            int damage = (int)Player.GetDamageWithVariance(player.Stats.Offensivepower * skill.Damage);
            ApplyDamage(target, damage);

            if (target.Stats.IsDead)
                HandleMonsterDefeat(target, ref deathCount, ref rewardsGold, droppedItems);

            PostAttackProcess(target, beforeLv);
            ShowAttackResult(target, damage, beforeHP);

            if (deathCount == monsters.Count)
            {
                ShowVictoryResult(deathCount, beforeLv, beforeExp, beforePlayerHP, beforeMP, rewardsGold, droppedItems);
                if (!stageAdvanced)
                {
                    stageManager.NextStage();
                    stageAdvanced = true;
                }
                return false;
            }

            return false;
        }

        private bool HandleMultiTargetSkill(Skill skill, ref int deathCount, int beforeLv, int beforeExp, int beforePlayerHP, int beforeMP, ref int rewardsGold, List<Item> droppedItems)
        {
            Random rand = new();
            var aliveMonsters = monsters.Where(m => !m.Stats.IsDead).ToList();
            var targets = aliveMonsters.OrderBy(x => rand.Next()).Take(skill.HitCount).ToList();

            foreach (var target in targets)
            {
                int beforeHP = target.Stats.monsterHP;
                int damage = (int)Player.GetDamageWithVariance(player.Stats.Offensivepower * skill.Damage);
                ApplyDamage(target, damage);

                if (target.Stats.IsDead)
                    HandleMonsterDefeat(target, ref deathCount, ref rewardsGold, droppedItems);

                ShowAttackResult(target, damage, beforeHP);
            }

            if (deathCount == monsters.Count)
            {
                ShowVictoryResult(deathCount, beforeLv, beforeExp, beforePlayerHP, beforeMP, rewardsGold, droppedItems);
                if (!stageAdvanced)
                {
                    stageManager.NextStage();
                    stageAdvanced = true;
                }
                return false;
            }

            return false;
        }

        private void ProcessMonsterTurn(int beforeHP, int beforeMP,ref int rewardsGold, List<Item> droppedItems)
        {
            foreach (var monster in monsters)
            {
                if (monster.Stats.IsDead) continue;

                int damage = Monster.GetDamageWithVariance(monster.Stats.monsterATK);
                player.Stats.HP -= damage;
                if (player.Stats.HP < 0) player.Stats.HP = 0;

                UIManager.PrintEnemyPhase(monster, player, damage, beforeHP);
                WaitForZeroInput();

                if (player.Stats.HP == 0)
                {
                    ShowLoseResult(beforeHP, beforeMP, rewardsGold, droppedItems);
                    break;
                }
            }
        }

        private void ApplyDamage(Monster target, int damage)
        {
            target.Stats.monsterHP -= damage;
            if (target.Stats.monsterHP <= 0)
            {
                target.Stats.monsterHP = 0;
                target.Stats.IsDead = true;
            }
        }

        private void HandleMonsterDefeat(Monster monster, ref int deathCount, ref int rewardsGold, List<Item> droppedItems)
        {
            deathCount++;
            var drop = new MonsterItemDrop().MonsterDrops(monster.Stats.Lv);
            if (drop != null)
            {
                droppedItems.AddRange(drop.DroppedItems);
                rewardsGold += monster.Stats.goldDrop;
            }
            player.VictoryBattleResult(monster, player);
        }

        private void PostAttackProcess(Monster target, int beforeLv)
        {
            player.VictoryBattleResult(target, player);
            var quest = QuestManager.Instance.CurrentQuest;
            if (quest?.Task is TaskMonster task) task.InProgress();
        }

        private void ShowAttackResult(Monster target, int damage, int beforeHP)
        {
            while (true)
            {
                UIManager.DisplayAttackResult(player.Stats.Name, target, damage, beforeHP);
                if (Console.ReadLine() == "0") break;

                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadKey();
            }
        }

        private void ShowVictoryResult(int deathcount, int beforeLv, int beforeExp, int beforeHP, int beforeMP, int rewardsGold, List<Item> droppedItems)
        {
            foreach (var item in droppedItems)
                player.Inven.AddItem(item);
            player.Stats.Gold += rewardsGold;
            
            while (true)
            {
                UIManager.PrintPlayerVictory(player, deathcount, beforeLv, beforeExp, beforeHP, beforeMP, player.Stats.Level > beforeLv, rewardsGold, droppedItems);
                if (Console.ReadLine() == "0") break;

                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadKey();
            }
            if (player.Stats.MP <= 40)
                player.Stats.MP += 10;
            else
                player.Stats.MP += player.Stats.Max_MP - player.Stats.MP; 
        }

        private void ShowLoseResult(int beforeHP, int beforeMP, int rewardsGold, List<Item> droppedItems)
        {
            foreach (var item in droppedItems)
                player.Inven.AddItem(item);
            player.Stats.Gold += rewardsGold;

            while (true)
            {
                UIManager.PrintPlayerLose(player, beforeHP, beforeMP, rewardsGold, droppedItems);
                if (Console.ReadLine() == "0") break;

                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadKey();
            }
        }

        private int GetTargetIndex()
        {
            UIManager.ChooseMonster(player, monsters);
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int index) || index < 0 || index > monsters.Count)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadKey();
                return -1;
            }

            return index == 0 ? -1 : index - 1;
        }

        private bool InvalidChoice()
        {
            Console.WriteLine("\n잘못된 선택입니다.");
            Console.ReadKey();
            return true;
        }

        private void WaitForZeroInput()
        {
            while (true)
            {
                if (Console.ReadLine() == "0") break;
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadKey();
            }
        }
    }
}
