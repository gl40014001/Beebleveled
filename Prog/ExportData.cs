using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notadesigner.ConwaysLife.Game

{
    class ExportData
    {
        //byte[] beebTile = new byte[64];

        public void ToBeebFiles(string Filename, Tiles tiles, LevelScreens screens)
        {
            string beebTilesFN = Filename + "_Unshifted.bin";
            string beebTilesShiftedFN = Filename + "_Shifted.bin";

            LeftShift leftShift = new LeftShift();
            Tiles shiftedTiles = new Tiles();


            WriteBeebTileFile(tiles, beebTilesFN);
 
            shiftedTiles = leftShift.ByTwoPixels(tiles);            
            WriteBeebTileFile(shiftedTiles, beebTilesShiftedFN);


            int i = 0;
            foreach (LevelScreen level in screens.list)
            {
                string beebLevelFN = Filename + "_Level" + i + ".bin";
                WriteBeebLevelFile(level, beebLevelFN);
                i++;
            }

   
        }




        private void WriteBeebTileFile(Tiles tiles, string FileName)
        {
            byte[] tgtTile;

            BinaryWriter binFile =
               new BinaryWriter(new FileStream(FileName, FileMode.Create));

            foreach (Tile tile in tiles.TileList)
            { 
                byte[] sourceTile = tile.pixels;
                tgtTile = ConvertToBeebTile(sourceTile);
                AppendToBeebTileFile(tgtTile, binFile);
            }

            binFile.Close();
        }
        
        

        private void AppendToBeebTileFile(byte[] tgtTile, BinaryWriter binFile )
        {
            foreach (byte beebByte in tgtTile)
                binFile.Write(beebByte);            
        }
            
        


      
        // Beeb Tiles for the game are stored in [byte] Column-major order.
        // ..and the Beeb (in Mode 1) stores 4 pixels in each byte.
        // Our source tiles from the tile editor are stored in
        // row-major.
        // Our tiles are 16*16px.
        // So we start from the top left of tile and get top left 4 pixels..
        // and then get the next 4 pixels below etc *16 rows
        // ..Then to the next column of 4 pixels * 16 rows etc.
        // til we have got 4 byte columns each of which is 16 rows deep
        // stored in a byte array.  
        private byte[] ConvertToBeebTile(byte[] origTile)
        {
            byte[] srcTile = origTile;
            byte[] tgtTile = new byte[64];
            int indexOffset = 0;
            int beebTileIndex = 0;
            int bottomOfColumn = Constants.CELLS_X * (Constants.CELLS_Y - 1);
            
            byte[] srcPixels;
            byte beebByte;

            while (bottomOfColumn < Constants.CELLS_X * Constants.CELLS_Y)
            {
                int srcTileIndex = indexOffset;
                while (srcTileIndex <= bottomOfColumn)
                {
                    srcPixels = NextFourPixels(srcTile, srcTileIndex);
                    beebByte = ConvertToBeebByte(srcPixels);
                    tgtTile[beebTileIndex] = beebByte;

                    beebTileIndex++;
                    srcTileIndex += Constants.CELLS_X;
                }

                indexOffset += 4;
                bottomOfColumn += 4;
            }

            return tgtTile;
        }


        //Called by ConvertToBeebTile
        private byte[] NextFourPixels(byte[] sourceTile, int index)
        {
            byte[] physCol = new byte[4];

            physCol[0] = 0;
            physCol[1] = 0b00000001;
            physCol[2] = 0b00010000;
            physCol[3] = 0b00010001;
            
            byte sourcePixel;
            byte[] beebPixels = new byte[4];
            
            for (int i = 0; i <= 3; i++)
            {
                sourcePixel = sourceTile[index];
                beebPixels[i] = physCol[sourcePixel];
                index++;
            }
        
            return beebPixels;
        }



        //Called by ConvertToBeebTile
        //See BBC AUG p465 for Mode 1 byte format.
        private byte ConvertToBeebByte(byte[] BeebPixels)
        {
            byte beebTileByte = 0;
            byte shiftedPixel;
            int j = 3;
            for (int i = 0; i<=3 ; i++)
            {
                int beebPixel = (int)BeebPixels[i] << j;
                shiftedPixel = (byte)beebPixel;
                int orResult = (int)shiftedPixel | (int)beebTileByte;
                beebTileByte = (byte)orResult;
                j--;

            }

            return beebTileByte;
        }



        private void WriteBeebLevelFile(LevelScreen levelScreen, string FileName)
        {
            BinaryWriter binFile =
              new BinaryWriter(new FileStream(FileName, FileMode.Create));

            int[] leveldata = levelScreen.tileRef;

            foreach(int tileRef in leveldata)
                binFile.Write((byte)tileRef);   
            
            binFile.Close();
        }
    }
}
