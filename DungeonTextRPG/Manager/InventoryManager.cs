using DungeonTextRPG.Manager.Game;
using DungeonTextRPG.Manager.Status;
using DungeonTextRPG.Manager.VisualText;
using System.Collections.Generic;
using System.Data;

namespace DungeonTextRPG.Manager.Inventory
{
    public class InventoryManager
    {
        private static InventoryManager _instance;

        public static InventoryManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InventoryManager();
                }
                return _instance;
            }
        }

        private InventoryManager()
        {
        }

        // 장착칸
        public Dictionary<string, EquipmentItem> SlotStatus = new Dictionary<string, EquipmentItem>
        {
              { "armor", null }, // 방어구 슬롯 (초기에는 null)
              { "lefthand", null }, // 왼쪽 손 슬롯 (초기에는 null)
              { "righthand", null }, // 오른쪽 손 슬롯 (초기에는 null)
              { "legs", null } // 다리 슬롯 (초기에는 null)
        };

        // 현재 인벤토리
        public List<EquipmentItem> MyInventory = new List<EquipmentItem>();

        int MyInventoryPage = 1; // 현재 페이지

        

        #region 인벤토리 뷰어
        public void DisplayPlayerInventory(bool myInventoryChanged, bool isDenied, bool onSale) // 인벤토리 그리기 및 행동 결정
        {
            if (!myInventoryChanged)
            {
                MyInventoryPage = 1;
            }

            Console.Clear();

            DrawInventoryItem();
            VisualTextManager.instance.DrawPainting(PaintingUI.Divider_x2);

            if (onSale)
            {
                Console.WriteLine(" 장착/해제할 아이템의 번호를 입력하세요.");

                int resultValue = GameManager.instance.PromptUserAction("1번 장비/2번 장비/3번 장비/4번 장비/5번 장비/나가기");

                switch (resultValue)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        EquipOrUnequipItem(resultValue);
                        DisplayPlayerInventory(true, true, true);
                        break;

                    case 6: // 나가기
                        DisplayPlayerInventory(true, true, false);
                        break;
                }
            }
            else
            {
                if (!isDenied)
                {
                    Console.WriteLine(" 그 행동은 할 수 없습니다.");
                }
                Console.WriteLine(" 현재 보유한 아이템을 확인하고 장착/해제할 수 있습니다.");

                int resultValue = GameManager.instance.PromptUserAction("장착,해제하기/이전 페이지/다음 페이지/나가기");

                switch (resultValue)
                {
                    case 1: // 장착 또는 해제하기
                        DisplayPlayerInventory(true, true, true);
                        break;

                    case 2: // 이전 페이지
                        DisplayPlayerInventory(true, ChangePage(false), false);
                        break;

                    case 3: // 다음 페이지
                        DisplayPlayerInventory(true, ChangePage(true), false);
                        break;

                    case 4: // 나가기
                        GameManager.instance.VillageMenu();
                        break;

                }
            }
        }

        void DrawInventoryItem()
        {
            int minIndex = (5 * MyInventoryPage) - 5;

            int maxPage = (int)Math.Ceiling(MyInventory.Count / 5f);

            // 한 페이지에 출력되는 아이템 칸 갯수
            int InfoSpaceCount = MyInventory.Count - minIndex;

            Console.WriteLine();
             Console.WriteLine("    ┌────────────────────────────────────────────────────────────────────────┐");
             Console.WriteLine("    │                                [인벤토리]                              │");
            Console.WriteLine($"    │                               골드: {GameManager.instance.MyPlayer.GoldAmount, +5} G                            │");
             Console.WriteLine("    ├────────────────────────────────────────────────────────────────────────┤");

            DisplayCurrentPage(InfoSpaceCount, minIndex);

            Console.WriteLine($"    ├────────────────────────────────────────────────────────────────────────┤");
            Console.WriteLine($"    │                               [{MyInventoryPage}/{maxPage}] 페이지                             │");
             Console.WriteLine("    └────────────────────────────────────────────────────────────────────────┘");
        } // 인벤토리 그리기

        void DisplayCurrentPage(int InfoSpaceCount, int minIndex) // 인벤토리 그리기(현재 페이지의 아이템 정보)
        {
            int maxIndex = 5 * MyInventoryPage;
            string powerType;

            for (int i = 0; i < 5; i++)
            {
                if (i < InfoSpaceCount)
                {
                    EquipmentData tmp = MyInventory[i + minIndex].GetEquipmentData();

                    if (tmp.Type == EquipmentType.Armor || tmp.Type == EquipmentType.Legs || tmp.Type == EquipmentType.Shield)
                    {
                        powerType = "방어력";
                    }
                    else
                    {
                        powerType = "공격력";
                    }

                    if (tmp.isEquiped)
                    {
                        Console.WriteLine($"      {i + minIndex + 1} - [E] [{tmp.Name}] {powerType}: {tmp.PowerValue,-2} | {tmp.Description}");
                    }
                    else
                    {
                        Console.WriteLine($"      {i + minIndex + 1} - [{tmp.Name}] {powerType}: {tmp.PowerValue,-2} | {tmp.Description}");
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
            int maxPage = (int)Math.Ceiling(MyInventory.Count / 5f);

            if (nextPage)
            {
                // 다음 페이지로 넘어갈 수 있으면
                if (MyInventoryPage < maxPage)
                {
                    MyInventoryPage++;
                    return true;
                }
            }
            else
            {
                // 이전 페이지로 넘어갈 수 있으면
                if (MyInventoryPage > 1)
                {
                    MyInventoryPage--;
                    return true;
                }
            }

            return false;
        }

        #endregion


        #region 장비 착용/해제
        public void SetupInitialEquipment() // 초기 장비 설정 함수
        {
            EquipmentItem initEquip1 = ItemDatabase.instance.GetItem("Armor_1_raggedClothes");
            EquipmentItem initEquip2 = ItemDatabase.instance.GetItem("OneHandedWeapon_1_dagger");
            MyInventory.Add(initEquip1);
            MyInventory.Add(initEquip2);

            initEquip1.SetEquippedState(true);
            initEquip2.SetEquippedState(true);
            EquipItemToSlot(initEquip1, initEquip1.GetEquipmentData().Type);
            EquipItemToSlot(initEquip2, initEquip2.GetEquipmentData().Type);
        }

        void EquipOrUnequipItem(int itemNumber)
        {

            // 현재 페이지에서 선택한 번호에 해당하는 아이템 가져오기
            int minIndex = (5 * MyInventoryPage) - 5;

            if (MyInventory.Count <= itemNumber - 1 + minIndex) // 장비가 없는 칸 선택시
            {
                DisplayPlayerInventory(true, false, true);
                return;
            }

            EquipmentItem itemToToggle = MyInventory[itemNumber - 1 + minIndex];

            // 아이템 상태가 장착 상태인지 확인하고, 반대로 설정 (장착 -> 해제, 해제 -> 장착)
            bool isEquipped = itemToToggle.GetEquipmentData().isEquiped;
            itemToToggle.SetEquippedState(!isEquipped);
            EquipmentData itemData = itemToToggle.GetEquipmentData();

            // 장착 또는 해제 처리
            if (itemData.isEquiped)
            {
                EquipItemToSlot(itemToToggle, itemData.Type);
            }
            else
            {
                UnequipItemFromSlot(itemToToggle, itemData.Type);
            }

            // 인벤토리 새로고침
            DisplayPlayerInventory(true, true, true);
        }

        void EquipItemToSlot(EquipmentItem item, EquipmentType type)
        {
            // 아이템을 해당 슬롯에 장착하는 처리
            switch (type)
            {
                case EquipmentType.One_HandedWeapon:
                case EquipmentType.Shield:
                    // OneHandedWeapon 또는 Shield를 장착하는 처리
                    if (SlotStatus["righthand"] == null)
                    {
                        SlotStatus["righthand"] = item;
                    }
                    else if (SlotStatus["lefthand"] == null)
                    {
                        SlotStatus["lefthand"] = item;
                    }
                    else
                    {
                        SlotStatus["righthand"] = item;
                    }
                    break;

                case EquipmentType.Two_HandedWeapon:
                    SlotStatus["righthand"] = item;
                    SlotStatus["lefthand"] = item;
                    break;

                case EquipmentType.Armor:
                    SlotStatus["armor"] = item;
                    break;

                case EquipmentType.Legs:
                    SlotStatus["legs"] = item;
                    break;
            }

            StatusManager.instance.UpdateStats();
        }

        void UnequipItemFromSlot(EquipmentItem item, EquipmentType type)
        {
            // 아이템을 해당 슬롯에서 해제하는 처리
            switch (type)
            {
                case EquipmentType.One_HandedWeapon:
                case EquipmentType.Shield:
                    if (SlotStatus["righthand"] == item)
                    {
                        SlotStatus["righthand"] = null;
                    }
                    if (SlotStatus["lefthand"] == item)
                    {
                        SlotStatus["lefthand"] = null;
                    }
                    break;

                case EquipmentType.Two_HandedWeapon:
                    SlotStatus["righthand"] = null;
                    SlotStatus["lefthand"] = null;
                    break;

                case EquipmentType.Armor:
                    SlotStatus["armor"] = null;
                    break;

                case EquipmentType.Legs:
                    SlotStatus["legs"] = null;
                    break;
            }

            StatusManager.instance.UpdateStats();
        }
        #endregion


    }
}
