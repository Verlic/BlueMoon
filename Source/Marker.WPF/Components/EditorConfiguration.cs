namespace BlueMoon.UI.Components
{
    using System.IO;
    using System.Reflection;

    using Newtonsoft.Json;

    public class EditorConfiguration
    {
        [JsonProperty("recent")]
        public string[] RecentDocuments { get; set; }

        public static EditorConfiguration Load()
        {
            var path = Path.Combine(
                new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName,
                "Data",
                "config.json");

            string json;
            using (var reader = new StreamReader(path))
            {
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<EditorConfiguration>(json);
        }
    }
}
