using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
namespace Notadesigner.ConwaysLife.Game
{
    public class Load
    {
        public void FromFile(Tiles tiles, PaletteModel palette, SelectedColourModel selectedcolours)
        {
            var stuff = System.IO.File.ReadAllText("blah.txt");
            string[] separatingChars = { Constants.SEPARATOR };
            string[] SplitData = stuff.Split(separatingChars, System.StringSplitOptions.None);
          //  var deserialized = new JavaScriptSerializer();
          //  tiles.TileList = deserialized.Deserialize<List<Tile>>(stuff);
            //palette.pal = deserialized.Deserialize<>
        }
    }
}
