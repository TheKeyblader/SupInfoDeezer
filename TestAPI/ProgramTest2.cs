using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;

namespace TestAPI
{

    class Program
    {
        static void Main(string[] args)
        {



            Search IdCherche = new Search();
        
            WebRequest request = WebRequest.Create(IdCherche.SearchAlbum("\"Ultra\"")); // requete a envoyer
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse reponse = (HttpWebResponse)request.GetResponse(); // variable de reception de la requete 
            request.Method = "GET"; // type de demande
            Stream dataStream = reponse.GetResponseStream(); // envoie de la requete
            Console.WriteLine(reponse.StatusDescription);
            StreamReader reader = new StreamReader(dataStream); //Lecture de la Requete
            string StringReponse = reader.ReadToEnd();// Convertion en String
                                                      // Console.WriteLine(StringReponse);
            Console.WriteLine(StringReponse);
            Search requestedObject = JsonConvert.DeserializeObject<Search>(StringReponse); // convertit string en objet

            Console.WriteLine(requestedObject.Id +" "+ requestedObject.Title);// objet creer par la classe
            
            Console.ReadLine();

        }
    }

    public class Search
    {
        public int Id { get; set; }
        public Boolean Readable { get; set; }
        public string Title { get; set; }
        public string Title_short { get; set; }
        public string Title_version { get; set; }
        public string URL { get; set; }
        public int Duration { get; set; }
        public int Rank { get; set; }
        public Boolean Explicit_lyrics { get; set; }
        public string Preview { get; set; }
        //public Artiste[];
        //pubmic Album[];
        
    public string SearchTrackByID(int ID)
        {
            return "https://api.deezer.com/track/" + ID.ToString();
        }
        public string SearchAlbumByID(int Album)
        {
            return "https://api.deezer.com/album/" + Album.ToString();

        }
        public string SearchPlaylistByID(long Playlist)
        {
            return "https://api.deezer.com/playlist/" + Playlist.ToString();
        }
        public string SearchArtisttByID(long Artist)
        {
            return "https://api.deezer.com/artist/" + Artist.ToString();
        }



        public string SearchTrack(string TrackName)
        {
            return "https://api.deezer.com/search/track?q=" + TrackName;

        }
        public string SearchArtist(string ArtistName)
        {
            return "https://api.deezer.com/search/artist?q=" + ArtistName;
        }
        public string SearchAlbum (string AlbumName)
        {
            return "https://api.deezer.com/search/album?q=" + AlbumName;
        }
        public string SearchTrack_Artist(string TrackName, string ArtistName)
        {
            return "https://api.deezer.com/search?q=track:" + TrackName + " artist:"+ ArtistName;

        }
        public string SearchAlbum_Artist(string AlbumName, string ArtistName)
        {
            return "https://api.deezer.com/search?q=track:" + AlbumName + " artist:" + ArtistName;

        }
        public string SearchTrack_Album(string TrackName, string AlbumName)
        {
            return "https://api.deezer.com/search?q=track:" + TrackName + " artist:" + AlbumName;

        }
    }
  
}
    



