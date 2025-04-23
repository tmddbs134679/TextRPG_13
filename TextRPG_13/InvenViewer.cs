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
                    Console.WriteLine("\n잘못된 입력입니다.");
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
                    Console.WriteLine("\n잘못된 입력입니다.");
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
                    //장착 가능한 아이템인지 확인
                    if (_player.Inven.GetItems()[i].IsEquipable)
                    {
                        _player.Inven.EquipItem(_player.Inven.GetItems()[i]);
                        _player.Stats.UpdateStats(_player);
                        break;
                    }
                }
            }

        }
    }
}
