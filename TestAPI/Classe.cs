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
    public class ApiDezzer
    {
      public int Id { get; set; }//Id musique de Deezer.com
      public string Title { get; set; } // titreMusique
      public string Singer { get; set; } //NomChanteur
      public string Album { get; set; } // Album chanson
      public string PublishYear { get; set; } // Annee parution

    }

    
}
