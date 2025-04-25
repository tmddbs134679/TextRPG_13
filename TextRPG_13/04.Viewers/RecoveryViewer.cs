using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class RecoveryViewer
    {
        private readonly Player _player;
        public RecoveryViewer(Player player)
        {
            _player = player;
        }

        public static void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        public void Recovery()
        {
            while (true)
            {
                var stat = _player.Stats;
                // 포션 보유 여부 확인
                var potionList = _player.Inven.GetItems();
                bool hasAnyPotion = potionList.Any(i => (i.Item.Id == 100 || i.Item.Id == 101 || i.Item.Id == 103) && i.Quantity > 0);

                if (!hasAnyPotion)
                {
                    Console.WriteLine("\n사용할 포션이 없습니다.");
                    Thread.Sleep(1000);
                    continue;
                }
                // 🔸 동적으로 포션만 출력 & 선택 번호 매핑
                Dictionary<int, int> optionMap = UIManager.SelectPotion(_player);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int selected))
                {
                    if (selected == 0)
                    {
                        Console.WriteLine("로비로 이동 중");
                        for (int i = 0; i < 3; i++)
                        {
                            Thread.Sleep(500);
                            Console.Write(".");
                        }
                        Thread.Sleep(500);
                        new Lobby(_player).GameLobby();
                        break;
                    }

                    if (!optionMap.ContainsKey(selected))
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        continue;
                    }

                    int itemId = optionMap[selected];
                    var itemStack = potionList.FirstOrDefault(i => i.Item.Id == itemId);

                    if (itemId == 100) // 소형 포션
                    {
                        if (stat.HP == stat.Max_HP)
                        {
                            Console.WriteLine("이미 최대치입니다. 회복할 수 없습니다.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            itemStack.Add(-1);
                            stat.HP = Math.Min(stat.HP + 30, stat.Max_HP);
                            Console.WriteLine("소형 포션 사용! 체력 30 회복");
                            Thread.Sleep(1000);
                        }
                    }
                    else if (itemId == 101) // 중형 포션
                    {
                        if (stat.HP == stat.Max_HP)
                        {
                            Console.WriteLine("이미 최대치입니다. 회복할 수 없습니다.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            itemStack.Add(-1);
                            stat.HP = Math.Min(stat.HP + 50, stat.Max_HP);
                            Console.WriteLine("중형 포션 사용! 체력 50 회복");
                            Thread.Sleep(1000);
                        }
                    }
                    else if (itemId == 103) // 마나 포션
                    {
                        if (stat.MP == stat.Max_MP)
                        {
                            Console.WriteLine("이미 최대치입니다. 회복할 수 없습니다.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            itemStack.Add(-1);
                            stat.MP = Math.Min(stat.MP + 30, stat.Max_MP);
                            Console.WriteLine("마나 포션 사용! 마나 30 회복");
                            Thread.Sleep(1000);
                        }    
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                }
            }
        }

        public void RecoveryInBattle()
        {
            var stat = _player.Stats;
            var optionMap = UIManager.SelectPotion(_player);

            string input = Console.ReadLine();
            if (int.TryParse(input, out int selected) && optionMap.ContainsKey(selected))
            {
                int itemId = optionMap[selected];
                var itemStack = _player.Inven.GetItems().FirstOrDefault(i => i.Item.Id == itemId);

                if (itemId == 100) // 소형 포션
                {
                    if (stat.HP == stat.Max_HP)
                    {
                        Console.WriteLine("이미 최대치입니다. 회복할 수 없습니다.");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        itemStack.Add(-1);
                        stat.HP = Math.Min(stat.HP + 30, stat.Max_HP);
                        Console.WriteLine("소형 포션 사용! 체력 30 회복");
                        Thread.Sleep(1000);
                    }
                }
                else if (itemId == 101) // 중형 포션
                {
                    if (stat.HP == stat.Max_HP)
                    {
                        Console.WriteLine("이미 최대치입니다. 회복할 수 없습니다.");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        itemStack.Add(-1);
                        stat.HP = Math.Min(stat.HP + 50, stat.Max_HP);
                        Console.WriteLine("중형 포션 사용! 체력 50 회복");
                        Thread.Sleep(1000);
                    }
                }
                else if (itemId == 103) // 마나 포션
                {
                    if (stat.MP == stat.Max_MP)
                    {
                        Console.WriteLine("이미 최대치입니다. 회복할 수 없습니다.");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        itemStack.Add(-1);
                        stat.MP = Math.Min(stat.MP + 30, stat.Max_MP);
                        Console.WriteLine("마나 포션 사용! 마나 30 회복");
                        Thread.Sleep(1000);
                    }
                }
            }
            else if (selected == 0)
            {
                Console.WriteLine("회복을 취소합니다.");
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1000);
            }
        }
    }
}
