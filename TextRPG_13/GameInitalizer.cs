using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class GameInitalizer
    {
        public static void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void intro(string text) 
        {
            //텍스트(인트로) 추가
            Console.WriteLine(text);
        }

        public Player InitPlayer()
        {
            string name = AskPlayerName();
            JOBTYPE selectedJob = SelectJob();

            Player player = new Player(selectedJob);
            player.Stats.Name = name;
            GameManager.CurrentPlayer = player;

            ShowCharacterCreationAnimation();

            return player;
        }

        private string AskPlayerName()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("  $$$$$$\\                      $$$$$$$\\ $$$$$$$$\\              $$$$$$$\\   ");
                Console.WriteLine(" $$  __$$\\                     $$  __$$\\\\__$$  __|             $$  __$$\\          ");
                Console.WriteLine(" $$ /  \\__| $$$$$$\\   $$$$$$\\  $$ |  $$ |  $$ | $$$$$$\\        $$ |  $$ |$$\\   $$\\ $$$$$$$\\   $$$$$$\\   $$$$$$\\   $$$$$$\\  $$$$$$$\\  ");
                Console.WriteLine(" \\$$$$$$\\  $$  __$$\\  \\____$$\\ $$$$$$$  |  $$ | \\____$$\\       $$ |  $$ |$$ |  $$ |$$  __$$\\ $$  __$$\\ $$  __$$\\ $$  __$$\\ $$  __$$\\ ");
                Console.WriteLine(" \\____$$\\  $$ /  $$ | $$$$$$$ |$$ c __$$|  $$ | $$$$$$$ |      $$ |  $$ |$$ |  $$ |$$ |  $$ |$$ /  $$ |$$$$$$$$ |$$ /  $$ |$$ |  $$ | ");
                Console.WriteLine(" $$\\   $$ |$$ |  $$ |$$  __$$ |$$ |  $$ |  $$ |$$  __$$ |      $$ |  $$ |$$ |  $$ |$$ |  $$ |$$ |  $$ |$$   ____|$$ |  $$ |$$ |  $$ |");
                Console.WriteLine(" $$\\   $$ |$$ |  $$ |$$  __$$ |$$ |  $$ |  $$ |$$  __$$ |      $$ |  $$ |$$ |  $$ |$$ |  $$ |$$ |  $$ |$$   ____|$$ |  $$ |$$ |  $$ |");
                Console.WriteLine(" \\$$$$$$  |$$$$$$$  |\\$$$$$$$ |$$ |  $$ |  $$ |\\$$$$$$$ |      $$$$$$$  |\\$$$$$$  |$$ |  $$ |\\$$$$$$$ |\\$$$$$$$\\ \\$$$$$$  |$$ |  $$ |");
                Console.WriteLine(" \\$$$$$$  |$$$$$$$  |\\$$$$$$$ |$$ |  $$ |  $$ |\\$$$$$$$ |      $$$$$$$  |\\$$$$$$  |$$ |  $$ |\\$$$$$$$ |\\$$$$$$$\\ \\$$$$$$  |$$ |  $$ |");
                Console.WriteLine("  \\______/ $$  ____/  \\_______|\\__|  \\__|  \\__| \\_______|      \\_______/  \\______/ \\__|  \\__| \\____$$ | \\_______| \\______/ \\__|  \\__|");
                Console.WriteLine("           $$ |                                                                              $$\\   $$ |   ");
                Console.WriteLine("           $$ |                                                                              \\$$$$$$  |      ");
                Console.WriteLine("           \\__|                                                                               \\______/           ");
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n                                                아무키나 눌러주세요                                     ");
                Console.CursorVisible = false;
                Console.ReadKey();
                
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("원하시는 이름을 설정해주세요.\n");
                Console.Write("이름 : ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("\n다시 입력해주세요.");
                    Thread.Sleep(1000);
                    continue;
                }

                Console.WriteLine($"\n입력한 이름: '{name}'");
                Console.WriteLine("1. 저장");
                Console.WriteLine("2. 다시 입력");
                Console.Write(">> ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    return name;
                }
                else if (choice == "2")
                {
                    Console.WriteLine("\n이름 입력을 다시 시작합니다..");
                    Thread.Sleep(1000);
                    continue;
                }
                else
                {
                    Console.WriteLine("\n 잘못된 입력입니다. 다시 선택해주세요.");
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }

        private void ShowCharacterCreationAnimation()
        {
            Console.Write("\n캐릭터를 생성하는 중");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Thread.Sleep(500);

            // 메시지 지우기
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);

            Console.Clear();

            intro(Constants.IntroArt);
            Thread.Sleep(3500);

            Console.Write("로비로 이동 중");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Thread.Sleep(500);
        }

        private JOBTYPE SelectJob()
        {
            while (true)
            {
                Console.WriteLine("\n직업을 선택해주세요:");
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 마법사");
                Console.WriteLine("3. 도적");
                Console.Write(">> ");

                if (int.TryParse(Console.ReadLine(), out int input) &&
                    Enum.IsDefined(typeof(JOBTYPE), input))
                {
                    return (JOBTYPE)input;
                }

                WriteColor("화면에 나와있는 번호 중 하나를 선택해주세요.", ConsoleColor.DarkYellow);
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
       
    }
}
