using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    internal class TaskEquip : IQuestTask
    {
        private bool equipped = false;
        private string requiredItemName;
        public string Descript => "";

        public bool IsCompleted => equipped;

        //public void ProgressEquip(Item item)
        //{
        //    if(requiredItemName == item.Name)
        //    {
        //        equipped = true;
        //    }
        //}

        public void InProgress()
        {
            //특정 장비를 장착하면 equipped = true;
        }
    }
}
