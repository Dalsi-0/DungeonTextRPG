using DungeonTextRPG.Manager.CreatePlayerAccount;
using DungeonTextRPG.Manager.SaveLoad;
using System;

namespace DungeonTextRPG.Manager.Game
{
	public class GameManager
	{
		private static GameManager _instance;

        public static GameManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        private GameManager()
        {
            Console.WriteLine("게임 매니저 생성");
        }


        public Player MyPlayer;





        public void StartGame()
        {
            if (SaveLoadManager.instance.LoadData())
            {
                Console.WriteLine("데이터 잇!");
                Console.ReadKey(); // 콘솔 종료 방지
            }
            else
            {
                MyPlayer = CreatePlayerAccountrManager.instance.SetPlayerAccount();
            }
        }

        public void EndGame()
        {

        }


        
        public int PromptUserAction(string[] actionMessages) // 행동 번호 입력 함수
        {
            int result = 0;

            while (true)
            {
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine();

                // actionMessages 배열을 사용하여 선택지 출력
                for (int i = 0; i < actionMessages.Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {actionMessages[i]}");
                }

                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine();


                if (int.TryParse(Console.ReadLine(), out result) && (result > 0 && result <= actionMessages.Length))
                {
                    break;
                }
                // 입력이 실패했을 경우 (int 값이 아니었을 경우)
                else
                {
                    Console.WriteLine("유효한 숫자가 아닙니다. 다시 시도해주세요.");
                }
            }

            return result;
        }
    }
}
