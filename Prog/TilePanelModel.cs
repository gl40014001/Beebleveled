using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notadesigner.ConwaysLife.Game
{
    class TilePanelModel
    {
        public delegate void OnUpdate(object sender);
        public event OnUpdate Update;
        public List<Tile> TPMList;

      
        public void UpdateTileList(TileModel tilemodel)
        {
            TPMList = tilemodel.tiles.TileList;
            if (null != this.Update)
            {
                this.Update(this);
            }
        }

        public List<Tile> Tiles
        {
            get
            { return TPMList; }
        }
    }
}
