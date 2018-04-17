using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notadesigner.ConwaysLife.Game
{
    public class Tile
    {
        public byte[] pixels;
        public byte[] prevPixels;
        public byte tileNumber;
        public UndoRedo undoredo = new UndoRedo();

        public Tile DeepCopy()
        {
            Tile other = (Tile)this.MemberwiseClone();
            // pixels.
            other.tileNumber = TileNumber;

            return other;
        }

        public Tile(byte tileNumber)
        {
            this.tileNumber = tileNumber;
            this.pixels = new byte[Constants.CELLS_X * Constants.CELLS_Y];
            prevPixels = new byte[Constants.CELLS_X * Constants.CELLS_Y];
            // Do();
        }

        public Tile()
        {
         //   Do();
        }

        public byte TileNumber
        {
            get { return tileNumber; }
            set { tileNumber = value; }
        }

        public byte[] Pixels
        {
            get { return pixels; }
            set {

                pixels = value;
               
            }
        }


        public void Undo()
        {
           
            Pixels = undoredo.CmdUndo(Pixels);
           
        }

        public void Do(int index, byte pixelValue)
        {
            pixels.CopyTo(prevPixels, 0);
            undoredo.CmdDo(prevPixels);

            Pixels[index] = pixelValue;
            
        }

    }
}
