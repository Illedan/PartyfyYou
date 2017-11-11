using Microsoft.Owin.Hosting;
using System;
using System.Configuration;

namespace Partify.Storage.SelfHost
{
    public class WebApplication
    {
        private IDisposable m_application;

        public void Start()
        {
            var startOptions = new StartOptions();
            var urlstring = ConfigurationManager.AppSettings["ListeningAddress"];
            startOptions.Urls.Add(urlstring); // 10.0.110.190
            m_application = WebApp.Start<Startup>(startOptions);
        }

        public void Stop()
        {
            m_application.Dispose();
        }
    }
}
