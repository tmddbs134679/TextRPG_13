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

        public void BattleSequence()
        {
            Player player = GameManager.CurrentPlayer;

            bool isPlayerTurn = true;
            bool isLvUp = false;
            int deathCount = 0;
            int beforeLv = player.Stats.Level;
            int beforeExp = player.Stats.Exp;
            int rewardsGold = 0;

            Monster.MonsterRandomSpawn();
            List<Monster> monsters = Monster.CurrentWave.ToList();

            while ((player.Stats.HP > 0) && monsters.Any(m => !m.Stats.IsDead))
            {
                UIManager.BattleStart(player, monsters); //공격 스킬 선택지 페이지 출력
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Console.ReadKey();
                    continue;
                }
                else if (choice == 1) //공격 선택지 선택
                {
                    if (isPlayerTurn == true)
                    {
                        while (true)
                        {
                            //choice에 따라 몬스터 선택 아니면 스킬창 페이지 출력
                            UIManager.DisplayMonstersAndPlayer(player, monsters, choice);
                            input = Console.ReadLine();

                            if (!int.TryParse(input, out int index) || (index < 0 || index > monsters.Count))
                            {
                                Console.WriteLine("\n잘못된 입력입니다.");
                                Console.ReadKey();
                                continue;
                            }
                            else if (index == 0) break; //0.취소 선택


                            Monster target = monsters[index - 1];

                            if (target.Stats.IsDead) //몬스터 Dead 여부
                            {
                                Console.WriteLine("\n이미 죽은 몬스터입니다.");
                                Console.ReadKey();
                                continue;
                            }
                            else
                            {
                                //플레이어 공격
                                float damage = Player.GetDamageWithVariance(player.Stats.Offensivepower);
                                int beforeHp = target.Stats.monsterHP;
                                target.Stats.monsterHP -= (int)damage;
                                if (target.Stats.monsterHP <= 0)
                                {
                                    target.Stats.monsterHP = 0;
                                    target.Stats.IsDead = true;
                                    deathCount++;

                                    // 현재 전투에서 처리된 몬스터가 드롭하는 아이템 리스트 저장
                                    var dropper = new MonsterItemDrop();
                                    MonsterItemDrop.DropResult result = dropper.MonsterDrops(target.Stats.Lv);
                                    if (result != null)
                                    {
                                        foreach (var item in result.DroppedItems)
                                        {
                                            droppedItems.Add(item);
                                            rewardsGold += target.Stats.goldDrop;
                                        }
                                    }
                                }

                                // 경험치 및 레벨업 처리
                                player.VictoryBattleResult(target,player);

                                // 레벨업 했는지확인
                                isLvUp = player.Stats.Level > beforeLv;

                                //퀘스트 몬스터
                                var quest = QuestManager.Instance.CurrentQuest;
                                if (quest != null && quest.Task is TaskMonster task)
                                {
                                    task.InProgress();
                                }

                                while (true)
                                {
                                    //공격 결과 출력
                                    UIManager.DisplayAttackResult(player.Stats.Name, target, (int)damage, beforeHp);

                                    input = Console.ReadLine();
                                    if (!int.TryParse(input, out int i) || (i != 0))
                                    {
                                        Console.WriteLine("\n잘못된 입력입니다.");
                                        Console.ReadKey();
                                        continue;
                                    }
                                    else if (i == 0)
                                    {
                                        isPlayerTurn = false;
                                        break;
                                    }
                                }
                                if (deathCount == monsters.Count) //몬스터 모두 처치
                                {
                                    foreach (var item in droppedItems)
                                    {
                                        player.Inven.AddItem(item); //인벤토리에 아이템 저장
                                        player.Stats.Gold += rewardsGold; // 드롭된 골드를 플레이어의 골드에 추가
                                    }
                                    while (true)
                                    {
                                        UIManager.PrintPlayerVictory(player, deathCount, beforeLv, beforeExp, isLvUp, rewardsGold, droppedItems); //수정 윈화면 출력되다가 몬스터턴으로 넘어감
                                        input = Console.ReadLine();
                                        if (!int.TryParse(input, out int j) || (j != 0))
                                        {
                                            Console.WriteLine("\n잘못된 입력입니다.");
                                            Console.ReadKey();
                                            continue;
                                        }
                                        else if (j == 0) break; //0.취소 선택
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                //else if(choice == 2)
                //{
                //    if (isPlayerTurn == true)
                //    {
                //        while (true)
                //        {
                //            //choice에 따라 몬스터 선택 아니면 스킬창 페이지 출력
                //            UIManager.DisplayMonstersAndPlayer(player, monsters, choice);
                //            input = Console.ReadLine();

                //            if (!int.TryParse(input, out int index) || (index < 0 || index > player.Skills.Count))
                //            {
                //                Console.WriteLine("\n잘못된 입력입니다.");
                //                Console.ReadKey();
                //                continue;
                //            }
                //            else if (index == 0) break; //0.취소 선택
                //            if(player.Skills[index].HitCount == 1)
                                
                //            if (target.Stats.IsDead) //몬스터 Dead 여부
                //            {
                //                Console.WriteLine("\n이미 죽은 몬스터입니다.");
                //                Console.ReadKey();
                //                continue;
                //            }
                //            else
                //            {
                //                //플레이어 스킬 공격
                //                if (player.Skills.[])
                //                player.UseSkill
                //                if (target.Stats.monsterHP <= 0)
                //                {
                //                    target.Stats.monsterHP = 0;
                //                    target.Stats.IsDead = true;
                //                    deathCount++;

                //                    // 현재 전투에서 처리된 몬스터가 드롭하는 아이템 리스트 저장
                //                    var dropper = new MonsterItemDrop();
                //                    MonsterItemDrop.DropResult result = dropper.MonsterDrops(target.Stats.Lv);
                //                    if (result != null)
                //                    {
                //                        foreach (var item in result.DroppedItems)
                //                        {
                //                            droppedItems.Add(item);
                //                            rewardsGold += target.Stats.goldDrop;
                //                        }
                //                    }
                //                }

                //                // 경험치 및 레벨업 처리
                //                player.VictoryBattleResult(target, player);

                //                // 레벨업 했는지확인
                //                isLvUp = player.Stats.Level > beforeLv;

                //                //퀘스트 몬스터
                //                var quest = QuestManager.Instance.CurrentQuest;
                //                if (quest != null && quest.Task is TaskMonster task)
                //                {
                //                    task.InProgress();
                //                }

                //                while (true)
                //                {
                //                    //공격 결과 출력
                //                    UIManager.DisplayAttackResult(player.Stats.Name, target, (int)damage, beforeHp);

                //                    input = Console.ReadLine();
                //                    if (!int.TryParse(input, out int i) || (i != 0))
                //                    {
                //                        Console.WriteLine("\n잘못된 입력입니다.");
                //                        Console.ReadKey();
                //                        continue;
                //                    }
                //                    else if (i == 0)
                //                    {
                //                        isPlayerTurn = false;
                //                        break;
                //                    }
                //                }
                //                if (deathCount == monsters.Count) //몬스터 모두 처치
                //                {
                //                    foreach (var item in droppedItems)
                //                    {
                //                        player.Inven.AddItem(item); //인벤토리에 아이템 저장
                //                        player.Stats.Gold += rewardsGold; // 드롭된 골드를 플레이어의 골드에 추가
                //                    }
                //                    while (true)
                //                    {
                //                        UIManager.PrintPlayerVictory(player, deathCount, beforeLv, beforeExp, isLvUp, rewardsGold, droppedItems); //수정 윈화면 출력되다가 몬스터턴으로 넘어감
                //                        input = Console.ReadLine();
                //                        if (!int.TryParse(input, out int j) || (j != 0))
                //                        {
                //                            Console.WriteLine("\n잘못된 입력입니다.");
                //                            Console.ReadKey();
                //                            continue;
                //                        }
                //                        else if (j == 0) break; //0.취소 선택
                //                    }
                //                }
                //            }
                //            break;
                //        }
                //    }
                //}
                if (!isPlayerTurn)
                {
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        if (!monsters[i].Stats.IsDead)
                        {
                            //몬스터의 공격
                            int monsterDamage = Monster.GetDamageWithVariance(monsters[i].Stats.monsterATK);
                            int beforePlayerHP = player.Stats.HP;
                            player.Stats.HP -= monsterDamage;
                            if (player.Stats.HP <= 0) player.Stats.HP = 0;
                            while (true)
                            {
                                //전투 결과 출력
                                UIManager.PrintEnemyPhase(monsters[i], player, monsterDamage, beforePlayerHP);
                                input = Console.ReadLine();
                                if (!int.TryParse(input, out int j) || (j != 0))
                                {
                                    Console.WriteLine("\n잘못된 입력입니다.");
                                    Console.ReadKey();
                                    continue;
                                }
                                else if (j == 0) break; //0.취소 선택
                            }
                            if (player.Stats.HP == 0)
                            {
                                foreach (var item in droppedItems)
                                {
                                    player.Inven.AddItem(item);  //인벤토리에 아이템 저장
                                    player.Stats.Gold += rewardsGold; // 드롭된 골드를 플레이어의 골드에 추가
                                }
                                while (true)
                                {
                                    UIManager.PrintPlayerLose(player, rewardsGold, droppedItems);
                                    input = Console.ReadLine();
                                    if (!int.TryParse(input, out int j) || (j != 0))
                                    {
                                        Console.WriteLine("\n잘못된 입력입니다.");
                                        Console.ReadKey();
                                        continue;
                                    }
                                    else if (j == 0) //취소선택
                                    {
                                        i = monsters.Count; //나머지 몬스터의 공격X
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    isPlayerTurn = true;
                }
            }
            //2. 스킬사용 추가
            //3. 포션 사용
        }

    }
   
}

