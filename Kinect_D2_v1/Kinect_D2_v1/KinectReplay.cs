using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_D2_v1
{
    using Microsoft.Kinect;
    using System;
    using System.IO;
    using System.Threading;

    public abstract class ReplayFrame
    {
        public int FrameNumber { get; protected set; }
        public long TimeStamp { get; protected set; }

        internal abstract void CreateFromReader(BinaryReader reader);
    }

    class ReplaySystem<T> where T : ReplayFrame, new()
    {
        internal event Action<T> FrameReady;
        readonly List<T> frames = new List<T>();

        CancellationTokenSource cancellationTokenSource;
        public bool IsFinished
        {
            get;
            private set;
        }

        internal void AddFrame(BinaryReader reader)
        {
            T frame = new T();

            frame.CreateFromReader(reader);

            frames.Add(frame);
        }

        public void Start()
        {
            Stop();
            IsFinished = false;

            cancellationTokenSource = new CancellationTokenSource();

            CancellationToken token = cancellationTokenSource.Token;

            Task.Factory.StartNew(() =>
            {
                foreach (T frame in frames)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(frame.TimeStamp));

                    if (token.IsCancellationRequested)
                        break;

                    if (FrameReady != null)
                        FrameReady(frame);
                }
                IsFinished = true;
            }, token);
        }

        public void Stop()
        {
            if (cancellationTokenSource == null)
                return;

            cancellationTokenSource.Cancel();
        }
    }

    public class ReplayColorImageFrameReadyEventArgs : EventArgs
    {
        public ReplayColorImageFrame ColorImageFrame { get; set; }
    }

    public class ReplayColorImageFrame : ReplayFrame
    {
        readonly ColorImageFrame internalFrame;
        long streamPosition;
        Stream stream;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int BytesPerPixel { get; private set; }
        public ColorImageFormat Format { get; private set; }
        public int PixelDataLength { get; set; }

        public ReplayColorImageFrame(ColorImageFrame frame)
        {
            Format = frame.Format;
            BytesPerPixel = frame.BytesPerPixel;
            FrameNumber = frame.FrameNumber;
            TimeStamp = frame.Timestamp;
            Width = frame.Width;
            Height = frame.Height;

            PixelDataLength = frame.PixelDataLength;
            internalFrame = frame;
        }

        public ReplayColorImageFrame()
        {

        }

        internal override void CreateFromReader(BinaryReader reader)
        {
            TimeStamp = reader.ReadInt64();
            BytesPerPixel = reader.ReadInt32();
            Format = (ColorImageFormat)reader.ReadInt32();
            Width = reader.ReadInt32();
            Height = reader.ReadInt32();
            FrameNumber = reader.ReadInt32();

            PixelDataLength = reader.ReadInt32();

            stream = reader.BaseStream;
            streamPosition = stream.Position;

            stream.Position += PixelDataLength;
        }

        public void CopyPixelDataTo(byte[] pixelData)
        {
            if (internalFrame != null)
            {
                internalFrame.CopyPixelDataTo(pixelData);
                return;
            }

            long savedPosition = stream.Position;
            stream.Position = streamPosition;

            stream.Read(pixelData, 0, PixelDataLength);

            stream.Position = savedPosition;
        }

        public static implicit operator ReplayColorImageFrame(ColorImageFrame frame)
        {
            return new ReplayColorImageFrame(frame);
        }
    }

    public class KinectReplay : IDisposable
    {
        BinaryReader reader;
        Stream stream;
        readonly SynchronizationContext synchronizationContext;

        // Events
        public event EventHandler<ReplayColorImageFrameReadyEventArgs> ColorImageFrameReady;
        //public event EventHandler<ReplayDepthImageFrameReadyEventArgs> DepthImageFrameReady;
        //public event EventHandler<ReplaySkeletonFrameReadyEventArgs> SkeletonFrameReady;


        // Replay
        ReplaySystem<ReplayColorImageFrame> colorReplay;
        //ReplaySystem<ReplayDepthImageFrame> depthReplay;
        //ReplaySystem<ReplaySkeletonFrame> skeletonReplay;

        public bool Started { get; internal set; }

        public bool IsFinished
        {
            get
            {
                if (colorReplay != null && !colorReplay.IsFinished)
                    return false;

                //if (depthReplay != null && !depthReplay.IsFinished)
                //    return false;

                //if (skeletonReplay != null && !skeletonReplay.IsFinished)
                //    return false;

                return true;
            }
        }

        public KinectReplay(Stream stream)
        {
            this.stream = stream;
            reader = new BinaryReader(stream);

            synchronizationContext = SynchronizationContext.Current;

            KinectRecordOptions options = (KinectRecordOptions)reader.ReadInt32();

            if ((options & KinectRecordOptions.Color) != 0)
            {
                colorReplay = new ReplaySystem<ReplayColorImageFrame>();
            }
            //if ((options & KinectRecordOptions.Depth) != 0)
            //{
            //    depthReplay = new ReplaySystem<ReplayDepthImageFrame>();
            //}
            //if ((options & KinectRecordOptions.Skeletons) != 0)
            //{
            //    skeletonReplay = new ReplaySystem<ReplaySkeletonFrame>();
            //}

            while (reader.BaseStream.Position +4 < reader.BaseStream.Length)
            {
                if (reader.BaseStream.Position > 67580000)
                    Console.WriteLine("Position: " + reader.BaseStream.Position + " of " + reader.BaseStream.Length);
                
                KinectRecordOptions header = (KinectRecordOptions)reader.ReadInt32();
                switch (header)
                {
                    case KinectRecordOptions.Color:
                        colorReplay.AddFrame(reader);
                        break;
                    //case KinectRecordOptions.Depth:
                    //    depthReplay.AddFrame(reader);
                    //    break;
                    //case KinectRecordOptions.Skeletons:
                    //    skeletonReplay.AddFrame(reader);
                    //    break;
                }
            }
        }

        public void Start()
        {
            if (Started)
                throw new Exception("KinectReplay already started");

            Started = true;

            if (colorReplay != null)
            {
                colorReplay.Start();
                colorReplay.FrameReady += frame => synchronizationContext.Send(state =>
                {
                    if (ColorImageFrameReady != null)
                        ColorImageFrameReady(this,
    new ReplayColorImageFrameReadyEventArgs { ColorImageFrame = frame });
                }, null);
            }

    //        if (depthReplay != null)
    //        {
    //            depthReplay.Start();
    //            depthReplay.FrameReady += frame => synchronizationContext.Send(state =>
    //            {
    //                if (DepthImageFrameReady != null)
    //                    DepthImageFrameReady(this,
    //new ReplayDepthImageFrameReadyEventArgs { DepthImageFrame = frame });
    //            }, null);
    //        }

    //        if (skeletonReplay != null)
    //        {
    //            skeletonReplay.Start();
    //            skeletonReplay.FrameReady += frame => synchronizationContext.Send(state =>
    //            {
    //                if (SkeletonFrameReady != null)
    //                    SkeletonFrameReady(this,
    //new ReplaySkeletonFrameReadyEventArgs { SkeletonFrame = frame });
    //            }, null);
    //        }
        }

        public void Stop()
        {
            if (colorReplay != null)
            {
                colorReplay.Stop();
            }

            //if (depthReplay != null)
            //{
            //    depthReplay.Stop();
            //}

            //if (skeletonReplay != null)
            //{
            //    skeletonReplay.Stop();
            //}

            Started = false;
        }

        public void Dispose()
        {
            Stop();

            colorReplay = null;
            //depthReplay = null;
            //skeletonReplay = null;

            if (reader != null)
            {
                reader.Dispose();
                reader = null;
            }

            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }
    }
}
