using System;
using System.Collections.Generic;
using System.Text;

namespace Api
{
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

    public class User
    {
       public int Id { get; set; }
       public string Name { get; set; }
       public string Lastname { get; set; }
       public string Firstname { get; set; }
       public string Email { get; set; }
       public int Status { get; set; }
       public DateTime Birthday { get; set; }
       public DateTime Inscription_Date { get; set; }
       public string Gender { get; set; }
       public string Link { get; set; }
       public string Picture_Small { get; set; }
       public string Picture_Medium { get; set; }
       public string Picture_Big { get; set; }
       public string Picture_XL { get; set; }
       public string Country { get; set; }
       public string Lang { get; set; }
       public bool Is_Kid { get; set; }
       public string Tracklist { get; set; }

    }
}
