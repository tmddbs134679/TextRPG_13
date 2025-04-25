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

        //스테이지 정보를 하나의 인스턴스로 관리
        //GameManager.Stage 라는 이름으로 어디서든 접근 가능하게 함 / 읽기전용
        public static StageManager Stage { get; } = new StageManager();
    }
}
