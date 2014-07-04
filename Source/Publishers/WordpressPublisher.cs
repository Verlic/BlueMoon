namespace Publishers
{
    using System;

    using JoeBlogs;

    public class WordpressPublisher
    {
        private readonly string link;

        private readonly string username;

        private readonly string password;

        public WordpressPublisher(string site, string username, string password)
        {
            this.link = site;
            this.username = username;
            this.password = password;
        }

        public int Publish(string title, string body, bool publish)
        {
            var wordPressWrapper = new WordPressWrapper(this.link + "/xmlrpc.php", this.username, this.password);
            var post = new Post { DateCreated = DateTime.Now, Title = title, Body = body };
            return wordPressWrapper.NewPost(post, publish);
        }
    }
}
