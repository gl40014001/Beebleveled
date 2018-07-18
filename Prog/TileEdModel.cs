using System;
using System.Windows.Threading;
using System.Collections.Generic;
namespace Notadesigner.ConwaysLife.Game
{
	public class TileEdModel
	{
        public delegate void OnUpdate(object sender);
        public event OnUpdate Update;

        private byte[] current;
        public byte maxTNum = 0;
        public byte TmTileNumber = 0;
        public int NumColours = 2;
        public byte ForegroundCol = 0;
        public byte BackgroundCol = 3;
        public Tile EdTile;
        public byte[] Cells(Tiles tiles)
        {
            EdTile = tiles.TileList[TmTileNumber];
            return this.current = tiles.TileList[TmTileNumber].Pixels;      
        }

      

		public TileEdModel()
		{

        }

        public void Init(Tiles tiles)
        {
            EdTile = tiles.TileList[TmTileNumber];
            this.current = tiles.TileList[TmTileNumber].Pixels;
        }
		
		public void Clear()
		{
            byte[] localPixels = new byte[Constants.CELLS_X * Constants.CELLS_Y];


            for (int i = 0; i < localPixels.Length; i++)
			{
				localPixels[i] = 0;
            }
           // current = localPixels;
            EdTile.Do(localPixels);

           

            if (null != this.Update)
                Update(this);   
		}


        public void Undo(Tiles tiles)
        {
            tiles.TileList[TmTileNumber].Undo();
            if (null != this.Update)
                Update(this);
        }


        public void Redo(Tiles tiles)
        {
            tiles.TileList[TmTileNumber].Redo();
            if (null != this.Update)
                Update(this);
        }


        public void NextTile()
		{
            byte minTileNumber = 0;
            TmTileNumber++;

            if (TmTileNumber>maxTNum)
             TmTileNumber = minTileNumber;

            if (null != this.Update)
                Update(this);
		}

        public void SelectTile(int x, int y)
        {
            TmTileNumber = (byte)(x+(y*4));

            bool ispositive = TmTileNumber >= 0;


            if (TmTileNumber <= Constants.NUM_OF_TILES && ispositive)
            {
                if (null != Update)
                    Update(this);
            }
        }


	

        public void PaletteUpdated(int[] selectedcolours)
        {

            if ((byte)selectedcolours[0] != ForegroundCol)
                ForegroundCol = (byte)selectedcolours[0];

            if ((byte)selectedcolours[1] != BackgroundCol)
                BackgroundCol = (byte)selectedcolours[1];
             

        }


        public void ToggleCell(int x, int y, Constants.Mousebutton button)
		{
			int i = y * Constants.CELLS_X + x;
            if (i < Constants.CELLS_X * Constants.CELLS_Y)
            {
                if (button == Constants.Mousebutton.Left)
                {
              
                    EdTile.Do(i,ForegroundCol);
                }

                if (button == Constants.Mousebutton.Right)
                {
                  
                    EdTile.Do(i,BackgroundCol);
                }
            }

			if (null != this.Update)
				Update(this);
		}
        

	}
}
