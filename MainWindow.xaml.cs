using System.Windows;
using System.Windows.Controls;
using Notadesigner.ConwaysLife.Game;

namespace Notadesigner.ConwaysLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Tiles tiles = new Tiles();
        public TileModel model = new TileModel();

        public PaletteModel palettemodel = new PaletteModel(Constants.defaultPalette);
        public SelectedColourModel selectedcolours = new SelectedColourModel(Constants.defaultPalette);
        public Save save = new Save();
        public Load load = new Load();
        public MainWindow()
        {
            InitializeComponent();

            //OnUpdate is a delegate which points to a function that is really an event handler.
            //model.Update is the event.
            //Upon event "Update" occurring
            //delegate in BoardModel calls MainWindow.model_update
            this.model.Update += new TileModel.OnUpdate(model_Update);
            palettemodel.Update += new PaletteModel.OnUpdate(palette_Update);
            selectedcolours.Update += new SelectedColourModel.OnUpdate(selectedcolours_Update);
            model.Init(tiles);
            tpview.Update(tiles.TileList, palettemodel.currentPalette);
            //    this.TPmodel.Update += new TilePaletteModel.OnUpdate(TPmodel_Update);
        }

		void view_Click(object sender, ClickEventArgs e)
		{
			this.model.ToggleCell(e.X, e.Y, e.BUTTON);
		}

        void model_Update(object sender)
        {
            //This is the event handler called by Boardmodel when it fires the Update event.
            //i.e model_Update handler now calls BoardView.Update method and passes an array of bytes
            //(returned by the BoardModel.Cells method) as a paremeter.
           
            view.Update(this.model.Cells(tiles));
            tpview.Update(tiles.TileList, palettemodel.currentPalette);
          
        }

     
        void palette_Update(object sender)
        {
            paletteview.Update(palettemodel.currentPalette);
            tpview.Update(tiles.TileList, palettemodel.currentPalette);
            selectedcolours.PaletteUpdated(palettemodel.ChangedPaletteIndex, palettemodel.currentPalette);
            scview.Update(selectedcolours.currentPalette);
            model.PaletteUpdated(selectedcolours.pal);
            view.Update(model.Cells(tiles), palettemodel.currentPalette);
        }

        void selectedcolours_Update(object sender)
        {
            scview.Update(selectedcolours.currentPalette);
        }



        private void tpview_Click(object sender, ClickEventArgs e)
        {
            model.SelectTile(e.X, e.Y);
        }

        private void paletteview_Click(object sender, ClickEventArgs e)
        {
            palettemodel.ChangeColour(e.X, e.Y);
        }

        private void scview_Click(object sender, ClickEventArgs e)
        {
             selectedcolours.ChangeColour(e.X, e.Y, palettemodel.currentPalette);
             model.PaletteUpdated(selectedcolours.pal);
           // view.Update(model.Cells, palettemodel.currentPalette);
        }

        private void tstLoad_Click(object sender, RoutedEventArgs e)
        {
            load.FromFile(tiles);

           
            //this.model.AddTile();

        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            //this.btnAdd.IsEnabled = true;
           
            this.model.Clear();
        }

    

        private void tstSave_Click(object sender, RoutedEventArgs e)
        {
            save.ToFile(tiles, palettemodel,selectedcolours);

        }

     
    }
}
