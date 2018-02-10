using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyListner.Web.Services
{
    public interface IRestClientWrapper
    {
        Task<T> GetAsync<T>(Guid id, string urlEnding);
        Task<T> GetAsync<T>(IEnumerable<Guid> ids, string urlEnding);
        Task<T> GetAsync<T>(Dictionary<string, object> identifierIdDictionary, string urlEnding);
        Task<T> GetAsync<T>(IEnumerable<Guid> ids, string urlEnding, string identifier);
        Task<T> GetAsync<T>(string id, string urlEnding);
        Task<T> GetAsync<T>(IEnumerable<string> ids, string urlEnding);
        Task<T> PostAsync<T>(object objects, string urlEnding);
        Task<T> PutAsync<T>(object body, Guid id, string urlEnding);
        Task PutAsync(object body, Guid id, string urlEnding);
        Task<HttpResponseMessage> PostAsync_HttpResponse(object checkResult, string v);
        Task<HttpResponseMessage> DeleteAsync(IEnumerable<Guid> ids, string urlEnding);
        Task<HttpResponseMessage> DeleteAsync(Guid id, string urlEnding);
        Task<T> GetAsync<T>(int elementId, string v);
    }
}
