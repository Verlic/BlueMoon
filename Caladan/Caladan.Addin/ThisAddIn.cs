namespace Caladan.Addin
{
    using Microsoft.Office.Interop.Word;

    public partial class ThisAddIn
    {
        public static Application CurrentApplication { get; set; }

        private void ThisAddIn_Startup(object sender, global::System.EventArgs e)
        {
            CurrentApplication = this.Application;
        }

        private void ThisAddIn_Shutdown(object sender, global::System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += this.ThisAddIn_Startup;
            this.Shutdown += this.ThisAddIn_Shutdown;
        }
        
        #endregion
    }
}
