using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TextRPG_13
{
    public class QuestLobby
    {
        private Player player;

        public void Quest()
        {
            player = GameManager.CurrentPlayer;

            
                Console.Clear();

                if (player.QuestManager.IsQuesting)
                {
                    //퀘스트 완료 시
                    if(player.QuestManager.CurrentQuest.IsCompleted)
                    {
                    
                        ShowQuestDetail(player.QuestManager.CurrentQuest);
                        AskReward();
                        return;
                    }
                    else
                    {
                        ShowQuestDetail(player.QuestManager.CurrentQuest);
                        Console.WriteLine("\n[진행 중인 퀘스트입니다]");
                        Console.ReadKey();
                        return;
                    }
                
                }

                UIManager.QuestUI();
                ShowQuestList();
                return;
            
        }

        private void ShowQuestList()
        {
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int selectedNum))
            {
                Console.WriteLine("숫자를 입력해주세요.");
                return;
            }

            Quest selectedQuest = CreateQuestByType((EQUESTTYPE)selectedNum);

            if (selectedQuest == null)
            {
                Console.WriteLine("해당 번호의 퀘스트가 없습니다.");
                return;
            }

            ShowQuestDetail(selectedQuest);
            AskToAcceptQuest(selectedQuest);
        }

        private void AskToAcceptQuest(Quest quest)
        {
            UIManager.AskToAcceptQuest();

            string input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("숫자를 입력해주세요.");
                return;
            }

            switch (choice)
            {
                case 1:
                    player.QuestManager.AddQuest(quest);
                    Console.WriteLine($"\n퀘스트 '{quest.QuestName}'를 수락했습니다!");
                    break;
                case 2:
                    Console.WriteLine("퀘스트를 거절했습니다.");
                    break;
                default:
                    Console.WriteLine("잘못된 번호입니다.");
                    break;
            }

            Console.WriteLine("아무 키나 누르세요...");
            Console.ReadKey();
        }

        private Quest CreateQuestByType(EQUESTTYPE type)
        {
            switch (type)
            {
                case EQUESTTYPE.MINION:
                    return new Quest
                    {
                        QuestName = "미니언 퇴치",
                        Task = new TaskMonster(),
                        Reward = new QuestReward { Gold = 100 }
                    };

                // case EQUESTTYPE.EQUIP: 등 추가 가능
                default:
                    return null;
            }
        }

        private void ShowQuestDetail(Quest quest)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Quest!!");
            Console.ResetColor();

            Console.WriteLine($"\n{quest.QuestName}");
            Console.WriteLine((quest.Task as TaskMonster)?.Descript ?? "퀘스트 설명 없음");

            Console.WriteLine("\n- 보상 -");
            Console.WriteLine($"골드: {quest.Reward.Gold}");
        }

        private void AskReward()
        {
            UIManager.AskRewardQuest();

            string input = Console.ReadLine();

            if (input == "1")
            {
                player.QuestManager.Reward(player);

                Console.WriteLine("\n보상을 받았습니다!");
                Console.WriteLine("\n아무키나 누르세요..");
                Console.ReadKey();
            }
          
        }
    }
}
