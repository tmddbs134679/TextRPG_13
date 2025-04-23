using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class TaskMonster : IQuestTask
    {
        public int requireCount { get; } =  1;
        public int currentKillCount { get; set; } = 0;

        public string Descript => $"미니언 {currentKillCount}/{requireCount} 마리 처치";

        public bool IsCompleted => currentKillCount >= requireCount;

        public void InProgress()
        {
            if(currentKillCount >= requireCount) 
            { 
                currentKillCount = requireCount;
            }
            else
            {
                currentKillCount++;
            }
        }
    }
}
