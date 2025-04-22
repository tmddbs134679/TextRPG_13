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
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상태 보기\n");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n\n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Lv. ");
            Console.ResetColor();
            Console.WriteLine($"{}\n");
            Console.WriteLine($"{}  ( {} )\n" +
                              $"공격력 : {}\n" +
                              $"방어력 : {}\n" +
                              $"체 력 : {} \n" +
                              $"Gold : {} \n\n" +
                              $"0. 나가기\n\n" +
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