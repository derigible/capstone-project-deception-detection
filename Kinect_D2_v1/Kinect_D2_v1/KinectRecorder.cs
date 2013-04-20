using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_D2_v1
{
    class KinectRecorder
    {
        Stream recordStream;
        readonly BinaryWriter writer;
        // Recorders
        readonly ColorRecorder colorRecoder;
        //readonly DepthRecorder depthRecorder;
        //readonly SkeletonRecorder skeletonRecorder;

        public KinectRecordOptions Options { get; set; }

        public KinectRecorder(KinectRecordOptions options, Stream stream)
        {
            Options = options;

            recordStream = stream;
            writer = new BinaryWriter(recordStream);

            writer.Write((int)Options);

            if ((Options & KinectRecordOptions.Color) != 0)
            {
                colorRecoder = new ColorRecorder(writer);
            }
            //if ((Options & KinectRecordOptions.Depth) != 0)
            //{
            //    depthRecorder = new DepthRecorder(writer);
            //}
            //if ((Options & KinectRecordOptions.Skeletons) != 0)
            //{
            //    skeletonRecorder = new SkeletonRecorder(writer);
            //}
        }

        //public void Record(SkeletonFrame frame)
        //{
        //    if (writer == null)
        //        throw new Exception("This recorder is stopped");

        //    if (skeletonRecorder == null)
        //        throw new Exception("Skeleton recording is not activated on this KinectRecorder");

        //    skeletonRecorder.Record(frame);
        //}

        public void Record(ColorImageFrame frame)
        {
            if (writer == null)
                throw new Exception("This recorder is stopped");

            if (colorRecoder == null)
                throw new Exception("Color recording is not activated on this KinectRecorder");

            colorRecoder.Record(frame);
        }

        //public void Record(DepthImageFrame frame)
        //{
        //    if (writer == null)
        //        throw new Exception("This recorder is stopped");

        //    if (depthRecorder == null)
        //        throw new Exception("Depth recording is not activated on this KinectRecorder");

        //    depthRecorder.Record(frame);
        //}

        public void Stop()
        {
            if (writer == null)
                throw new Exception("This recorder is already stopped");

            writer.Close();
            writer.Dispose();

            recordStream.Dispose();
            recordStream = null;
        }
    }
}
