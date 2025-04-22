using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public interface IQuestTask
    {
        string Descript { get; }
        bool IsCompleted { get; }

        void InProgress();

    }
}
