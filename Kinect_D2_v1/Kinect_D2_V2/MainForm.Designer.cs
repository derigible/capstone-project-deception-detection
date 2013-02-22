namespace Kinect_D2_V2
{
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using Microsoft.Kinect;

    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.standardMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VideoTab = new System.Windows.Forms.TabControl();
            this.Video = new System.Windows.Forms.TabPage();
            this.Features = new System.Windows.Forms.TabPage();
            this.Export = new System.Windows.Forms.TabPage();
            this.SkeletonViewer = new System.Windows.Forms.PictureBox();
            this.standardMenu.SuspendLayout();
            this.VideoTab.SuspendLayout();
            this.Video.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SkeletonViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // standardMenu
            // 
            this.standardMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.standardMenu.Location = new System.Drawing.Point(0, 0);
            this.standardMenu.Name = "standardMenu";
            this.standardMenu.Size = new System.Drawing.Size(785, 24);
            this.standardMenu.TabIndex = 0;
            this.standardMenu.Text = "menuStrip1";
            this.standardMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.standardMenu_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem1
            // 
            this.viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            this.viewToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem1.Text = "View";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // VideoTab
            // 
            this.VideoTab.Controls.Add(this.Video);
            this.VideoTab.Controls.Add(this.Features);
            this.VideoTab.Controls.Add(this.Export);
            this.VideoTab.Location = new System.Drawing.Point(0, 27);
            this.VideoTab.Name = "VideoTab";
            this.VideoTab.SelectedIndex = 0;
            this.VideoTab.Size = new System.Drawing.Size(785, 503);
            this.VideoTab.TabIndex = 1;
            // 
            // Video
            // 
            this.Video.Controls.Add(this.SkeletonViewer);
            this.Video.Location = new System.Drawing.Point(4, 22);
            this.Video.Name = "Video";
            this.Video.Padding = new System.Windows.Forms.Padding(3);
            this.Video.Size = new System.Drawing.Size(777, 477);
            this.Video.TabIndex = 0;
            this.Video.Text = "Video";
            this.Video.UseVisualStyleBackColor = true;
            // 
            // Features
            // 
            this.Features.Location = new System.Drawing.Point(4, 22);
            this.Features.Name = "Features";
            this.Features.Padding = new System.Windows.Forms.Padding(3);
            this.Features.Size = new System.Drawing.Size(777, 477);
            this.Features.TabIndex = 1;
            this.Features.Text = "Features";
            this.Features.UseVisualStyleBackColor = true;
            // 
            // Export
            // 
            this.Export.Location = new System.Drawing.Point(4, 22);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(777, 477);
            this.Export.TabIndex = 2;
            this.Export.Text = "Export";
            this.Export.UseVisualStyleBackColor = true;
            // 
            // SkeletonViewer
            // 
            this.SkeletonViewer.Location = new System.Drawing.Point(0, 0);
            this.SkeletonViewer.Name = "SkeletonViewer";
            this.SkeletonViewer.Size = new System.Drawing.Size(492, 306);
            this.SkeletonViewer.TabIndex = 0;
            this.SkeletonViewer.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 530);
            this.Controls.Add(this.VideoTab);
            this.Controls.Add(this.standardMenu);
            this.MainMenuStrip = this.standardMenu;
            this.Name = "MainForm";
            this.Text = "Kinect Deception Detection";
            this.standardMenu.ResumeLayout(false);
            this.standardMenu.PerformLayout();
            this.VideoTab.ResumeLayout(false);
            this.Video.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SkeletonViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void nui_VideoFrameReady(object sender, ImageFrameReadyEventArgs e)
        {
            PlanarImage imageColor = e.ImageFrame.Image;
            image2.Source = BitmapSource.Create(imageColor.Width, imageColor.Height, 96, 96, PixelFormats.Bgr32, null, imageColor.Bits, imageColor.Width * imageColor.BytesPerPixel);
            BitmapSource myBitmapColor = BitmapSource.Create(imageColor.Width, imageColor.Height, 96, 96, PixelFormats.Bgr32, null, imageColor.Bits, imageColor.Width * imageColor.BytesPerPixel);

            if (button2.IsEnabled == false)
            {


                FileStream streamColor = new FileStream(@"C:\dev\testKinect\realValtestColor.bmp", FileMode.Create);
                BmpBitmapEncoder encoderColor = new BmpBitmapEncoder();
                TextBlock myTextBlockColor = new TextBlock();
                myTextBlockColor.Text = "Codec Author is: Mike" + encoderColor.CodecInfo.Author.ToString();
                encoderColor.Frames.Add(BitmapFrame.Create(myBitmapColor));
                encoderColor.Save(streamColor);
                button2.IsEnabled = true;
            }

        }

        #endregion

        private System.Windows.Forms.MenuStrip standardMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TabControl VideoTab;
        private System.Windows.Forms.TabPage Video;
        private System.Windows.Forms.TabPage Features;
        private System.Windows.Forms.TabPage Export;
        private System.Windows.Forms.PictureBox SkeletonViewer;
    }
}

