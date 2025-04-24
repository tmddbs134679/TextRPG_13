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

        public void Enter()
        {
            player = GameManager.CurrentPlayer;

                Console.Clear();
                
                //퀘스트 이미 받은 경우
                if (QuestManager.Instance.IsQuesting)
                {
                    //퀘스트 완료 시
                    if(QuestManager.Instance.CurrentQuest.IsCompleted)
                    {
                    
                        ShowQuestDetail(QuestManager.Instance.CurrentQuest);
                        HandleQuestReward();
                        return;
                    }
                    else
                    {
                        ShowQuestDetail(QuestManager.Instance.CurrentQuest);
                        Console.WriteLine("\n[진행 중인 퀘스트입니다]");
                        Console.ReadKey();
                        return;
                    }
                
                }

                //퀘스트 안받은 경우
                UIManager.QuestUI();
                HandleQuestSelection();
                return;
            
        }

        private void HandleQuestSelection()
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
            ConfirmQuestAcceptance(selectedQuest);
        }

        private void ConfirmQuestAcceptance(Quest quest)
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
                    QuestManager.Instance.AddQuest(quest);
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
                        Reward = new QuestReward { Gold = 100,
                                                   RewardItem = new Item(100, ITEMTYPE.POTION, "소형물약", 0, 0, 100, "체력을 조금 회복시켜준다.", 30)
                        }
                    };

                case EQUESTTYPE.EQUIP:
                    return new Quest
                    {
                        QuestName = "낡은 검 장착 해보기",
                        Task = new TaskEquip("낡은 검"),
                        Reward = new QuestReward { Gold = 100 }
                    };
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
            Console.WriteLine(quest.Task .Descript ?? "퀘스트 설명 없음");


            Console.WriteLine("\n- 보상 -");
            Console.WriteLine($"골드: {quest.Reward.Gold}");
            if (quest.Reward.RewardItem != null)
            {
                Console.WriteLine($"아이템: {quest.Reward.RewardItem.Name}\n");
            }
        }

        private void HandleQuestReward()
        {
            UIManager.AskRewardQuest();

            string input = Console.ReadLine();

            if (input == "1")
            {
                QuestManager.Instance.Reward(player);

                Console.WriteLine("\n보상을 받았습니다!");
                Console.WriteLine("\n아무키나 누르세요..");
                Console.ReadKey();
            }
          
        }
    }
}
