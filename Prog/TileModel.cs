using System;
using System.Windows.Threading;
using System.Collections.Generic;
namespace Notadesigner.ConwaysLife.Game
{
	public class TileModel
	{
        public delegate void OnUpdate(object sender);
        public event OnUpdate Update;

        private byte[] current;
        public byte maxTNum = 0;
        public byte TmTileNumber = 0;
       // public Tiles tiles = new Tiles();
        public int NumColours = 2;
        public byte ForegroundCol = 0;
        public byte BackgroundCol = 1;
        public byte[] Cells(Tiles tiles)
        {
            
            
                return this.current = tiles.TileList[TmTileNumber].Pixels;
            
        }


		public TileModel()
		{
            
        }

        public void Init(Tiles tiles)
        {
            this.current = tiles.TileList[TmTileNumber].Pixels;
        }
		
		public void Clear()
		{
			for (int i = 0; i < this.current.Length; i++)
			{
				this.current[i] = 0;
            }

            if (null != this.Update)
                this.Update(this);   
		}


		public void NextTile()
		{
            byte minTileNumber = 0;
            TmTileNumber++;

            if (TmTileNumber>maxTNum)
             TmTileNumber = minTileNumber;

            if (null != this.Update)
                this.Update(this);
		}

        public void SelectTile(int x, int y)
        {
            TmTileNumber = (byte)(x+(y*4));

            if (null != Update)
                Update(this);
        }


		//public void AddTile()
		//{
      // /     maxTNum++;
        //    tiles.Add(maxTNum);
         //   TmTileNumber = maxTNum;

          //  if (null != this.Update)
          //      this.Update(this);
		//}


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
                   this.current[i] = ForegroundCol;

                if (button == Constants.Mousebutton.Right)
                    this.current[i] = BackgroundCol; 
            }

			if (null != this.Update)
				this.Update(this);
		}
        

	}
}
