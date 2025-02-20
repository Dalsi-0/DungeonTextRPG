﻿using DungeonTextRPG.Manager;
using static TSVReader;

namespace DungeonTextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 프로그램 종료 시 실행할 코드 등록
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            new TSVLoader();

            GameManager.instance.StartGame();

        }


        // 프로세스 종료 시 실행될 함수
        static void OnProcessExit(object sender, EventArgs e)
        {
            SaveLoadManager.instance.SaveData();
        }
    }
}
