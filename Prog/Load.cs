using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
namespace Notadesigner.ConwaysLife.Game
{
    public class Load
    {
        public void FromFile(string Filename, Tiles tiles, PaletteModel palette, SelectedColourModel selectedcolours, LevelScreens screens)
        {
            var stuff = System.IO.File.ReadAllText(Filename);
            string[] separatingChars = { Constants.SEPARATOR };
            string[] SplitData = stuff.Split(separatingChars, System.StringSplitOptions.None);

            var deserialized = new JavaScriptSerializer();
            tiles.TileList = deserialized.Deserialize<List<Tile>>(SplitData[0]);
            palette.CurrentPalette = deserialized.Deserialize<Constants.Colour[]>(SplitData[1]);
            selectedcolours.pal = deserialized.Deserialize<int[]>(SplitData[3]);
            screens.list = deserialized.Deserialize< List<LevelScreen>>(SplitData[4]);

            //  var deserialized = new JavaScriptSerializer();
            //  tiles.TileList = deserialized.Deserialize<List<Tile>>(stuff);
            //palette.pal = deserialized.Deserialize<>
        }
    }
}
