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


namespace API
{
    public class Authentification
    {
        public string App_ID = "263102";
        public string App_SECREt = "45c24250c1e2be4fa836f9a92aabb4fa";
        public string MyURL = "www.deezer.com";
        public string NumCOde { get; set; }



        public void Authorisation()
        {
            System.Diagnostics.Process.Start("https://connect.deezer.com/oauth/auth.php?app_id=263102&redirect_uri=http://www.deezer.com&perms=basic_access,manage_library,delete_library,listening_history,access_token"); 
        }




        public void Token()
        {
            Console.WriteLine("N°CodeDeezer"); 
            NumCOde = Console.ReadLine();
            //requete d'envois 
            WebRequest Access_Token = WebRequest.Create("https://connect.deezer.com/oauth/access_token.php?app_id="+App_ID+"&secret="+App_SECREt+"&code="+NumCOde);
            Access_Token.Method = "T";
            HttpWebResponse ReceptionToken = (HttpWebResponse)Access_Token.GetResponse();
            Stream DataToken = ReceptionToken.GetResponseStream();
            Console.WriteLine(ReceptionToken);
            StreamReader reader = new StreamReader(DataToken); //Lecture de la Requete
            string StringReponse = reader.ReadToEnd();// Convertion en String                                               
            Console.WriteLine(StringReponse);
            Console.ReadLine();
        }

    }

    public class ModifPlaylist
    {
        public string NameListe { get; set; }
        


        public void CreatePlaylist (string IDUser, string PlayslistName)
        {
            WebRequest Create_Liste = WebRequest.Create("https://api.deezer.com/user/" + IDUser + "/playlists?title="+ PlayslistName);
            Create_Liste.Method = "POST";
            HttpWebResponse ReceptionPost = (HttpWebResponse)Create_Liste.GetResponse();
            Stream DataCreation = ReceptionPost.GetResponseStream();
            Console.WriteLine(ReceptionPost);
            StreamReader reader = new StreamReader(DataCreation);
            string StringReponse = reader.ReadToEnd();
            Console.WriteLine(StringReponse); 
            
            
        }
    }
}