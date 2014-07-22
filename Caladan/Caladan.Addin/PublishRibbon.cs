using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace Caladan.Addin
{
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Linq;

    using Caladan.Addin.Publishers;
    using Caladan.Addin.Views.WordPress;

    using HtmlAgilityPack;

    using Microsoft.Office.Interop.Word;

    using HtmlDocument = HtmlAgilityPack.HtmlDocument;
    using Task = System.Threading.Tasks.Task;

    public partial class PublishRibbon
    {
        private void PublishRibbonLoad(object sender, RibbonUIEventArgs e)
        {

        }

        private async void PublishWordPressButtonClick(object sender, RibbonControlEventArgs e)
        {
            if (!ThisAddIn.CurrentApplication.ActiveDocument.Saved)
            {
                MessageBox.Show("You must save the document first before publishing.");
                return;
            }

            var publisher = new WordPressPublisher();
            if (publisher.Login())
            {
                var publishDialog = new WordPressPublishDialog();
                if (publishDialog.ShowDialog() == DialogResult.OK)
                {
                    var token = await publisher.AuthenticateAsync();
                    var html = this.GetHtml();
                    var settings = publishDialog.Settings;
                    await publisher.Post(settings.SiteUrl, settings.PostTitle, html, settings.AsDraft, settings.Tags, token.Token);
                }
            }
        }

        private string GetHtml()
        {
            var location = new FileInfo(ThisAddIn.CurrentApplication.ActiveDocument.Name);
            var filePath = Path.Combine(
                ThisAddIn.CurrentApplication.ActiveDocument.Path,
                ThisAddIn.CurrentApplication.ActiveDocument.Name);
            var htmlPath = Path.Combine(ThisAddIn.CurrentApplication.ActiveDocument.Path, location.Name.Replace(location.Extension, string.Empty) + ".html");
            ThisAddIn.CurrentApplication.ActiveDocument.SaveAs2(htmlPath, WdSaveFormat.wdFormatFilteredHTML);
            ThisAddIn.CurrentApplication.ActiveDocument.Close();
            ThisAddIn.CurrentApplication.Documents.Open(filePath);
            using (var reader = File.Open(htmlPath, FileMode.Open, FileAccess.Read))
            {
                var streamReader = new StreamReader(reader);
                var content = streamReader.ReadToEnd();

                var document = new HtmlDocument();
                document.LoadHtml(content);
                return document.DocumentNode.SelectSingleNode("//body").OuterHtml;
            }
        }
    }
}
