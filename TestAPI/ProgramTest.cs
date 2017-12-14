using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api;
using System.Net.Http.Headers;

namespace TestAPI
{
    class ProgramTest
    {
       /* static HttpClient client = new HttpClient(); // creation d'une variable pour requete sur API 
        //Affichage des requetes
        static void WriteTrack(ApiDezzer track) => Console.WriteLine($"Name : {track.Title}, Singer :  {track.Singer}, Album: {track.Album}");// maniere d'écriture pour les methodes à une seule ligne

        //Get pour optenir les données
        static async Task<ApiDezzer> GetApiDezzerAsync(string track)
        {
            ApiDezzer Api = null;
            HttpResponseMessage reponse = await client.GetAsync(track);
            if (reponse.IsSuccessStatusCode)
            {
                Api = await reponse.Content.ReadAsAsync<ApiDezzer>();
            }
            return Api; 
        }*/

 
      /*  static void Main(string[] args)
        {
            
    

        }*/
    }
}
