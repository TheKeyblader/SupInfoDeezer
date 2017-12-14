using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Api
{
    public class Class1
    {
        public async static void Display(string value)
        {
            Console.WriteLine("--Tracks :");
            //DisplayMusics(await SearchTrackByString(value));
            Console.WriteLine();

            Console.WriteLine("--Albums :");
            DisplayAlbums(await SearchAlbumByString(value));
            Console.WriteLine();

            Console.WriteLine("--Artists :");
            DisplayArtists(await SearchArtistByString(value));
            Console.WriteLine();

            Console.WriteLine("--Playlists :");
            DisplayPlaylists(await SearchPlaylistByString(value));
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

        public async static Task<IEnumerable<Track>> SearchTrackByString(string value)
        {
            string api = "https://api.deezer.com/search?q=" + value;
            string json = await Request(api);
            TrackList tracks = JsonConvert.DeserializeObject<TrackList>(json);
            //Console.WriteLine("Request finished");

            return tracks.Data;
        }

        public async static Task<List<Album>> SearchAlbumByString(string value)
        {
            string api = "https://api.deezer.com/search/album?q=" + value;
            string json = await Request(api);
            AlbumList albums = JsonConvert.DeserializeObject<AlbumList>(json);
            //Console.WriteLine("Request finished");

            return albums.Data;
        }

        public async static Task<List<Artist>> SearchArtistByString(string value)
        {
            string api = "https://api.deezer.com/search/artist?q=" + value;
            string json = await Request(api);
            ArtistList artists = JsonConvert.DeserializeObject<ArtistList>(json);
            //Console.WriteLine("Request finished");

            return artists.Data;
        }

        public async static Task<List<Playlist>> SearchPlaylistByString(string value)
        {
            string api = "https://api.deezer.com/search/playlist?q=" + value;
            string json = await Request(api);
            PlaylistList playlists = JsonConvert.DeserializeObject<PlaylistList>(json);
            //Console.WriteLine("Request finished");

            return playlists.Data;
        }

        public async static Task<Track> SearchTrackByID(long id)
        {
            string api = "https://api.deezer.com/track/" + id.ToString();
            string json = await Request(api);
            Track track = JsonConvert.DeserializeObject<Track>(json);
            return track;
        }

        public async static Task<Album> SearchAlbumByID(long id)
        {
            string api = "https://api.deezer.com/album/" + id.ToString();
            string json = await Request(api);
            Album album = JsonConvert.DeserializeObject<Album>(json);
            return album;
        }

        public async static Task<Artist> SearchArtistByID(long id)
        {
            string api = "https://api.deezer.com/artist/" + id.ToString();
            string json = await Request(api);
            Artist artist = JsonConvert.DeserializeObject<Artist>(json);
            return artist;
        }

        public async static Task<Playlist> SearchPlaylistByID(long id)
        {
            string api = "https://api.deezer.com/playlist/" + id.ToString();
            string json = await Request(api);
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

        public async static Task<string> Request(string value)
        {
            WebRequest request = WebRequest.Create(value);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();
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
}
