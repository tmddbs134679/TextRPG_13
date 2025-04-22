using System.Numerics;

namespace TextRPG_13
{

    internal class Program
    {
        static void Main(string[] args)
        {
            // 1) 몬스터 웨이브 한 번 스폰 & 화면에 출력
            Monster.MonsterRandomSpawn();

            // 2) 생성된 웨이브 리스트 가져오기
            var wave = Monster.CurrentWave;

            // 3) 두 번째 몬스터(인덱스 1)만 체력 -5
            if (wave != null && wave.Count > 1)
            {
                var second = wave[1];
                second.Stats.monsterHP -= 5;
                Console.WriteLine($"\n두 번째 몬스터 [{second.Stats.monsterName}]의 체력을 5 깎았습니다. " +
                                  $"남은 HP: {second.Stats.monsterHP}\n");
            }
            else
            {
                Console.WriteLine("\n두 번째 몬스터가 없습니다.\n");
            }

            // 4) 변경된 웨이브 상태를 다시 출력
            var ui = new UIManager();
            Console.WriteLine("=== 변경 후 웨이브 상태 ===");
            foreach (var m in wave)
                ui.PrintRandomMonster(m);

            Console.WriteLine("\n엔터키를 누르면 종료합니다.");
            Console.ReadLine();
        }

    }
}
