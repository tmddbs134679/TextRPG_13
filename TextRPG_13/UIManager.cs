using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class UIManager
    {
        public void GameLobby()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("스파르타 마을에 오신 여러분, 환영합니다.\n" +
                                      "이제 전투를 시작할 수 있습니다.\n\n" +
                                      "1. 상태 보기\n" +
                                      "2. 전투 시작\n" +
                                      "3. 회복 아이템\n\n" +
                                      "원하시는 행동을 입력해주세요.\n" +
                                      ">> "); int inp2 = int.Parse(Console.ReadLine());

                        switch ((Lobbychoice)inp2)
                        {
                            case Lobbychoice.PlayerStat:
                                PlayerStat();
                                break;
                            case Lobbychoice.Denjeon:

                                break;
                            case Lobbychoice.Potion:

                                break;
                        } 
                }
                catch (Exception e)
                {

                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    GameLobby();
                }
            }
        }
    }
}
