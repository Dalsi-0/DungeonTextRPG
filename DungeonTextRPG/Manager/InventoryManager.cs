using DungeonTextRPG.Manager.Status;
using System;

namespace DungeonTextRPG.Manager.Inventory
{
    public class InventoryManager
    {
        private static InventoryManager _instance;

        public static InventoryManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InventoryManager();
                }
                return _instance;
            }
        }

        private InventoryManager()
        {
        }
    }
}
