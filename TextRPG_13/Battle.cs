using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Battle
    {


        public static void BattleSequence()
        {
            Player player = GameManager.CurrentPlayer;

            bool isPlayerTurn = true;

            int deathCount = 0;

            Monster.MonsterRandomSpawn();
            List<Monster> monsters = Monster.CurrentWave.ToList();

            while ((player.Stats.HP > 0) && monsters.Any(m => !m.Stats.IsDead))
            {
                UIManager.BattleStart(player, monsters); //공격 선택지 페이지 출력
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
                            //몬스터 선택 페이지 출력
                            UIManager.DisplayMonsters(player, monsters);
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
                                int damage = GetDamageWithVariance(player.Stats.Offensivepower);
                                int beforeHp = target.Stats.monsterHP;
                                target.Stats.monsterHP -= damage;
                                if (target.Stats.monsterHP <= 0)
                                {
                                    target.Stats.monsterHP = 0;
                                    target.Stats.IsDead = true;
                                    deathCount++;
                                    if (deathCount == monsters.Count)
                                    {
                                        UIManager.PrintPlayerVictory(player, deathCount);
                                    }
                                }

                                while (true)
                                {
                                    //공격 결과 출력
                                    UIManager.DisplayAttackResult(player.Stats.Name, target, damage, beforeHp, target.Stats.monsterHP);

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
                                break;
                            }

                        }
                    }
                    if (!isPlayerTurn)
                    {
                        for (int i = 0; i < monsters.Count; i++)
                        {
                            if (!monsters[i].Stats.IsDead)
                            {
                                //몬스터의 공격
                                int monsterDamage = GetDamageWithVariance(monsters[i].Stats.monsterATK);
                                int beforePlayerHP = player.Stats.HP;
                                player.Stats.HP -= monsterDamage;

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

                            }
                        }
                        isPlayerTurn = true;
                    }

                    //레벨업
                }
                if (deathCount == monsters.Count)
                {
                    UIManager.PrintPlayerVictory(player, deathCount); //수정 윈화면 출력되다가 몬스터턴으로 넘어감
                }
                else if (player.Stats.HP <= 0)
                {
                    UIManager.PrintPlayerLose(player);
                    Thread.Sleep(1000);
                    break;
                }
            }
            //2. 스킬사용 추가
        }

        private static int GetDamageWithVariance(int baseAtk)
        {
            Random rand = new Random();
            double offset = Math.Ceiling(baseAtk * 0.1);
            int chance = rand.Next(1, 101);
            int finalDamage = baseAtk + rand.Next(-(int)offset, (int)offset);

            if (chance <= 15)
            {
                finalDamage = (int)(finalDamage * 1.6);
            }
            return finalDamage;
        }
    }
}
