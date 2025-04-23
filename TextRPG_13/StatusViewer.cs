using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    internal class StatusViewer
    {
        public void Showstatus()
        {
            UIManager Ui = new UIManager();

            //int Bonusoff = _player.인벤토리아이템.Where(i => i.IsEquipped).Sum(i => i.); 

            // 인벤토리에서 장착한 아이템을 Bonusoff,Bonusdf 에 선언하여 각각 공격력,방어력에 합산하여 상태 표시 창에서 합산된 값 표시.

            //int Bonusdf = _player.인벤토리아이템.Where(i => i.IsEquipped).Sum(i => i.);


            // 인벤토리 추가 될 시 인벤토리에서 장착한 아이템을 Bonusdf에 장비에 따른 공격력 추가 및 기본 방어력에 합산,
            // 기본 공격력엔 Bonusdf가 합산된 전체값 표기
            // Bonus 에는 장착한 장비유형에 따른 값 표시 (방어력 : 6 일 경우 bonusoff 에는 그 장비의 방어력 6 추가) 

            int inp = int.Parse(Console.ReadLine());

            if (inp == 0)
            {
                Thread.Sleep(1000);
                Console.Clear();
                Ui.Gamelobby();
            }
        }
    }
}
