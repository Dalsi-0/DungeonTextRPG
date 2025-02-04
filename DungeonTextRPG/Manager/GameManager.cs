using static TSVReader;

namespace DungeonTextRPG.Manager
{
    public class GameManager
	{
		private static GameManager _instance;

        public static GameManager instance => _instance ??= new GameManager();

        private GameManager() { }


        public Player MyPlayer;
        public bool isLoadData = false;

        public void StartGame()
        {
            ShowLoadingAnimation();

            Console.Clear();

            if (SaveLoadManager.instance.LoadData())
            {
                DisplayIntroMessage();
            }
            else
            {
                MyPlayer = CreatePlayerAccountrManager.instance.SetPlayerAccount();
            }

            StatusManager.instance.UpdateStats();
            VillageMenu();
        }

        #region Loading, 인트로 관련
        private void ShowLoadingAnimation()
        {
            int count = 0;
            while (!isLoadData)
            {
                Console.Clear();
                Console.WriteLine($"Loading{new string('.', count % 5 + 1)}");
                count++;
                Thread.Sleep(150);
            }
        }

        private void DisplayIntroMessage()
        {
            VisualTextManager.instance.DrawPainting(PaintingUI.Title);
            Console.WriteLine("────────────────────────────────────────────────────────────────");
            Console.WriteLine(" 던전 텍스트 RPG에 오신 여러분 환영합니다.");
            Console.WriteLine("           데이터 로드 완료! \n 게임을 시작하려면 아무 키나 누르세요...");
            Console.WriteLine("────────────────────────────────────────────────────────────────");
            Console.ReadKey();
        }
        #endregion




        #region 마을 관련
        public void VillageMenu()
        {
            VisualTextManager.instance.DrawPainting(PaintingVillage.Village);

            Console.WriteLine(" 던전 밥벌이 마을입니다.");
            Console.WriteLine(" 어떤 활동을 하시겠습니까?");

            int resultValue = PromptUserAction("상태 보기/인벤토리/상점/던전 입장/휴식하기");

            switch (resultValue)
            {
                case 1: // 상태보기
                    StatusManager.instance.DisplayPlayerStatus();
                    break;

                case 2: // 인벤토리
                    InventoryManager.instance.DisplayPlayerInventory("", false, false);
                    break;

                case 3: // 상점
                    ShopManager.instance.DisplayShop("", false, false);
                    break;

                case 4: // 던전 입장
                    DungeonManager.instance.DisplayDungeon(MyPlayer.Health <= 0);
                    break;

                case 5: // 휴식하기
                    RestMenu();
                    break;
            }
        }

        void RestMenu() // 휴식하기
        {
            VisualTextManager.instance.DrawPainting(PaintingVillage.Hotel);

            Console.WriteLine(" 휴식을 하러 여관에 들어왔습니다.");
            Console.Write(" 500 G 를 내면 체력을 100까지 회복할 수 있습니다.");
            Console.WriteLine($"(보유 골드 : {MyPlayer.GoldAmount} G)");

            int resultValue = PromptUserAction("휴식하기/나가기");

            if(resultValue == 1)
            {
                if(MyPlayer.GoldAmount >= 500)
                {
                    MyPlayer.GoldAmount -= 500;
                    MyPlayer.Health = 100;
                    Console.WriteLine(" 휴식을 완료했습니다.");
                }
                else
                {
                    Console.WriteLine(" 골드가 부족합니다.");
                }
                Console.WriteLine(" 아무 키를 눌러 여관을 나갑니다...");
                Console.ReadKey();
            }

            VillageMenu();
        }
        #endregion


        #region Player 관련
        public void LoseHealth(int value)
        {
            MyPlayer.Health -= value;
            if (MyPlayer.Health < 0) { MyPlayer.Health = 0; }
        }
        public void GainGold(int value)
        {
            MyPlayer.GoldAmount += value;
        }
        public bool GetEXP()
        {
            MyPlayer.DungeonClearEXP++;
            if (MyPlayer.Level == MyPlayer.DungeonClearEXP) { LevelUP(); return true; }
            return false;
        }
        void LevelUP()
        {
            MyPlayer.DungeonClearEXP = 0;
            MyPlayer.Level++;
            MyPlayer.StatAttack += 0.5f;
            MyPlayer.StatDefense++;
        }
        #endregion

        public int PromptUserAction(string actionMessages) // 행동 번호 입력 함수
        {
            int result = 0;
            string[] arrayActionMessages = actionMessages.Split('/');

            while (true)
            {
                VisualTextManager.instance.DrawPainting(PaintingUI.Divider);

                // actionMessages 배열을 사용하여 선택지 출력
                for (int i = 0; i < arrayActionMessages.Length; i++)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;  // 텍스트 색상도 바꿔야 보입니다
                    Console.WriteLine($" {i + 1} - {arrayActionMessages[i]}");
                }

                VisualTextManager.instance.DrawPainting(PaintingUI.Divider);
                Console.WriteLine(" 원하시는 행동을 선택하세요.");
                Console.Write(" >> ");


                if (int.TryParse(Console.ReadLine(), out result) && (result > 0 && result <= arrayActionMessages.Length))
                {
                    break;
                }
                // 입력이 실패했을 경우 (int 값이 아니었을 경우)
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                }
            }

            return result;
        }
    }
}
