using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        
        Random random = new Random();

      
        
        public void BattleSequence(UIManager uiManager)//Player,Monster 매개변수 추가해야함, 
        {
            int input = 0;
            int deathCount = 0;
            int MaxMonster = random.Next(1,4);

            Monster[] monsters = new Monster[MaxMonster];

            var types = Enum.GetValues(typeof(Enums.Monsters));

            //임시 몬스터 생성
            for (int i = 0; i < MaxMonster;i++) 
            {
                Enums.Monsters type = ((Enums.Monsters)types.GetValue(i));
                monsters[i] = new Monster(type);
            }

            while (playerHP > 0 && deathCount < monsters.Length )
            {
                // 플레이어턴 
                if (isPlayerTurn == true)
                {
                    //몬스터 죽이는 임시 로직 (나중에 지워야함)
                    Console.Clear();
                    Console.WriteLine("어떤 몬스터를 죽이시겠습니까?");
                    for (int i = 0; i < monsters.Length; i++)
                    {
                        Console.WriteLine($"{i+1}. {monsters[i].monsterStat.Type} 체력:{monsters[i].monsterStat.Health}");
                    }

                    bool isInt = int.TryParse(Console.ReadLine(),out input);

                    if (isInt && input <= MaxMonster && input > 0)
                    {
                        monsters[input - 1].monsterStat.Health -= playerAttack;
                        if (monsters[input - 1].monsterStat.Health <= 0)
                        {
                            deathCount++;
                        }
                        isPlayerTurn = false;
                    }
                    
                   
                }
                // 몬스터 턴
                else if(isPlayerTurn == false)
                {

                    for (int i = 0; i < monsters.Length; i++)
                    {
                        if (monsters[i].monsterStat.Health > 0)
                        {
                            int baseATK = monsters[i].monsterStat.Attack;
                            int damageScope = + (int)(Math.Round(baseATK * 0.1,MidpointRounding.AwayFromZero)); //반올림 함수 사용
                            int randomDamage = baseATK + (random.Next(-damageScope, damageScope+1));

                            playerHP -= randomDamage;

                            uiManager.PrintEnemyPhase(monsters[i], randomDamage); //전투화면 출력
                            input = int.Parse(Console.ReadLine()); //다음으로 넘어가기 임시기능

                            if(playerHP <= 0)
                            {
                                //패배 화면 출력
                                uiManager.PrintPlayerLose();
                                Thread.Sleep(1000);
                                break;
                            }
                        }
                    }
                    isPlayerTurn=true; //다시 플레이어 턴
                }
                    
            }
            uiManager.PrintPlayerVitory(monsters.Length);

        }

    }


}
