using DungeonTextRPG.Manager.Game;
using System;

namespace DungeonTextRPG.Manager.Status
{
    public class StatusManager
    {
        private static StatusManager _instance;

        public static StatusManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StatusManager();
                }
                return _instance;
            }
        }

        private StatusManager()
        {
        }
    }
}
