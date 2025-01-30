﻿
using DungeonTextRPG.Manager.Game;
using DungeonTextRPG.Manager.VisualText;
using System;
using System.ComponentModel;
using System.Numerics;
using System.Xml.Linq;
namespace DungeonTextRPG.Manager.CreatePlayerAccount
{
    public class CreatePlayerAccountrManager
    {
        private static CreatePlayerAccountrManager _instance;

        public static CreatePlayerAccountrManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CreatePlayerAccountrManager();
                }
                return _instance;
            }
        }

        private CreatePlayerAccountrManager()
        {
        }


        
        public Player SetPlayerAccount() // 최초 플레이어 계정 생성
        {
            // 이름 설정
            string name;
            while (true)
            {
                VisualTextManager.instance.DrawPainting(PaintingUI.Divider);
                Console.WriteLine(" 던전 텍스트 RPG에 오신 여러분 환영합니다.");
                Console.WriteLine(" 플레이어의 이름을 입력해주세요.");
                VisualTextManager.instance.DrawPainting(PaintingUI.Divider);

                Console.Write(" >> ");
                name = Console.ReadLine();

                // 문자열이 비어 있지 않은지 확인
                if (!string.IsNullOrWhiteSpace(name))
                {
                    VisualTextManager.instance.DrawPainting(PaintingUI.Divider);
                    Console.WriteLine($" 입력하신 이름은 {name}입니다.");
                    Console.WriteLine(" 정말로 사용하시겠습니까?");

                    int resultValue = GameManager.instance.PromptUserAction("저장/취소");

                    Console.Clear();
                    if (resultValue == 1) { break; }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(" 이름을 제대로 입력해주세요. 다시 시도해주세요.");
                }
            }

            // 직업 설정
            Job playerJob = Job.None;
            while (true)
            {
                VisualTextManager.instance.DrawPainting(PaintingUI.Divider);
                Console.WriteLine($" 플레이어 [{name}]의 직업을 설정합니다.");

                int resultValue_1 = GameManager.instance.PromptUserAction("전사/기사/용병");

                Console.WriteLine();
                VisualTextManager.instance.DrawPainting(PaintingUI.Divider);

                string selectedJob = "";

                switch (resultValue_1)
                {
                    case 1:
                        selectedJob = "전사";
                        playerJob = Job.Warrior;
                        break;
                    case 2:
                        selectedJob = "기사";
                        playerJob = Job.Knight;
                        break;
                    case 3:
                        selectedJob = "용병";
                        playerJob = Job.Mercenary;
                        break;
                }

                Console.WriteLine($" 선택하신 직업은 {selectedJob}입니다.");
                Console.WriteLine(" 정말로 사용하시겠습니까?");

                int resultValue_2 = GameManager.instance.PromptUserAction("저장/취소");

                Console.Clear();
                if (resultValue_2 == 1) { break; }
            }

            Player player = new Player(1, name, playerJob, 10, 5, 100, 1500);

            return player;
        }








    }
}