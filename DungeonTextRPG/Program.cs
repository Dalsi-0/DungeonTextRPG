using DungeonTextRPG.Manager.Game;
using DungeonTextRPG.Manager.SaveLoad;

namespace DungeonTextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 프로그램 종료 시 실행할 코드 등록
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            GameManager.instance.StartGame();

        }


        // 프로세스 종료 시 실행될 함수
        static void OnProcessExit(object sender, EventArgs e)
        {
            SaveLoadManager.instance.SaveData();
            // 저장 등 종료 시 처리할 작업을 여기에 작성
        }
    }
}
