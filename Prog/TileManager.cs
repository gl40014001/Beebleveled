﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notadesigner.ConwaysLife.Game
{
    public class TileManager
    {
        public byte TMnumber = 0;
        public List<Tile> TileList = new List<Tile>();
        public TileManager()
        {
            this.Add(TMnumber);
        }

        public void Add(byte tileNumber)
        {
            TileList.Add(new Tile(TMnumber) );   
        }
    }
}