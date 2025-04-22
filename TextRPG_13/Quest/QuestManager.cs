using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class QuestManager
    {

        private List<Quest> quests = new List<Quest>();
        public Quest CurrentQuest { get; private set; }

        public void AddQuest(Quest quest)
        {
            quests.Add(quest); 
        }

        public void Reward(Player player)
        {
            if(CurrentQuest != null) { return; }

            if(!CurrentQuest.IsCompleted) { return; } 

            if(CurrentQuest.IsRewarded) { return; }

            CurrentQuest.RewardPlayer(player);

            CurrentQuest = null;
        }

    }
}
