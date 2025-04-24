using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class TaskEquip : IQuestTask
    {
        private bool equipped = false;
        private string requiredItemName;
        public string Descript => "";

        public bool IsCompleted => equipped;

        public TaskEquip(string itemName)
        {
            requiredItemName = itemName;
        }



        public void ProgressEquip(Item item)
        {
            if (requiredItemName == item.Name)
            {
                equipped = true;
            }
        }

        public void InProgress()
        {
          
        }
    }
}
