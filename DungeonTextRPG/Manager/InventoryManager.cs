using DungeonTextRPG.Manager.Game;
using DungeonTextRPG.Manager.Status;
using DungeonTextRPG.Manager.VisualText;
using System;

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
        // 0:왼쪽손, 1:오른쪽 손, 2:상의, 3:하의
        public EquipmentItem[] MyEquipSlot = new EquipmentItem[4];

        // 현재 인벤토리
        public List<EquipmentItem> MyInventory = new List<EquipmentItem>();

        int MyInventoryPage = 1; // 현재 페이지

        
        public void SetupInitialEquipment() // 초기 장비 설정 함수
        {
            OneHandedWeapon_1_dagger basicEquip_Weapon = new OneHandedWeapon_1_dagger();
            Armor_1_raggedClothes basicEquip_Armor = new Armor_1_raggedClothes();

            basicEquip_Weapon.InitSetting();
            basicEquip_Armor.InitSetting();

            MyInventory.Add(basicEquip_Weapon);
            MyInventory.Add(basicEquip_Armor);
        }


        public void DisplayPlayerInventory(bool myInventoryChanged)
        {
            if (!myInventoryChanged)
            {
                MyInventoryPage = 1;
            }

            Console.Clear();
            DrawInventoryItem();
            VisualTextManager.instance.DrawPainting(PaintingUI.Divider_x2);

            Console.WriteLine("현재 보유한 아이템을 확인하고 장착/해제할 수 있습니다.");

            int resultValue = GameManager.instance.PromptUserAction("장착하기/이전 페이지/다음 페이지/나가기");

            switch (resultValue)
            {
                case 1: // 장착하기

                    break;

                case 2: // 이전 페이지
                    MyInventoryPage--;
                    DisplayPlayerInventory(true);
                    break;

                case 3: // 다음 페이지
                    MyInventoryPage++;
                    DisplayPlayerInventory(true);
                    break;

                case 4: // 나가기
                    GameManager.instance.VillageMenu();
                    break;

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
        }

        void DisplayCurrentPage(int InfoSpaceCount, int minIndex)
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

                    Console.WriteLine($"      {i + minIndex +1} - [{tmp.Name}] {powerType}: {tmp.PowerValue, -2} | {tmp.Description}");
                }
                else
                {
                    Console.WriteLine($"      x - [ ] ...              ");
                }
            }
        }









    }
}
