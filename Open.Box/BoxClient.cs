﻿using Open.IO;
using Open.Net.Http;
using Open.OAuth2;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Open.Box
{
    public class BoxClient : OAuth2Client
    {
        #region ** fields

        private static readonly string ApiServiceUri = "https://api.box.com/2.0/";
        private static readonly string UploadApiServiceUri = "https://upload.box.com/api/2.0/files/content";
        private static readonly string OAuthUri = "https://www.box.com/api/oauth2/authorize";
        private static readonly string OAuthToken = "https://www.box.com/api/oauth2/token";
        private string _accessToken = null;

        #endregion

        #region ** initialization

        public BoxClient()
        {
        }

        public BoxClient(string accessToken)
        {
            _accessToken = accessToken;
        }

        #endregion

        #region ** authentication

        public static string GetRequestUrl(string clientId, string scope, string callbackUrl)
        {
            return OAuth2Client.GetRequestUrl(OAuthUri, clientId, scope, callbackUrl);
        }

        public static async Task<OAuth2Token> ExchangeCodeForAccessTokenAsync(string code, string clientId, string clientSecret, string callbackUrl)
        {
            return await ExchangeCodeForAccessTokenAsync(OAuthToken, code, clientId, clientSecret, callbackUrl);
        }

        public static async Task<OAuth2Token> RefreshAccessTokenAsync(string refreshToken, string clientId, string clientSecret, CancellationToken cancellationToken)
        {
            return await OAuth2Client.RefreshAccessTokenAsync(OAuthToken, refreshToken, clientId, clientSecret, cancellationToken);
        }

        #endregion

        #region ** public methods

        public async Task<User> GetCurrentUser(CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>();

            var uri = BuildApiUri(GetUsersPath(), parameters);
            var client = CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<User>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Item> GetFolderAsync(string folderId, string fields = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (offset.HasValue)
                parameters["offset"] = offset.ToString();
            if (limit.HasValue)
                parameters["limit"] = limit.ToString();
            if (!string.IsNullOrWhiteSpace(fields))
                parameters["fields"] = fields;

            var uri = BuildApiUri(GetFolderPath(folderId), parameters);
            var client = CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Item>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<ItemCollection> GetFolderItemsAsync(string folderId, int? limit = null, string offset = null, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (limit.HasValue)
                parameters["limit"] = limit.ToString();
            if (!string.IsNullOrWhiteSpace(offset))
                parameters["offset"] = offset;
            if (!string.IsNullOrWhiteSpace(fields))
                parameters["fields"] = fields;

            var uri = BuildApiUri(string.Format("folders/{0}/items", folderId), parameters);
            var client = CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<ItemCollection>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Stream> GetThumbnailAsync(string itemId, double minWidth = double.NaN, double maxWidth = double.NaN, double minHeight = double.NaN, double maxHeight = double.NaN, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (!double.IsNaN(minWidth))
                parameters["min_width"] = minWidth.ToString(CultureInfo.InvariantCulture);
            if (!double.IsNaN(maxWidth))
                parameters["max_width"] = maxWidth.ToString(CultureInfo.InvariantCulture);
            if (!double.IsNaN(minHeight))
                parameters["min_height"] = minHeight.ToString(CultureInfo.InvariantCulture);
            if (!double.IsNaN(maxHeight))
                parameters["max_height"] = maxHeight.ToString(CultureInfo.InvariantCulture);

            var uri = BuildApiUri(string.Format("files/{0}/thumbnail.png", itemId), parameters);
            var client = CreateClient(new RetryMessageHandler());
            var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                return new StreamWithLength(await response.Content.ReadAsStreamAsync(), response.Content.Headers.ContentLength);
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Item> CreateFolderAsync(Item folder, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(fields))
                parameters.Add("fields", fields);

            var uri = BuildApiUri("folders", parameters);
            var client = CreateClient();
            var content = new StringContent(folder.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Item>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        //public Uri GetDownloadUrl(string fileId)
        //{
        //    var parameters = new Dictionary<string, string>();
        //    return BuildApiUri(string.Format("files/{0}/content", fileId), parameters);
        //}

        public async Task<ItemCollection> UploadFileAsync(Item file, Stream fileStream, IProgress<StreamProgress> progress, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>();
            var client = CreateClient(new RetryMessageHandler());
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(file.SerializeJson()), "attributes");
            content.Add(new StreamedContent(fileStream, progress, cancellationToken), "file", file.Name);
            var response = await client.PostAsync(UploadApiServiceUri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<ItemCollection>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Stream> DownloadFileAsync(string fileId, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>();
            var uri = BuildApiUri(string.Format("files/{0}/content", fileId), parameters);
            var client = CreateClient(new RetryMessageHandler());
            var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return new StreamWithLength(await response.Content.ReadAsStreamAsync(), response.Content.Headers.ContentLength);
            }
            else
            {
                throw await ProcessException(response);
            }
            //int statusCode = (int)(response as HttpWebResponse).StatusCode;
            //if (statusCode == 200)
            //{
            //    var respStream = response.GetResponseStream();
            //    return await Task<IInputStream>.Factory.StartNew(() =>
            //    {
            //        var memoryStream = new System.IO.MemoryStream();
            //        respStream.CopyTo(memoryStream);
            //        return memoryStream.AsInputStream();
            //    });
            //}
            //else if (statusCode == 202)
            //{
            //    var retryAfter = int.Parse(response.Headers["Retry-After"]);
            //    throw new RetryAfterException(TimeSpan.FromSeconds(retryAfter));
            //}
            //throw new NotImplementedException();
        }

        public async Task DeleteFolderAsync(string folderId, bool recursive = true, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(fields))
                parameters["fields"] = fields;
            if (recursive)
                parameters["recursive"] = "true";

            var uri = BuildApiUri(GetFolderPath(folderId), parameters);
            var client = CreateClient();
            var response = await client.DeleteAsync(uri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw await ProcessException(response);
            }
        }

        public async Task DeleteFileAsync(string fileId, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(fields))
                parameters["fields"] = fields;

            var uri = BuildApiUri(string.Format("files/{0}", fileId), parameters);
            var client = CreateClient();
            var response = await client.DeleteAsync(uri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Item> UpdateFolderAsync(Item folder, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(fields))
                parameters["fields"] = fields;

            var uri = BuildApiUri(GetFolderPath(folder.Id), parameters);
            var client = CreateClient();
            var content = new StringContent(folder.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Item>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Item> UpdateFileAsync(Item file, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(fields))
                parameters["fields"] = fields;

            var uri = BuildApiUri(string.Format("files/{0}", file.Id), parameters);
            var client = CreateClient();
            var content = new StringContent(file.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Item>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Item> CopyFolderAsync(Item folder, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(fields))
                parameters["fields"] = fields;

            var uri = BuildApiUri(string.Format("folders/{0}/copy", folder.Id), parameters);
            var client = CreateClient();
            var content = new StringContent(folder.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Item>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Item> CopyFileAsync(Item file, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(fields))
                parameters["fields"] = fields;

            var uri = BuildApiUri(string.Format("files/{0}/copy", file.Id), parameters);
            var client = CreateClient();
            var content = new StringContent(file.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Item>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<ItemCollection> SearchAsync(string query, int limit = 50, int offset = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            parameters["query"] = query;
            parameters["limit"] = limit.ToString();
            parameters["offset"] = offset.ToString();

            var uri = BuildApiUri("search", parameters);
            var client = CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<ItemCollection>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }


        public async Task<CommentsCollection> GetCommentsAsync(string fileId, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(fields))
                parameters["fields"] = fields;

            var uri = BuildApiUri(string.Format("files/{0}/comments", fileId), parameters);
            var client = CreateClient();
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<CommentsCollection>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Comment> AddCommentAsync(Comment comment, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(fields))
                parameters["fields"] = fields;

            var uri = BuildApiUri("comments", parameters);
            var client = CreateClient();
            var content = new StringContent(comment.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Comment>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        private Uri BuildApiUri(string path, Dictionary<string, string> parameters)
        {
            UriBuilder builder = new UriBuilder(ApiServiceUri);
            builder.Path += path;
            builder.Query = (parameters != null && parameters.Count() > 0 ? string.Join("&", parameters.Select(pair => pair.Key + "=" + EscapeUriString(pair.Value)).ToArray()) + "&" : "") + "access_token=" + Uri.EscapeUriString(this._accessToken);
            return builder.Uri;
        }

        private string EscapeUriString(string text)
        {
            var builder = new StringBuilder(Uri.EscapeDataString(text));
            builder.Replace("!", "%21");
            builder.Replace("'", "%27");
            builder.Replace("(", "%28");
            builder.Replace(")", "%29");
            return builder.ToString();
        }

        #endregion

        #region ** private stuff

        private static string GetFolderPath(string folderId)
        {
            return string.Format("folders/{0}", folderId);
        }

        private static string GetUsersPath(string userId = "me")
        {
            return string.Format("users/{0}", userId ?? "me");
        }

        private static HttpClient CreateClient(HttpMessageHandler filter = null)
        {
            HttpClient client;
            if (filter != null)
                client = new HttpClient(filter);
            else
                client = new HttpClient();
            client.Timeout = Timeout.InfiniteTimeSpan;
            return client;
        }

        private async Task<Exception> ProcessException(HttpResponseMessage response)
        {
            var error = await response.Content.ReadJsonAsync<Error>();
            return new BoxException(response.StatusCode, error);
        }

        //private static HttpWebRequest CreateRequest(Uri uri)
        //{
        //    var request = HttpWebRequest.Create(uri) as HttpWebRequest;
        //    //request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip,deflate";
        //    return request;
        //}

        //private static Exception ProcessException(Exception exc)
        //{
        //    if (exc is WebException)
        //    {
        //        try
        //        {
        //            var webException = exc as WebException;
        //            if (webException.Response != null)
        //            {
        //                var responseStream = webException.Response.GetResponseStream();
        //                var ser = new DataContractJsonSerializer(typeof(Error));
        //                var error = ser.ReadObject(responseStream) as Error;
        //                if (error != null)
        //                    return new BoxException(error);
        //                else
        //                {
        //                    if (responseStream.CanSeek)
        //                        responseStream.Seek(0, SeekOrigin.Begin);
        //                    var responseString = new StreamReader(responseStream).ReadToEnd();
        //                }
        //            }
        //        }
        //        catch { }
        //    }
        //    return exc;
        //}

        #endregion
    }
}
