using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class UIManager
    {
        public static void BattleStart(Player player, List<Monster> monsters)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                var monster = monsters[i];
                string status = monster.Stats.IsDead ? "Dead" : $"HP {monster.Stats.monsterHP}";
                Console.ForegroundColor = monster.Stats.IsDead ? ConsoleColor.DarkGray : ConsoleColor.White;
                //Console.WriteLine($"{i + 1} {monster.Stats.monsterName}  {status}");
                Console.WriteLine($"Lv.{monster.Stats.Lv} {monster.Stats.monsterName}  {status}");

            }
            Console.ResetColor();

            DisplayPlayerInfo(player);

            Console.WriteLine("\n1. 공격\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
        }

        public static void DisplayMonsters(Player player, List<Monster> monsters)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                var monster = monsters[i];
                string status = monster.Stats.IsDead ? "Dead" : $"HP {monster.Stats.monsterHP}";
                Console.ForegroundColor = monster.Stats.IsDead ? ConsoleColor.DarkGray : ConsoleColor.White;
                Console.WriteLine($"{i + 1} Lv.{monster.Stats.Lv} {monster.Stats.monsterName}  {status}");

            }
            Console.ResetColor();

            DisplayPlayerInfo(player);

            Console.WriteLine("\n0. 취소\n");
            Console.Write("대상을 선택해주세요.\n>> ");
        }
        public static void DisplayPlayerInfo(Player player)
        {
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{player.Stats.Level} {player.Stats.Name}");
            Console.WriteLine($"HP.{player.Stats.HP}/{player.Stats.Max_HP}");
        }

        public static void DisplayAttackResult(string attackerName, Monster target, int damage, int beforeHp)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            if (damage == 0)
            {
                Console.WriteLine($"{attackerName}의 공격!");
                Console.WriteLine($"{target.Stats.monsterName} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            }
            else
            {
                Console.WriteLine($"{attackerName}의 공격!");
                Console.WriteLine($"{target.Stats.monsterName} 을(를) 맞췄습니다. [데미지 : {damage}]");

                Console.WriteLine($"\n{target.Stats.monsterName}");
                if(target.Stats.IsDead)
                {
                    Console.WriteLine($"{beforeHp} -> Dead\n");
                }
                else
                {
                    Console.WriteLine($"{beforeHp} -> {target.Stats.monsterHP}");
                }
            }
            Console.Write("\n0. 다음\n>>");
        }
        
        public static void PrintEnemyPhase(Monster monster, Player player, int damage, int beforeHp) //머지 할때 
        {
            Console.Clear();

            WriteColor("Battle!!\n", ConsoleColor.DarkRed);
            Console.WriteLine($"Lv.{monster.Stats.Lv} {monster.Stats.monsterName}의 공격! ");
            if (damage == 0)
            {
                Console.WriteLine($"{player.Stats.Name}을(를) 공격했지만 아무일도 일어나지 않았습니다.\n");
            }
            else
            {
                Console.WriteLine($"{player.Stats.Name}을(를) 맞췄습니다. [데미지: {damage}]\n");
                Console.WriteLine($"HP {beforeHp} -> {player.Stats.HP}\n");
            }

            Console.WriteLine("\n0.다음");
            WriteColor(">>", ConsoleColor.DarkYellow);
        }

        public static void PrintPlayerLose(Player player, int gold, List<Item> items) 
        {
            Console.Clear();
            WriteColor("You Lose\n", ConsoleColor.Red);

            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{player.Stats.Level} {player.Stats.Name}");
            Console.WriteLine($"HP{player.Stats.Max_HP} -> {player.Stats.HP}");

            DisplayRewards(gold, items);

            Console.WriteLine("\n0.다음");
            WriteColor(">>", ConsoleColor.DarkYellow);
        }

        public static void PrintPlayerVictory(Player player, int maxMonster,int beforerLv,int beforeExp,bool isLvUp, int gold, List<Item> items)
        {
            Console.Clear();
            WriteColor("Vicoty\n", ConsoleColor.DarkGreen);
            Console.ResetColor();

            Console.WriteLine($"던전에서 몬스터 {maxMonster}마리를 잡았습니다.\n");

            Console.WriteLine("[캐릭터 정보]");
            Console.Write($"Lv.{beforerLv} {player.Stats.Name}");
            if (isLvUp == true) Console.WriteLine($" -> Lv.{player.Stats.Level} {player.Stats.Name}");
            Console.WriteLine($"exp {beforeExp} -> {player.Stats.Exp}");
            Console.WriteLine($"HP{player.Stats.Max_HP} -> {player.Stats.HP}");

            DisplayRewards(gold, items);

            Console.WriteLine("\n0.다음");
            WriteColor(">>", ConsoleColor.DarkYellow);
        }
        public static void DisplayRewards(int gold, List<Item> items)
        {
            Console.WriteLine("\n[획득아이템]");
            Console.WriteLine($"{gold}G");
            if (items.Count == 0)
            {
                Console.WriteLine("드롭된 아이템이 없습니다.");
                return;
            }

            var groupedItems = items
                .GroupBy(item => item.Name)
                .Select(group => new
                {
                    Name = group.Key,
                    Count = group.Count(),
                });

            foreach (var g in groupedItems)
            {
                Console.WriteLine($"{g.Name} x{g.Count}");
            }
        }

        public static void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void ItemList(Player player)
        {
            if (player.Inven.Count == 0)
            {
                Console.WriteLine("인벤토리에 아이템이 없습니다.");
            }
            else
            {
                int idx = 1;

                foreach (var stack in player.Inven.GetItems())
                {
                    var item = stack.Item;
                    var quantity = stack.Quantity;
                    string statText = item.ATKbonus > 0 ? $"공격력 +{item.ATKbonus}" :
                                        item.DEFbonus > 0 ? $"방어력 +{item.DEFbonus}" :
                                        item.HealAmount > 0 ? $"회복량 +{item.HealAmount}" : "-";
                    string equipMark = item.IsEquipped ? " [E]" : "";  // 장착여부 표기
                    Console.WriteLine($"- {idx++} {equipMark} {item.Name} [x{quantity}] | {statText} | {item.Description}");
                }
            }
        }
        public static void ShowInventory(Player player)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");

            ItemList(player);

            Console.WriteLine("\n1. 장착관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }

        public static void ShowEquipMenu(Player player)
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");

            ItemList(player);

            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("\n장착/해제할 대상을 입력해주세요.\n>>");
        }


        public static void Gamelobby(Player player)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분, 환영합니다.\n" +
                              "이제 전투를 시작할 수 있습니다.\n");

            WriteColor("1. ", ConsoleColor.DarkYellow);
            Console.WriteLine("상태 보기");

            WriteColor("2. ", ConsoleColor.DarkYellow);
            Console.WriteLine($"던전 입장");

            WriteColor("3. ", ConsoleColor.DarkYellow);
            Console.WriteLine("회복 아이템");

            WriteColor("4. ", ConsoleColor.DarkYellow);
            Console.WriteLine("인벤토리");

            WriteColor("5. ", ConsoleColor.DarkYellow);
            Console.WriteLine("퀘스트");

            WriteColor("0. ", ConsoleColor.DarkYellow);
            Console.WriteLine("설정 창\n\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.\n");
            WriteColor(">> ", ConsoleColor.DarkGreen);

        }

        public static void Deonjoenlobby(Player player)
        {
            Console.Clear();
            WriteColor("던전\n",ConsoleColor.DarkYellow);
            Console.WriteLine("전투 시작 전, 만반의 준비를 마친 뒤 시작해 주세요.\n");

            WriteColor("1. ", ConsoleColor.DarkYellow);
            Console.WriteLine($"전투 시작 (현재 스테이지: {GameManager.Stage.CurrentStage})");

            WriteColor("2. ", ConsoleColor.DarkYellow);
            Console.WriteLine("인벤토리");

            WriteColor("0. ", ConsoleColor.DarkYellow);
            Console.WriteLine("나가기\n\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.\n");
            WriteColor(">> ", ConsoleColor.DarkGreen);
        }

        public static void PlayerStat(Player player)
        {
            // 플레이어 초기 스탯 불러오기
            var stat = player.Stats;

            string atkText = stat.bonusATK > 0 ? $"{stat.Offensivepower} (+{stat.bonusATK})" : $"{stat.baseATK}";
            string defText = stat.bonusDEF > 0 ? $"{stat.Defensivepower} (+{stat.bonusDEF})" : $"{stat.baseDEF}";

            Console.Clear();

            WriteColor("상태 보기\n", ConsoleColor.DarkYellow);
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n\n");

            WriteColor("Lv. ", ConsoleColor.DarkGray);

            Console.WriteLine($"{stat.Level}\n");
            Console.WriteLine($"{stat.Name}  ( {stat.Job} )\n");

            Console.Write($"공격력 : ");
            WriteColor($"{atkText}\n", ConsoleColor.DarkGray);

            Console.Write("방어력 : ");
            WriteColor($"{defText}\n", ConsoleColor.DarkGray);

            Console.Write("체 력 : ");
            WriteColor($"{stat.HP}\n", ConsoleColor.DarkGray);

            Console.Write("Gold : ");
            WriteColor($"{stat.Gold}\n", ConsoleColor.DarkGray);

            Console.WriteLine("0. 나가기\n\n" +
                              $"원하시는 행동을 입력해주세요.");

            WriteColor(">> ", ConsoleColor.DarkGreen);
        }

        public static void PlayerRecovery(Player player)
        {
            var stat = player.Stats;
            Console.Clear();
            WriteColor("회복\n", ConsoleColor.DarkYellow);
            Console.WriteLine("포션을 사용하면 체력을 회복할 수 있습니다.");

            PotionList(player);

            WriteColor("\n1", ConsoleColor.Red);
            Console.WriteLine(". 사용하기"); 
            WriteColor("0", ConsoleColor.Red);
            Console.WriteLine(". 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            WriteColor(">> ", ConsoleColor.DarkGreen);
        }

        public static void SelectPotion(Player player)
        {
            Console.Clear();
            WriteColor("회복\n", ConsoleColor.DarkYellow);
            Console.WriteLine("포션을 사용하면 체력을 회복할 수 있습니다.");

            PotionList(player);

            Console.WriteLine("\n1. 소형 포션");
            Console.WriteLine("2. 중형 포션");
            Console.WriteLine("0. 나가기\n");

            Console.WriteLine("사용할 포션을 입력해주세요.");
            WriteColor(">> ", ConsoleColor.DarkGreen);
        }

        public static void PotionList(Player player)
        {
            var s_potionCount = player.Inven.GetItems()
                .FirstOrDefault(stack => stack.Item.Id == 100)?.Quantity ?? 0;
            var m_potionCount = player.Inven.GetItems()
                .FirstOrDefault(stack => stack.Item.Id == 101)?.Quantity ?? 0;

            if (s_potionCount > 0)
            {
                Console.Write($"\n소형 포션\n- 체력을 30 회복 할 수 있습니다. (남은 개수 : ");
                WriteColor($"{s_potionCount})\n", ConsoleColor.Red);
            }
            if (m_potionCount > 0)
            {
                Console.Write($"\n중형 포션\n- 체력을 50 회복 할 수 있습니다. (남은 개수 : ");
                WriteColor($"{m_potionCount})\n", ConsoleColor.Red);
            }
            if((s_potionCount == 0) && (m_potionCount == 0))
            {
                Console.WriteLine("포션이 없습니다.\n");
            }
        }

        public static void QuestUI()
        {
            Console.Clear();
            Console.WriteLine(" Quest!! ");
            Console.WriteLine("1. 마을을 위협하는 미니언 처치");
            Console.WriteLine("2. 장비를 장착해보자");
            Console.WriteLine("\n\n");

            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
            Console.WriteLine(">>");
        }

        public static void Quest_1()
        {
            Console.Clear();
            Console.WriteLine(" Quest!! \n");
            Console.WriteLine("마을을 위협하는 미니언 처치\n");
            Console.WriteLine("이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\r\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n모험가인 자네가 좀 처치해주게!");

        }

        public static void Quest_2()
        {
            Console.Clear();
            Console.WriteLine(" Quest!! ");
            Console.WriteLine("1. 마을을 위협하는 미니언 처치");
            Console.WriteLine("2. 장비를 장착해보자");
            Console.WriteLine("\n\n");

            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
            Console.WriteLine(">>");

        }

        public static void AskToAcceptQuest()
        {
            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.WriteLine(">>");
        }

        public static void AskRewardQuest()
        {
            Console.WriteLine("");
            Console.WriteLine("1. 보상받기");
            Console.WriteLine("2. 돌아가기");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.WriteLine(">>");
        }

        public static void AskSaveFile()
        {
            Console.WriteLine("");
            Console.WriteLine("1. 저장하기");
            Console.WriteLine("2. 삭제하기");
            Console.WriteLine("3. 게임종료");
            Console.WriteLine("0. 돌아가기");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.WriteLine(">>");
        }

    }

}
