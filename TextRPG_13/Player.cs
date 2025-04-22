using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static TextRPG_13.Enums;

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
                Name = preset.Name,
                Job = preset.Job,
                Offensivepower = preset.Offensivepower,
                Defensivepower = preset.Defensivepower,
                HP = preset.HP,
                Gold = preset.Gold
            };
        }
    }
}