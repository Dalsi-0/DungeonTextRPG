using DungeonTextRPG.Manager.Game;
using DungeonTextRPG.Manager.Inventory;
using DungeonTextRPG.Manager.VisualText;

namespace DungeonTextRPG.Manager.Status
{
    public class StatusManager
    {
        private static StatusManager _instance;

        public static StatusManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StatusManager();
                }
                return _instance;
            }
        }

        private StatusManager()
        {
        }

        public float sumAttackPower;
        public float sumDefensePower;

        public void DisplayPlayerStatus()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine( "    ┌────────────────────────────────┐");
            Console.WriteLine( "    │         [플레이어 상태]        │");
            Console.WriteLine( "    ├────────────────────────────────┤");
            Console.WriteLine($"    │  이름  : [  {GameManager.instance.MyPlayer.Name.PadRight(15)}  ] │");
            Console.WriteLine($"    │  직업  : [  {GameManager.instance.MyPlayer.PlayerJob.ToString().PadRight(15)}  ] │");
            Console.WriteLine($"    │  레벨  : [  Lv. {GameManager.instance.MyPlayer.Level.ToString().PadRight(11)}  ] │");
            Console.WriteLine( "    ├────────────────────────────────┤");
            Console.WriteLine($"    │  체력  : [  {GameManager.instance.MyPlayer.Health.ToString().PadRight(8)} / 100   ] │");
            Console.WriteLine($"    │  공격력: [  {(GameManager.instance.MyPlayer.StatAttack + sumAttackPower).ToString().PadRight(8)} +({sumAttackPower.ToString().PadLeft(3)})  ] │");
            Console.WriteLine($"    │  방어력: [  {(GameManager.instance.MyPlayer.StatDefense + sumDefensePower).ToString().PadRight(8)} +({sumDefensePower.ToString().PadLeft(3)})  ] │");
            Console.WriteLine($"    │  골드  : [  {GameManager.instance.MyPlayer.GoldAmount.ToString().PadRight(14)}G  ] │");
            Console.WriteLine( "    └────────────────────────────────┘");
            VisualTextManager.instance.DrawPainting(PaintingUI.Divider_x2);

            Console.WriteLine($" {GameManager.instance.MyPlayer.Name} : 어서 던전에 들어가서 돈이나 벌자...");

            int resultValue = GameManager.instance.PromptUserAction("나가기");

            if (resultValue == 1)
            {
                GameManager.instance.VillageMenu();
            }
        }

        public void UpdateStats() // 장비의 능력치 종합
        {
            sumAttackPower = 0;
            sumDefensePower = 0;

            // 각 장비 슬롯에 대한 능력치를 업데이트
            UpdateSlotStats("righthand");
            UpdateSlotStats("lefthand");
            UpdateSlotStats("armor");
            UpdateSlotStats("legs");
        }

        void UpdateSlotStats(string slot)
        {
            if (InventoryManager.instance.SlotStatus[slot] == null)
            {
                return; // 장비가 없는 슬롯은 처리하지 않음
            }

            EquipmentData tmp = InventoryManager.instance.SlotStatus[slot].GetEquipmentData();

            // 능력치를 추가하는 함수 호출
            if (IsAttackItem(tmp))
            {
                AddAttackPower(tmp);
            }
            else
            {
                AddDefensePower(tmp);
            }
        }

        bool IsAttackItem(EquipmentData _data) // true: 공격력 아이템, false: 방어력 아이템
        {
            switch (_data.Type)
            {
                case EquipmentType.One_HandedWeapon:
                case EquipmentType.Two_HandedWeapon:
                    return true;

                case EquipmentType.Shield:
                case EquipmentType.Armor:
                case EquipmentType.Legs:
                    return false;

                default:
                    return false;
            }
        }

        void AddAttackPower(EquipmentData item)
        {
            if (item.Type == EquipmentType.Two_HandedWeapon)
            {
                sumAttackPower += item.PowerValue / 2f; // 두 손 무기는 반값으로 처리
            }
            else
            {
                sumAttackPower += item.PowerValue;
            }
        }

        void AddDefensePower(EquipmentData item)
        {
            sumDefensePower += item.PowerValue;
        }
    }
}
