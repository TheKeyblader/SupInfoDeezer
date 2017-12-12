using System;
using System.Collections.Generic;
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
using Api;

namespace SupinfoDeezer
{
    /// <summary>
    /// Logique d'interaction pour PageAlbum.xaml
    /// </summary>
    public partial class PageAlbum : Page
    {
        public string Image { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }

        public PageAlbum()
        {
            InitializeComponent();
            DataContext = this;
            List<Album> album = Class1.SearchAlbumByString("All Falls Down");
            GetAlbumInfo(album.First());
        }

        public void GetAlbumInfo(Album album)
        {
            Image = album.Cover;
            Album = album.Title;
            Artist = album.Artist.Name;
        }
    }
}
