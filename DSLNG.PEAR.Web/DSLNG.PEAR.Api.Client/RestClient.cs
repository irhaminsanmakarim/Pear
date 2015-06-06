using DSLNG.PEAR.Api.Client.Authentication;
using System;
using System.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace DSLNG.PEAR.Api.Client
{
    /// <summary>
    /// A wrapper for a web api REST service that optionally allows different levels
    /// of authentication to be added to the header of the request that will then be
    /// checked using the SecretAuthenticationFilter in the web api controller methods.
    /// 
    /// Example Usage:
    ///   No authentication...
    ///     var productsClient = new RestClient<Product>("http://localhost/ServiceTier/api/");
    ///   Simple authentication...
    ///     var productsClient = new RestClient<Product>("http://localhost/ServiceTier/api/","productscontrollersecret");
    ///   HMAC authentication...
    ///     var productsClient = new RestClient<Product>("http://localhost/ServiceTier/api/","productscontrollersecret", true);
    /// 
    /// Example method calls:
    ///   var getManyResult = productsClient.GetMultipleItemsRequest("products?page=1").Result;
    ///   var getSingleResult = productsClient.GetSingleItemRequest("products/1").Result;
    ///   var postResult = productsClient.PostRequest("products", new Product { Id = 3, ProductName = "Dynamite", ProductDescription = "Acme bomb" }).Result;
    ///   productsClient.PutRequest("products/3", new Product { Id = 3, ProductName = "Dynamite", ProductDescription = "Acme bomb" }).Wait();
    ///   productsClient.DeleteRequest("products/3").Wait();
    /// </summary>
    /// <typeparam name="T">The class being manipulated by the REST api</typeparam>
    public class RestClient<T> where T : class
    {
        private readonly string _baseAddress;
        private readonly string _sharedSecretName;
        private readonly bool _hmacSecret;

        public RestClient(string baseAddress) : this(baseAddress, null, false) { }
        public RestClient(string baseAddress, string sharedSecretName) : this(baseAddress, sharedSecretName, false) { }
        public RestClient(string baseAddress, string sharedSecretName, bool hmacSecret)
        {
            // e.g. http://localhost/ServiceTier/api/
            _baseAddress = baseAddress;
            _sharedSecretName = sharedSecretName;
            _hmacSecret = hmacSecret;
        }

        /// <summary>
        /// Used to setup the base address, that we want json, and authentication headers for the request
        /// </summary>
        /// <param name="client">The HttpClient we are configuring</param>
        /// <param name="methodName">GET, POST, PUT or DELETE. Aim to prevent hacker changing the 
        /// method from say GET to DELETE</param>
        /// <param name="apiUrl">The end bit of the url we use to call the web api method</param>
        /// <param name="content">For posts and puts the object we are including</param>
        private void SetupClient(HttpClient client, string methodName, string apiUrl, T content = null)
        {
            // Three versions in one.
            // Just specify a base address and no secret token will be added
            // Specify a sharedSecretName and we will include the contents of it found in the web.config as a SecretToken in the header
            // Ask for HMAC and a HMAC will be generated and added to the request header
            const string secretTokenName = "SecretToken";

            client.BaseAddress = new Uri(_baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (_hmacSecret)
            {
                // hmac using shared secret a representation of the message, as we are
                // including the time in the representation we also need it in the header
                // to check at the other end.
                // You might want to extend this to also include a username if, for instance,
                // the secret key varies by username
                client.DefaultRequestHeaders.Date = DateTime.UtcNow;
                var datePart = client.DefaultRequestHeaders.Date.Value.UtcDateTime.ToString(CultureInfo.InvariantCulture);

                var fullUri = _baseAddress + apiUrl;

                var contentMD5 = "";
                if (content != null)
                {
                    var json = new JavaScriptSerializer().Serialize(content);
                    contentMD5 = Hashing.GetHashMD5OfString(json);
                }

                var messageRepresentation =
                    methodName + "\n" +
                    contentMD5 + "\n" +
                    datePart + "\n" +
                    fullUri;

                var sharedSecretValue = ConfigurationManager.AppSettings[_sharedSecretName];

                var hmac = Hashing.GetHashHMACSHA256OfString(messageRepresentation, sharedSecretValue);
                client.DefaultRequestHeaders.Add(secretTokenName, hmac);
            }
            else if (!string.IsNullOrWhiteSpace(_sharedSecretName))
            {
                var sharedSecretValue = ConfigurationManager.AppSettings[_sharedSecretName];
                client.DefaultRequestHeaders.Add(secretTokenName, sharedSecretValue);

            }
        }

        /// <summary>
        /// For getting a single item from a web api uaing GET
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        /// api get method, e.g. "products/1" to get a product with an id of 1</param>
        /// <returns>The item requested</returns>
        public async Task<T> GetSingleItemRequest(string apiUrl)
        {
            T result = null;

            using (var client = new HttpClient())
            {
                SetupClient(client, "GET", apiUrl);

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }

            return result;
        }

        /// <summary>
        /// For getting multiple (or all) items from a web api using GET
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        /// api get method, e.g. "products?page=1" to get page 1 of the products</param>
        /// <returns>The items requested</returns>
        public async Task<T[]> GetMultipleItemsRequest(string apiUrl)
        {
            T[] result = null;

            using (var client = new HttpClient())
            {
                SetupClient(client, "GET", apiUrl);

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T[]>(x.Result);
                });
            }

            return result;
        }

        /// <summary>
        /// For creating a new item over a web api using POST
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        /// api post method, e.g. "products" to add products</param>
        /// <param name="postObject">The object to be created</param>
        /// <returns>The item created</returns>
        public async Task<T> PostRequest(string apiUrl, T postObject)
        {
            T result = null;

            using (var client = new HttpClient())
            {
                SetupClient(client, "POST", apiUrl, postObject);

                var response = await client.PostAsync(apiUrl, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);

                });
            }

            return result;
        }

        /// <summary>
        /// For updating an existing item over a web api using PUT
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        /// api put method, e.g. "products/3" to update product with id of 3</param>
        /// <param name="putObject">The object to be edited</param>
        public async Task PutRequest(string apiUrl, T putObject)
        {
            using (var client = new HttpClient())
            {
                SetupClient(client, "PUT", apiUrl, putObject);

                var response = await client.PutAsync(apiUrl, putObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// For deleting an existing item over a web api using DELETE
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        /// api delete method, e.g. "products/3" to delete product with id of 3</param>
        public async Task DeleteRequest(string apiUrl)
        {
            using (var client = new HttpClient())
            {
                SetupClient(client, "DELETE", apiUrl);

                var response = await client.DeleteAsync(apiUrl).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
