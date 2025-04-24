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
        public string TaskType { get; set; }  // ← 이걸로 실제 타입 구분

        public QuestReward Reward { get; set; }
        public bool IsRewarded { get; set; } = false;
        public bool IsCompleted => Task?.IsCompleted ?? false;
        public bool IsFinished => Task?.IsCompleted == true && !IsRewarded;

        public void QuestInProgress() => Task.InProgress();


        public void RewardPlayer(Player player)
        {
            if(!IsCompleted) { return; }

            Reward.RewardPlayer(player);
            IsRewarded = true;

        }
    }
}
