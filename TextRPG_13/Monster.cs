using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Monster
    {
        public MonsterType Type { get; }
        public MonsterStatement Stats { get; }

        private static readonly Dictionary<MonsterType, MonsterStatement> monsterPresets =
            new Dictionary<MonsterType, MonsterStatement>
            {
                {MonsterType.MINION, new MonsterStatement("미니언", 2, 15, 5 ) },
                { MonsterType.VOIDWORM,    new MonsterStatement("공허충", 5, 200, 20) },
                { MonsterType.SIEGEMINION, new MonsterStatement("대포미니언", 3, 100, 10) },
            };
        private static readonly Random random = new Random();

    }
}
