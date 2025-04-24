using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class InvenViewer
    {
        private readonly Player _player;

        public static void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        public InvenViewer(Player player)
        {
            _player = player;
        }
        public void ShowInventory()
        {
            while (true)
            {
                UIManager.ShowInventory(_player);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    WriteColor("화면에 표기된 번호중 하나를 선택해주세요.", ConsoleColor.DarkYellow);
                    Console.ReadKey();
                    continue;
                }
                else if (input == "0")
                {
                    break; //로비화면
                }
                else if (input == "1")
                {
                    ShowEquipMenu();
                }
            }

        }

        private void ShowEquipMenu()
        {
            while (true)
            {
                UIManager.ShowEquipMenu(_player);

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice) || choice < 0 || choice > _player.Inven.Count)
                {
                    WriteColor("화면에 표기된 번호중 하나를 선택해주세요.", ConsoleColor.DarkYellow);
                    Console.ReadKey();
                    continue;
                }
                else if (input == "0")
                {
                    break; //로비화면
                }
                else
                {
                    int i = choice - 1;
                    var selectedStack = _player.Inven.GetItems()[i];
                    var selectedItem = selectedStack.Item;

                    //장착 가능한 아이템인지 확인
                    if (selectedItem.IsEquipable)
                    {
                        _player.Inven.EquipItem(selectedItem);
                        _player.Stats.UpdateStats(_player);
                    }
                }
            }
        }
    }
}
