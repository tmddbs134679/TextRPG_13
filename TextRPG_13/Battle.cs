using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Battle
    {
        public static void BattleSequence(Player player, List<Monster> monsters)
        {
            while (true)
            {
                //몬스터 랜덤 생성
                Monster.MonsterRandomSpawn();

                UIManager.BattleStart(player, monsters);
                string choice = Console.ReadLine();
                if (int.Parse(choice) == 1) //공격 선택지 선택
                {
                        //플레이어 공격 페이지 출력
                        UIManager.DisplayMonsters(player, m);
                        string input = Console.ReadLine();

                        if (!int.TryParse(input, out int index) || (index < 1 || index > monsters.Count))
                        {
                            Console.WriteLine("\n잘못된 입력입니다.");
                            Console.ReadLine();
                            continue;
                        }
                        else if (index == 0) break; //0.취소 선택

                        Monster target = monsters[index - 1];
                        if (target.IsDead) //몬스터 Dead 여부
                        {
                            Console.WriteLine("\n이미 죽은 몬스터입니다.");
                            continue;
                        }

                        int damage = GetDamageWithVariance(player.Attack);
                        int beforeHp = target.Hp;
                        target.Hp -= damage;
                        if (target.Hp <= 0)
                        {
                            target.Hp = 0;
                            target.IsDead = true;
                        }

                        UIManager.DisplayAttackResult(player.Name, target, damage, beforeHp, target.Hp);
                        Console.ReadLine();
                        //break;

                    else if (isPlayerTurn == false)
                    {

                        for (int i = 0; i < monsters.Length; i++)
                        {
                            if (monsters[i].monsterStat.Health > 0)
                            {
                                //몬스터의 공격
                                int monsterDamage = GetDamageWithVariance(monsters[i].Attack);
                                int beforePlayerHP = player.Hp;
                                player.Hp -= monsterDamage;

                                //전투 결과 출력
                                UIManager.PrintEnemyPhase(monsters[i], player, monsterDamage, beforePlayerHP, player.Hp); //전투화면 출력

                            }
                        }
                    }
                    

                    
                    //레벨업

                } //2. 스킬사용 추가
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                    continue;
                }
            }
        }

        private static int GetDamageWithVariance(int baseAtk) //공격 오차
        {
            double offset = Math.Ceiling(baseAtk * 0.1);
            Random rand = new Random();
            return baseAtk + rand.Next(-(int)offset, (int)offset + 1);
        }

    }
}
