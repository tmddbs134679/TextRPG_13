using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class GameManager
    {
        //gameinitalizer에서 선택한 job에 대한 값을 { get; set; }을 통하여 저장 
        public static Player CurrentPlayer { get; set; }

        //UIManager에 있는 각각의 UI에 저장하고 가져옴
        public static UIManager UI { get; set; }
    }
}
