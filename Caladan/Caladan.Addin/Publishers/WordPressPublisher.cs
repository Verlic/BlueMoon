namespace Caladan.Addin.Publishers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web;

    using Newtonsoft.Json;

    public class WordPressPublisher
    {
        private const string ApiUrl = "https://public-api.wordpress.com/rest/v1/sites/{0}/posts/new?pretty=1";

        private const string MyClientId = "35872";

        private const string MyClientSecret = "v6TkdsWrT4tNjxl2AuoGAow6Wt1kDUgl1PtHjrsZy4rheTq8ESrEyYAJycWeY6TP";

        private const string Redirect = "http://www.wordpress.com/?returnToken=1";

        private static WordPressToken token;

        private static string accessCode;

        private readonly Uri tokenUri = new Uri(string.Format("https://public-api.wordpress.com/oauth2/authorize?client_id={0}&redirect_uri={1}&response_type=code", MyClientId, Redirect));

        public Uri TokenUri
        {
            get
            {
                return this.tokenUri;
            }
        }

        public Uri AuthUri
        {
            get
            {
                return new Uri(string.Format("https://public-api.wordpress.com/oauth2/token"));
            }
        }

        public bool Login()
        {
            if (token != null)
            {
                return true;
            }

            var dialog = new OAuthDialog(this);
            var result = dialog.ShowDialog();
            accessCode = dialog.BearerCode;
            return result == System.Windows.Forms.DialogResult.OK;
        }
        
        public async Task<WordPressToken> AuthenticateAsync()
        {
            if (token != null)
            {
                return token;
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.MaxResponseContentBufferSize = int.MaxValue;
                httpClient.DefaultRequestHeaders.ExpectContinue = false;
                var content =
                    new FormUrlEncodedContent(
                        new[]
                            {
                                new KeyValuePair<string, string>("client_id", MyClientId),
                                new KeyValuePair<string, string>("redirect_uri", Redirect),
                                new KeyValuePair<string, string>("client_secret", MyClientSecret),
                                new KeyValuePair<string, string>("code", accessCode),
                                new KeyValuePair<string, string>("grant_type", "authorization_code")
                            });

                var httpRequestMessage = new HttpRequestMessage { Method = HttpMethod.Post, RequestUri = this.AuthUri, Content = content };

                var response = await httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseContentRead);
                var jsonString = await response.Content.ReadAsStringAsync();

                var json = JsonConvert.DeserializeObject<WordPressToken>(jsonString);
                token = json;
                return json;
            }
        }

        public async Task Post(string siteUrl, string title, string content, bool asDraft, string tags, string accessToken)
        {
            var url = string.Format(ApiUrl, siteUrl, title, content, asDraft, tags);

            var data =
                new FormUrlEncodedContent(
                    new[]
                        {
                            new KeyValuePair<string, string>("title", title),
                            new KeyValuePair<string, string>("content", content),
                            new KeyValuePair<string, string>("status", asDraft ? "draft" : "publish"),
                            new KeyValuePair<string, string>("tags", tags),
                        });
        
            using (var httpClient = new HttpClient())
            {
                httpClient.MaxResponseContentBufferSize = int.MaxValue;
                httpClient.DefaultRequestHeaders.ExpectContinue = false;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var httpRequestMessage = new HttpRequestMessage { Method = HttpMethod.Post, RequestUri = new Uri(url), Content = data };

                var response = await httpClient.SendAsync(httpRequestMessage);

                if (!response.IsSuccessStatusCode)
                {
                    return;
                }

                var jsonString = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
