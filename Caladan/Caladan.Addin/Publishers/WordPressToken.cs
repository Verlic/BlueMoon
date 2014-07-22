namespace Caladan.Addin.Publishers
{
    using Newtonsoft.Json;

    public class WordPressToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("blog_id")]
        public string BlogId { get; set; }
    }
}
