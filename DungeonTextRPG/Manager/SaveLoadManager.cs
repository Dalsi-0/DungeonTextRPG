using System;

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


        bool LoadingScene()
        { 
            bool isFirstPlay = false;

            /*
            string saveFilePath = "saveData.json";

            bool isSaveFileExist = SaveLoadManager.IsSaveFileExist(saveFilePath);
            if (isSaveFileExist)
            {
                Console.WriteLine("저장 파일이 존재합니다.");
            }
            else
            {
                Console.WriteLine("저장 파일이 존재하지 않습니다.");
            }*/


            return isFirstPlay;
        }

    }
}
