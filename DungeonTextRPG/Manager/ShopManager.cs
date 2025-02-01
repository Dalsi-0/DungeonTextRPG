using DungeonTextRPG.Manager.Game;
using DungeonTextRPG.Manager.Inventory;
using DungeonTextRPG.Manager.Status;
using DungeonTextRPG.Manager.VisualText;
using System;
using System.Net.WebSockets;

namespace DungeonTextRPG.Manager.Shop
{
    public class ShopManager
    {
        private static ShopManager _instance;

        public static ShopManager instance => _instance ??= new ShopManager();

        private ShopManager()
        {
        }


        int ShopPage = 1; // 현재 페이지

        #region 상점 뷰어
        public void DisplayShop(string message, bool pageChanged, bool buyItem)
        {
            if (!pageChanged) ShopPage = 1;
            Console.Clear();
            VisualTextManager.instance.DrawPainting(PaintingVillage.Shop);
            DrawShopItem();
            VisualTextManager.instance.DrawPainting(PaintingUI.Divider_x2);

            if(message != "")
            {
                Console.WriteLine($" {message}");
            }
            if (buyItem)
            {
                Console.WriteLine(" 구매할 아이템 번호를 입력하세요.");
                Console.WriteLine(" 상점 주인 : 어떤 장비를 살텐가?");
                HandleBuySelection(GameManager.instance.PromptUserAction("1번 장비/2번 장비/3번 장비/4번 장비/5번 장비/뒤로 가기"));
            }
            else
            {
                Console.WriteLine(" 상점 주인 : 어서오게나, 좋은 장비가 많다네");
                HandleShopSelection(GameManager.instance.PromptUserAction("아이템 구매/아이템 판매/이전 페이지/다음 페이지/나가기"));
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
            var itemList = ItemDatabase.instance.Items.ToList();

            for (int i = 0; i < 5; i++)
            {
                if (i < InfoSpaceCount)
                {
                    EquipmentData tmp = itemList[i + minIndex].Value.GetEquipmentData();
                    string equipType = itemList[i + minIndex].Value.GetTypeToString();
                    string powerType = tmp.Type is EquipmentType.Armor or EquipmentType.Legs or EquipmentType.Shield ? "방어력" : "공격력";
                    string soldOut = tmp.isSoldOut ? "[매진]" : "";
                    Console.WriteLine($"      {i + minIndex + 1} - {soldOut} [{tmp.Name}] {powerType}: {tmp.PowerValue,-2} | {equipType} | {tmp.Description} | {tmp.Price} G");
                }
                else
                {
                    Console.WriteLine($"      x - [ ] ...              ");
                }
            }
        }

        void HandleShopSelection(int resultValue)
        {
            switch (resultValue)
            {
                case 1: DisplayShop("", true, true); break; // 아이템 구매
                case 2:  break; // 아이템 판매
                case 3: // 이전 페이지
                    bool canChagePre = ChangePage(false);
                    DisplayShop(canChagePre ? "" : " 페이지가 없습니다.", canChagePre, false);
                    break;
                case 4: // 다음 페이지
                    bool canChageNext = ChangePage(true);
                    DisplayShop(canChageNext ? "" : " 페이지가 없습니다.", canChageNext, false);
                    break;
                case 5: GameManager.instance.VillageMenu(); break;// 나가기
            }
        }

        private bool ChangePage(bool nextPage) // 인벤토리 페이지 넘김
        {
            int maxPage = (int)Math.Ceiling(ItemDatabase.instance.Items.Count / 5f);
            if (nextPage && ShopPage < maxPage) { ShopPage++; return true; }
            if (!nextPage && ShopPage > 1) { ShopPage--; return true; }
            return false;
        }
        #endregion

        #region 장비 구매/판매

        void HandleBuySelection(int resultValue) // 아이템 구매
        {
            if (resultValue >= 1 && resultValue <= 5)
            {
                BuyItem(resultValue);
            }
            else if (resultValue == 6) // 뒤로 가기
            {
                DisplayShop("", true, false);
            }
        }

        void BuyItem(int itemNumber)
        {
            var itemList = ItemDatabase.instance.Items.ToList();
            // 현재 페이지에서 선택한 번호에 해당하는 아이템 가져오기
            int minIndex = (5 * ShopPage) - 5;
            if (ItemDatabase.instance.Items.Count <= itemNumber - 1 + minIndex) { DisplayShop(" 잘못된 입력입니다. 다시 시도해주세요.", true, false); return; } // 장비가 없는 칸 선택시
            
            EquipmentItem item = itemList[itemNumber - 1 + minIndex].Value;
            EquipmentData itemData = item.GetEquipmentData();

            // 아이템 상태가 매진 상태인지 확인하고 매진이 아닐경우만 판매
            if (item.GetEquipmentData().isSoldOut)
            {
                DisplayShop(" 이미 구매한 아이템입니다.", true, true);
            }
            else if (GameManager.instance.MyPlayer.GoldAmount >= itemData.Price)
            {
                GameManager.instance.MyPlayer.GoldAmount -= itemData.Price;
                item.SetSoldOutState(true);
                InventoryManager.instance.MyInventory.Add(ItemDatabase.instance.GetItem(itemData.Code));
                DisplayShop(" 구매를 완료했습니다.", true, true);
            }
            else if(GameManager.instance.MyPlayer.GoldAmount < itemData.Price)
            {
                DisplayShop(" 골드가 부족합니다.", true, true);
            }
        }







        void HandleSellSelection() // 아이템 판매 
        {

        }

        #endregion



    }
}
