public enum PaintingVillage
{
    None,
    Village,
    Shop,
    Hotel,
    Dungeon
}
public enum PaintingUI
{
    None,
    Title,
    Divider,
    Divider_x2,
    DungeonClear,
    DungeonFail
}

namespace DungeonTextRPG.Manager
{
    public class VisualTextManager
    {
        private static VisualTextManager _instance;

        public static VisualTextManager instance => _instance ??= new VisualTextManager();

        private VisualTextManager() { }

        public void DrawPainting(PaintingUI type)
        {
            switch (type)
            {
                case PaintingUI.Title:
                    Console.WriteLine();
                    Console.WriteLine("  □□□□□□□  □□□□□□□    □          □    □□□□□□□   ");
                    Console.WriteLine("        □        □                  □      □            □         ");
                    Console.WriteLine("        □        □                    □  □              □         ");
                    Console.WriteLine("        □        □□□□□□□          □                □         ");
                    Console.WriteLine("        □        □                    □  □              □         ");
                    Console.WriteLine("        □        □                  □      □            □         ");
                    Console.WriteLine("        □        □□□□□□□    □          □          □         ");
                    Console.WriteLine();
                    Console.WriteLine("           □□□□□□     □□□□□□     □□□□□□              ");
                    Console.WriteLine("           □          □   □          □  □                         ");
                    Console.WriteLine("           □          □   □          □  □                         ");
                    Console.WriteLine("           □□□□□□     □□□□□□    □      □□□             ");
                    Console.WriteLine("           □     □        □              □          □             ");
                    Console.WriteLine("           □       □      □              □          □             ");
                    Console.WriteLine("           □         □    □                □□□□□               ");
                    break;

                case PaintingUI.Divider:
                    Console.WriteLine();
                    Console.WriteLine("───────────────────────────────────────────────────────────────────────────────────────────────");
                    Console.WriteLine();
                    break;

                case PaintingUI.Divider_x2:
                    Console.WriteLine();
                    Console.WriteLine("───────────────────────────────────────────────────────────────────────────────────────────────");
                    Console.WriteLine("───────────────────────────────────────────────────────────────────────────────────────────────");
                    Console.WriteLine();
                    break;

                case PaintingUI.DungeonClear:
                    Console.WriteLine();
                    Console.WriteLine("     □□□□□   □   □□□□□□       □□       □□□□□□    ");
                    Console.WriteLine("   □             □   □               □    □     □          □  ");
                    Console.WriteLine("   □             □   □             □        □   □          □  ");
                    Console.WriteLine("   □             □   □□□□□     □□□□□□   □□□□□□    ");
                    Console.WriteLine("   □             □   □             □        □   □      □      ");
                    Console.WriteLine("   □             □   □             □        □   □        □    ");
                    Console.WriteLine("     □□□□□   □   □□□□□□   □        □   □          □  ");
                    Console.WriteLine();
                    break;

                case PaintingUI.DungeonFail:
                    Console.WriteLine();
                    Console.WriteLine("   □□□□□□       □□       □□□   □         ");
                    Console.WriteLine("   □               □    □       □     □         ");
                    Console.WriteLine("   □             □        □     □     □         ");
                    Console.WriteLine("   □□□□□     □□□□□□     □     □         ");
                    Console.WriteLine("   □             □        □     □     □         ");
                    Console.WriteLine("   □             □        □     □     □         ");
                    Console.WriteLine("   □             □        □   □□□   □□□□□ ");
                    Console.WriteLine();
                    break;


            }
        }

        public void DrawPainting(PaintingVillage type)
        {
            Console.Clear();

            switch (type)
            {
                case PaintingVillage.Village:
                    Console.WriteLine();
                    Console.WriteLine("      _________________        _________________        _________________                ");
                    Console.WriteLine("     /    __   ＼＼＼＼＼     /    __   ＼＼＼＼＼     /    __   ＼＼＼＼＼              ");
                    Console.WriteLine("    /    |__|    ＼＼＼＼＼  /    |__|    ＼＼＼＼＼  /    |__|    ＼＼＼＼＼            ");
                    Console.WriteLine("    |             |||||||||  |             |||||||||  |             |||||||||            ");
                    Console.WriteLine("    |             |||||||||  |             |||||||||  |             |||||||||            ");
                    Console.WriteLine("    |    ____     |||||||||  |    ____     |||||||||  |    ____     |||||||||            ");
                    Console.WriteLine("    |   |    |    |||||||||  |   |    |    |||||||||  |   |    |    |||||||||            ");
                    Console.WriteLine("    |___|____|____|||||||||  |___|____|____|||||||||  |___|____|____|||||||||            ");
                    DrawPainting(PaintingUI.Divider_x2);
                    break;

                case PaintingVillage.Shop:
                    Console.WriteLine();
                    Console.WriteLine("                                  _________________                             ");
                    Console.WriteLine("                                 /    __   ＼＼＼＼＼                           ");
                    Console.WriteLine("                                /    |__|    ＼＼＼＼＼                         ");
                    Console.WriteLine("                                |   *    *    |||||||||                         ");
                    Console.WriteLine("                                |  * SHOP *   |||||||||                         ");
                    Console.WriteLine("                                | *  ____  *  |||||||||                         ");
                    Console.WriteLine("                                |  *|    |*   |||||||||                         ");
                    Console.WriteLine("                                |___|____|____|||||||||                         ");
                    DrawPainting(PaintingUI.Divider_x2);
                    break;

                case PaintingVillage.Hotel:
                    Console.WriteLine();
                    Console.WriteLine("                                  _________________                             ");
                    Console.WriteLine("                                 /    __   ＼＼＼＼＼                           ");
                    Console.WriteLine("                                /    |__|    ＼＼＼＼＼                         ");
                    Console.WriteLine("                                | (o oooo o)  |||||||||                         ");
                    Console.WriteLine("                                | (o REST o)  |||||||||                         ");
                    Console.WriteLine("                                | (o ____ o)  |||||||||                         ");
                    Console.WriteLine("                                | (o|    |o)  |||||||||                         ");
                    Console.WriteLine("                                |___|____|____|||||||||                         ");
                    DrawPainting(PaintingUI.Divider_x2);
                    break;

                case PaintingVillage.Dungeon:
                    Console.WriteLine();
                    Console.WriteLine("                               _________________________                        ");
                    Console.WriteLine("                              |   ___________________   |                       ");
                    Console.WriteLine("                              |  |     ________      |  |                       ");
                    Console.WriteLine("                              |  |    | !!!!!! |     |  |                       ");
                    Console.WriteLine("                              |  |    |_DANGER_|     |  |                       ");
                    Console.WriteLine("                              |  |    __________     |  |                       ");
                    Console.WriteLine("                              |  |   |          |    |  |                       ");
                    Console.WriteLine("                              |  |   |          |    |  |                       ");
                    Console.WriteLine("                              |__|___|__________|____|__|                       ");
                    DrawPainting(PaintingUI.Divider_x2);
                    break;

            }
        }



    }
}
