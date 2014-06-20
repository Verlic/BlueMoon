namespace Converters.Markdown
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    public class HtmlPreviewUpdater
    {
        private readonly Action<string> callback;

        private readonly Queue<string> updateQueue;

        private bool updating;

        private HtmlToMarkdown htmlToMarkdown;

        public HtmlPreviewUpdater(Action<string> callback)
        {
            this.callback = callback;
            this.InitializeConverter();
            this.updateQueue = new Queue<string>(5);
        }

        public async void Start(string text)
        {
            var slowTask = Task.Factory.StartNew(
               () =>
               {
                   if (this.updateQueue.Count == 1)
                   {
                       this.updateQueue.Dequeue();
                   }

                   this.updateQueue.Enqueue(text);
                   if (!this.updating)
                   {
                       this.Preview();
                   }
               });
            await slowTask;
        }

        private async void Preview()
        {
            this.updating = true;
            var markdown = this.updateQueue.Dequeue();
            this.callback(await this.htmlToMarkdown.ToHtml(markdown));
            this.updating = false;
            if (this.updateQueue.Count > 0)
            {
                this.Preview();
            }
        }

        private void InitializeConverter()
        {
            var templatePath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName
                               + "\\Templates\\DefaultTheme\\content.tt";

            this.htmlToMarkdown = new HtmlToMarkdown(templatePath);
        }
    }
}
