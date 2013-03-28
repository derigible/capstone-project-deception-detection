using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Kinect_D2_v1
{
    class VideoWriter
    {
        [DllImportAttribute("KERNEL32.DLL", EntryPoint = "MoveFileW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        public static extern bool MoveFile(String src, String dst);

        //[DllImport("avcodec-53.dll")]

        public void writeVideo()
        {
            int width = 320;
            int height = 240;

            // create instance of video writer
            VideoFileWriter writer = new VideoFileWriter();
            // create new video file
            writer.Open("test.avi", width, height, 25, VideoCodec.MPEG4);
            // create a bitmap to save into the video file
            Bitmap image = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            // write 1000 video frames
            for (int i = 0; i < 1000; i++)
            {
                image.SetPixel(i % width, i % height, Color.Red);
                writer.WriteVideoFrame(image);
            }
            writer.Close();
        }
    }
}
