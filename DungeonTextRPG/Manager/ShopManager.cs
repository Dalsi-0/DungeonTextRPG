using DungeonTextRPG.Manager.Game;
using DungeonTextRPG.Manager.Status;
using DungeonTextRPG.Manager.VisualText;
using System;

namespace DungeonTextRPG.Manager.Shop
{
    public class ShopManager
    {
        private static ShopManager _instance;

        public static ShopManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ShopManager();
                }
                return _instance;
            }
        }

        private ShopManager()
        {
        }


        int ShopPage = 1; // 현재 페이지

        public void DisplayShop(bool isDenied)
        {
            VisualTextManager.instance.DrawPainting(PaintingVillage.Shop);

          
            DrawShopItem();

            VisualTextManager.instance.DrawPainting(PaintingUI.Divider_x2);
            if (!isDenied)
            {
                Console.WriteLine(" 그 행동은 할 수 없습니다.");
            }
            Console.WriteLine(" 상점 주인 : 어서오게나, 좋은 장비가 많다네");

            int resultValue = GameManager.instance.PromptUserAction("아이템 구매/아이템 판매/이전 페이지/다음 페이지/나가기");

            switch (resultValue)
            {
                case 1:
                case 2:
                case 3: // 이전 페이지
                    DisplayShop(ChangePage(false));
                    break;

                case 4: // 다음 페이지
                    DisplayShop(ChangePage(true));
                    break;

                case 5: // 나가기
                    GameManager.instance.VillageMenu();
                    break;
            }
        }
        
        void DrawShopItem()
        {
            int minIndex = (5 * ShopPage) - 5;

            int maxPage = (int)Math.Ceiling(ItemDatabase.instance.Items.Count / 5f);

            // 한 페이지에 출력되는 아이템 칸 갯수
            int InfoSpaceCount = ItemDatabase.instance.Items.Count - minIndex;

            Console.WriteLine();
            Console.WriteLine("    ┌──────────────────────────────────────────────────────────────────────────────────┐");
            Console.WriteLine("    │                                       [상점]                                     │");
            Console.WriteLine($"    │                                    골드: {GameManager.instance.MyPlayer.GoldAmount,+5} G                                 │");
            Console.WriteLine("    ├──────────────────────────────────────────────────────────────────────────────────┤");

            DisplayCurrentPage(InfoSpaceCount, minIndex);

            Console.WriteLine($"    ├──────────────────────────────────────────────────────────────────────────────────┤");
            Console.WriteLine($"    │                                    [{ShopPage}/{maxPage}] 페이지                                  │");
            Console.WriteLine("    └──────────────────────────────────────────────────────────────────────────────────┘");

        }

        void DisplayCurrentPage(int InfoSpaceCount, int minIndex) // 인벤토리 그리기(현재 페이지의 아이템 정보)
        {
            int maxIndex = 5 * ShopPage;
            string powerType;
            string equipType;

            var itemList = ItemDatabase.instance.Items.ToList();

            for (int i = 0; i < 5; i++)
            {
                if (i < InfoSpaceCount)
                {
                    EquipmentData tmp = itemList[i + minIndex].Value.GetEquipmentData();
                    equipType = itemList[i + minIndex].Value.GetTypeToString();

                    if (tmp.Type == EquipmentType.Armor || tmp.Type == EquipmentType.Legs || tmp.Type == EquipmentType.Shield)
                    {
                        powerType = "방어력";
                    }
                    else
                    {
                        powerType = "공격력";
                    }

                    if (tmp.IsSoldOut)
                    {
                        Console.WriteLine($"      {i + minIndex + 1} - [매진] [{tmp.Name}] {powerType}: {tmp.PowerValue,-2} | {equipType} | {tmp.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"      {i + minIndex + 1} - [{tmp.Name}] {powerType}: {tmp.PowerValue,-2} | {equipType} | {tmp.Description}");
                    }
                }
                else
                {
                    Console.WriteLine($"      x - [ ] ...              ");
                }
            }
        }




        bool ChangePage(bool nextPage) // 인벤토리 페이지 넘김
        {
            int maxPage = (int)Math.Ceiling(ItemDatabase.instance.Items.Count / 5f);

            if (nextPage)
            {
                // 다음 페이지로 넘어갈 수 있으면
                if (ShopPage < maxPage)
                {
                    ShopPage++;
                    return true;
                }
            }
            else
            {
                // 이전 페이지로 넘어갈 수 있으면
                if (ShopPage > 1)
                {
                    ShopPage--;
                    return true;
                }
            }

            return false;
        }






    }
}
