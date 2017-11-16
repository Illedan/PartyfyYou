using System;
namespace AppAuthenticationServer.Model
{
    public struct OneTimeCode
    {
        public OneTimeCode(string code, string url)
        {
            Code = code;
            URL = url;
        }

        public string Code { get; }
        public string URL { get; }
    }
}
