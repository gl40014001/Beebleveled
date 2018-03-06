using System;
using System.Windows.Media;

namespace Notadesigner.ConwaysLife.Game
{
    public class Constants
    {
        public const double CELL_SIZE = 10;

        public const int CELLS_X = 16;

        public const int CELLS_Y = 16;

        public Colour[] defaultPalette;
        public const int Logical_BLACK = 0;
        public const int Logical_RED = 1;
        public const int Logical_GREEN = 2;
        public const int Logical_YELLOW = 3;
        public const int Logical_BLUE = 4;
        public const int Logical_MAGENTA = 5;
        public const int Logical_CYAN = 6;
        public const int Logical_WHITE = 7;

        public Constants()
        {
            DefaultPalette[0] = Colour.Yellow;
            DefaultPalette[1] = Colour.Green;
            DefaultPalette[2] = Colour.Red;
            DefaultPalette[3] = Colour.Black;

        }



        public enum Colour
        {
            Black = 0,
            Red = 1,
            Green = 2,
            Yellow = 3,
            Blue = 4,
            Magenta = 5,
            Cyan = 6,
            White = 7
        }


        public static SolidColorBrush GetWindowsColour(Colour beebColour)
        {
            switch (beebColour)
            {
                case Colour.Black: return Brushes.Black;
                case Colour.Red: return Brushes.Red;
                case Colour.Green: return Brushes.Green;
                case Colour.Yellow: return Brushes.Yellow;
                case Colour.Blue: return Brushes.Blue;
                case Colour.Magenta: return Brushes.Magenta;
                case Colour.Cyan: return Brushes.Cyan;
                case Colour.White: return Brushes.White;
                default: throw new Exception("Unknown colour");
            }
        }



        public Colour[] DefaultPalette
        {
            get { return this.defaultPalette; }
            set { this.defaultPalette = value; }
        }

    }
}
