using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace TextRPG_13
{
    public class QuestManager
    {
        // ✅ 싱글톤 인스턴스
        private static QuestManager instance;
        public static QuestManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new QuestManager();
                return instance;
            }
        }

        private QuestManager() { }

        private Quest quest = new Quest();
        public Quest CurrentQuest { get; private set; }

        public bool IsQuesting => CurrentQuest != null;

        public void AddQuest(Quest quest)
        {
            if (CurrentQuest == null)
                CurrentQuest = quest;
        }

        public void Reward(Player player)
        {
            if (CurrentQuest == null || !CurrentQuest.IsCompleted || CurrentQuest.IsRewarded)
                return;

            CurrentQuest.RewardPlayer(player);
            CurrentQuest = null;
        }

        public void OnItemEquipped(Item item)
        {
            if (CurrentQuest?.Task is TaskEquip task)
            {
                task.ProgressEquip(item);
            }
        }
    }
}