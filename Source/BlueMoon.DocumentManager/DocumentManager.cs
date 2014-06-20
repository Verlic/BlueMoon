namespace BlueMoon.DocumentManager
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class DocumentManager
    {
        private static int newDocumentInstancesCount;

        private static bool saving;

        public static MarkdownDocument StartNewDocument()
        {
            newDocumentInstancesCount += 1;
            var newDocumentTitle = string.Format("Document{0}", newDocumentInstancesCount);
            var document = new MarkdownDocument(newDocumentTitle);
            try
            {
                Directory.CreateDirectory(document.WorkingFolder);
                using (var fileStream = File.CreateText(document.DocumentPath))
                {
                    fileStream.Flush();
                }
            }
            catch (IOException ioex)
            {
                throw new Exception("Unable to create new document.", ioex);
            }

            return document;
        }

        public static bool CanSave()
        {
            return !saving;
        }

        public static async Task SaveDocument(MarkdownDocument document)
        {
            if (!saving)
            {
                saving = true;
                using (var writer = new StreamWriter(document.DocumentPath))
                {
                    await writer.WriteAsync(document.Markdown);
                }

                saving = false;
            }
        }
    }
}
