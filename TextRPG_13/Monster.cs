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
                { MonsterType.VOIDWORM,    new MonsterStatement("공허충", 3, 10, 9) },
                { MonsterType.SIEGEMINION, new MonsterStatement("대포미니언", 5, 25, 8) },
            };

        // 내부 생성자
        private Monster(MonsterType type, MonsterStatement stats)
        {
            Type = type;
            Stats = stats;
        }
        //랜덤
        private static readonly Random random = new Random();
        public static Monster CreateRandom()
        {
            var types = monsterPresets.Keys.ToArray();
            var randomType = types[random.Next(types.Length)];
            var stats = monsterPresets[randomType];
            return new Monster(randomType, stats);
        }
        public static Monster Create(MonsterType type)
        {
            return new Monster(type, monsterPresets[type]);
        }

        //Monster.MonsterRandomSpawn();한줄로 다른 곳에서 몬스터 랜덤 스폰 메서드 사용가능
        private static List<Monster> currentWave; 
        public static IReadOnlyList<Monster> CurrentWave => currentWave;
        //var monsterInfo = Monster.CurrentWave[0];으로 랜덤 생성된 몬스터의 정보보기 가능
        //예) {monsterInfo.Stats.monsterHP} 첫번째에 저장된 몬스터의 체력 정보보기
        public static void MonsterRandomSpawn()
        {
            var ui = new UIManager();

            // 1~4마리 랜덤 등장
            int count = random.Next(1, 5);
            if (currentWave == null)
            {
                currentWave = new List<Monster>(capacity: count);
                for (int i = 0; i < count; i++)
                {
                    currentWave.Add(CreateRandom());
                }
            }

            foreach (var m in currentWave)
            {
                ui.PrintRandomMonster(m);
            }
            Console.WriteLine();
        }


    }
}
