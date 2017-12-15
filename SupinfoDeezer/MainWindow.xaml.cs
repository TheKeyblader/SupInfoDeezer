using Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Threading;

namespace SupinfoDeezer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Track> Track { get; set; }
        public Process player;
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new Uri("TrackContainer.xaml", UriKind.RelativeOrAbsolute));
            DataContext = this;
            Track = new ObservableCollection<Track>();
            Track.Add(new Track
            {
                Title_Short = "",
                Title_Version = "",
                Artist = new Artist
                {
                    Name = ""
                },
                Album = new Album
                {
                    Cover = ""
                },
            });
            player = new Process();
            player.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            player.StartInfo.Arguments = "";
            player.StartInfo.FileName = "TestWebPlayer.exe";

            player.StartInfo.UseShellExecute = false;
            player.StartInfo.RedirectStandardInput = true;
            player.StartInfo.RedirectStandardOutput = true;
            player.StartInfo.RedirectStandardError = true;
            player.StartInfo.CreateNoWindow = true;
            player.ErrorDataReceived += Receive;
            player.OutputDataReceived += Receive;
            player.EnableRaisingEvents = true;
            player.Start();
            player.BeginOutputReadLine();
            player.BeginErrorReadLine();
            Application.Current.Exit += new ExitEventHandler(Exit);
        }
        public void Receive(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
        public void Exit(object sender, ExitEventArgs e)
        {
            player.Kill();
        }
        private async void BarreRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;

            if(textbox.Text != null && textbox.Text != "" && mainFrame != null)
            {
                if(mainFrame.Content is AlbumContainer)
                {
                    await ((AlbumContainer)mainFrame.Content).GetAlbums(textbox.Text);
                }
                else if(mainFrame.Content is TrackContainer)
                {
                    await ((TrackContainer)mainFrame.Content).GetTracks(textbox.Text);
                }
            }
        }

        private async void SwitchToTracks(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = null;
            mainFrame.Navigate(new Uri("TrackContainer.xaml", UriKind.RelativeOrAbsolute));
            mainFrame.NavigationService.Refresh();
            mainFrame.Loaded += new RoutedEventHandler(LoadTracks);
        }

        private async void LoadTracks(object sender, RoutedEventArgs e)
        {
            if (BarreRecherche.Text != null && BarreRecherche.Text != "")
            {
                await ((TrackContainer)mainFrame.Content).GetTracks(BarreRecherche.Text);
            }
            else
            {
                await ((TrackContainer)mainFrame.Content).GetTracks("Daft Punk");
            }
        }

        private void SwitchToAlbums(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = null;
            mainFrame.Source = new Uri("AlbumContainer.xaml", UriKind.RelativeOrAbsolute);
            //mainFrame.Navigate(new Uri("AlbumContainer.xaml", UriKind.RelativeOrAbsolute));
            mainFrame.NavigationService.Refresh();
            mainFrame.Loaded += new RoutedEventHandler(LoadAlbums);
        }

        private async void LoadAlbums(object sender, RoutedEventArgs e)
        {
            if (BarreRecherche.Text != null && BarreRecherche.Text != "")
            {
                await ((AlbumContainer)mainFrame.Content).GetAlbums(BarreRecherche.Text);
            }
            else
            {
                await ((AlbumContainer)mainFrame.Content).GetAlbums("Daft Punk");
            }
            mainFrame.Loaded -= LoadAlbums;
        }

        private void PlayCLick(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                player.StandardInput.WriteLineAsync("play").Wait();
                player.StandardInput.FlushAsync().Wait();
            }),DispatcherPriority.Send);
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                player.StandardInput.WriteLineAsync("pause").Wait();
                player.StandardInput.FlushAsync().Wait();
            }),DispatcherPriority.Send);
        }

        private void Augmente(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                player.StandardInput.WriteLineAsync("+").Wait();
                player.StandardInput.FlushAsync().Wait();
            }), DispatcherPriority.Send);
        }

        private void Baisser(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                player.StandardInput.WriteLineAsync("-").Wait();
                player.StandardInput.FlushAsync().Wait();
            }), DispatcherPriority.Send);
        }
    }
}
