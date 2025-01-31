﻿using DungeonTextRPG.Manager.Game;
using DungeonTextRPG.Manager.Status;
using DungeonTextRPG.Manager.VisualText;

namespace DungeonTextRPG.Manager.Inventory
{
    public class InventoryManager
    {
        private static InventoryManager _instance;

        public static InventoryManager instance => _instance ??= new InventoryManager();

        private InventoryManager() { }

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
        private int MyInventoryPage = 1; // 현재 페이지

        #region 인벤토리 뷰어
        public void DisplayPlayerInventory(bool inventoryChanged, bool isDenied, bool equipment) // 인벤토리 그리기 및 행동 결정
        {
            if (!inventoryChanged) MyInventoryPage = 1;
            Console.Clear();
            DrawInventoryItem();
            VisualTextManager.instance.DrawPainting(PaintingUI.Divider_x2);

            if (equipment)
            {
                Console.WriteLine(" 장착/해제할 아이템 번호를 입력하세요.");
                int resultValue = GameManager.instance.PromptUserAction("1번 장비/2번 장비/3번 장비/4번 장비/5번 장비/나가기");
                HandleEquipmentSelection(resultValue);
            }
            else
            {
                if (!isDenied) Console.WriteLine(" 그 행동은 할 수 없습니다.");
                Console.WriteLine(" 현재 보유한 아이템을 확인하고 장착/해제할 수 있습니다.");
                int resultValue = GameManager.instance.PromptUserAction("장착,해제하기/이전 페이지/다음 페이지/나가기");
                HandleInventorySelection(resultValue);
            }
        }

        private void HandleEquipmentSelection(int resultValue)
        {
            if (resultValue >= 1 && resultValue <= 5)
            {
                EquipOrUnequipItem(resultValue);
                DisplayPlayerInventory(true, true, true);
            }
            else if (resultValue == 6) // 나가기
            {
                DisplayPlayerInventory(true, true, false);
            }
        }
        private void HandleInventorySelection(int resultValue)
        {
            switch (resultValue)
            {
                case 1: DisplayPlayerInventory(true, true, true); break;  // 장착 또는 해제하기
                case 2: DisplayPlayerInventory(true, ChangePage(false), false); break; // 이전 페이지
                case 3: DisplayPlayerInventory(true, ChangePage(true), false); break; // 다음 페이지
                case 4: GameManager.instance.VillageMenu(); break; // 나가기
            }
        }

        private void DrawInventoryItem() // 인벤토리 그리기
        {
            int minIndex = (5 * MyInventoryPage) - 5;
            int maxPage = (int)Math.Ceiling(MyInventory.Count / 5f);
            Console.WriteLine();
            Console.WriteLine("    ┌───────────────────────────────────────────────────────────────────┐");
            Console.WriteLine("    │                             [인벤토리]                            │");
            Console.WriteLine($"    │                           골드: {GameManager.instance.MyPlayer.GoldAmount,5} G                  │");
            Console.WriteLine("    ├───────────────────────────────────────────────────────────────────┤");
            DisplayCurrentPage(minIndex);
            Console.WriteLine($"    ├───────────────────────────────────────────────────────────────────┤");
            Console.WriteLine($"    │                           [{MyInventoryPage}/{maxPage}] 페이지                           │");
            Console.WriteLine("    └───────────────────────────────────────────────────────────────────┘");
        }

        private void DisplayCurrentPage(int minIndex) // 현재 페이지의 아이템 정보
        {
            int maxIndex = Math.Min(minIndex + 5, MyInventory.Count);
            for (int i = minIndex; i < minIndex + 5; i++)
            {
                if (i < maxIndex)
                {
                    EquipmentData item = MyInventory[i].GetEquipmentData();
                    string equipStatus = item.isEquiped ? "[E] " : "";
                    string powerType = item.Type is EquipmentType.Armor or EquipmentType.Legs or EquipmentType.Shield ? "방어력" : "공격력";
                    Console.WriteLine($"      {i + 1} - {equipStatus}[{item.Name}] {powerType}: {item.PowerValue,-2} | {MyInventory[i].GetTypeToString()} | {item.Description}");
                }
                else
                {
                    Console.WriteLine("      x - [ ] ...");
                }
            }
        }

        private bool ChangePage(bool nextPage) // 인벤토리 페이지 넘김
        {
            int maxPage = (int)Math.Ceiling(MyInventory.Count / 5f);
            if (nextPage && MyInventoryPage < maxPage) { MyInventoryPage++; return true; }
            if (!nextPage && MyInventoryPage > 1) { MyInventoryPage--; return true; }
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

            EquipmentItem item = MyInventory[itemNumber - 1 + minIndex];

            // 아이템 상태가 장착 상태인지 확인하고, 반대로 설정 (장착 -> 해제, 해제 -> 장착)
            item.SetEquippedState(!item.GetEquipmentData().isEquiped);

            if (item.GetEquipmentData().isEquiped)
            {
                EquipItemToSlot(item, item.GetEquipmentData().Type);
            }
            else
            {
                UnequipItemFromSlot(item, item.GetEquipmentData().Type);
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
                    if (SlotStatus["righthand"] == null) SlotStatus["righthand"] = item;
                    else if (SlotStatus["lefthand"] == null) SlotStatus["lefthand"] = item;
                    else SlotStatus["righthand"] = item;
                    break;

                case EquipmentType.Two_HandedWeapon:
                    SlotStatus["righthand"] = SlotStatus["lefthand"] = item;
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
                    if (SlotStatus["righthand"] == item) SlotStatus["righthand"] = null;
                    if (SlotStatus["lefthand"] == item) SlotStatus["lefthand"] = null;
                    break;

                case EquipmentType.Two_HandedWeapon:
                    SlotStatus["righthand"] = SlotStatus["lefthand"] = null;
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
