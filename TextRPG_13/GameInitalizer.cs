using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;


public enum JOBTYPE
{
    NONE,
    WARRIOR,
    MAGICIAN,
    ARCHER,

}

namespace TextRPG_13
{
    public class GameInitalizer
    {
        public Player InitPlayer()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.\n");
            Console.Write("이름 : ");
            string name = Console.ReadLine();

            int job = int.Parse(Console.ReadLine());

            Player player = new Player();
            // player.Status.name = name;


            switch (job)
            {
                case (int)JOBTYPE.WARRIOR:
                    // 클래스 따로 안만들면 Status정보 수정
                    break;
                case (int)JOBTYPE.MAGICIAN:

                    break;
                case (int)JOBTYPE.ARCHER:

                    break;
                default:
                    //잘못입력
                    break;
            }

            // player.Status.job = job;


            return player;
        }
       
    }
}
