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
        public static void BattleSequence(Player player)
        {
            bool isPlayerTurn = true;

            int deathCount = 0;

            Monster.MonsterRandomSpawn();
            List<Monster> monsters = Monster.CurrentWave.ToList();

            while (player.Stats.IsAlive && deathCount <= monsters.Count)
            {
                //var monsterInfo = Monster.CurrentWave[0];으로 랜덤 생성된 몬스터의 정보보기 가능
                //예) {monsterInfo.Stats.monsterHP} 첫번째에 저장된 몬스터의 체력 정보보기
                //몬스터 랜덤 생성
                Monster.MonsterRandomSpawn();
                monsters = Monster.CurrentWave.ToList();

                UIManager.BattleStart(player, monsters);
                string choice = Console.ReadLine();
                if (int.Parse(choice) == 1) //공격 선택지 선택
                {
                    if (isPlayerTurn == true)
                    {
                        //플레이어 공격 페이지 출력
                        UIManager.DisplayMonsters(player, monsters);
                        string input = Console.ReadLine();

                        if (!int.TryParse(input, out int index) || (index < 1 || index > monsters.Count))
                        {
                            Console.WriteLine("\n잘못된 입력입니다.");
                            Console.ReadLine();
                            continue;
                        }
                        else if (index == 0) break; //0.취소 선택

                        Monster target = monsters[index - 1];
                        if (target.Stats.IsDead) //몬스터 Dead 여부
                        {
                            Console.WriteLine("\n이미 죽은 몬스터입니다.");
                            continue;
                        }

                        int damage = GetDamageWithVariance(player.Stats.Offensivepower);
                        int beforeHp = target.Stats.monsterHP;
                        target.Stats.monsterHP -= damage;
                        if (target.Stats.monsterHP <= 0)
                        {
                            target.Stats.monsterHP = 0;
                            target.Stats.IsDead = true;
                        }

                        UIManager.DisplayAttackResult(player.Stats.Name, target, damage, beforeHp, target.Stats.monsterHP);
                        Console.ReadLine();
                        //break;
                    }
                    else if (!isPlayerTurn)
                    {
                        for (int i = 0; i < monsters.Count; i++)
                        {
                            if (!monsters[i].Stats.IsDead)
                            {
                                //몬스터의 공격
                                int monsterDamage = GetDamageWithVariance(monsters[i].Stats.monsterATK);
                                int beforePlayerHP = player.Stats.HP;
                                player.Stats.HP -= monsterDamage;

                                //전투 결과 출력
                                UIManager.PrintEnemyPhase(monsters[i], player, monsterDamage, beforePlayerHP);
                                string input = Console.ReadLine();

                                if(!player.Stats.IsAlive)
                                {
                                    UIManager.PrintPlayerLose();
                                    Thread.Sleep(1000);
                                    break;
                                }
                            }
                        }
                        isPlayerTurn = true;
                    }
                    UIManager.PrintPlayerVictory(monsters.Count);
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
