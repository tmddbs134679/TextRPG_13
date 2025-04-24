
using TextRPG_13;

namespace TextRPG_13
{
    public class Program
    {
        static void Main(string[] args)
        {
            GameInitalizer initializer = new GameInitalizer();

            // 플레이어 생성
            Player player = initializer.InitPlayer();

            // GameManager에 저장
            GameManager.CurrentPlayer = player;
            GameManager.UI = new UIManager();

            // 게임 시작
            initializer.InitPlayer();
        }
    }
}