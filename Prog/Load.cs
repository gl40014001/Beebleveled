using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
namespace Notadesigner.ConwaysLife.Game
{
    public class Load
    {
        public void FromFile(Tiles tiles)
        {
            var stuff = System.IO.File.ReadAllText("blah.txt");

            var deserialized = new JavaScriptSerializer();
            tiles.TileList = deserialized.Deserialize<List<Tile>>(stuff);

        }
    }
}
