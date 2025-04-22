using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class UIManager
    {
        private readonly Player _player;

        //내부 생성자 추가
        public UIManager(Player player) 
        {
            _player = player;
        }
        public void Gamelobby()
        {
            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분, 환영합니다.\n" +
                                  "이제 전투를 시작할 수 있습니다.\n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("1. ");
                Console.ResetColor();

                Console.WriteLine("상태 보기");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("2. ");
                Console.ResetColor();

                Console.WriteLine("전투 시작");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("3. ");
                Console.ResetColor();

                Console.WriteLine("회복 아이템\n\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.\n");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(">> ");
                Console.ResetColor();

                Battle Denjeon = new Battle();
                string input = Console.ReadLine();

                if (int.TryParse(input, out int inp))
                {
                    if (inp >= 1 && inp <= 3)
                    {
                        switch ((LOBBYCHOICE)inp)
                        {
                            case LOBBYCHOICE.PLYAYERSTAT:
                                PlayerStat();
                                break;
                            case LOBBYCHOICE.DENJEON:
                                Denjeon.BattleSequence();
                                break;
                            case LOBBYCHOICE.POTION:

                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("화면에 나와있는 번호중 하나를 선택해주세요.");
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
        }

        //몬스터 순서 랜덤 나열
        public void PrintRandomMonster(Monster monster)
        {
            Console.WriteLine($"Lv.{monster.Stats.Lv} " +
                $"{monster.Stats.monsterName} " +
                $"HP {monster.Stats.monsterHP}");
        }

        public void PlayerStat()
        {
            PlayerStatement _Playerstat = GameManager.CurrentPlayer.Stats;
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
                Gamelobby();
            }
        }
        

        public static void PrintEnemyPhase(Monster monster, int randomDamage) //플레이어 매개변수는 Player.cs 미구현으로 임시변수로 사용
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();


            Console.WriteLine($"Lv.{monster.Stats.Lv} {monster.Stats.monsterName}의 공격! ");
            Console.WriteLine($"을(를) 맞췄습니다. [데미지: {randomDamage}]\n");
            Console.WriteLine("HP {player} -> {player.health}\n");
            Console.WriteLine("\n0.다음");
            Console.Write(">>", Color.DarkOrange);
        }

        public static void PrintPlayerLose() //플레이어 매개변수는 플레이어 클래스 미구현으로 임시변수로 사용
        {
            Console.Clear();
            Console.WriteLine("You Lose\n", Color.Red);
            Console.ResetColor();

            Console.WriteLine("Lv.{player.level} {player.name}");
            Console.WriteLine("HP{player.maxHP} -> {player.HP}\n");
            Console.WriteLine("\n0.다음");
            Console.Write(">>", Color.DarkOrange);
        }

        public static void PrintPlayerVitory(int maxMonster) //플레이어 클래스 필요
        {
            Console.Clear();
            Console.WriteLine("Vicoty\n", Color.DarkOliveGreen);
            Console.ResetColor();

            Console.WriteLine($"던전에서 몬스터 {maxMonster}마리를 잡았습니다.");
            Console.WriteLine("Lv.{player.level} {player.name}");
            Console.WriteLine("HP{player.maxHP} -> {player.HP}");
            Console.WriteLine("\n0.다음");
            Console.Write(">>", Color.DarkOrange);
        }
    }
}
