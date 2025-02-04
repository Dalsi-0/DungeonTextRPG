<div align="center">

# ⚔️ DungeonRPG - 콘솔 기반 텍스트 RPG

</div>
<p align="center">
  <img src="https://github.com/Dalsi-0/DungeonTextRPG/blob/main/ReadmeImage/Title.png?raw=true" alt="게임 대표 이미지">
</p>

---

## 📌 프로젝트 개요
**🏷 프로젝트명:** Dungeon Text RPG  
**🛠 개발 환경**: C# (콘솔 기반)  
**🕰️ 개발 기간**: 25.01.31 ~ 25.02.03 (4일)  
**👤 개발자:** 개인 프로젝트  
**🎯 주요 기능:** 상태 보기, 아이템 시스템, 던전 탐험, 상점, 장비 착용, 휴식  

DungeonTextRPG는 **텍스트만으로 이루어진 던전 탐험 RPG**입니다.  
플레이어는 번호 선택지 중 원하는 번호를 입력하여 던전을 탐험하고, 전투를 진행하며, 장비를 획득합니다.  

---

## 🎮 게임 설명
Dungeon Text RPG는 순수 텍스트 기반으로 진행되는 RPG 게임으로  
플레이어는 번호 선택지를 통해 캐릭터를 조종하며 던전을 탐험하여 성장시킵니다.

---

## 🕹️ **플레이 방법**  
1. 🏅 **캐릭터 생성:** 시작 시 기본 장비와 함께 게임 시작
2. 🏡 **마을 탐색:** 여관에서 휴식을 취하고, 상점에서 아이템 구매/판매 가능
3. ⚔️ **던전 탐험:** 던전을 탐험하며 경험치와 아이템 획득
4. 🎒 **장비 관리:** 상점 및 인벤토리를 활용하여 강해지기
5. ❤️ **체력:** HP가 부족하면 던전 탐험 불가

---

## 📸 **스크린샷**  
🔹 *게임 실행 화면*  
![콘솔 실행 화면](https://github.com/Dalsi-0/DungeonTextRPG/blob/main/ReadmeImage/Main.png?raw=true)  

🔹 *던전 장면*  
![던전 화면](https://github.com/Dalsi-0/DungeonTextRPG/blob/main/ReadmeImage/Dungeon.png?raw=true)  

🔹 *상태창*  
![상태창](https://github.com/Dalsi-0/DungeonTextRPG/blob/main/ReadmeImage/Status.png?raw=true)  

🔹 *아이템 및 인벤토리*  
![인벤토리](https://github.com/Dalsi-0/DungeonTextRPG/blob/main/ReadmeImage/Inventory.png?raw=true)  

---

## 🛠 **개발 및 기술적 접근**

### ✅ 사용된 기술
- 개발 언어: C#
- 프레임워크: .NET Console
- 데이터 관리: Google Spreadsheet (TSV 형식)
- 버전 관리: GitHub

### 📊 데이터 연동 (Google Spreadsheet)
이 프로젝트에서는 Google Spreadsheet를 이용하여 아이템 정보, 던전 데이터 등을 관리하고, 이를 코드에서 불러와 활용합니다.

🔗 **사용된 스프레드시트 데이터**
- 아이템 데이터 [(보기)](https://docs.google.com/spreadsheets/d/1It4-oR-oFmeYBxu8bO_hokMhEJwK96By3014bM6gt5c/edit?gid=0#gid=0)
- 세이브 파일 경로 [(보기)](https://docs.google.com/spreadsheets/d/1It4-oR-oFmeYBxu8bO_hokMhEJwK96By3014bM6gt5c/edit?gid=112729730#gid=112729730)
- 던전 정보 [(보기)](https://docs.google.com/spreadsheets/d/1It4-oR-oFmeYBxu8bO_hokMhEJwK96By3014bM6gt5c/edit?gid=1233562025#gid=1233562025)
  
🛠️ **데이터 로딩 방식**
```
// Google Spreadsheet에서 TSV 데이터 가져오기
const string URL_itemsSheet = "https://docs.google.com/spreadsheets/.../export?format=tsv&range=A1:F14";
const string URL_dungeonSheet = "https://docs.google.com/spreadsheets/.../export?format=tsv&gid=1233562025&range=A1:C4";
const string URL_savefilePathSheet = "https://docs.google.com/spreadsheets/.../export?format=tsv&gid=112729730&range=A1:D2";

using (HttpClient client = new HttpClient())
{
    var itemData = await client.GetStringAsync(URL_itemsSheet);
    var dungeonData = await client.GetStringAsync(URL_dungeonSheet);
    var savefilePathData = await client.GetStringAsync(URL_savefilePathSheet);
    
    // 비동기 작업이 완료될 때까지 기다립니다.
    await Task.WhenAll(itemData, dungeonData, savefilePathData);
    ...
}
```
  
### 💻 입력 시스템 (사용자 선택)

사용자가 번호를 입력하여 진행하는 방식으로, 잘못된 입력을 방지하고 직관적인 UI를 출력합니다.

```
public int PromptUserAction(string actionMessages)
{
    int result = 0;
    string[] arrayActionMessages = actionMessages.Split('/');

    while (true)
    {
        Console.WriteLine("====================");

        for (int i = 0; i < arrayActionMessages.Length; i++)
        {
            Console.WriteLine($" {i + 1} - {arrayActionMessages[i]}");
        }

        Console.WriteLine("====================");
        Console.Write(" >> ");

        if (int.TryParse(Console.ReadLine(), out result) && (result > 0 && result <= arrayActionMessages.Length))
        {
            break;
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
        }
    }
    return result;
}
```

👉 **주요 기능:**
- 번호 기반 선택지 시스템
- int.TryParse()를 활용한 유효한 입력 검증
- Console.WriteLine()을 활용한 콘솔 UI 연출