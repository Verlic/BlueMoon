namespace BlueMoon.DocumentManager
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class DocumentManager
    {
        private static int newDocumentInstancesCount;

        private static bool saving;

        public static MarkdownDocument StartNewDocument()
        {
            newDocumentInstancesCount += 1;
            var newDocumentTitle = string.Format("Document{0}.md", newDocumentInstancesCount);
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

        public static async Task SaveDocumentAsync(MarkdownDocument document)
        {
            if (!saving)
            {
                saving = true;
                using (var writer = new StreamWriter(document.DocumentPath))
                {
                    await writer.WriteAsync(document.Markdown);
                }

                document.HasChanges = false;
                saving = false;
            }
        }

        public static Task SaveDocumentFromTempAsync(MarkdownDocument document, string fileName)
        {
            var destinationDirectory = Path.GetDirectoryName(fileName);
            var workingImagesDirectory = Path.Combine(document.WorkingFolder, "Images");

            // Check if Temp folder has an Images folder
            if (Directory.Exists(workingImagesDirectory))
            {
                var images = Directory.GetFiles(workingImagesDirectory);
                var destinationImagesDirectory = Path.Combine(destinationDirectory, "Images");

                if (!Directory.Exists(destinationImagesDirectory))
                {
                    Directory.CreateDirectory(destinationImagesDirectory);
                }

                foreach (var fileInfo in images.Select(image => new FileInfo(image)))
                {
                    fileInfo.CopyTo(Path.Combine(destinationImagesDirectory, fileInfo.Name), true);
                    fileInfo.Delete();
                }

                Directory.Delete(document.WorkingFolder, true);
            }

            document.IsTemporary = false;
            document.DocumentPath = fileName;
            document.WorkingFolder = Path.GetDirectoryName(fileName);
            return SaveDocumentAsync(document);
        }

        public static async Task<MarkdownDocument> OpenDocumentAsync(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            var directory = fileInfo.DirectoryName;
            string markdown;
            using (var fileStream = new StreamReader(fileName))
            {
                markdown = await fileStream.ReadToEndAsync();
            }

            return new MarkdownDocument(fileInfo.Name)
            {
                DocumentPath = fileName,
                WorkingFolder = directory,
                Markdown = markdown,
                HasChanges = false,
                IsTemporary = false
            };
        }

        public static void CloseDocument(MarkdownDocument currentDocument)
        {
            if (currentDocument.IsTemporary)
            {
                Directory.Delete(currentDocument.WorkingFolder, true);
            }
        }
    }
}
