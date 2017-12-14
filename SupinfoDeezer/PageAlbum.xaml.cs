﻿using System;
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
        public List<Album> Albums { get; set; } = new List<Album>();

        public PageAlbum()
        {
            InitializeComponent();
            DataContext = this;
            GetAlbums();
        }

        public async Task GetAlbums(string text = "David Guetta")
        {
            Albums = Class1.SearchAlbumByString(text);
        }
    }
}
