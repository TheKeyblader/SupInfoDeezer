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
using System.Windows.Threading;

namespace SupinfoDeezer
{
    /// <summary>
    /// Logique d'interaction pour TrackContainer.xaml
    /// </summary>
    /// 
    

    public partial class TrackContainer : Page
    {
        public ObservableCollection<Track> Tracks { get; set; } = new ObservableCollection<Track>();

        public TrackContainer()
        {
            InitializeComponent();
            DataContext = this;
            GetTracks();
        }

        public async Task GetTracks(string text = "Daft Punk")
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
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Track.Clear();
            mainWindow.Track.Add(Tracks[list.SelectedIndex]);
            Dispatcher.BeginInvoke(new Action(() =>
            {
                mainWindow.player.StandardInput.WriteLineAsync("p "+mainWindow.Track.First().Id.ToString()).Wait();
                mainWindow.player.StandardInput.FlushAsync();
            }),DispatcherPriority.Send);
            
        }

        private void Container_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
        }
    }
}
