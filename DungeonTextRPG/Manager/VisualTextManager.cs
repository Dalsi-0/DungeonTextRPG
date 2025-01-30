﻿using DungeonTextRPG.Manager.Game;
using System;
public enum PaintingVillage
{
    None,
    Village,
    Shop,
    Hotel,
    Dungeon
}

namespace DungeonTextRPG.Manager.VisualText
{
    public class VisualTextManager
    {
        private static VisualTextManager _instance;

        public static VisualTextManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new VisualTextManager();
                }
                return _instance;
            }
        }

        private VisualTextManager()
        {
        }

        public void DrawPainting()
        {
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
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine();
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
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine();
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
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine();
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
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    Console.WriteLine();
                    break;

            }
        }



    }
}
