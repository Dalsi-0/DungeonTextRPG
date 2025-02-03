using Newtonsoft.Json;
using System.Text;

namespace DungeonTextRPG.Manager
{
    public class SaveLoadManager
    {
        private static SaveLoadManager _instance;

        public static SaveLoadManager instance => _instance ??= new SaveLoadManager();

        private SaveLoadManager() { }

        // 파일 경로 지정
        string filePath_Player = "saveData_Player.json";
        string filePath_Inventory = "saveData_Inventory.json";
        string filePath_Equip = "saveData_Equip.json";
        string filePath_Shop = "saveData_Shop.json";

        public void SaveData()
        {
            if (GameManager.instance.MyPlayer == null) { return; }


            /// player 저장
            string json_Player = JsonConvert.SerializeObject(GameManager.instance.MyPlayer, Formatting.Indented);
            File.WriteAllText(filePath_Player, json_Player, Encoding.UTF8);


            /// 인벤토리 저장
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
            }; 
            string json_Inventory = JsonConvert.SerializeObject(InventoryManager.instance.MyInventory, settings);
            File.WriteAllText(filePath_Inventory, json_Inventory, Encoding.UTF8);


            /// 장착아이템 저장
            string json_Equip = JsonConvert.SerializeObject(InventoryManager.instance.SlotStatus, settings);
            File.WriteAllText(filePath_Equip, json_Equip, Encoding.UTF8);


            /// 상점 진열 아이템 매진 현황 저장
            string json_Shop = JsonConvert.SerializeObject(ItemDatabase.instance.Items, settings);
            File.WriteAllText(filePath_Shop, json_Shop, Encoding.UTF8);
            

            Console.WriteLine("게임 데이터를 저장했습니다.");
        }

        public bool LoadData() // 저장파일 로드
        {

            if (File.Exists(filePath_Player))
            {
                Console.WriteLine("저장 파일이 존재합니다.");
                ApplyLoadedData();

                Console.WriteLine("계속하려면 아무 키나 누르세요...");
                Console.ReadKey(); // 콘솔 종료 방지

                Console.Clear();

                return true;
            }
            else
            {
                Console.WriteLine("저장 파일이 존재하지 않습니다.");

                Console.WriteLine("게임을 시작하려면 아무 키나 누르세요...");
                Console.ReadKey(); // 콘솔 종료 방지

                Console.Clear();
                
                return false;
            }
        }

        void ApplyLoadedData()
        {
            /// player
            string jsonData_Player = File.ReadAllText(filePath_Player);
            GameManager.instance.MyPlayer = JsonConvert.DeserializeObject<Player>(jsonData_Player);

            
            /// inventory
            string jsonData_Inventory = File.ReadAllText(filePath_Inventory);
            // 역직렬화 시 TypeNameHandling.Auto를 사용하여 타입 정보도 처리
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            InventoryManager.instance.MyInventory = JsonConvert.DeserializeObject<List<EquipmentItem>>(jsonData_Inventory, settings);


            /// Equip
            string jsonData_Equip = File.ReadAllText(filePath_Equip);
            InventoryManager.instance.SlotStatus = JsonConvert.DeserializeObject<Dictionary<string, EquipmentItem>>(jsonData_Equip, settings);


            /// Shop
            string jsonData_Shop = File.ReadAllText(filePath_Shop);
            ItemDatabase.instance.Items = JsonConvert.DeserializeObject<Dictionary<string, EquipmentItem>>(jsonData_Shop, settings);
        }
    }
}
