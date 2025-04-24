using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class SaveView
    {
        
        public void Enter()
        {
            UIManager.AskSaveFile();

            string input = Console.ReadLine();

            if(input == "1")
            {
                SaveManager.Save(GameManager.CurrentPlayer);
                
            }
            else if(input == "2")
            {
                //데이터 초기화
                SaveManager.Reset();
                Console.Clear();
                Environment.Exit(0);
            }
            else if(input == "3")
            {
                Environment.Exit(0);
            }
            else
            {
                return;
            }


            return;
        }
    }
}
