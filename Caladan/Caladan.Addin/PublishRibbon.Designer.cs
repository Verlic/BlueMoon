namespace Caladan.Addin
{
    partial class PublishRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public PublishRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.publishTab = this.Factory.CreateRibbonTab();
            this.blogGroup = this.Factory.CreateRibbonGroup();
            this.socialGroup = this.Factory.CreateRibbonGroup();
            this.publishWordPressButton = this.Factory.CreateRibbonButton();
            this.publishBloggerButton = this.Factory.CreateRibbonButton();
            this.publishQuoraButton = this.Factory.CreateRibbonButton();
            this.postFacebookButton = this.Factory.CreateRibbonButton();
            this.postTwitterButton = this.Factory.CreateRibbonButton();
            this.publishTab.SuspendLayout();
            this.blogGroup.SuspendLayout();
            this.socialGroup.SuspendLayout();
            // 
            // publishTab
            // 
            this.publishTab.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.publishTab.Groups.Add(this.blogGroup);
            this.publishTab.Groups.Add(this.socialGroup);
            this.publishTab.Label = "Publish";
            this.publishTab.Name = "publishTab";
            // 
            // blogGroup
            // 
            this.blogGroup.Items.Add(this.publishWordPressButton);
            this.blogGroup.Items.Add(this.publishBloggerButton);
            this.blogGroup.Items.Add(this.publishQuoraButton);
            this.blogGroup.Label = "Blogs";
            this.blogGroup.Name = "blogGroup";
            // 
            // socialGroup
            // 
            this.socialGroup.Items.Add(this.postFacebookButton);
            this.socialGroup.Items.Add(this.postTwitterButton);
            this.socialGroup.Label = "Social";
            this.socialGroup.Name = "socialGroup";
            // 
            // publishWordPressButton
            // 
            this.publishWordPressButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.publishWordPressButton.Image = global::Caladan.Addin.Properties.Resources.wordpress_logo;
            this.publishWordPressButton.Label = "WordPress";
            this.publishWordPressButton.Name = "publishWordPressButton";
            this.publishWordPressButton.ShowImage = true;
            this.publishWordPressButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.PublishWordPressButtonClick);
            // 
            // publishBloggerButton
            // 
            this.publishBloggerButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.publishBloggerButton.Image = global::Caladan.Addin.Properties.Resources.blogspot_logo;
            this.publishBloggerButton.Label = "Blogger";
            this.publishBloggerButton.Name = "publishBloggerButton";
            this.publishBloggerButton.ShowImage = true;
            // 
            // publishQuoraButton
            // 
            this.publishQuoraButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.publishQuoraButton.Image = global::Caladan.Addin.Properties.Resources.Quora_Logo;
            this.publishQuoraButton.Label = "Quora";
            this.publishQuoraButton.Name = "publishQuoraButton";
            this.publishQuoraButton.ShowImage = true;
            // 
            // postFacebookButton
            // 
            this.postFacebookButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.postFacebookButton.Image = global::Caladan.Addin.Properties.Resources.facebook_logo;
            this.postFacebookButton.Label = "Facebook";
            this.postFacebookButton.Name = "postFacebookButton";
            this.postFacebookButton.ShowImage = true;
            // 
            // postTwitterButton
            // 
            this.postTwitterButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.postTwitterButton.Image = global::Caladan.Addin.Properties.Resources.twitter_logo;
            this.postTwitterButton.Label = "Twitter";
            this.postTwitterButton.Name = "postTwitterButton";
            this.postTwitterButton.ShowImage = true;
            // 
            // PublishRibbon
            // 
            this.Name = "PublishRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.publishTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.PublishRibbonLoad);
            this.publishTab.ResumeLayout(false);
            this.publishTab.PerformLayout();
            this.blogGroup.ResumeLayout(false);
            this.blogGroup.PerformLayout();
            this.socialGroup.ResumeLayout(false);
            this.socialGroup.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab publishTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup blogGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton publishWordPressButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton publishBloggerButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton publishQuoraButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup socialGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton postFacebookButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton postTwitterButton;
    }

    partial class ThisRibbonCollection
    {
        internal PublishRibbon PublishRibbon
        {
            get { return this.GetRibbon<PublishRibbon>(); }
        }
    }
}
