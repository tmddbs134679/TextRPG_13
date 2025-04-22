using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static TextRPG_13.Enums;

namespace TextRPG_13
{
    public class Player
    {

        public PlayerStatement _Playerstat = new PlayerStatement();
        //PlayerStatement 에 있는 Player의 상태 정보를 _playet에 변수별로 가져옴
        public void PlayerStat()
        {
            Console.Clear();

            //int Bonusoff = _player.인벤토리아이템.Where(i => i.IsEquipped).Sum(i => i.); 

            // 인벤토리에서 장착한 아이템을 Bonusoff,Bonusdf 에 선언하여 각각 공격력,방어력에 합산하여 상태 표시 창에서 합산된 값 표시.

            //int Bonusdf = _player.인벤토리아이템.Where(i => i.IsEquipped).Sum(i => i.);


            //ForegroundColor = ConsoleColor. 각각의 텍스트에 구분되게 컬러를 입혀 유저분들이 텍스트를 더욱 가독성 있게 볼 수 있게 해줌.
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상태 보기\n");
            Console.ResetColor();

            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n\n");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Lv. ");
            Console.ResetColor();
            
            Console.WriteLine($"{_Playerstat.Level}");
            Console.WriteLine($"{_Playerstat.Name}  ( {_Playerstat.Job} )");
            Console.Write($"공격력 : ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{_Playerstat.Offensivepower}");

            // 인벤토리 추가 될 시 인벤토리에서 장착한 아이템을 Bonusoff에 장비에 따른 공격력 추가 및 기본 공격력에 합산,
            // 기본 공격력엔 Bonusoff가 합산된 전체값 표기
            // Bonus 에는 장착한 장비유형에 따른 값 표시 (공격력 : 6 일 경우 bonusoff 에는 그 장비의 공격력 6 추가)

            Console.ResetColor();

            Console.Write("방어력 : ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{_Playerstat.Defensivepower}");

            // 인벤토리 추가 될 시 인벤토리에서 장착한 아이템을 Bonusdf에 장비에 따른 공격력 추가 및 기본 방어력에 합산,
            // 기본 공격력엔 Bonusdf가 합산된 전체값 표기
            // Bonus 에는 장착한 장비유형에 따른 값 표시 (방어력 : 6 일 경우 bonusoff 에는 그 장비의 방어력 6 추가) 

            Console.ResetColor();

            Console.Write("체 력 : ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{_Playerstat.HP}");
            Console.ResetColor();

            Console.Write("Gold : ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{_Playerstat.Gold}\n");
            Console.ResetColor();

            Console.WriteLine("0. 나가기\n\n" +
                              $"원하시는 행동을 입력해주세요.");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(">> ");
            Console.ResetColor();

            int inp = int.Parse(Console.ReadLine());

            if (inp == 0)
            {
                Thread.Sleep(1000);
                Console.Clear();
                // 게임 로비 출력
            }
        }
    }
}
