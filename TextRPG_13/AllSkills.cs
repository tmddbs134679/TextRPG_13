using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public static class AllSkills
    {
        public static List<Skill> GetSkills(JOBTYPE job)
        {
            switch(job)
            {
                case JOBTYPE.WARRIOR:
                    return new List<Skill>
                    {
                        new Skill("알파 스트라이크","공격력 * 2로 하나의 적을 공격합니다.",10,2f,1),
                        new Skill("더블 스트라이크","공격력 * 1.5로 2명의 적을 랜덤으로 공격합니다.",15,1.5f,2)
                    };
                case JOBTYPE.WIZARD:
                    return new List<Skill>
                    {
                        new Skill("메테오 샤워", "공격력 * 3로 3명의 적을 랜덤으로 공격합니다.", 50, 3f,3),
                        new Skill("얼음송곳", "공격력 * 1.7로 하나의 적을 공격합니다.", 10, 1.7f,1)
                    };
                case JOBTYPE.ASSASSIN:
                    return new List<Skill>
                    {
                        new Skill("암습","공격력 * 4로 하나의 적을 공격합니다.",40,4f,1),
                        new Skill("수리검투척","공격력 * 1.2로 하나의 적을 공격합니다.",5,1.2f,1)
                    };
                default:
                    return new List<Skill>();

            }
        }
    }
}
