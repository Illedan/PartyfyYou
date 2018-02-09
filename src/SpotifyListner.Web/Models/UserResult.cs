using System;

namespace SpotifyListner.Web.Models
{
    public class UserResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string SpotifyUserId { get; set; }
    }
}