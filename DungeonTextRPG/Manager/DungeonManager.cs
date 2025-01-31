using DungeonTextRPG.Manager.CreatePlayerAccount;
using DungeonTextRPG.Manager.Status;
using System;

namespace DungeonTextRPG.Manager.Dungeon
{
    public class DungeonManager
    {
        private static DungeonManager _instance;

        public static DungeonManager instance => _instance ??= new DungeonManager();

        private DungeonManager()
        {
        }
    }
}
