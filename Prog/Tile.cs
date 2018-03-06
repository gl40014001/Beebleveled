using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notadesigner.ConwaysLife.Game
{
    public class Tile
    {
        private byte[] pixels;
        private byte tileNumber;
        private int tilePaletteX;
        private int tilePaletteY;


        public Tile(byte tileNumber)
        {
            this.tileNumber = tileNumber;
            this.pixels = new byte[Constants.CELLS_X * Constants.CELLS_Y];

        }
        public byte TileNumber
        {
            get { return tileNumber; }
            set { tileNumber = value; }
        }

        public byte[] Pixels
        {
            get { return pixels; }
            set { pixels = value; }
        }

    }
}
