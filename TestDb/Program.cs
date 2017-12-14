using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MoreLinq;

namespace TestDb
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            // Création de la requete
            WebRequest request = WebRequest.Create("https://api.deezer.com/track/3135556");
            // Définition de la requete en mode "GET"
            request.Method = "GET";
            // Stockage de l'état de la requete 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Affichage de l'état de la requette
            Console.WriteLine(response.StatusDescription);
            // Stockage de la réponse du server
            Stream dataStream = response.GetResponseStream();

            // Lecture de la réponse pour la transformer en string
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            Track requestedObject = JsonConvert.DeserializeObject<Track>(responseFromServer);

            // Clean
            reader.Close();
            dataStream.Close();
            response.Close();
            */

            
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Search something");
                string entry = Console.ReadLine();
                Console.WriteLine();

                Display(entry);



                Console.ReadLine();
            }
        }

        public static void Display(string value)
        {
            Console.WriteLine("--Tracks :");
            DisplayMusics(SearchTrackByString(value));
            Console.WriteLine();

            Console.WriteLine("--Albums :");
            DisplayAlbums(SearchAlbumByString(value));
            Console.WriteLine();

            Console.WriteLine("--Artists :");
            DisplayArtists(SearchArtistByString(value));
            Console.WriteLine();

            Console.WriteLine("--Playlists :");
            DisplayPlaylists(SearchPlaylistByString(value));
            Console.WriteLine();
        }

        public static void DisplayMusics(List<Track> tracks)
        {
            if (tracks == null)
            {
                Console.WriteLine("No music found");
            }
            else
            {
                foreach (Track track in tracks)
                {
                    Console.Write("[" + track.Title + "] by " + track.Artist.Name);
                    Console.WriteLine();
                }
            }
        }

        public static void DisplayAlbums(List<Album> albums)
        {
            if (albums == null)
            {
                Console.WriteLine("No album found");
            }
            else
            {
                foreach (Album album in albums)
                {
                    Console.Write(album.Title);
                    Console.WriteLine();
                }
            }
        }

        public static void DisplayArtists(List<Artist> artists)
        {
            if (artists == null)
            {
                Console.WriteLine("No artist found");
            }
            else
            {
                foreach (Artist artist in artists)
                {
                    Console.Write(artist.Name);
                    Console.WriteLine();
                }
            }
        }

        public static void DisplayPlaylists(List<Playlist> playlists)
        {
            if (playlists == null)
            {
                Console.WriteLine("No playlist found");
            }
            else
            {
                foreach (Playlist playlist in playlists)
                {
                    Console.Write(playlist.Title);
                    Console.WriteLine();
                }
            }
        }

        public static List<Track> SearchTrackByString(string value)
        {
            string api = "https://api.deezer.com/search?q=" + value;
            string json = Request(api);
            TrackList tracks = JsonConvert.DeserializeObject<TrackList>(json);
            //Console.WriteLine("Request finished");

            return tracks.Data;
        }

        public static List<Album> SearchAlbumByString(string value)
        {
            string api = "https://api.deezer.com/search/album?q=" + value;
            string json = Request(api);
            AlbumList albums = JsonConvert.DeserializeObject<AlbumList>(json);
            //Console.WriteLine("Request finished");

            return albums.Data;
        }

        public static List<Artist> SearchArtistByString(string value)
        {
            string api = "https://api.deezer.com/search/artist?q=" + value;
            string json = Request(api);
            ArtistList artists = JsonConvert.DeserializeObject<ArtistList>(json);
            //Console.WriteLine("Request finished");

            return artists.Data;
        }

        public static List<Playlist> SearchPlaylistByString(string value)
        {
            string api = "https://api.deezer.com/search/playlist?q=" + value;
            string json = Request(api);
            PlaylistList playlists = JsonConvert.DeserializeObject<PlaylistList>(json);
            //Console.WriteLine("Request finished");

            return playlists.Data;
        }

        public static Track SearchTrackByID(long id)
        {
            string api = "https://api.deezer.com/track/" + id.ToString();
            string json = Request(api);
            Track track = JsonConvert.DeserializeObject<Track>(json);
            return track;
        }

        public static Album SearchAlbumByID(long id)
        {
            string api = "https://api.deezer.com/album/" + id.ToString();
            string json = Request(api);
            Album album = JsonConvert.DeserializeObject<Album>(json);
            return album;
        }

        public static Artist SearchArtistByID(long id)
        {
            string api = "https://api.deezer.com/artist/" + id.ToString();
            string json = Request(api);
            Artist artist = JsonConvert.DeserializeObject<Artist>(json);
            return artist;
        }

        public static Playlist SearchPlaylistByID(long id)
        {
            string api = "https://api.deezer.com/playlist/" + id.ToString();
            string json = Request(api);
            Playlist playlist = JsonConvert.DeserializeObject<Playlist>(json);
            return playlist;
        }

        public static string RequestDebug(string value)
        {
            try
            {
                Console.WriteLine("Request starting");
                value.Replace(' ', '+');
                // Création de la requete
                WebRequest request = WebRequest.Create(value);
                // Définition de la requete en mode "GET"
                request.Method = "GET";
                // Stockage de l'état de la requete 
                Console.WriteLine("Connecting to API");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine("Connected");
                // Affichage de l'état de la requette
                Console.WriteLine(response.StatusDescription);
                // Stockage de la réponse du server
                Stream dataStream = response.GetResponseStream();

                // Lecture de la réponse pour la transformer en string
                StreamReader reader = new StreamReader(dataStream);
                string json = reader.ReadToEnd();

                // Clean
                reader.Close();
                dataStream.Close();
                response.Close();

                return json;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string Request(string value)
        {
            WebRequest request = WebRequest.Create(value);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            
            StreamReader reader = new StreamReader(dataStream);
            string json = reader.ReadToEnd();
            
            reader.Close();
            dataStream.Close();
            response.Close();

            return json;
        }

        public static string Get_Duration(int duration)
        {
            int hours = duration / 3600;
            int minutes = (duration % 3600) / 60;
            int seconds = duration % 60;

            if (hours > 0)
            {
                return hours + "h" + minutes + "m" + seconds + "s";
            }
            else if (minutes > 0)
            {
                return minutes + "m" + seconds + "s";
            }
            else
            {
                return seconds + "s";
            }
        }
    }

    public class TrackList
    {
        public List<Track> Data { get; set; }
    }

    public class AlbumList
    {
        public List<Album> Data { get; set; }
    }

    public class ArtistList
    {
        public List<Artist> Data { get; set; }
    }

    public class PlaylistList
    {
        public List<Playlist> Data { get; set; }
    }

    public class Track
    {
        public long Id { get; set; }
        public bool Readable { get; set; }
        public string Title { get; set; }
        public string Title_Short { get; set; }
        public string Title_Version { get; set; }
        public bool Unseen { get; set; }
        public string Isrc { get; set; }
        public string Link { get; set; }
        public string Share { get; set; }
        public int Duration { get; set; }
        public int Track_Position { get; set; }
        public int Disk_Number { get; set; }
        public int Rank { get; set; }
        public DateTime Release_Date { get; set; }
        public bool Explicit_Lyrics { get; set; }
        public string Preview { get; set; }
        public float Bpm { get; set; }
        public float Gain { get; set; }
        public List<string> Available_Countries { get; set; }
        public Track Alternative { get; set; }
        public List<string> Contributors { get; set; }
        public Artist Artist { get; set; }
        public Album Album { get; set; }
    }

    public class Album
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Upc { get; set; }
        public string Link { get; set; }
        public string Share { get; set; }
        public string Cover { get; set; }
        public string Cover_Small { get; set; }
        public string Cover_Medium { get; set; }
        public string Cover_Big { get; set; }
        public string Cover_Xl { get; set; }
        public int Genre_Id { get; set; }
        public List<string> Genres { get; set; }
        public string Label { get; set; }
        public int Nb_Tracks { get; set; }
        public int Duration { get; set; }
        public int Fans { get; set; }
        public int Rating { get; set; }
        public DateTime Release_Date { get; set; }
        public bool Available { get; set; }
        public string Tracklist { get; set; }
        public bool Explicit_Lyrics { get; set; }
        public List<string> Contributors { get; set; }
        public Artist Artist { get; set; }
        public List<Track> Tracks { get; set; }
    }

    public class Artist
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Share { get; set; }
        public string Picture { get; set; }
        public string Picture_Small { get; set; }
        public string Picture_Medium { get; set; }
        public string Picture_Big { get; set; }
        public string Picture_Xl { get; set; }
        public int Nb_Album { get; set; }
        public int Nb_Fan { get; set; }
        public bool Radio { get; set; }
        public string Tracklist { get; set; }
    }

    public class Playlist
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool Public { get; set; }
        public bool Is_Loved_Track { get; set; }
        public bool Collaborative { get; set; }
        public int Rating { get; set; }
        public int Nb_Track { get; set; }
        public int Unseen_Track_Count { get; set; }
        public int Fans { get; set; }
        public string Link { get; set; }
        public string Url { get; set; }
        public string Picture { get; set; }
        public string Picture_Small { get; set; }
        public string Picture_Medium { get; set; }
        public string Picture_Big { get; set; }
        public string Picture_Xl { get; set; }
        public string Checksum { get; set; }
        public Creator Creator { get; set; }
        public List<Track> Tracks { get; set; }
    }

    public class Creator
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
