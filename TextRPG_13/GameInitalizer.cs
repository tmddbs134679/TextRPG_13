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
            Console.SetWindowSize(100, 40);

            int WindowsWidth = Console.WindowWidth;
            int WindowsHeight = Console.WindowHeight;

            //텍스트(인트로) 추가
            Console.WriteLine(text);
        }
        public Player InitPlayer()
        {

            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.\n");
            Console.Write("이름 : ");
            string name = Console.ReadLine();
            JOBTYPE selectedJob = SelectJob();

            // 선택한 직업으로 Player 생성
            Player player = new Player(selectedJob);
            //이름 저장
            player.Stats.Name = name;

            
            GameManager.CurrentPlayer = player;

            Console.Write("\n캐릭터를 생성하는 중");
            for (int i = 0; i < 1; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Thread.Sleep(500);

            // 메시지 지우기
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop); // 원래 위치로 커서 이동

            Console.Clear();
            intro("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$=*$$$*-$$$$$$$$$$$*-$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$=$$$-$$$$$$$$$$*!.~$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$*,~:$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$~$$$$;~=:$$$$$$*,~$$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$.=*$$~!: :$$$#~.-=$$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$$$#$#~$$$$$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$=-#$##*#$$$$$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$**=##=#$$$$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$:,;$#=.$$$$$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$#=.-*##- !#$$$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$#$--*$#!!*#$$$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$:$$;~###-$-;#$$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$-$#* ;=#=$. !#$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$;$$* .=#$  :~!$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$!;$#:,   .=$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$$###*;~,  =$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$$####;!;  =$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$$####;;, :$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$$$##$,.  !$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$$$##$, ~$-=$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$$$##== ~~.=$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$$$#$#$! ;$$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$$$#$$$*.;$$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$$=$$$$$:;$$$=$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$!-*$$$$;,!$$$$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$!::-=$#=*;-~::*$$$$$$$$$$$$$$$$$$$\r\n$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$\r\n");
            Thread.Sleep(5000);

            Console.Write("로비로 이동 중");
                for (int i = 0; i < 1; i++)
                {
                    Thread.Sleep(500);
                    Console.Write(".");
                }
                Thread.Sleep(500);

                // 메시지 지우기
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop); // 원래 위치로 커서 이동

            Console.Clear();
            //new Lobby(player).GameLobby();
            return player;
        }

        private JOBTYPE SelectJob()
        {
            while (true)
            {
                Console.WriteLine("\n직업을 선택해주세요:");
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 위자드");
                Console.WriteLine("3. 어쌔신");
                Console.Write(">> ");

                if (int.TryParse(Console.ReadLine(), out int input) &&
                    Enum.IsDefined(typeof(JOBTYPE), input))
                {
                    return (JOBTYPE)input;
                }

                WriteColor("화면에 나와있는 번호중 하나를 선택해주세요.", ConsoleColor.DarkYellow);
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
}
