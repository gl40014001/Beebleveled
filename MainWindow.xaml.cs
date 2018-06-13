using System.Windows;
using System.Windows.Controls;
using Notadesigner.ConwaysLife.Game;
using Microsoft.Win32;

namespace Notadesigner.ConwaysLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Tiles tiles = new Tiles();
        public TileEdModel tileEdModel = new TileEdModel();

        public PaletteModel palettemodel = new PaletteModel(Constants.defaultPalette);
        public SelectedColourModel selectedcolours = new SelectedColourModel(Constants.defaultPalette);
        public Save save = new Save();
        public Load load = new Load();
        public UndoRedo undoredo = new UndoRedo();
        public LevelWindow levelwindow = new LevelWindow();
        public MainWindow()
        {
            InitializeComponent();
           
           
            //OnUpdate is a delegate which points to a function that is really an event handler.
            //model.Update is the event.
            //Upon event "Update" occurring
            //delegate in BoardModel calls MainWindow.model_update
            this.tileEdModel.Update += new TileEdModel.OnUpdate(TileEd_Update);
            palettemodel.Update += new PaletteModel.OnUpdate(Palette_Update);
            selectedcolours.Update += new SelectedColourModel.OnUpdate(Selectedcolours_Update);
            tileEdModel.Init(tiles);
            tilePanelView.Update(tiles.TileList, palettemodel.currentPalette);
           
            //    this.TPmodel.Update += new TilePaletteModel.OnUpdate(TPmodel_Update);
            levelwindow.Show();

            levelwindow.levelEdView.Update(tiles.TileList, palettemodel.currentPalette, tileEdModel.TmTileNumber);
        }

        void View_Click(object sender, ClickEventArgs e)
		{
			this.tileEdModel.ToggleCell(e.X, e.Y, e.BUTTON);
		}

        void TileEd_Update(object sender)
        {
            //This is the event handler called by TileEdModel when it fires the Update event.
            
            
            tileEdView.Update(this.tileEdModel.Cells(tiles));
            tilePanelView.Update(tiles.TileList, palettemodel.currentPalette);
        
        }

     
        void Palette_Update(object sender)
        {
            paletteview.Update(palettemodel.currentPalette);
            tilePanelView.Update(tiles.TileList, palettemodel.currentPalette);
            selectedcolours.PaletteUpdated(palettemodel.ChangedPaletteIndex, palettemodel.currentPalette);
            scview.Update(selectedcolours.currentPalette);
            tileEdModel.PaletteUpdated(selectedcolours.pal);
            tileEdView.Update(tileEdModel.Cells(tiles), palettemodel.currentPalette);
        }

        void Selectedcolours_Update(object sender)
        {
            scview.Update(selectedcolours.currentPalette);
        }



        private void Tpview_Click(object sender, ClickEventArgs e)
        {
            tileEdModel.SelectTile(e.X, e.Y);
            levelwindow.levelEdView.Update(tiles.TileList, palettemodel.currentPalette, tileEdModel.TmTileNumber);
        }

        private void paletteview_Click(object sender, ClickEventArgs e)
        {
            palettemodel.ChangeColour(e.X, e.Y);
        }

        private void Scview_Click(object sender, ClickEventArgs e)
        {
             selectedcolours.ChangeColour(e.X, e.Y, palettemodel.currentPalette);
             tileEdModel.PaletteUpdated(selectedcolours.pal);
           // view.Update(model.Cells, palettemodel.currentPalette);
        }

        private void tstLoad_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Level Data (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            
                load.FromFile(openFileDialog.FileName, tiles,palettemodel,  selectedcolours, levelwindow.levelEdView.screens);

           // tileEdView.Update(this.tileEdModel.Cells(tiles));


            paletteview.Update(palettemodel.currentPalette);
            tilePanelView.Update(tiles.TileList, palettemodel.currentPalette);
           selectedcolours.PaletteLoad(palettemodel.currentPalette);
           // scview.Update(selectedcolours.currentPalette);
            tileEdModel.PaletteUpdated(selectedcolours.pal);
            tileEdView.Update(tileEdModel.Cells(tiles), palettemodel.currentPalette);
            levelwindow.levelEdView.Update(tiles.TileList, palettemodel.currentPalette, tileEdModel.TmTileNumber);

            //  this.model.AddTile();

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //this.btnAdd.IsEnabled = true;
           
            this.tileEdModel.Clear();
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {


            tileEdModel.Undo(tiles);
            tileEdView.Update(this.tileEdModel.Cells(tiles));
            tilePanelView.Update(tiles.TileList, palettemodel.currentPalette);
        }
    

        private void tstSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Level Data (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
                save.ToFile(saveFileDialog.FileName, tiles, palettemodel,selectedcolours,levelwindow.levelEdView.screens);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // if (lw != null)
            levelwindow.Close();
            
        }
    }
}
