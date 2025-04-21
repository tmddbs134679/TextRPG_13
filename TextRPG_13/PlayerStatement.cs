using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    internal class PlayerStatement
    {
            public void PlayerStat()
    {
        Console.WriteLine("상태 보기\n");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n\n");
        Console.WriteLine($"Lv. {Level}\n" +
                          $"{Name}  ( {} )\n" +
                          $"공격력 : {}\n" +
                          $"방어력 : {}\n" +
                          $"체 력 : {} \n" +
                          $"Gold : {} \n\n" +
                          $"0. 나가기\n\n" +
                          $"원하시는 행동을 입력해주세요.\n" +
                          $">>"); int inp = int.Parse(Console.ReadLine());

        if (inp == 0)
        {
            Thread.Sleep(1000);
            Console.Clear();

        }
    }
}
