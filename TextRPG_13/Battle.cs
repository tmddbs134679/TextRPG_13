using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Battle
    {
        //임시 플레이어 StateMent
        string playerName = "플레이어";
        int playerHP = 100;
        int playerAttack = 10;
        int playerDefend = 5;

        bool isPlayerTurn = true; //턴 넘겨주기
        
      
        
        public void BattleSequence()
        {
            int input = 0;
            int deathCount = 0;
            Random random = new Random();

            Monster[] monsters = new Monster[random.Next(1,5)]; //최대 4마리 생성

            // 몬스터 생성
            for(int i = 0; i < monsters.Length;i++)
            {
                monsters[i] = Monster.CreateRandom();
                Console.WriteLine(monsters[i].Stats.monsterName);
            }


            while (playerHP > 0 && deathCount <= monsters.Length )
            {
                // 플레이어턴 
                if (isPlayerTurn == true)
                {
                    //몬스터 죽이는 임시 로직 (나중에 지워야함)
                    Console.Clear();
                    Console.WriteLine("어떤 몬스터를 죽이시겠습니까?");
                    for (int i = 0; i < monsters.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {monsters[i].Stats.monsterName} 체력:{monsters[i].Stats.monsterHP}");
                    }

                    bool isint = int.TryParse(Console.ReadLine(), out input);

                    if (isint && input <= monsters.Length + 1 && input > 0)
                    {
                        monsters[input - 1].Stats.monsterHP -= playerAttack;
                        if (monsters[input - 1].Stats.monsterHP <= 0)
                        {
                            deathCount++;
                        }
                        isPlayerTurn = false;
                    }


                }
                // 몬스터 턴
                else if (isPlayerTurn == false)
                {

                    for (int i = 0; i < monsters.Length; i++)
                    {
                        if (monsters[i].Stats.monsterHP > 0)
                        {
                            int baseATK = monsters[i].Stats.monsterATK;
                            int damageScope = +(int)(Math.Round(baseATK * 0.1,MidpointRounding.AwayFromZero)); //반올림 함수 사용
                            int randomdamage = baseATK + (random.Next(-damageScope, damageScope + 1));

                            playerHP -= randomdamage;

                            UIManager.PrintEnemyPhase(monsters[i], randomdamage); //전투화면 출력
                            input = int.Parse(Console.ReadLine()); //다음으로 넘어가기 임시기능

                            if (playerHP <= 0)
                            {
                                //패배 화면 출력
                                UIManager.PrintPlayerLose();
                                Thread.Sleep(1000);
                                break;
                            }
                        }
                    }
                    isPlayerTurn = true; //다시 플레이어 턴
                }

            }
                UIManager.PrintPlayerVitory(monsters.Length);

        }

    }


}
