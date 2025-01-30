using DungeonTextRPG.Manager.CreatePlayerAccount;
using DungeonTextRPG.Manager.SaveLoad;
using DungeonTextRPG.Manager.VisualText;
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
        }


        public Player MyPlayer;


        public void StartGame()
        {
            if (SaveLoadManager.instance.LoadData())
            {
                VisualTextManager.instance.DrawPainting();
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("던전 텍스트 RPG에 오신 여러분 환영합니다.");
                Console.WriteLine("게임을 시작하려면 아무 키나 누르세요...");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine();
                Console.ReadKey(); // 콘솔 종료 방지

            }
            else
            {
                MyPlayer = CreatePlayerAccountrManager.instance.SetPlayerAccount();
            }


            VillageMenu();
        }

        void VillageMenu()
        {
            VisualTextManager.instance.DrawPainting(PaintingVillage.Village);

            Console.WriteLine("던전 밥벌이 마을입니다.");
            Console.WriteLine("어떤 활동을 하시겠습니까?");

            int resultValue = PromptUserAction("상태 보기/인벤토리/상점/던전 입장/휴식하기");

            switch (resultValue)
            {
                case 1:
                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    break;

                case 5:
                    RestMenu();
                    break;
            }
        }




        void RestMenu() // 휴식하기
        {
            VisualTextManager.instance.DrawPainting(PaintingVillage.Hotel);

            Console.WriteLine("휴식을 하러 여관에 들어왔습니다.");
            Console.Write("500 G 를 내면 체력을 100까지 회복할 수 있습니다.");
            Console.WriteLine($"(보유 골드 : {MyPlayer.GoldAmount} G)");

            int resultValue = PromptUserAction("휴식하기/나가기");

            if(resultValue == 1)
            {
                if(MyPlayer.GoldAmount >= 500)
                {
                    MyPlayer.GoldAmount -= 500;
                    MyPlayer.Health = 100;
                    Console.WriteLine("휴식을 완료했습니다.");
                }
                else
                {
                    Console.WriteLine("골드가 부족합니다.");
                }
                Console.WriteLine("아무 키를 눌러 여관을 나갑니다...");
                Console.ReadKey();
            }

            VillageMenu();
        }

        public int PromptUserAction(string actionMessages) // 행동 번호 입력 함수
        {
            int result = 0;
            string[] arrayActionMessages = actionMessages.Split('/');

            while (true)
            {
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine();

                // actionMessages 배열을 사용하여 선택지 출력
                for (int i = 0; i < arrayActionMessages.Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {arrayActionMessages[i]}");
                }

                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("원하시는 행동을 선택하세요.");
                Console.Write(">> ");


                if (int.TryParse(Console.ReadLine(), out result) && (result > 0 && result <= arrayActionMessages.Length))
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
