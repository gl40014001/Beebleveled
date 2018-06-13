using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Script.Serialization;
namespace Notadesigner.ConwaysLife.Game
{
    public class Save
    {
        public void ToFile(string Filename, Tiles tiles, PaletteModel palette, SelectedColourModel selectedcolours, LevelScreens screens )
        {
            
            var serializer = new JavaScriptSerializer();
            var serializedResult = 
                        serializer.Serialize (tiles.TileList.Select (x=> new { x.tileNumber, x.Pixels, x.prevPixels } ));
            var serPalette = new JavaScriptSerializer();
            var serPaletteResult = serPalette.Serialize(palette.CurrentPalette);
            var serPalRes = serPalette.Serialize(palette.pal);
            var serSelColRes = serPalette.Serialize(selectedcolours.pal);
            var serLevelScreens = serializer.Serialize(screens.list);
            var AllResults =
                   serializedResult + Constants.SEPARATOR +
                   serPaletteResult + Constants.SEPARATOR +
                   serPalRes + Constants.SEPARATOR +
                   serSelColRes+ Constants.SEPARATOR +
                   serLevelScreens;


                   
            System.IO.File.WriteAllText(Filename,AllResults);
        }
        
        
    }
}
