using DungeonTextRPG.Manager.Game;
using DungeonTextRPG.Manager.VisualText;
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

        public void DisplayPlayerStatus()
        {
            Console.Clear();
            Console.WriteLine("┌────────────────────────────────┐");
            Console.WriteLine("│         [플레이어 상태]        │");
            Console.WriteLine("├────────────────────────────────┤");
            Console.WriteLine($"│  이름  : [  {GameManager.instance.MyPlayer.Name.PadRight(15)}  ] │");
            Console.WriteLine($"│  직업  : [  {GameManager.instance.MyPlayer.PlayerJob.ToString().PadRight(15)}  ] │");
            Console.WriteLine($"│  레벨  : [  Lv. {GameManager.instance.MyPlayer.Level.ToString().PadRight(11)}  ] │");
            Console.WriteLine($"│  경험치: [  1200 / 2000 ] │");
            Console.WriteLine("├────────────────────────────────┤");
            Console.WriteLine($"│  체력  : [  85 / 100  ] │");
            Console.WriteLine($"│  공격력: [  {GameManager.instance.MyPlayer.StatAttack.ToString().PadRight(15)}  ] │");
            Console.WriteLine($"│  방어력: [  {GameManager.instance.MyPlayer.StatDefense.ToString().PadRight(15)}  ] │");
            Console.WriteLine($"│  골드  : [  {GameManager.instance.MyPlayer.GoldAmount.ToString().PadRight(14)} G ] │");
            Console.WriteLine("└────────────────────────────────┘");
            VisualTextManager.instance.DrawPainting(PaintingUI.Divider);
        }


    }
}
