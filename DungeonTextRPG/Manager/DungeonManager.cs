using DungeonTextRPG.Manager.CreatePlayerAccount;
using DungeonTextRPG.Manager.Game;
using DungeonTextRPG.Manager.Shop;
using DungeonTextRPG.Manager.Status;
using DungeonTextRPG.Manager.VisualText;
using System;
using System.Collections.ObjectModel;
using System.Numerics;

namespace DungeonTextRPG.Manager.Dungeon
{
    public class DungeonManager
    {
        private static DungeonManager _instance;

        public static DungeonManager instance => _instance ??= new DungeonManager();

        private DungeonManager() { }

        private string[] DungeonName = { "쉬운 던전", "일반 던전", "어려운 던전" };
        private int[] DungeonRecommendedDef = { 5, 11, 17 };

        public void DisplayDungeon(bool isHPLow)
        {
            Console.Clear();
            VisualTextManager.instance.DrawPainting(PaintingVillage.Dungeon);

            Player player = GameManager.instance.MyPlayer;
            Console.WriteLine($" 현재 방어력 : {player.StatDefense}");
            Console.WriteLine($" 현재 체력 : {player.Health} \n");

            if (isHPLow)
            {
                Console.WriteLine(" 탐험할 체력이 부족합니다!");
                if(GameManager.instance.PromptUserAction("나가기") == 1) GameManager.instance.VillageMenu();
                return;
            }

            Console.WriteLine(" 탐험할 던전을 선택하세요.");

            HandleDungeonSelection(GameManager.instance.PromptUserAction(
                $"{DungeonName[0],-10}| 방어력 {DungeonRecommendedDef[0]} 이상 권장/" +
                $"{DungeonName[1],-10}| 방어력 {DungeonRecommendedDef[1]} 이상 권장/" +
                $"{DungeonName[2],-9}| 방어력 {DungeonRecommendedDef[2]} 이상 권장/" +
                $"나가기"));
        }

        void HandleDungeonSelection(int resultValue)
        {
            if (resultValue >= 1 && resultValue <= 3) DungeonResult(resultValue);
            else if (resultValue == 4) GameManager.instance.VillageMenu();
        }

        void DungeonResult(int dungeonType)
        {
            bool isClear = false;

            // 권장방어력 판별
            if (GameManager.instance.MyPlayer.StatDefense < DungeonRecommendedDef[dungeonType - 1])
            {
                if(new Random().Next(1, 101) > 40) 
                {
                    isClear = true;
                }
            }
            else
            {
                isClear = true;
            }

            Console.Clear();
            VisualTextManager.instance.DrawPainting(isClear ? PaintingUI.DungeonClear : PaintingUI.DungeonFail);
            VisualTextManager.instance.DrawPainting(PaintingUI.Divider_x2);
            Console.WriteLine($" {DungeonName[dungeonType - 1]} 탐험에 {(isClear ? "성공했습니다" : "실패했습니다")}");

            ProcessResult(dungeonType, isClear);

            if (GameManager.instance.PromptUserAction("나가기") == 1)
            {
                GameManager.instance.VillageMenu();
            }
        }

        void ProcessResult(int dungeonType, bool isClaer)
        {
            Player player = GameManager.instance.MyPlayer;
            int minHealthLoss = (int)(20 + (DungeonRecommendedDef[dungeonType - 1] - player.StatDefense));
            int healthLoss = new Random().Next(minHealthLoss, minHealthLoss + 16);
            int rewardGold = 0;

            if (!isClaer) { healthLoss /= 2; }
            else
            {
                int BonusRate = new Random().Next((int)player.StatAttack, (int)player.StatAttack*2);

                // 클리어 보상 골드 지급
                switch (dungeonType)
                {
                    case 1:
                        rewardGold = (int)Math.Round(1000 + (1000 * (BonusRate * 0.01f)));
                        break;

                    case 2:
                        rewardGold = (int)Math.Round(1700 + (1700 * (BonusRate * 0.01f)));
                        break;

                    case 3:
                        rewardGold = (int)Math.Round(2500 + (2500 * (BonusRate * 0.01f)));
                        break;
                }

                if (GameManager.instance.GetEXP()) { Console.WriteLine(" !!레벨 업!!"); };
                GameManager.instance.GainGold(rewardGold);
            }
            DisplayResult(healthLoss, rewardGold);
            GameManager.instance.LoseHealth(healthLoss);
        }

        void DisplayResult(int healthLoss, int rewardGold)
        {
            Player player = GameManager.instance.MyPlayer;
            bool minusHealth = player.Health - healthLoss < 0;
            Console.WriteLine(" [탐험 결과]");
            Console.WriteLine($" 체력 {player.Health} => {(minusHealth ? 0 : player.Health - healthLoss)}");
            if (rewardGold > 0) { Console.WriteLine($" Gold : {rewardGold} 획득"); }
        }

    }
}