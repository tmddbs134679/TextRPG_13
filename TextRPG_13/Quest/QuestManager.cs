using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class QuestManager
    {

        //private List<Quest> quests = new List<Quest>();
        private Quest quest = new Quest();
        public Quest CurrentQuest { get; private set; }

        public void AddQuest(Quest quest)
        {
            if(CurrentQuest == null)
                 CurrentQuest = quest;
        }

        public void Reward(Player player)
        {
            if(CurrentQuest == null) { return; }

            if(!CurrentQuest.IsCompleted) { return; } 

            if(CurrentQuest.IsRewarded) { return; }

            CurrentQuest.RewardPlayer(player);

            CurrentQuest = null;
        }

        public bool IsQuesting => CurrentQuest != null;

    }
}
