using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyListner.Web.Models
{
    public class ExternalUrls
    {
        public string spotify { get; set; }
    }

    public class Artist
    {
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Image
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

    public class Album
    {
        public string album_type { get; set; }
        public IList<Artist> artists { get; set; }
        public IList<string> available_markets { get; set; }
        public ExternalUrls external_urls { get; set; }
    public string href { get; set; }
    public string id { get; set; }
    public IList<Image> images { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string uri { get; set; }
}


    public class ExternalIds
{
    public string isrc { get; set; }
}

public class Item
{
    public Album album { get; set; }
    public IList<Artist> artists { get; set; }
    public IList<string> available_markets { get; set; }
    public int disc_number { get; set; }
    public int duration_ms { get; set; }
    //public bool explicit { get; set; }
    public ExternalIds external_ids { get; set; }
    public ExternalUrls external_urls { get; set; }
    public string href { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public int popularity { get; set; }
    public string preview_url { get; set; }
    public int track_number { get; set; }
    public string type { get; set; }
    public string uri { get; set; }
    }

    public class Context
{
    public ExternalUrls external_urls { get; set; }
    public string href { get; set; }
    public string type { get; set; }
    public string uri { get; set; }
    }

    public class SpotifyContent
{
    public long timestamp { get; set; }
    public int progress_ms { get; set; }
    public bool is_playing { get; set; }
    public Item item { get; set; }
    public Context context { get; set; }
}
    public class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }

    public class RefreshTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
    }

    public class SpotifySearchResult
    {
        public string Name { get; set; }
        public string ThumbnailUrl { get; set; }
        public string VideoId { get; set; }
    }

    

    public class Followers
    {
        public object href { get; set; }
        public int total { get; set; }
    }

    

    public class SpotifyUser
    {
        public string birthdate { get; set; }
        public string country { get; set; }
        public string display_name { get; set; }
        public string email { get; set; }
        public ExternalUrls external_urls { get; set; }
        //public Followers followers { get; set; }
        public string href { get; set; }
        public string id { get; set; }
       // public List<Image> images { get; set; }
        public string product { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
}
