
using TextRPG_13;

namespace TextRPG_13
{
    public class Program
    {
        static void Main(string[] args)
        {

            var _ = QuestManager.Instance;

            Player player = GameStart();


            // GameManager에 저장
            GameManager.CurrentPlayer = player;
            GameManager.UI = new UIManager();

            // 게임 시작
            new Lobby(player).GameLobby();
        }

        private static Player GameStart()
        {
            //if (SaveManager.Exists())
            //{
            //    return SaveManager.Load();

            //}
            //else
            //{
                GameInitalizer initializer = new GameInitalizer();
                return initializer.InitPlayer();
            //}
        }

    }
}