using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class PlayerStatement
    {
        // {}은 Player에서 따로 할당이 됐을경우 주입
        public void PlayerStat()
        {
            Console.Clear();

            int Bonusoff = Splayer.InventoryItems.Where(i => i.IsEquipped).Sum(i => i.);

            // 인벤토리에서 장착한 아이템을 Bonusoff,Bonusdf 에 선언하여 각각 공격력,방어력에 합산하여 상태 표시 창에서 합산된 값 표시.

            int Bonusdf = Splayer.InventoryItems.Where(i => i.IsEquipped).Sum(i => i.);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상태 보기\n");
            Console.ResetColor();

            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n\n");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Lv. ");
            Console.ResetColor();

            Console.WriteLine($"{}\n");
            Console.WriteLine($"{}  ( {} )\n");
            Console.Write($"공격력 : ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{} (+{Bonusoff})");
            // 인벤토리 추가 될 시 인벤토리에서 장착한 아이템을 Bonusoff에 장비에 따른 공격력 추가 및 기본 공격력에 합산,
            // 기본 공격력엔 Bonusoff가 합산된 전체값 표기
            // Bonus 에는 장착한 장비유형에 따른 값 표시 (공격력 : 6 일 경우 bonusoff 에는 그 장비의 공격력 6 추가)
            Console.ResetColor();

            Console.Write("방어력 : ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{} (+{BonusDf})");
            // 인벤토리 추가 될 시 인벤토리에서 장착한 아이템을 Bonusdf에 장비에 따른 공격력 추가 및 기본 방어력에 합산,
            // 기본 공격력엔 Bonusdf가 합산된 전체값 표기
            // Bonus 에는 장착한 장비유형에 따른 값 표시 (방어력 : 6 일 경우 bonusoff 에는 그 장비의 방어력 6 추가) 
            Console.ResetColor();

            Console.Write("체 력 : ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{}");
            Console.ResetColor();

            Console.Write("Gold : ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{}");
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
                Gamelobby();
            }
        }
    }