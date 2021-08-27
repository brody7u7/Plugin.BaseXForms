using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TomsonsApp.Settings;

namespace WaspApp.Utilities
{
    /// <summary>
    /// Class to handle web services using and API url provided
    /// </summary>
    public class RESTClient
    {
        private Uri _apiUri;
        private HttpClient _httpClient;


        /// <summary>
        /// Initialize the REST client with url as base address
        /// </summary>
        /// <param name="url">Base address to use for endpoints</param>
        public RESTClient (string url)
        {
            _apiUri = new Uri(url);

            _httpClient = new HttpClient();
            //_httpClient.Timeout = TimeSpan.FromMinutes(1);
            _httpClient.BaseAddress = _apiUri;
        }

        /// <summary>
        /// Set the authorization header to the rest client
        /// </summary>
        /// <param name="token">Bearer token</param>
        public void SetAuthHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// Executes a post verbose to the requested method and with the content provided
        /// </summary>
        /// <typeparam name="T">Object to return</typeparam>
        /// <param name="method">Endpoint to request</param>
        /// <param name="content">Content to send</param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string method, object content)
        {
            try
            {
                var json = JsonConvert.SerializeObject(content);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(method, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsObjectAsync<T>();
                }
                else
                {
                    return await ErrorConvention<T>(response);
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return default;
        }

        /// <summary>
        /// Executes a get verbose to the requested method
        /// </summary>
        /// <typeparam name="T">Object to return</typeparam>
        /// <param name="method">Endpoint to request</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string method)
        {
            try
            {
                var response = await _httpClient.GetAsync(method);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsObjectAsync<T>();
                }
                else
                {
                    return await ErrorConvention<T>(response);
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.ToString());
            }
            return default;
        }

        /// <summary>
        /// Executes a put verbose to the requested method and with the content provided
        /// </summary>
        /// <typeparam name="T">Object to return</typeparam>
        /// <param name="method">Endpoint to request</param>
        /// <param name="content">Content to send</param>
        /// <returns></returns>
        public async Task<T> PutAsync<T>(string method, object content)
        {
            try
            {
                var json = JsonConvert.SerializeObject(content);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(method, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsObjectAsync<T>();
                }
                else
                {
                    return await ErrorConvention<T>(response);
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.ToString());
            }
            return default;
        }

        /// <summary>
        /// Executes a delete verbose to the requested method
        /// </summary>
        /// <typeparam name="T">Object to return</typeparam>
        /// <param name="method">Endpoint to request</param>
        /// <returns></returns>
        public async Task<T> DeleteAsync<T>(string method)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(method);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsObjectAsync<T>();
                }
                else
                {
                    return await ErrorConvention<T>(response);
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.ToString());
            }
            return default;
        }


        protected async Task<T> ErrorConvention<T>(HttpResponseMessage response)
        {
            try
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                // There is no response from API
                var result = JsonConvert.DeserializeObject<T>("{ }");
                return result;
            }
        }
    }

    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsObjectAsync<T>(this HttpContent content)
        {
            using (var stream = await content.ReadAsStreamAsync())
            using (var reader = new System.IO.StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                return new JsonSerializer().Deserialize<T>(json);
            }
        }
    }
}
