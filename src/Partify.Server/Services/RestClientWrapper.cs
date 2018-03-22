using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Partify.Server.Services
{
    public class RestClientWrapper : IRestClientWrapper
    {
        private readonly HttpClient m_httpClient;

        public RestClientWrapper()
        {
            m_httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://partifystoragewebapp.azurewebsites.net/api/")
            };
            m_httpClient.DefaultRequestHeaders.Accept.Clear();
            m_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> DeleteAsync(IEnumerable<Guid> ids, string urlEnding)
        {
            var idsString = string.Join("&id=", ids);

            var response = await m_httpClient.DeleteAsync(urlEnding + idsString);

            return response;
        }

        public async Task<T> GetAsync<T>(Guid id, string urlEnding)
        {
            HttpResponseMessage response = null;

            if (id == Guid.Empty)
            {
                // Hent alle
                response = await m_httpClient.GetAsync(urlEnding);
            }
            else
            {
                response = await m_httpClient.GetAsync(urlEnding + id);
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> GetAsync<T>(string text, string urlEnding)
        {
            HttpResponseMessage response = await m_httpClient.GetAsync(urlEnding + text);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> GetAsync<T>(IEnumerable<Guid> ids, string urlEnding)
        {
            var idString = string.Join("&id=", ids);

            var response = await m_httpClient.GetAsync(urlEnding + idString);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }


        public async Task<T> GetAsync<T>(IEnumerable<Guid> ids, string urlEnding, string identifier)
        {
            var idString = string.Join($"&{identifier}=", ids);
            idString = "?" + identifier + "=" + idString;
            var response = await m_httpClient.GetAsync(urlEnding + idString);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> GetAsync<T>(Dictionary<string,object> identifierIdDictionary, string urlEnding)
        {//http://localhost:5000/api/Suggestion?songId=asd&modeId=a59c6963-7188-4b5f-b3a8-d2f96177c40e&userId=98dd4033-1880-424a-acff-56cf38f0cda0
            var idString = "?";

           
            foreach (var item in identifierIdDictionary)
            {
                idString += item.Key + "=" + item.Value.ToString() + "&";
            }
            idString = idString.Remove(idString.Length - 1);

            var response = await m_httpClient.GetAsync(urlEnding + idString);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> GetAsync<T>(IEnumerable<string> id, string urlEnding)
        {
            var idString = string.Join("&id=", id);

            var response = await m_httpClient.GetAsync(urlEnding + idString);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> GetAsync<T>(int elementId, string urlEnding)
        {
            HttpResponseMessage response = await m_httpClient.GetAsync(urlEnding + elementId);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> PostAsync<T>(object objects, string urlEnding)
        {
            var myContent = JsonConvert.SerializeObject(objects);

            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await m_httpClient.PostAsync(urlEnding, byteContent);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<HttpResponseMessage> PostAsync_HttpResponse(object checkResult, string urlEnding)
        {
            var myContent = JsonConvert.SerializeObject(checkResult);

            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await m_httpClient.PostAsync(urlEnding, byteContent);
            return response;
        }

        public async Task<T> PutAsync<T>(object body, Guid id, string urlEnding)
        {
            var myContent = JsonConvert.SerializeObject(body);

            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await m_httpClient.PutAsync(urlEnding + id, byteContent);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task PutAsync(object body, Guid id, string urlEnding)
        {
            var myContent = JsonConvert.SerializeObject(body);

            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await m_httpClient.PutAsync(urlEnding + id, byteContent);
            return;
        }

        public async Task<HttpResponseMessage> DeleteAsync(Guid id, string urlEnding)
        {
            var response = await m_httpClient.DeleteAsync(urlEnding + id);

            return response;
        }
    }
}