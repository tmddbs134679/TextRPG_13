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
        int input = 0;

        Random random = new Random();

      
        
        public void BattleSequence()
        {
            int MaxMonster = random.Next(1,4);
            Monster[] monsters = new Monster[MaxMonster];

            var types = Enum.GetValues(typeof(Enums.Monsters));

            for (int i = 0; i < MaxMonster;i++)
            {
                Enums.Monsters type = ((Enums.Monsters)types.GetValue(i));
                monsters[i] = new Monster(type);

                Console.WriteLine($"{type} 생성됨 - 체력: {monsters[i].monsterStat.Health}, 공격력: {monsters[i].monsterStat.Attack}");
            }

            while (playerHP > 0 )
            {
                // 플레이어턴 
                if (isPlayerTurn == true)
                {
                    //몬스터 죽이는 로직
                    
                    isPlayerTurn = false;
                }
                else if(isPlayerTurn == false)
                {

                    for (int i = 0; i < monsters.Length; i++)
                    {
                        if (monsters[i].monsterStat.Health > 0)
                        {
                            int baseATK = monsters[i].monsterStat.Attack;
                            int damageScope = + (int)(Math.Round(baseATK * 0.1,MidpointRounding.AwayFromZero)); //반올림 함수 사용
                            int randomATK = baseATK + (random.Next(-damageScope, damageScope+1));

                            playerHP -= randomATK;

                            Console.WriteLine($"Lv.{monsters[i].monsterStat.Level} {monsters[i].monsterStat.Type}의 공격!\r\n");
                            Console.WriteLine($"{playerName}을(를) 맞췄습니다. [데미지: {randomATK}]\r\n");

                            if(playerHP <= 0)
                            {
                                //패배 화면 출력 되게
                                Console.Clear();
                                Console.WriteLine("you die");
                                break;
                            }
                        }
                    }
                    isPlayerTurn=true;
                }
                    
            }

        }

    }


}
