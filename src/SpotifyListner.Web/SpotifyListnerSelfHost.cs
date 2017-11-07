using System;

namespace SpotifyListner.Web
{
    public class SpotifyListnerSelfHost
    {
        public SpotifyListnerSelfHost(string listeningInfo)
        {
            Console.WriteLine($"SpotifyListner listening on: {listeningInfo}");
        }

        public bool Start()
        {
            return true;
        }

        public bool Stop()
        {
            return false;
        }
    }
}
