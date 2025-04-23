using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class StatusViewer
    {
        private readonly Player _player;
        public StatusViewer(Player player)
        {
            _player = player;
        }

        public void Showstatus()
        {
            while (true)
            {
                UIManager.PlayerStat(_player);
                //int Bonusoff = _player.인벤토리아이템.Where(i => i.IsEquipped).Sum(i => i.); 

                // 인벤토리에서 장착한 아이템을 Bonusoff,Bonusdf 에 선언하여 각각 공격력,방어력에 합산하여 상태 표시 창에서 합산된 값 표시.

                //int Bonusdf = _player.인벤토리아이템.Where(i => i.IsEquipped).Sum(i => i.);


                // 인벤토리 추가 될 시 인벤토리에서 장착한 아이템을 Bonusdf에 장비에 따른 공격력 추가 및 기본 방어력에 합산,
                // 기본 공격력엔 Bonusdf가 합산된 전체값 표기
                // Bonus 에는 장착한 장비유형에 따른 값 표시 (방어력 : 6 일 경우 bonusoff 에는 그 장비의 방어력 6 추가) 

                if (int.TryParse(Console.ReadLine(), out int choice) && choice == 0)
                {
                    Console.WriteLine("선택 화면으로 이동 합니다...");
                    Thread.Sleep(1000);
                    Console.Clear();

                    Lobby lobby = new Lobby(_player);
                    lobby.GameLobby(); 
                    return;
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
