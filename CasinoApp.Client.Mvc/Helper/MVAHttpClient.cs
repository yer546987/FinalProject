using System.Net;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System;
using CasinoApp.Entities.Empleado;

namespace CasinoApp.Client.Helper
{
    public class MVAHttpClient
    {
        
        #region Fields
        private HttpClient _httpClient;
        private string URLBASE = "https://localhost:44335";
        private const string _contentTypeSuport = "application/json";
        #endregion

        #region Builder
        public MVAHttpClient()
        {
            _httpClient = new HttpClient();
        }
        #endregion

        #region Methods

        public T Get<T>(string url)
        {
            using (HttpRequestMessage request = createRequest(HttpMethod.Get, url))
            {
                HttpResponseMessage response = _httpClient.Send(request);
                return deserializeResponse<T>(response);
            }
        }

        public T Post<T>(string url, object body)
        {
            using (HttpRequestMessage request = createRequest(HttpMethod.Post, url))
            {

                if (body != null)
                {
                    var json = string.Empty;
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    json = JsonSerializer.Serialize(body, options);
                    request.Content = new StringContent(json, Encoding.UTF8, _contentTypeSuport);
                }
                var response = _httpClient.Send(request);
                return deserializeResponse<T>(response);
            }
        }

        #endregion


        #region PrivateMethods

        private HttpRequestMessage createRequest(HttpMethod method, string url)
        {
            Uri uri = new Uri(URLBASE + url);
            HttpRequestMessage request = new HttpRequestMessage(method, uri);
            request.Headers.Add("Accept", _contentTypeSuport);
            request.Headers.Add("Access-Control-Allow-Origin", "*");
            request.Headers.Add("Access-Control-Allow-Credentials", "true");
            request.Headers.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return request;
        }

        private T deserializeResponse<T>(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return default;
                case HttpStatusCode.UnprocessableEntity:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.OK:
                    string jsonString = response.Content.ReadAsStringAsync().Result;
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        PropertyNamingPolicy = null,
                        IncludeFields = true
                    };
                    var respuesta = JsonSerializer.Deserialize<T>(jsonString, options);

                    return respuesta;
                //case HttpStatusCode.InternalServerError:
                //    throw new IntegrationException($"Ha ocurrido un error en el consumo del servicio. Status code: {response.StatusCode} {response.Headers} ", new HttpRequestException(await response.Content.ReadAsStringAsync()));
                //case HttpStatusCode.BadRequest:
                //    throw new IntegrationException($"Ha ocurrido un error en el consumo del servicio. Status code: {response.StatusCode} {response.Headers} ", new HttpRequestException(await response.Content.ReadAsStringAsync()));
                default:
                    throw new Exception($"Ha ocurrido un error en el consumo del servicio. Status code: {response.StatusCode} {response.Headers} Content: {response.Content.ReadAsStringAsync().Result}");
            }
        }


        #endregion

    }
}

