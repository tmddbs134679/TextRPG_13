using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class Monster
    {
        public MONSTERTYPE Type { get; }
        public MonsterStatement Stats { get; }

        private static readonly Dictionary<MONSTERTYPE, MonsterStatement> monsterPresets =
            new Dictionary<MONSTERTYPE, MonsterStatement>
            {                                           // 이름,레벨,체력,공격력,최소골드,최대골드
                { MONSTERTYPE.MINION,      new MonsterStatement("미니언", 1, 10, 5, 100, 300 ) },
                { MONSTERTYPE.MELEEMINION, new MonsterStatement("전사미니언", 2, 10, 8, 300, 500)},
                { MONSTERTYPE.VOIDWORM,    new MonsterStatement("공허충", 3, 15, 8, 500, 700) },
                { MONSTERTYPE.OCKLEPOD,    new MonsterStatement("외눈박이문어", 4, 18, 9, 700, 1000) },
                { MONSTERTYPE.SIEGEMINION, new MonsterStatement("대포미니언", 5, 20, 8, 1000, 1500) },
            };

        // 내부 생성자
        private Monster(MONSTERTYPE type, MonsterStatement stats)
        {
            Type = type;
            Stats = stats;
        }
        //새 Monster 인스턴스 생성, 내부에서는 템플릿을 Clone해서 복제본 사용
        public static Monster Create(MONSTERTYPE type)
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

           // 이전 웨이브는 덮어쓰기
            currentWave = new List<Monster>(count);
            for (int i = 0; i < count; i++)
            {
                currentWave.Add(CreateRandom());
            }

        }
        //스테이지 기능 추가하면서 만든 코드
        public static Monster CreateRandomByLevel(int level)
        {
            //레벨이 같은 MonsterType 키만 추리기
            var types = monsterPresets
                .Where(kv => kv.Value.Lv == level)
                .Select(kv => kv.Key)
                .ToArray();

            //그 중 하나를 랜덤 선택
            var chosen = types[random.Next(types.Length)];

            //기존 Create(type) 메서드 사용
            return Create(chosen);
        }
        public static List<Monster> SpawnWave(
            int[] allowedLevels,
            int minCount,
            int maxCount)
        {
            var wave = new List<Monster>();
            int count = random.Next(minCount, maxCount + 1);

            for (int i = 0; i < count; i++)
            {
                int lvl = allowedLevels[random.Next(allowedLevels.Length)];
                wave.Add(CreateRandomByLevel(lvl));
            }

            return wave;
        }

        //몬스터에서 스킬데미지 처리
        public int TakeSkillDamage(float dmg,Player player)
        {
            Stats.monsterHP -= (int)Math.Ceiling(player.Stats.baseATK * dmg);

            return (int)Math.Ceiling(player.Stats.baseATK * dmg);
        }
        public static int GetDamageWithVariance(float baseAtk,Player player) 
        {
            Random rand = new Random();
            double offset = Math.Ceiling(baseAtk * 0.1);
            int critalChance = rand.Next(1, 101);
            int avoidAttack = rand.Next(1, 101);
            int finalDamage = 0;

            if (avoidAttack > 10)
            {
                finalDamage = (int)baseAtk + rand.Next(-(int)offset, (int)offset);
                if (critalChance <= 15)
                {
                    finalDamage = (int)Math.Ceiling((finalDamage * 1.5));
                }

                finalDamage -= (int)Math.Ceiling(player.Stats.Defensivepower / 2);

                if(finalDamage < 0) finalDamage = 0;
            }

            return finalDamage ;
        }
    }
}