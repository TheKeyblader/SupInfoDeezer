using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDb
{
    public class Database : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;UserId=postgres;Password=admin;Database=DeezerSupinfo;");
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class User
    {
        [Key]
        public string Name { get; set; }
        public List<Playlist> Playlists { get; set; }
        public string FavouriteAlbums { get; set; }

        public List<int> AlbumList
        {
            get
            {
                return FavouriteAlbums.Split(',').Cast<int>().ToList();
            }
            set
            {
                FavouriteAlbums = "";
                foreach (int album in value)
                {
                    FavouriteAlbums += album + ",";
                }
            }
        }


    }

    public class Playlilkjhgfdsth
    {
        [Key]
        public string Name { get; set; }
        public User Author { get; set; }
        public string Musics { get; set; }
        
        public List<int> MusicList {
            get
            {
                return Musics.Split(',').Cast<int>().ToList();
            }
            set
            {
                Musics = "";
                foreach(int music in value)
                {
                    Musics += music + ",";
                }
            }
        }
    }
}
