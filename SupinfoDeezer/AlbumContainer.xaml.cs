using Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupinfoDeezer
{
    /// <summary>
    /// Logique d'interaction pour AlbumContainer.xaml
    /// </summary>
    public partial class AlbumContainer : Page
    {
        public ObservableCollection<Album> Albums { get; set; }

        public AlbumContainer()
        {
            InitializeComponent();
            DataContext = this;
            GetAlbums();
        }

        public async Task GetAlbums(string text = "Daft Punk")
        {
            Albums.Clear();
            foreach (Album album in await Class1.SearchAlbumByString(text))
            {
                Albums.Add(album);
            }
        }

        private void GetId(object sender, MouseButtonEventArgs e)
        {
            var list = (ListView)sender;
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Track.Clear();
            mainWindow.Track.Add(Albums[list.SelectedIndex].Tracks[0]);
            MessageBox.Show(mainWindow.Track[0].Title_Short);
        }
    }
}
