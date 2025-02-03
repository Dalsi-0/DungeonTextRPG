using DungeonTextRPG.Manager;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System;
using System.Net;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


public class CsvReader
{
    private string _filePath;

    public CsvReader(string filePath)
    {
        _filePath = filePath;
    }

    public List<string[]> ReadCSV()
    {
        List<string[]> data = new List<string[]>();

        using (StreamReader reader = new StreamReader(_filePath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(','); // 쉼표(,) 기준으로 분리
                data.Add(values);
            }
        }
        return data;
    }

    // 구글 스프레드시트

    const string URL_itemsSheet = "https://docs.google.com/spreadsheets/d/1It4-oR-oFmeYBxu8bO_hokMhEJwK96By3014bM6gt5c/export?format=tsv&range=A1:F14"; // items
    const string URL_dungeonSheet = "https://docs.google.com/spreadsheets/d/1It4-oR-oFmeYBxu8bO_hokMhEJwK96By3014bM6gt5c/export?format=tsv&gid=1233562025&range=A1:C4"; // Info dungeon 
    const string URL_savefilePathSheet = "https://docs.google.com/spreadsheets/d/1It4-oR-oFmeYBxu8bO_hokMhEJwK96By3014bM6gt5c/export?format=tsv&gid=112729730&range=A1:D2"; // savefilePath


    public class TSVLoader
    {
        public List<List<string>> Data { get; private set; } = new List<List<string>>();

        public TSVLoader()
        {
            LoadTSVData();
        }

        private async void LoadTSVData()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // 비동기적으로 모든 데이터를 동시에 가져옵니다.
                    var taskItems = client.GetStringAsync(URL_itemsSheet);
                    var taskSavefilePath = client.GetStringAsync(URL_savefilePathSheet);
                    var taskDungeon = client.GetStringAsync(URL_dungeonSheet);

                    // 비동기 작업이 완료될 때까지 기다립니다.
                    await Task.WhenAll(taskItems, taskSavefilePath, taskDungeon);

                    // 각 데이터를 파싱합니다.
                    ParseTSV_Items(taskItems.Result);
                    ParseTSV_Dungeon(taskDungeon.Result);
                    ParseTSV_SavefilePath(taskSavefilePath.Result);

                    GameManager.instance.isLoadData = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("TSV 데이터 로드 실패: " + e.Message);
            }
        }
        private void ParseTSV_Items(string tsv)
        {
            string[] rows = tsv.Split('\n');

            bool isFirstRow = true; // 첫 번째 행이 헤더이므로, 첫 번째 행을 건너뛰도록 설정

            foreach (var row in rows)
            {
                // 빈 행을 처리하기 위해서 필터링
                if (string.IsNullOrWhiteSpace(row)) continue;

                // 각 항목이 탭으로 구분되어 있는 데이터라면
                string[] cols = row.Trim().Split('\t');

                // 첫 번째 행은 헤더이므로 건너뜁니다
                if (isFirstRow)
                {
                    isFirstRow = false;
                    continue;
                }

                string code = cols[0].Trim(); // Code
                if (ItemDatabase.instance.Items.ContainsKey(code))
                {
                    continue;
                }
                string name = cols[1].Trim(); // Name
                string typeStr = cols[2].Trim().ToLower(); // Type
                int powerValue = int.Parse(cols[3].Trim()); // PowerValue
                string description = cols[4].Trim(); // Des
                int price = int.Parse(cols[5].Trim()); // Price

                EquipmentData _Data = new EquipmentData
                {
                    Code = code,
                    Name = name,
                    Type = SwitchEnumType(typeStr),
                    PowerValue = powerValue,
                    Description = description,
                    Price = price
                };

                ItemDatabase.instance.EquipmentFactory(_Data);
            }
        }

        EquipmentType SwitchEnumType(string tmp)
        {
            switch (tmp)
            {
                case "onehand":
                    return EquipmentType.One_HandedWeapon;
                    break;

                case "twohand":
                    return EquipmentType.Two_HandedWeapon;
                    break;

                case "armor":
                    return EquipmentType.Armor;
                    break;

                case "legs":
                    return EquipmentType.Legs;
                    break;

                default:
                    return EquipmentType.None;
                    break;
            }
        }


        private void ParseTSV_Dungeon(string tsv)
        {
            List<string> dungeonName = new List<string>();
            List<int> dungeonRecommendedDef = new List<int>();

            string[] rows = tsv.Split('\n');

            bool isFirstRow = true; // 첫 번째 행이 헤더이므로, 첫 번째 행을 건너뛰도록 설정

            foreach (var row in rows)
            {
                // 빈 행을 처리하기 위해서 필터링
                if (string.IsNullOrWhiteSpace(row)) continue;

                // 각 항목이 탭으로 구분되어 있는 데이터라면
                string[] cols = row.Trim().Split('\t');

                // 첫 번째 행은 헤더이므로 건너뜁니다
                if (isFirstRow)
                {
                    isFirstRow = false;
                    continue;
                }

                // DungeonName은 cols[1], DungeonRecommendedDef는 cols[2]에 해당
                for (int i = 0; i < cols.Length; i++)
                {
                    if(i == 0)
                    {
                        dungeonName.Add(cols[1]); 
                    }
                    else if (i == 1) 
                    {
                        dungeonRecommendedDef.Add(int.Parse(cols[2])); 
                    }
                }

            }
            DungeonManager.instance.SetDungeonInfo(dungeonName.ToArray(), dungeonRecommendedDef.ToArray());
        }
        private void ParseTSV_SavefilePath(string tsv)
        {
            List<string> savefilePath = new List<string>();

            string[] rows = tsv.Split('\n');

            bool isFirstRow = true; // 첫 번째 행이 헤더이므로, 첫 번째 행을 건너뛰도록 설정

            foreach (var row in rows)
            {
                // 빈 행을 처리하기 위해서 필터링
                if (string.IsNullOrWhiteSpace(row)) continue;

                // 각 항목이 탭으로 구분되어 있는 데이터라면
                string[] cols = row.Trim().Split('\t');

                // 첫 번째 행은 헤더이므로 건너뜁니다
                if (isFirstRow)
                {
                    isFirstRow = false;
                    continue;
                }

                // 데이터를 처리하는 부분
                for (int i = 0; i < cols.Length; i++)
                {
                    savefilePath.Add(cols[i]);
                }
            }
            SaveLoadManager.instance.SetSavefilePath(savefilePath.ToArray());
        }


    }
}
