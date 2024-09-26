using InvoiceBulkRegisteration.Logging;
using Newtonsoft.Json;
using sassy.bulk.Exception;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sassy.bulk.Webhooks
{
    internal static class Webhook
    {
        private static HttpClient _httpClient = new HttpClient();
        public static string BearerToken { get; set; }
        /// <summary>
        /// Sends an asynchronous webhook request to the specified URL.
        /// </summary>
        /// <typeparam name="TData">The type of data to be sent in the request body.</typeparam>
        /// <param name="endPoint">The URL of the webhook endpoint.</param>
        /// <param name="data">The data to be sent in the request body.</param>
        /// <param name="contentType">The content type of the request body.</param>
        /// <param name="additionalHeaders">Optional additional headers to be included in the request.</param>
        /// <returns>A task that returns an HttpResponseMessage object representing the response from the webhook endpoint.</returns>
        /// <exception cref="BulkDataExeException">Throws an exception if an error occurs during the request.</exception>
        public static async Task<HttpResponseMessage> SendAsync<TData>(string endPoint, TData data, string contentType, IEnumerable<KeyValuePair<string, string>> additionalHeaders = null, CancellationToken token = default)
        {
            var request = CreateRequest(HttpMethod.Post, endPoint, data, contentType, additionalHeaders);
            HttpResponseMessage response = null;
            token.ThrowIfCancellationRequested();
            try
            {
                response = await _httpClient.PostAsync(endPoint, request.Content, token).ConfigureAwait(false);
            }
            catch (BulkDataExeException e)
            {
                var logService = new LogService();
                logService.Exception(e);
            }
            return response;
        }

        /// <summary>
        /// Sends an asynchronous GET request to the specified endpoint and returns the deserialized response.
        /// </summary>
        /// <typeparam name="TEntity">The type of the expected success response object.</typeparam>
        /// <typeparam name="TError">The type of the expected error response object.</typeparam>
        /// <param name="endPoint">The URL of the endpoint to send the request to.</param>
        /// <param name="contentType">The content type of the request.</param>
        /// <param name="additionalHeaders">Additional headers to include in the request.</param>
        /// <param name="token">A cancellation token to signal that the operation should be canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result will be either the deserialized success response object or the deserialized error response object.</returns>
        public static async Task<object> SendAsync<TEntity, TError>(string endPoint, string contentType, IEnumerable<KeyValuePair<string, string>> additionalHeaders = null, CancellationToken token = default) where TEntity : class where TError : class
        {
            var request = CreateRequest(HttpMethod.Get, endPoint, null, contentType, additionalHeaders);
            token.ThrowIfCancellationRequested();
            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.SendAsync(request, token).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    TEntity entity;
                    var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    entity = JsonConvert.DeserializeObject<TEntity>(stringContent);
                    return entity;
                }
                if (!response.IsSuccessStatusCode)
                {
                    TError error;
                    var errorContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    error = JsonConvert.DeserializeObject<TError>(errorContent);
                    return error;
                }
            }
            catch (BulkDataExeException ex)
            {
                PrintException(ex);
            }
            return null;
        }
        /// <summary>
        /// Sends an asynchronous GET request to the specified URL.
        /// </summary>
        /// <typeparam name="TResponse">The type of data expected in the response body.</typeparam>
        /// <param name="endPoint">The URL of the endpoint to retrieve data from.</param>
        /// <param name="contentType">The content type expected in the response body.</param>
        /// <param name="requestParameter">Optional request parameters to be included in the URL as query string parameters.</param>
        /// <param name="additionalHeaders">Optional additional headers to be included in the request.</param>
        /// <returns>A task that returns a TResponse object deserialized from the response body.</returns>
        /// <exception cref="WebhookException">Throws a WebhookException if an error occurs during the request or response deserialization.</exception>
        //public static async Task<TResponse> GetAsync<TResponse>(string endPoint, string contentType, object requestParameter = null, IEnumerable<KeyValuePair<string, string>> additionalHeaders = null, CancellationToken token = default)
        //{
        //    var request = CreateRequest(HttpMethod.Get, endPoint, requestParameter, contentType, additionalHeaders);
        //    token.ThrowIfCancellationRequested();
        //    HttpResponseMessage response = null;
        //    try
        //    {
        //        response = await _httpClient.SendAsync(request, token).ConfigureAwait(false);
        //    }
        //    catch (BulkDataExeException ex)
        //    {
        //        PrintException(ex);
        //    }

        //    HandleResponse(response);

        //    TResponse data;
        //    try
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        data = JsonConvert.DeserializeObject<TResponse>(json);
        //    }
        //    catch (BulkDataExeException ex)
        //    {
        //        PrintException(ex);
        //    }
        //    return data;
        //}
        private static async void HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                throw new System.Exception($"Webhook failed with status code: {response.StatusCode}. Response content: {responseContent}");
            }
        }
        private static HttpRequestMessage CreateRequest(HttpMethod method, string url, object data, string contentType, IEnumerable<KeyValuePair<string, string>> additionalHeaders)
        {
            _httpClient = new HttpClient();
            var request = new HttpRequestMessage(method, url);
            if (contentType != null)
            {
                if (!string.IsNullOrEmpty(BearerToken))
                {
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {BearerToken}");
                }
                _httpClient.DefaultRequestHeaders.Add("Accept", contentType);
            }

            if (data != null)
            {
                string serializedData;
                switch (contentType)
                {
                    case "application/json":
                        serializedData = JsonConvert.SerializeObject(data);
                        break;
                    case "":
                        serializedData = new FormUrlEncodedContent(data as IEnumerable<KeyValuePair<string, string>>).ReadAsStringAsync().Result;
                        break;
                    default:
                        throw new ArgumentNullException("Unsupported Content Type");
                };
                request.Content = new StringContent(serializedData, Encoding.UTF8, contentType);
            }

            if (additionalHeaders != null)
            {
                foreach (var header in additionalHeaders)
                {
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            return request;
        }
        private static void PrintException(BulkDataExeException ex)
        {
            var logService = new LogService();
            logService.Exception(ex);
        }
    }
}
