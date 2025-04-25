using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class UIManager
    {
        public static void BattleStart(Player player, List<Monster> monsters)
        {
            DisplayMonstersAndPlayer(player,monsters);
            DisplayPlayerInfo(player);
            Console.WriteLine("\n1. 공격\n2. 스킬\n3. 포션\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
        }

        public static void DisplayPlayerInfo(Player player)
        {
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{player.Stats.Level} {player.Stats.Name}");
            Console.WriteLine($"HP.{player.Stats.HP}/{player.Stats.Max_HP}");
            Console.WriteLine($"MP.{player.Stats.MP}/{player.Stats.Max_MP}");
        }

        public static void DisplayMonstersAndPlayer(Player player, List<Monster> monsters)
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
        }

        public static void ChooseMonster(Player player, List<Monster> monsters)
        {
            DisplayMonstersAndPlayer(player, monsters);
            DisplayPlayerInfo(player);
            Console.Write("\n대상을 선택해주세요.\n>>");
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
        public static void PrintSkills(Player player)
        {
                for (int i = 0; i < player.Skills.Count;i++)
                {
                    Console.WriteLine($"\n{i+1}. {player.Skills[i].Name} - MP:{player.Skills[i].Mpcost}\n" +
                        $"{player.Skills[i].Description}");
                }
                Console.WriteLine("\n0. 취소\n");
                Console.Write("스킬을 선택해주세요\n>>");
        }

        public static void PrintEnemyPhase(Monster monster, Player player, int damage, int beforeHp) 
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
                Console.WriteLine($"{player.Stats.Name}을(를) 맞췄습니다.[{(int)(Math.Ceiling(player.Stats.baseDEF/2))}의 데미지 감소!]" +
                    $" [데미지: {damage}]\n");
                Console.WriteLine($"HP {beforeHp} -> {player.Stats.HP}\n");
            }

            Console.WriteLine("\n0.다음");
            WriteColor(">>", ConsoleColor.DarkYellow);
        }

        public static void PrintPlayerLose(Player player,int beforeHP, int beforeMP, int gold, List<Item> items) 
        {
            Console.Clear();
            WriteColor("You Lose\n", ConsoleColor.Red);

            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{player.Stats.Level} {player.Stats.Name}");
            Console.WriteLine($"HP{beforeHP} -> {player.Stats.HP}");
            Console.WriteLine($"HP{beforeMP} -> {player.Stats.MP}");

            DisplayRewards(gold, items);

            Console.WriteLine("\n0.다음");
            WriteColor(">>", ConsoleColor.DarkYellow);
        }

        public static void PrintPlayerVictory(Player player, int maxMonster,int beforerLv,int beforeExp, int beforeHP, int beforeMP, bool isLvUp, int gold, List<Item> items)
        {
            Console.Clear();
            WriteColor("Vicoty\n", ConsoleColor.DarkGreen);
            Console.ResetColor();

            Console.WriteLine($"던전에서 몬스터 {maxMonster}마리를 잡았습니다.\n");

            Console.WriteLine("[캐릭터 정보]");
            Console.Write($"Lv.{beforerLv} {player.Stats.Name}");
            if (isLvUp == true) Console.Write($" -> Lv.{player.Stats.Level} {player.Stats.Name}");
            Console.WriteLine($"\nexp {beforeExp} -> {player.Stats.Exp}");
            Console.WriteLine($"HP {beforeHP} -> {player.Stats.HP}");
            Console.WriteLine($"HP {beforeMP} -> {player.Stats.MP} + MP 10 회복");

            DisplayRewards(gold, items);

            Console.WriteLine("\n0.다음");
            WriteColor(">>", ConsoleColor.DarkYellow);
        }
        public static void DisplayRewards(int gold, List<Item> items)
        {
            Console.WriteLine("\n[획득아이템]");
            Console.WriteLine($"{gold}");

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

            if (player.Inven.Count == 0) //아이템 없다면 장착관리 안되게 구현 후 삭제
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
            Console.WriteLine("\n0. 나가기");
            Console.Write("\n장착/해체할 아이템을 선택해주세요.\n>>");
        }


        public static void Gamelobby(Player player)
        {
            Console.Clear();
            Console.WriteLine("┏" + new string('━', 51) + "┓");
            Console.WriteLine("┃" + "     스파르타 마을에 오신 여러분, 환영합니다!   ".PadRight(34) + "┃");
            Console.WriteLine("┃" + "        이제 용기를 내어 전투를 시작하세요.        ".PadRight(35) + "┃");
            Console.WriteLine("┗" + new string('━', 51) + "┛");
            Console.WriteLine();

            WriteColor("[1] ", ConsoleColor.DarkYellow);
            Console.WriteLine("상태 보기");

            WriteColor("[2] ", ConsoleColor.DarkYellow);
            Console.WriteLine($"던전 입장");

            WriteColor("[3] ", ConsoleColor.DarkYellow);
            Console.WriteLine("포션 사용");

            WriteColor("[4] ", ConsoleColor.DarkYellow);
            Console.WriteLine("인벤토리");

            WriteColor("[5] ", ConsoleColor.DarkYellow);
            Console.WriteLine("퀘스트");

            WriteColor("[0] ", ConsoleColor.DarkYellow);
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

            WriteColor("3. ", ConsoleColor.DarkYellow);
            Console.WriteLine("포션 사용");

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

            WriteColor("Lv. ", ConsoleColor.Yellow);

            Console.WriteLine($"{stat.Level}\n");
            Console.WriteLine($"{stat.Name}  ( {stat.Job} )\n");

            Console.Write($"공격력 : ");
            WriteColor($"{atkText}\n", ConsoleColor.Red);

            Console.Write("방어력 : ");
            WriteColor($"{defText}\n", ConsoleColor.Red);

            Console.Write("체 력 : ");
            WriteColor($"{stat.HP}\n", ConsoleColor.Red);

            Console.Write("마 나 : ");
            WriteColor($"{stat.MP}\n", ConsoleColor.Red);

            Console.Write("Gold : ");
            WriteColor($"{stat.Gold}\n", ConsoleColor.Red);

            Console.WriteLine("0. 나가기\n\n" +
                              $"원하시는 행동을 입력해주세요.");

            WriteColor(">> ", ConsoleColor.DarkGreen);
        }

        public static Dictionary<int, int> SelectPotion(Player player)
        {
            Console.Clear();
            WriteColor("포션 사용\n", ConsoleColor.DarkYellow);
            Console.WriteLine("포션을 사용하면 체력 및 마나를 회복할 수 있습니다.\n");

            var potionOptions = new Dictionary<int, int>(); // Key: 번호, Value: 아이템 ID
            int optionNum = 1;

            var inventory = player.Inven.GetItems();
            var sPotion = inventory.FirstOrDefault(i => i.Item.Id == 100);
            var mPotion = inventory.FirstOrDefault(i => i.Item.Id == 101);
            var mpPotion = inventory.FirstOrDefault(i => i.Item.Id == 103);

            if (sPotion != null && sPotion.Quantity > 0)
            {
                Console.WriteLine($"{optionNum}. 소형 포션 (HP +30) | 남은 개수: {sPotion.Quantity}");
                potionOptions[optionNum++] = 100;
            }

            if (mPotion != null && mPotion.Quantity > 0)
            {
                Console.WriteLine($"{optionNum}. 중형 포션 (HP +50) | 남은 개수: {mPotion.Quantity}");
                potionOptions[optionNum++] = 101;
            }

            if (mpPotion != null && mpPotion.Quantity > 0)
            {
                Console.WriteLine($"{optionNum}. 마나 포션 (MP +30) | 남은 개수: {mpPotion.Quantity}");
                potionOptions[optionNum++] = 103;
            }

            Console.WriteLine("\n0. 나가기");
            WriteColor("\n>> ", ConsoleColor.DarkGreen);

            return potionOptions; // 선택지 번호 ↔ 포션ID 매핑 반환
        }
        
        
        public static void QuestUI()
        {
            Console.Clear();
            Console.WriteLine("┏━━━━━━━━━━━━━━━━ Quest Board ━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine("┃                퀘스트 발생!                  ┃");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
            Console.WriteLine("[1] 마을을 위협하는 미니언 처치");
            Console.WriteLine("[2] 장비를 장착해보자");
            Console.WriteLine("\n\n");

            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
            Console.Write(">> ");
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
            Console.Write(">> ");

        }

        public static void AskToAcceptQuest()
        {
            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">> ");
        }

        public static void AskRewardQuest()
        {
            Console.WriteLine("");
            Console.WriteLine("1. 보상받기");
            Console.WriteLine("2. 돌아가기");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
        }

        public static void AskSaveFile()
        {
            Console.WriteLine("");
            Console.WriteLine("1. 저장하기");
            Console.WriteLine("2. 삭제하기");
            Console.WriteLine("3. 게임종료");
            Console.WriteLine("0. 돌아가기");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
        }

        public static void AskNameSave()
        {
            Console.WriteLine("1. 저장하기");
            Console.WriteLine("2. 삭제하기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
        }

    }

}
