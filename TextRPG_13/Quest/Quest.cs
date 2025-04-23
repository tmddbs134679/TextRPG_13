using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum QUESTSTATUS
{
    PREV,
    DO,
    FINISH
}

namespace TextRPG_13
{
    public class Quest
    {
        public string QuestName { get; set; }
        public IQuestTask Task { get; set; }
        public QuestReward Reward { get; set; }
        public bool IsRewarded { get; set; } = false;
        public bool IsCompleted => Task.IsCompleted;
        public bool IsFinished => IsCompleted && IsRewarded;

        public void QuestInProgress() => Task.InProgress();


        public void RewardPlayer(Player player)
        {
            if(!IsCompleted) { return; }

            Reward.RewardPlayer(player);
            IsRewarded = true;

        }
    }
}
