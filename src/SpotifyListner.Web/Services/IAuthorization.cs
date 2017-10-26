using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyListner.Web.Services
{
    public interface IAuthorization
    {
        string GetNewToken(string originalToken);
    }
}
