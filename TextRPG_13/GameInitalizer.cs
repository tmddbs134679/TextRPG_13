using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;



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


            Console.WriteLine("\n직업을 선택해주세요:");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 위자드");
            Console.WriteLine("3. 어쌔신");
            Console.Write(">> ");

            int jobInput = int.Parse(Console.ReadLine());

            JOBTYPE selectedJob = (JOBTYPE)jobInput;
            var stats = GetJobStats(selectedJob);

            Player player = new Player();
            //player._Playerstat.Name = name;
            //player._Playerstat.Job = selectedJob;
            //player._Playerstat.Offensivepower = stats.offensive;
            //player._Playerstat.Defensivepower = stats.defensive;

            return player;
        }

        private (int offensive, int defensive) GetJobStats(JOBTYPE job)
        {
            return job switch
            {
                JOBTYPE.WARRIOR => (10, 10),
                JOBTYPE.WIZARD => (13, 5),
                JOBTYPE.ASSASSIN => (8, 8),
                _ => (10,10),
            };
        }



    }
}
