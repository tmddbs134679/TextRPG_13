using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Lobby
    {

        private readonly Player _player;
        public Lobby(Player player)
        {
            _player = player;
        }

        public void GameLobby()
        {
            while (true)
            {
                UIManager.Gamelobby(_player);

                if (int.TryParse(Console.ReadLine(), out int inp))
                {
                    if (inp >= 1 && inp <= 3)
                    {
                        switch ((LOBBYCHOICE)inp)
                        {
                            case LOBBYCHOICE.PLYAYERSTAT:
                                new StatusViewer(_player).Showstatus();
                                break;
                            case LOBBYCHOICE.DENJEON:
                                new Battle().BattleSequence();
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
                    Console.WriteLine("화면에 나와있는 번호중 하나를 선택해주세요.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
        }
    }
}
