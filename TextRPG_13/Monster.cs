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
        public static IReadOnlyList<Monster> CurrentWave => currentWave; //다른 곳에서 몬스터 생성 못하게 읽게만
        //var monsterInfo = Monster.CurrentWave[0];으로 랜덤 생성된 몬스터의 정보보기 가능
        //예) {monsterInfo.Stats.monsterHP} 첫번째에 저장된 몬스터의 체력 정보보기
        public static void MonsterRandomSpawn()
        {
            var ui = new UIManager();

            // 1~4마리 랜덤 등장
            int count = random.Next(1, 5);
            if (currentWave == null)//아직 리스트에 몬스터 정보가 없다면
            {
                // 리스트 용량을 count(랜덤값)만큼 미리 예약
                currentWave = new List<Monster>(capacity: count);//그냥 count로 해도 무방. 저건 성능용
                for (int i = 0; i < count; i++)
                {
                    currentWave.Add(CreateRandom());
                }
            }

            //다음에 MonsterRandomSpawn()를 다시 호출시 앞에 if문 건너뛰고 여기로
            foreach (var m in currentWave)
            {
                ui.PrintRandomMonster(m);
            }
            Console.WriteLine();
        }


    }
}
