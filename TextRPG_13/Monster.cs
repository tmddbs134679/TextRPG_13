using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Monster
    {
        public MMONSTERTYPE Type { get; }
        public MonsterStatement Stats { get; }

        private static readonly Dictionary<MMONSTERTYPE, MonsterStatement> monsterPresets =
            new Dictionary<MMONSTERTYPE, MonsterStatement>
            {
                {MMONSTERTYPE.MINION, new MonsterStatement("미니언", 2, 15, 5 ) },
                { MMONSTERTYPE.VOIDWORM,    new MonsterStatement("공허충", 3, 10, 9) },
                { MMONSTERTYPE.SIEGEMINION, new MonsterStatement("대포미니언", 5, 25, 8) },
            };

        // 내부 생성자
        private Monster(MMONSTERTYPE type, MonsterStatement stats)
        {
            Type = type;
            Stats = stats;
        }
        //새 Monster 인스턴스 생성, 내부에서는 템플릿을 Clone해서 복제본 사용
        public static Monster Create(MMONSTERTYPE type)
        {
            var statsCopy = monsterPresets[type].Clone();
            return new Monster(type, statsCopy);
        }
        //랜덤
        private static readonly Random random = new Random();
        public static Monster CreateRandom()
        {
            var types = monsterPresets.Keys.ToArray();
            var randomType = types[random.Next(types.Length)];
            return Create(randomType);
        }

        //Monster.MonsterRandomSpawn();한줄로 다른 곳에서 몬스터 랜덤 스폰 메서드 사용가능
        private static List<Monster> currentWave;
        public static IReadOnlyList<Monster> CurrentWave => currentWave;

        public static void MonsterRandomSpawn()
        {
            // 1~4마리 랜덤 등장
            int count = random.Next(1, 5);

            if (currentWave == null)
            {
                currentWave = new List<Monster>(count);
                for (int i = 0; i < count; i++)
                {
                    currentWave.Add(CreateRandom());
                }
            }
        }


    }
}