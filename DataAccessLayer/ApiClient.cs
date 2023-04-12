using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using CommonEntities;
using CommonEntities.Constants;
using CommonEntities.UsersModels;
using System.Net.Http.Json;
using System.Data;
using Microsoft.Extensions.Options;

namespace DataAccessLayer
{
    public class ApiClient : IApiClient
    {
        private readonly Uri _baseEndpoint;
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;


        public ApiClient(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _baseEndpoint = new Uri(_appSettings.WebapiBaseUrl);
            _httpClient = new HttpClient();
            AddHeaders();
        }

        // GET
        public async Task<T?> GetAsync<T>(string relativePath = "") where T : class
        {
            try 
            { 
                var requestUrl = CreateRequestUri(AppConstant.ENDPOINT_USER, relativePath);
                var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var userDetails = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(userDetails);
            }
            catch (Exception)
            {
                throw;
            }
}

        //POST
        public async Task<UserDetail?> PostAsync<T>(T content)
        {
            try
            {
                var requestUrl = CreateRequestUri(AppConstant.ENDPOINT_USER);
                var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent(content).Content);
                response.EnsureSuccessStatusCode();
                var userDetail = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserDetail>(userDetail);
             }
            catch (Exception)
            {
                throw;
            }
    
        }

        //PUT
        public async Task<UserDetail?> PutAsync<T>(string relativePath,T content)
        {
            try
            {
                var requestUrl = CreateRequestUri(String.Concat(AppConstant.ENDPOINT_USER, relativePath));
                var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent(content).Content);
                response.EnsureSuccessStatusCode();
                var userDetail = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserDetail>(userDetail);
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        //DELETE
        public async Task<UserDetail?> DeleteAsync(string relativePath = "")
        {
            try
            {
                var requestUrl = CreateRequestUri(relativePath);
                var response = await _httpClient.DeleteAsync(requestUrl.ToString());
                response.EnsureSuccessStatusCode();
                var userDetail = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserDetail>(userDetail);
               
            }
            catch (Exception)
            {
                throw;
            }
    
        }


        #region Utilities
        // Add Headers
        private void AddHeaders()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AppConstant.ENDPOINT_TOKEN, _appSettings.AccessToken);
        }

        // Create Request Uri
        private Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endPoint = new Uri(_baseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endPoint)
            {
                Query = queryString
            };
            return uriBuilder.Uri;
        }

        // Create Http Content
        private static HttpRequestMessage CreateHttpContent<T>(T content)
        {
            var jsonUserDetail = JsonConvert.SerializeObject(content);
            var reqMessage = new HttpRequestMessage
            {
                Content = new StringContent(jsonUserDetail, Encoding.UTF8, "application/json")
            };
            reqMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return reqMessage;
        }
        
        #endregion



    }
}