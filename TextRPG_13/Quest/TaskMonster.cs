using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class TaskMonster : IQuestTask
    {
        private int requireCount = 5;
        private int currentKillCount = 0;

        public string Descript => "";

        public bool IsCompleted => currentKillCount >= requireCount;

        public void InProgress()
        {
            currentKillCount++;
        }
    }
}
