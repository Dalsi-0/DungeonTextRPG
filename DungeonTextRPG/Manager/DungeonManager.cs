using DungeonTextRPG.Manager.Status;
using System;

namespace DungeonTextRPG.Manager.Dungeon
{
    public class DungeonManager
    {
        private static DungeonManager _instance;

        public static DungeonManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DungeonManager();
                }
                return _instance;
            }
        }

        private DungeonManager()
        {
        }
    }
}
