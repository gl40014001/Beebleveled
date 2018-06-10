using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notadesigner.ConwaysLife.Game
{
    public class Tiles
    {
       

        public byte TMnumber;
        public List<Tile> TileList = new List<Tile>();
       
        public Tiles()
        {

            for (TMnumber = 0; TMnumber <= 15; TMnumber++)
            {
                Add(TMnumber);
            }
        }

        public void Add(byte tileNumber)
        {
            TileList.Add(new Tile(TMnumber) );   
        
        }


       

        
    }
}
