using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG_13;

namespace TextRPG_13
{
    public class Battle
    {
        private List<Item> droppedItems = new List<Item>();  // 드롭된 아이템을 저장할 리스트
        private int rewardsGold = 0;
        private int deathCount = 0;
        private bool isLvUp = false;
        private int beforeLv;
        private int beforeExp;
        private Player player;
        private List<Monster> monsters;
        public void BattleSequence()
        {

            Player player = GameManager.CurrentPlayer;
            beforeLv = player.Stats.Level;
            beforeExp = player.Stats.Exp;

            Monster.MonsterRandomSpawn();
            monsters = Monster.CurrentWave.ToList();

            bool isPlayerTurn = true;
            while (player.Stats.HP > 0 && monsters.Any(m => !m.Stats.IsDead))
            {
                if (isPlayerTurn)
                {
                    isPlayerTurn = ProcessPlayerTurn(player, monsters, deathCount, beforeLv, isLvUp, rewardsGold, droppedItems);
                }
                else
                {
                    ProcessMonsterTurn(player, monsters, rewardsGold, droppedItems);
                    isPlayerTurn = true;
                }
            }
        }

        private bool ProcessPlayerTurn(Player player, List<Monster> monsters, int deathCount, int beforeLv, bool isLvUp, int rewardsGold, List<Item> droppedItems)
        {
            UIManager.BattleStart(player, monsters);
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadKey();
                return true;
            }

            switch (choice)
            {
                case 1:
                    return HandleBasicAttack(player, monsters, deathCount, beforeLv, isLvUp, rewardsGold, droppedItems);
                case 2:
                    return HandleSkillAttack(player);
                // case 3: return HandleUsePotion();
                default:
                    Console.WriteLine("\n잘못된 선택입니다.");
                    Console.ReadKey();
                    return true;
            }
        }

        private bool HandleBasicAttack(Player player, List<Monster> monsters, int deathCount, int beforeLv, bool isLvUp, int rewardsGold, List<Item> droppedItems)
        {
            while (true)
            {
                int targetIndex = GetMonsterIndexFromInput(player, monsters);
                if (targetIndex == -1) return true;

                Monster target = monsters[targetIndex];
                if (target.Stats.IsDead)
                {
                    Console.WriteLine("\n이미 죽은 몬스터입니다.");
                    Console.ReadKey();
                    continue;
                }

                PerformPlayerAttack(player, target, out int damage, out int beforeHP);

                if (target.Stats.IsDead)
                    HandleMonsterDefeat(target, deathCount, rewardsGold, droppedItems);

                PostAttackPlayerProcess(player, target, beforeLv, isLvUp);
                ShowAttackResult(player, target, damage, beforeHP);

                if (deathCount == monsters.Count)
                {
                    ShowVictoryResult(player, deathCount, beforeLv, beforeExp, isLvUp, rewardsGold, droppedItems);
                    return false;
                }

                return false;
            }
        }

        private int GetMonsterIndexFromInput(Player player, List<Monster> monsters)
        {
            UIManager.ChooseMonster(player, monsters);
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int index) || index < 0 || index > monsters.Count)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadKey();
                return GetMonsterIndexFromInput(player, monsters);
            }

            if (index == 0) return -1;
            return index - 1;
        }

        private void PerformPlayerAttack(Player player, Monster target, out int damage, out int beforeHP)
        {
            beforeHP = target.Stats.monsterHP;
            float rawDamage = Player.GetDamageWithVariance(player.Stats.Offensivepower);
            damage = (int)rawDamage;
            target.Stats.monsterHP -= damage;

            if (target.Stats.monsterHP <= 0)
            {
                target.Stats.monsterHP = 0;
                target.Stats.IsDead = true;
            }
        }


        bool HandleSkillAttack(Player player)
        {
            while (true)
            {


                UIManager.PrintSkills(player);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Console.ReadKey();
                    return true;
                }

                Skill selectedSkill = player.Skills[choice - 1];

                if (player.Stats.MP < selectedSkill.Mpcost)
                {
                    Console.WriteLine("\nMP가 부족합니다.");
                    Console.ReadKey();
                    return true;
                }

                player.Stats.MP -= selectedSkill.Mpcost;

                if (selectedSkill.HitCount == 1)
                {
                    return HandleSingleTargetSkill(player, selectedSkill, monsters);
                }
                else
                {
                    return HandleMultiTargetSkill(player, selectedSkill, monsters);
                }
            }
        }

        private void HandleMonsterDefeat(Monster monster, int deathCount, int rewardsGold, List<Item> droppedItems)
        {
            deathCount++;
            var dropper = new MonsterItemDrop();
            var result = dropper.MonsterDrops(monster.Stats.Lv);
            if (result != null)
            {
                droppedItems.AddRange(result.DroppedItems);
                rewardsGold += monster.Stats.goldDrop;
            }
        }
        private bool HandleSingleTargetSkill(Player player, Skill skill, List<Monster> monsters)
        {
            while (true)
            {
                int targetIndex = GetMonsterIndexFromInput(player, monsters);
                if (targetIndex == -1) return true;

                Monster target = monsters[targetIndex];
                if (target.Stats.IsDead)
                {
                    Console.WriteLine("\n이미 죽은 몬스터입니다.");
                    Console.ReadKey();
                    continue;
                }

                ApplySkillDamage(player, target, skill);

                if (target.Stats.IsDead)
                    HandleMonsterDefeat(target, deathCount, rewardsGold, droppedItems);

                PostAttackPlayerProcess(player, target, beforeLv, isLvUp);
                ShowAttackResult(player, target, (int)(player.Stats.Offensivepower * skill.Damage), target.Stats.monsterHP);

                if (deathCount == monsters.Count)
                {
                    ShowVictoryResult(player, deathCount, beforeLv, beforeExp, isLvUp, rewardsGold, droppedItems);
                    return false;
                }

                return false;
            }
        }

        private bool HandleMultiTargetSkill(Player player, Skill skill, List<Monster> monsters)
        {
            Random rand = new Random();
            var aliveMonsters = monsters.Where(m => !m.Stats.IsDead).ToList();

            int hitCount = Math.Min(skill.HitCount, aliveMonsters.Count);
            var targets = aliveMonsters.OrderBy(x => rand.Next()).Take(hitCount).ToList();

            foreach (var target in targets)
            {
                ApplySkillDamage(player, target, skill);

                if (target.Stats.IsDead)
                    HandleMonsterDefeat(target, deathCount, rewardsGold, droppedItems);

                ShowAttackResult(player, target, (int)(player.Stats.Offensivepower * skill.Damage), target.Stats.monsterHP);
            }

            PostAttackPlayerProcess(player, null, beforeLv, isLvUp);

            if (deathCount == monsters.Count)
            {
                ShowVictoryResult(player, deathCount, beforeLv, beforeExp, isLvUp, rewardsGold, droppedItems);
                return false;
            }

            return false;
        }

        private void ApplySkillDamage(Player player, Monster target, Skill skill)
        {
            int beforeHP = target.Stats.monsterHP;
            float damage = Player.GetDamageWithVariance(player.Stats.Offensivepower * skill.Damage);
            target.Stats.monsterHP -= (int)damage;

            if (target.Stats.monsterHP <= 0)
            {
                target.Stats.monsterHP = 0;
                target.Stats.IsDead = true;
            }
        }

        private void PostAttackPlayerProcess(Player player, Monster target, int beforeLv, bool isLvUp)
        {
            player.VictoryBattleResult(target, player);
            isLvUp = player.Stats.Level > beforeLv;

            var quest = QuestManager.Instance.CurrentQuest;
            if (quest?.Task is TaskMonster task)
                task.InProgress();
        }

        private void ShowAttackResult(Player player, Monster target, int damage, int beforeHP)
        {
            while (true)
            {
                UIManager.DisplayAttackResult(player.Stats.Name, target, damage, beforeHP);
                string input = Console.ReadLine();
                if (input == "0") break;

                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadKey();
            }
        }

        private void ShowVictoryResult(Player player, int deathCount, int beforeLv, int beforeExp, bool isLvUp, int rewardsGold, List<Item> droppedItems)
        {
            foreach (var item in droppedItems)
                player.Inven.AddItem(item);
            player.Stats.Gold += rewardsGold;

            while (true)
            {
                UIManager.PrintPlayerVictory(player, deathCount, beforeLv, beforeExp, isLvUp, rewardsGold, droppedItems);
                string input = Console.ReadLine();
                if (input == "0") break;

                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadKey();
            }
        }

        private void ProcessMonsterTurn(Player player, List<Monster> monsters, int rewardsGold, List<Item> droppedItems)
        {
            foreach (var monster in monsters)
            {
                if (monster.Stats.IsDead) continue;

                int damage = Monster.GetDamageWithVariance(monster.Stats.monsterATK);
                int beforeHP = player.Stats.HP;
                player.Stats.HP -= damage;
                if (player.Stats.HP < 0) player.Stats.HP = 0;

                UIManager.PrintEnemyPhase(monster, player, damage, beforeHP);
                WaitForZeroInput();

                if (player.Stats.HP == 0)
                {
                    ShowLoseResult(player, rewardsGold, droppedItems);
                    break;
                }
            }
        }

        private void ShowLoseResult(Player player, int rewardsGold, List<Item> droppedItems)
        {
            foreach (var item in droppedItems)
                player.Inven.AddItem(item);
            player.Stats.Gold += rewardsGold;

            while (true)
            {
                UIManager.PrintPlayerLose(player, rewardsGold, droppedItems);
                string input = Console.ReadLine();
                if (input == "0") break;

                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadKey();
            }
        }

        private void WaitForZeroInput()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "0") break;

                Console.WriteLine("\n잘못된 입력입니다.");
                Console.ReadKey();
            }
        }
    }
}




//private List<Item> droppedItems = new List<Item>();  // 드롭된 아이템을 저장할 리스트
//private StageManager stageManager;

//public void BattleSequence()
//{

//    Player player = GameManager.CurrentPlayer;
//    beforeLv = player.Stats.Level;
//    beforeExp = player.Stats.Exp;

//    bool isPlayerTurn = true;
//    bool isLvUp = false;
//    int deathCount = 0;
//    int beforeLv = player.Stats.Level;
//    int beforeExp = player.Stats.Exp;
//    int rewardsGold = 0;

//    //전투 시작할때마다 SpawnWave()호출
//    List<Monster> monsters = stageManager.SpawnWave();

//    bool isPlayerTurn = true;
//    while (player.Stats.HP > 0 && monsters.Any(m => !m.Stats.IsDead))
//    {
//        if (isPlayerTurn)
//        {
//            isPlayerTurn = ProcessPlayerTurn(player, monsters, deathCount, beforeLv, isLvUp, rewardsGold, droppedItems);
//        }
//        else
//        {
//            ProcessMonsterTurn(player, monsters, rewardsGold, droppedItems);
//            isPlayerTurn = true;
//        }
//    }
//}

//private bool ProcessPlayerTurn(Player player, List<Monster> monsters, int deathCount, int beforeLv, bool isLvUp, int rewardsGold, List<Item> droppedItems)
//{
//    UIManager.BattleStart(player, monsters);
//    string input = Console.ReadLine();

//        if (!int.TryParse(input, out int choice))
//        {
//            WriteColor("화면에 표기된 번호중 하나를 선택해주세요.", ConsoleColor.DarkYellow);
//            Console.ReadKey();
//            continue;
//        }
//        else if (choice == 1) //공격 선택지 선택
//        {
//            if (isPlayerTurn == true)
//            {
//                while (true)
//                {
//                    //몬스터 선택 페이지 출력
//                    UIManager.DisplayMonsters(player, monsters);
//                    input = Console.ReadLine();

//                    if (!int.TryParse(input, out int index) || (index < 0 || index > monsters.Count))
//                    {
//                        WriteColor("화면에 표기된 번호중 하나를 선택해주세요.", ConsoleColor.DarkYellow);
//                        Console.ReadKey();
//                        continue;
//                    }
//                    else if (index == 0) break; //0.취소 선택

//        if (index == 0) return true;

//        Monster target = monsters[index - 1];
//        if (target.Stats.IsDead)
//        {
//            Console.WriteLine("\n이미 죽은 몬스터입니다.");
//            Console.ReadKey();
//            continue;
//        }

//        int beforeHP = target.Stats.monsterHP;
//        float damage = Player.GetDamageWithVariance(player.Stats.Offensivepower);
//        target.Stats.monsterHP -= (int)damage;
//        if (target.Stats.monsterHP <= 0)
//        {
//            target.Stats.monsterHP = 0;
//            target.Stats.IsDead = true;
//            HandleMonsterDefeat(target, deathCount, rewardsGold, droppedItems);
//        }

//        PostAttackPlayerProcess(player, target, beforeLv, isLvUp);
//        ShowAttackResult(player, target, (int)damage, beforeHP);

//        if (deathCount == monsters.Count)
//        {
//            ShowVictoryResult(player, deathCount, beforeLv, beforeExp, isLvUp, rewardsGold, droppedItems);
//            return false;
//        }

//                        while (true)
//                        {
//                            //공격 결과 출력
//                            UIManager.DisplayAttackResult(player.Stats.Name, target, (int)damage, beforeHp);

//                            input = Console.ReadLine();
//                            if (!int.TryParse(input, out int i) || (i != 0))
//                            {
//                                WriteColor("화면에 표기된 번호중 하나를 선택해주세요.", ConsoleColor.DarkYellow);
//                                Console.ReadKey();
//                                continue;
//                            }
//                            else if (i == 0)
//                            {
//                                isPlayerTurn = false;
//                                break;
//                            }
//                        }
//                        if (deathCount == monsters.Count) //몬스터 모두 처치
//                        {
//                            foreach (var item in droppedItems)
//                            {
//                                player.Inven.AddItem(item); //인벤토리에 아이템 저장
//                                player.Stats.Gold += rewardsGold; // 드롭된 골드를 플레이어의 골드에 추가
//                            }
//                            while (true)
//                            {
//                                UIManager.PrintPlayerVictory(player, deathCount, beforeLv, beforeExp, isLvUp, rewardsGold, droppedItems); //수정 윈화면 출력되다가 몬스터턴으로 넘어감

//                                //승리할때만 다음 스테이지
//                                stageManager.NextStage();
//                                input = Console.ReadLine();
//                                if (!int.TryParse(input, out int j) || (j != 0))
//                                {
//                                    WriteColor("화면에 표기된 번호중 하나를 선택해주세요.", ConsoleColor.DarkYellow);
//                                    Console.ReadKey();
//                                    continue;
//                                }
//                                else if (j == 0) break; //0.취소 선택
//                            }
//                        }
//                    }
//                    break;
//                }
//            }
//        }
//        if (!isPlayerTurn)
//        {
//            for (int i = 0; i < monsters.Count; i++)
//            {
//                if (!monsters[i].Stats.IsDead)
//                {
//                    //몬스터의 공격
//                    int monsterDamage = GetDamageWithVariance(monsters[i].Stats.monsterATK);
//                    int beforePlayerHP = player.Stats.HP;
//                    player.Stats.HP -= monsterDamage;
//                    if (player.Stats.HP <= 0) player.Stats.HP = 0;
//                    while (true)
//                    {
//                        //전투 결과 출력
//                        UIManager.PrintEnemyPhase(monsters[i], player, monsterDamage, beforePlayerHP);
//                        input = Console.ReadLine();
//                        if (!int.TryParse(input, out int j) || (j != 0))
//                        {
//                            Console.WriteLine("\n잘못된 입력입니다.");
//                            Console.ReadKey();
//                            continue;
//                        }
//                        else if (j == 0) break; //0.취소 선택
//                    }
//                    if (player.Stats.HP == 0)
//                    {
//                        foreach (var item in droppedItems)
//                        {
//                            player.Inven.AddItem(item);  //인벤토리에 아이템 저장
//                            player.Stats.Gold += rewardsGold; // 드롭된 골드를 플레이어의 골드에 추가
//                        }
//                        while (true)
//                        {
//                            UIManager.PrintPlayerLose(player, rewardsGold, droppedItems);
//                            input = Console.ReadLine();
//                            if (!int.TryParse(input, out int j) || (j != 0))
//                            {
//                                Console.WriteLine("\n잘못된 입력입니다.");
//                                Console.ReadKey();
//                                continue;
//                            }
//                            else if (j == 0) //취소선택
//                            {
//                                i = monsters.Count; //나머지 몬스터의 공격X
//                                break;
//                            }
//                        }
//                    }
//                }
//            }
//            isPlayerTurn = true;
//        }
//    }
//    //2. 스킬사용 추가
//    //3. 포션 사용