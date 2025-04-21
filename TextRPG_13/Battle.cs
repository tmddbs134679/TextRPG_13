using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Battle
    {
        string playerName = "플레이어";
        int playerHP = 100;
        int playerAttack = 10;
        int playerDefend = 5;

        int randomATK = 0;
        int input = 0;

        Random random = new Random();

        bool isPlayerTurn = true;
        
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
                //대충 플레이어턴 매서드
                if (isPlayerTurn == true)
                {
                    //몬스터 죽이는 메서드
                    
                    isPlayerTurn = false;
                }
                else if(isPlayerTurn == false)
                {

                    for (int i = 0; i < monsters.Length; i++)
                    {
                        if (monsters[i].monsterStat.Health > 0)
                        {
                            int attackStat = monsters[i].monsterStat.Attack;
                            int damageScope = + (int)(Math.Round(attackStat * 0.1,MidpointRounding.AwayFromZero));
                            randomATK = attackStat + (random.Next(-damageScope, damageScope+1));

                            playerHP -= randomATK;

                            Console.WriteLine($"Lv.{monsters[i].monsterStat.Level} {monsters[i].monsterStat.Type}의 공격!\r\n");
                            Console.WriteLine($"{playerName}을(를) 맞췄습니다. [데미지: {randomATK}]\r\n");

                            if(playerHP <= 0)
                            {
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
