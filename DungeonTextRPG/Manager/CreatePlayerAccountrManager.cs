namespace DungeonTextRPG.Manager
{
    public class CreatePlayerAccountrManager
    {
        private static CreatePlayerAccountrManager _instance;
        public static CreatePlayerAccountrManager instance => _instance ??= new CreatePlayerAccountrManager();
        private CreatePlayerAccountrManager()
        {
        }


        public Player SetPlayerAccount() // 최초 플레이어 계정 생성
        {
            // 이름 설정
            string name;
            while (true)
            {
                VisualTextManager.instance.DrawPainting(PaintingUI.Title);
                VisualTextManager.instance.DrawPainting(PaintingUI.Divider);
                Console.WriteLine(" 던전 텍스트 RPG에 오신 여러분 환영합니다.");
                Console.WriteLine(" 새롭게 모험을 떠날 플레이어의 이름을 입력해주세요.");
                VisualTextManager.instance.DrawPainting(PaintingUI.Divider);

                Console.Write(" >> ");
                name = Console.ReadLine();

                // 문자열이 비어 있지 않은지 확인
                if (!string.IsNullOrWhiteSpace(name))
                {
                    VisualTextManager.instance.DrawPainting(PaintingUI.Divider);
                    Console.WriteLine($" 입력하신 이름은 {name}입니다.");
                    Console.WriteLine(" 정말로 사용하시겠습니까?");

                    if (GameManager.instance.PromptUserAction("저장/취소") == 1) break;
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(" 이름을 제대로 입력해주세요. 다시 시도해주세요.");
                }
            }

            Job playerJob = SelectPlayerJob(name);
            Player player = new Player(1, name, playerJob, 10, 5, 100, 1500, 0);
            InventoryManager.instance.SetupInitialEquipment();

            return player;
        }

        private Job SelectPlayerJob(string name)
        {
            while (true)
            {
                Console.Clear();
                VisualTextManager.instance.DrawPainting(PaintingUI.Title);
                VisualTextManager.instance.DrawPainting(PaintingUI.Divider);
                Console.WriteLine($" 플레이어 [{name}]의 직업을 설정합니다.");
                int choice = GameManager.instance.PromptUserAction("전사/기사/용병");
                Console.WriteLine();
                VisualTextManager.instance.DrawPainting(PaintingUI.Divider);

                string selectedJob = "";
                Job job = Job.None;

                switch (choice)
                {
                    case 1: 
                        selectedJob = "전사"; 
                        job = Job.Warrior; 
                        break;
                    case 2: 
                        selectedJob = "기사";
                        job = Job.Knight;
                        break;
                    case 3: 
                        selectedJob = "용병"; 
                        job = Job.Mercenary; 
                        break;
                }

                Console.WriteLine($" 선택하신 직업은 {selectedJob}입니다.");
                Console.WriteLine(" 정말로 사용하시겠습니까?");
                if (GameManager.instance.PromptUserAction("저장/취소") == 1)
                {
                    Console.Clear();
                    return job;
                }
            }
        }







    }
}