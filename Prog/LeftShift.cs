using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notadesigner.ConwaysLife.Game
{
    class LeftShift
    {

        //Starting from first tile in list, copys set of source tiles
        //to target tiles, offset by two pixels to the left.
        public Tiles ByTwoPixels(Tiles tiles)
        {
            Tiles shiftedTiles = new Tiles();

            int last = (tiles.TileList.Count)-1;
            Tile srcCurrentTile;
            Tile srcNextTile;
            Tile targetTile;

            int index = 0;

            while (index < last)
            {
                srcCurrentTile = tiles.TileList[index];
                srcNextTile = tiles.TileList[index + 1];
                targetTile = shiftedTiles.TileList[index];

                targetTile.pixels = ShiftedCopy(srcCurrentTile);
                Left2PxColToRight2PxCol(srcNextTile, targetTile);

                index++;
            }

            srcCurrentTile = tiles.TileList[index];
            targetTile = shiftedTiles.TileList[index];
            targetTile.pixels = ShiftedCopy(srcCurrentTile);

            return shiftedTiles;
        }




        //Copys the src tile to target tile, offset by two pixels.
        //Called from ByTwoPixels.
        byte[] ShiftedCopy(Tile sourceTile)
        {
            byte[] targetPixels = new byte[Constants.CELLS_X * Constants.CELLS_Y];

            Array.ConstrainedCopy(sourceTile.pixels, 2, targetPixels, 0, 254);

            return targetPixels;
        }




        //Copys the Leftmost two-pixel column of the next tile in the src tile list
        //to the right-most twopixel column of the target tile.
        //Called from ByTwoPixels.
        void Left2PxColToRight2PxCol (Tile sourceTile, Tile targetTile)
        {
            int srcIndex = 0;
            int tgtIndex = Constants.CELLS_X - 2;
            int lastIndex = (Constants.CELLS_X * Constants.CELLS_Y) - 1;

            
            while (tgtIndex <= lastIndex)
            {
                Array.ConstrainedCopy(
                    sourceTile.pixels, srcIndex, targetTile.pixels, tgtIndex, 2);

                srcIndex += Constants.CELLS_X;
                tgtIndex += Constants.CELLS_X;
            }

        }
    }
}
