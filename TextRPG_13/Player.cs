using System;
using System.Collections;
using System.Collections.Generic;

namespace TextRPG_13
{
    public class Player
    {
        public JOBTYPE Type { get; }
        public PlayerStatement Stats { get; }

        // 생성자: 직업을 받아서 해당 프리셋 적용
        public Player(JOBTYPE job)
        {
            Type = job;

            // 프리셋에서 가져와 복사
            var preset = PlayerStatement.GetPreset(job);

            Stats = new PlayerStatement
            {
                Max_HP = 100,
                Name = preset.Name,
                Job = preset.Job,
                Offensivepower = preset.Offensivepower,
                Defensivepower = preset.Defensivepower,
                Max_HP = preset.Max_HP,
                HP = preset.HP,
                Gold = preset.Gold
            };
        }
    }
}