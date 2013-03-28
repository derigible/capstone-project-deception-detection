using AviFile;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_D2_v1
{
    class WriteAvi
    {
        public void testWrite(List<System.Drawing.Bitmap> bitmaps){
            //load the first image
            Bitmap bm = bitmaps[0];
            //create a new AVI file
            AviManager aviManager =
                new AviManager("new.avi", false);
            //add a new video stream and one frame to the new file
            VideoStream aviStream =
                aviManager.AddVideoStream(true, 2, bm);
            int count = 0;
            foreach(System.Drawing.Bitmap bitmap in bitmaps)
            {
                aviStream.AddFrame(bitmap);
                bitmap.Dispose();
                count++;
            }
            aviManager.Close();
        }
    }
}
