
using TextRPG_13;

namespace TextRPG_13
{
    public class Program
    {
        static void Main(string[] args)
        {
            Battle battle = new Battle();
            GameInitalizer initializer = new GameInitalizer();

            // 플레이어 생성
            Player player = initializer.InitPlayer();

            // GameManager에 저장
            GameManager.CurrentPlayer = player;
            GameManager.UI = new UIManager(player);

            // 게임 시작
            GameManager.UI.Gamelobby();
           
            Console.WriteLine("전투가 끝났습니다. 아무 키나 누르세요...");
            Console.ReadLine();
          
          
        }
    }
}