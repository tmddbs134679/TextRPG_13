using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class StageConfig
    {
        public int StageNumber { get; }
        public int[] MonsterLv { get; }
        public (int Min, int Max) SpawnRange { get; }

        public StageConfig(int stageNumber, int[] monsterLv, (int Min, int Max) spawnRange)
        {
            StageNumber = stageNumber;
            MonsterLv = monsterLv;
            SpawnRange = spawnRange;
        }
    }
    public class StageManager
    {
        private static readonly Random random = new Random();

        private static readonly StageConfig[] configs =
        {
            null,
            new StageConfig(1, new[]{1,2}, (1,4)),
            new StageConfig(2, new[]{2,3}, (1,4)),
            new StageConfig(3, new[]{3,4}, (1,4)),
            new StageConfig(4, new[]{4,5}, (1,4))
        };

        public int CurrentStage { get; private set; } = 1; //초기 스테이지는 1

        public void NextStage()//배틀에서 승리할때만 다음 스테이지로
        {
            CurrentStage++;      
        }

        public List<Monster> SpawnWave()
        {
            int idx = Math.Min(CurrentStage, configs.Length - 1);
            var cfg = configs[idx];

            //몇 마리 뽑을지
            int count = random.Next(cfg.SpawnRange.Min, cfg.SpawnRange.Max + 1);

            var wave = new List<Monster>(capacity: count);

            for (int i = 0; i < count; i++)
            {
                //레벨 배열에서 랜덤 인덱스
                int levelIndex = random.Next(cfg.MonsterLv.Length);
                int level = cfg.MonsterLv[levelIndex];

                //해당 레벨 몬스터 생성
                wave.Add(Monster.CreateRandomByLevel(level));
            }

            return wave;
        }

    }
}
