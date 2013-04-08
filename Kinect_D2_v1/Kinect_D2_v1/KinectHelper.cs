using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_D2_v1
{
    class KinectHelper
    {
        DbSet<RawData> raw;
        private List<System.Drawing.Bitmap> bitmaps = new List<System.Drawing.Bitmap>();

        public void addColorStream(ColorImageFrame colorFrame)
        {
            if (colorFrame != null)
            {
                byte[] bytes = new byte[colorFrame.PixelDataLength];
                colorFrame.CopyPixelDataTo(bytes);

                System.Drawing.Bitmap bmap = new System.Drawing.Bitmap(colorFrame.Width, colorFrame.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                BitmapData bmapdata = bmap.LockBits(
                    new System.Drawing.Rectangle(0, 0, colorFrame.Width, colorFrame.Height),
                    ImageLockMode.WriteOnly,
                    bmap.PixelFormat);
                IntPtr ptr = bmapdata.Scan0;
                Marshal.Copy(bytes, 0, ptr, colorFrame.PixelDataLength);
                bmap.UnlockBits(bmapdata);
                bitmaps.Add(bmap);
            }
        }

        public void saveSkeletonToRaw(Skeleton[] skeletons)
        {
            foreach (Skeleton skel in skeletons)
            {
                if (skel.TrackingState == SkeletonTrackingState.Tracked)
                {
                    RawData tmp = new RawData();
                    tmp.SetJoints(skel.Joints);
                    raw.Add(tmp);
                }
            }
        }

        public void createDatabase()
        {
            raw = new Kinect_D2_v1.KinectDatabaseEntities1().Set<RawData>();
        }

        public void saveToDatabase(Participant_condition pc)
        {
            KinectDatabaseEntities1 ent = new KinectDatabaseEntities1();
            foreach (RawData rawdata in raw.Local)
            {
                rawdata.participant_condition_id = pc.participant_condition_id;
                ent.RawDatas.Add(rawdata);
            }
            ent.SaveChanges();
        }

        public void writeVideoReplay(String fileName)
        {
            WriteAvi write = new WriteAvi();
            write.testWrite(bitmaps, fileName);
        }
    }
}
