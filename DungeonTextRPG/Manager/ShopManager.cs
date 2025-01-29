using DungeonTextRPG.Manager.Status;
using System;

namespace DungeonTextRPG.Manager.Shop
{
    public class ShopManager
    {
        private static ShopManager _instance;

        public static ShopManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ShopManager();
                }
                return _instance;
            }
        }

        private ShopManager()
        {
        }
    }
}
