using System;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DungeonTextRPG.Manager.Game;
using Newtonsoft.Json;

namespace DungeonTextRPG.Manager.SaveLoad
{
    public class SaveLoadManager
    {
        private static SaveLoadManager _instance;

        public static SaveLoadManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SaveLoadManager();
                }
                return _instance;
            }
        }

        private SaveLoadManager()
        {
            Console.WriteLine("저장 매니저 생성");
        }


        public void SaveData()
        {
            // Player 객체를 JSON 문자열로 직렬화
            string json = JsonConvert.SerializeObject(GameManager.instance.MyPlayer, Formatting.Indented);

            // 파일 경로 지정
            string filePath = "saveData.json";

            // JSON 데이터를 파일에 저장
            File.WriteAllText(filePath, json, Encoding.UTF8);

            Console.WriteLine("게임 데이터를 저장했습니다.");
        }

        public bool LoadData() // 저장파일 로드
        {
            string filePath = "saveData.json";

            if (File.Exists(filePath))
            {
                Console.WriteLine("저장 파일이 존재합니다.");
                ApplyLoadedData(filePath);

                Console.WriteLine("게임을 시작하려면 아무 키나 누르세요...");
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

        void ApplyLoadedData(string filePath)
        {
            // JSON 데이터를 Player 객체로 변환
            string jsonData = File.ReadAllText(filePath);
            GameManager.instance.MyPlayer = JsonConvert.DeserializeObject<Player>(jsonData);
        }
    }
}
