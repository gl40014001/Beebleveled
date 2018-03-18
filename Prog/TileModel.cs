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
        
        private byte[] next;

        public byte maxTNum = 0;
        public byte TmTileNumber = 0;
        public Tiles tiles = new Tiles();

        public byte[] Cells
        {
            get
            {
                return this.current = tiles.TileList[TmTileNumber].Pixels;



            }
        }

		public TileModel()
		{
            this.current = tiles.TileList[TmTileNumber].Pixels;
        }

		public Boolean IsActive
		{
			get;
			private set;
		}

		public void Start()
		{
			this.IsActive = true;
		}

		public void Stop()
		{
			this.IsActive = false;
		}

		public void Clear()
		{
			if (this.IsActive)
			{
				this.Stop();
			}

			for (int i = 0; i < this.current.Length; i++)
			{
				this.current[i] = 0;
            }

            if (null != this.Update)
            {
                this.Update(this);
            }
		}

		public void NextTile()
		{
            byte minTileNumber = 0;
            TmTileNumber++;
            if (TmTileNumber>maxTNum)
            { TmTileNumber = minTileNumber; }


            if (this.IsActive)
			{
				this.Stop();
			}

			

            if (null != this.Update)
            {
                this.Update(this);
            }
		}

        public void SelectTile(int x, int y)
        {
            TmTileNumber = (byte)(x+(y*4));

            if (null != Update)
                Update(this);
        }

		public void AddTile()
		{
            maxTNum++;
            
            tiles.Add(maxTNum);
            TmTileNumber = maxTNum;

            if (null != this.Update)
            {
                this.Update(this);
            }
		}

		public void ToggleCell(int x, int y)
		{
			int i = y * Constants.CELLS_X + x;
            if (i < Constants.CELLS_X * Constants.CELLS_Y)
            {
                if (0 == this.current[i])
                {
                    this.current[i] = 1;
                }
                else
                {
                    this.current[i] = 0;
                }
            }

			if (null != this.Update)
			{
				this.Update(this);
			}
		}
        
	}
}
