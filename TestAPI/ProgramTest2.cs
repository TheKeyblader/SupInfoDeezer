using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;


namespace API
{

    class Program
    {
        static void Main(string[] args)
        {
            Authentification Login = new Authentification();

            Login.Authorisation();
            Login.Token();
            

           ModifPlaylist Modif = new ModifPlaylist();
            Modif.CreatePlaylist("9070265", "grosTest");
           

            /*// Requete POST (modification)
            WebRequest Modif = WebRequest.Create("https://api.deezer.com/user/9070265/playlists/?title=test");
            Modif.Method = "POST";
            HttpWebResponse ResponseModif = (HttpWebResponse)Modif.GetResponse();
            //Stream respose = Modif.GetRequestStream();
            Console.WriteLine(ResponseModif.StatusDescription);

            //Requete GET (obtention d'information) 
            Search IdCherche = new Search();
            WebRequest request = WebRequest.Create(IdCherche.SearchPlaylistByUser(9070265,"zamiquie")); // requete a envoyer
            request.Credentials = CredentialCache.DefaultCredentials; 
            HttpWebResponse reponse = (HttpWebResponse)request.GetResponse(); // variable de reception de la requete 
            request.Method = "POST"; // type de demande (get = obtenir, post = modifier, Delete)
            Stream dataStream = reponse.GetResponseStream(); // envoie de la requete
            //Console.WriteLine(reponse.StatusDescription);// retourne statut de la requete 
            StreamReader reader = new StreamReader(dataStream); //Lecture de la Requete
            string StringReponse = reader.ReadToEnd();// Convertion en String                                                  // Console.WriteLine(StringReponse);
            Console.WriteLine(StringReponse);
            
             // Mise en objet
            //Search requestedObject = JsonConvert.DeserializeObject<Search>(StringReponse); // convertit string en objet
            //Console.WriteLine(requestedObject.Id + " " + requestedObject.Title);// objet creer par la classe
            
            Console.ReadLine();*/

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


        public string SearchUser(string User)
        {
            return "https://api.deezer.com/search?q=user:" + User;
        }
        public string SearchUserByID(long IDUser)
        {
            return "https://api.deezer.com/user/" + IDUser.ToString();
        }

        public string SearchPlaylistByUser(int IDUser, string NameUser)
        {
            return "https://api.deezer.com/playlist/" + IDUser.ToString() + "/" + NameUser;
        }



    }



}