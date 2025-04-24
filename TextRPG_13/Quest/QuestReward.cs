using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class QuestReward
    {
        public int Gold { get; set; }
        public Item RewardItem { get; set; }

        public void RewardPlayer(Player player)
        {
            player.Stats.Gold += Gold;

            if( RewardItem != null )
                player.Inven.AddItem(RewardItem);
    
        }

    }
}
