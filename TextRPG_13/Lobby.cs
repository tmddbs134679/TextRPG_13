using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Lobby
    {
        public void GameLobby()
        {
            

            Battle Denjeon = new Battle();
      
            string input = Console.ReadLine();

            if (int.TryParse(input, out int inp))
            {
                if (inp >= 1 && inp <= 3)
                {
                    switch ((LOBBYCHOICE)inp)
                    {
                        case LOBBYCHOICE.PLYAYERSTAT:
                            GameManager.UI.PlayerStat();
                            break;
                        case LOBBYCHOICE.DENJEON:
                            Denjeon.BattleSequence();
                            break;
                        case LOBBYCHOICE.POTION:

                            break;
                    }
                }
                else
                {
                    Console.WriteLine("화면에 나와있는 번호중 하나를 선택해주세요.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
}
