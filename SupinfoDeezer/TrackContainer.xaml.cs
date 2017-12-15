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
using System.Diagnostics;
using Api;

namespace SupinfoDeezer
{
    /// <summary>
    /// Logique d'interaction pour TrackContainer.xaml
    /// </summary>
    /// 
    

    public partial class TrackContainer : Page
    {
        public ObservableCollection<Track> Tracks { get; set; } = new ObservableCollection<Track>();
        public long Id { get; set; }

        public TrackContainer()
        {
            InitializeComponent();
            DataContext = this;
            GetAlbums();
        }

        public async Task GetAlbums(string text = "Daft Punk")
        {
            Tracks.Clear();
            foreach(Track track in await Class1.SearchTrackByString(text))
            {
                Tracks.Add(track);
            }
        }

        private void GetId(object sender, MouseButtonEventArgs e)
        {
            var list = (ListView)sender;
            Id = Tracks[list.SelectedIndex].Id;
            MessageBox.Show(Id.ToString());
            //PlayerManager.commandToDo = new Command()
            //{
            //    command = TODO.Load,
            //    song = "dzmedia:///track/" + Id.ToString(),
            //};
        }
    }
}
