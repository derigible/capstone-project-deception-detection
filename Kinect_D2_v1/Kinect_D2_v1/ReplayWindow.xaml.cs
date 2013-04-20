using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kinect_D2_v1
{
    /// <summary>
    /// Interaction logic for ReplayWindow.xaml
    /// </summary>
    public partial class ReplayWindow : Window
    {
        KinectReplay replay;
        private WriteableBitmap colorBitmap;
        /// <summary>
        /// Intermediate storage for the color data received from the camera
        /// </summary>
        private byte[] colorPixels;

        public ReplayWindow(int FrameWidth, int FrameHeight)
        {
            InitializeComponent();
            // This is the bitmap we'll display on-screen
            this.colorBitmap = new WriteableBitmap(FrameWidth, FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);

            // Set the image we display to point to the bitmap where we'll put the image data
            this.Image.Source = this.colorBitmap;

            this.LaunchReplay();
        }

        private void LaunchReplay()
        {
            OpenFileDialog openFileDialog =
        new OpenFileDialog { Title = "Select filename", Filter = "Replay files|*.replay" };

            if (openFileDialog.ShowDialog() == true)
            {
                Stream recordStream = File.OpenRead(openFileDialog.FileName);

                replay = new KinectReplay(recordStream);

                replay.ColorImageFrameReady += replay_ColorImageFrameReady;

                replay.Start();
            }
        }

        void replay_ColorImageFrameReady (object sender, ReplayColorImageFrameReadyEventArgs e)
        {
            ProcessFrame(e.ColorImageFrame);
        }

        void ProcessFrame(ReplayColorImageFrame frame)
        {       
            if (frame != null)
            {
                // Copy the pixel data from the image to a temporary array
                frame.CopyPixelDataTo(this.colorPixels);

                // Write the pixel data into our bitmap
                this.colorBitmap.WritePixels(
                    new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                    this.colorPixels,
                    this.colorBitmap.PixelWidth * sizeof(int),
                    0);
            }  
        }
    }
}
