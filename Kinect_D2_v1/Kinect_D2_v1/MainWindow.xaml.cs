﻿//------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Kinect_D2_v1
{
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using Microsoft.Kinect;
    using Microsoft.Samples.Kinect.WpfViewers;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data;
    using Microsoft.Kinect.Toolkit;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Controls;
    using System.Runtime.InteropServices;
    using System;
    using System.ComponentModel;
    using System.Drawing.Imaging;
    using System.Windows.Media.Imaging;
    

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variable declarations
        /// <summary>
        /// Width of output drawing
        /// </summary>
        private const float RenderWidth = 640.0f;

        /// <summary>
        /// Height of our output drawing
        /// </summary>
        private const float RenderHeight = 480.0f;

        /// <summary>
        /// Thickness of drawn joint lines
        /// </summary>
        private const double JointThickness = 3;

        /// <summary>
        /// Thickness of body center ellipse
        /// </summary>
        private const double BodyCenterThickness = 10;

        /// <summary>
        /// Thickness of clip edge rectangles
        /// </summary>
        private const double ClipBoundsThickness = 10;

        /// <summary>
        /// Brush used to draw skeleton center point
        /// </summary>
        private readonly Brush centerPointBrush = Brushes.Blue;

        /// <summary>
        /// Brush used for drawing joints that are currently tracked
        /// </summary>
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        /// <summary>
        /// Brush used for drawing joints that are currently inferred
        /// </summary>        
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        /// <summary>
        /// Pen used for drawing bones that are currently tracked
        /// </summary>
        private readonly Pen trackedBonePen = new Pen(Brushes.Green, 6);

        /// <summary>
        /// Pen used for drawing bones that are currently inferred
        /// </summary>        
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);

        /// <summary>
        /// Drawing group for skeleton rendering output
        /// </summary>
        private DrawingGroup drawingGroup;

        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage imageSource;

        /// <summary>
        /// Skeleton array to hold sensor skeletons
        /// </summary>
        private Skeleton[] skeletons;

        private readonly KinectSensorChooser sensorChooser = new KinectSensorChooser();

        private readonly KinectWindowViewModel viewModel;

        private KinectRecorder record;
        private FileStream colorStreamFile;
        private List<System.Drawing.Bitmap> bitmaps = new List<System.Drawing.Bitmap>();

        //this is needed in order to clear out and stop the sensor
        //BackgroundWorker bw = new BackgroundWorker();

        //Kinect_D2_v1.KinectDatabaseDataSet1TableAdapters.JointValuesTableAdapter kinectDatabaseDataSet1JointValuesTableAdapter;
        //DbSet<JointValue> vals;
        DbSet<RawData> raw;

        public static readonly DependencyProperty KinectSensorManagerProperty =
            DependencyProperty.Register(
                "KinectSensorManager",
                typeof(KinectSensorManager),
                typeof(MainWindow),
                new PropertyMetadata(null));
        #endregion variable declarations

        #region Window Managament
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            this.KinectSensorManager = new KinectSensorManager();
            this.KinectSensorManager.KinectSensorChanged += this.KinectSensorChanged;
            //this.DataContext = this.KinectSensorManager;

            this.viewModel = new KinectWindowViewModel();

            // The KinectSensorManager class is a wrapper for a KinectSensor that adds
            // state logic and property change/binding/etc support, and is the data model
            // for KinectDiagnosticViewer.
            this.viewModel.KinectSensorManager = this.KinectSensorManager;

            Binding sensorBinding = new Binding("KinectSensor");
            sensorBinding.Source = this;
            BindingOperations.SetBinding(this.viewModel.KinectSensorManager, KinectSensorManager.KinectSensorProperty, sensorBinding);

            this.DataContext = this.viewModel;

            InitializeComponent();
            //showColumnChart();

            this.SensorChooserUI.KinectSensorChooser = sensorChooser;
        }

        /// <summary>
        /// A ViewModel for a KinectWindow.
        /// </summary>
        public class KinectWindowViewModel : DependencyObject
        {
            public static readonly DependencyProperty KinectSensorManagerProperty =
                DependencyProperty.Register(
                    "KinectSensorManager",
                    typeof(KinectSensorManager),
                    typeof(KinectWindowViewModel),
                    new PropertyMetadata(null));

            public static readonly DependencyProperty DepthTreatmentProperty =
                DependencyProperty.Register(
                    "DepthTreatment",
                    typeof(KinectDepthTreatment),
                    typeof(KinectWindowViewModel),
                    new PropertyMetadata(KinectDepthTreatment.ClampUnreliableDepths));

            public KinectSensorManager KinectSensorManager
            {
                get { return (KinectSensorManager)GetValue(KinectSensorManagerProperty); }
                set { SetValue(KinectSensorManagerProperty, value); }
            }

            public KinectDepthTreatment DepthTreatment
            {
                get { return (KinectDepthTreatment)GetValue(DepthTreatmentProperty); }
                set { SetValue(DepthTreatmentProperty, value); }
            }
        }

        #endregion Window Management

        #region kinect management

        public KinectSensorManager KinectSensorManager
        {
            get { return (KinectSensorManager)GetValue(KinectSensorManagerProperty); }
            set { SetValue(KinectSensorManagerProperty, value); }
        }

        private void KinectSensorChanged(object sender, KinectSensorManagerEventArgs<KinectSensor> args)
        {
            if (null != args.OldValue)
            {
                this.UninitializeKinectServices(args.OldValue);
            }

            if (null != args.NewValue)
            {
                this.InitializeKinectServices(this.KinectSensorManager, args.NewValue);
            }
        }

        // Kinect enabled apps should customize which Kinect services it initializes here.
        private void InitializeKinectServices(KinectSensorManager kinectSensorManager, KinectSensor sensor)
        {
            // Application should enable all streams first.
            kinectSensorManager.ColorFormat = ColorImageFormat.RgbResolution640x480Fps30;
            kinectSensorManager.ColorStreamEnabled = true;

            sensor.SkeletonFrameReady += this.SkeletonsReady;
            sensor.ColorFrameReady += this.ColorStreamReady;
            kinectSensorManager.TransformSmoothParameters = new TransformSmoothParameters
            {
                Smoothing = 0.5f,
                Correction = 0.5f,
                Prediction = 0.5f,
                JitterRadius = 0.05f,
                MaxDeviationRadius = 0.04f
            };
            kinectSensorManager.SkeletonStreamEnabled = true;
            kinectSensorManager.KinectSensorEnabled = true;

        }

        // Kinect enabled apps should uninitialize all Kinect services that were initialized in InitializeKinectServices() here.
        private void UninitializeKinectServices(KinectSensor sensor)
        {
            sensor.SkeletonFrameReady -= this.SkeletonsReady;
            sensor.ColorFrameReady -= this.ColorStreamReady;
        }

        private void ColorStreamReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
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
        }

        public static System.Drawing.Bitmap BytesToBitmap(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                System.Drawing.Bitmap img = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(ms);
                return img;
            }
        }

        private void SkeletonsReady(object sender, SkeletonFrameReadyEventArgs e)
        {
       
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {

                    if ((skeletons == null) || (skeletons.Length != skeletonFrame.SkeletonArrayLength))
                    {
                        skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    }

                    skeletonFrame.CopySkeletonDataTo(skeletons);

                    using (DrawingContext dc = this.drawingGroup.Open())
                    {
                        // Draw a transparent background to set the render size
                        dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));
                        foreach (Skeleton skeleton in skeletons)
                        {                      
                            RenderClippedEdges(skeleton, dc);

                            if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                            {
                                this.DrawBonesAndJoints(skeleton, dc);
                                this.SaveSkeletonToRaw(skeletons);

                            }
                            else if (skeleton.TrackingState == SkeletonTrackingState.PositionOnly)
                            {
                                dc.DrawEllipse(
                                this.centerPointBrush,
                                null,
                                this.SkeletonPointToScreen(skeleton.Position),
                                BodyCenterThickness,
                                BodyCenterThickness);
                            }
                        }
                    }
                }
            }
        }


        #endregion kinect management

        #region render window
        /// <summary>
        /// Draws indicators to show which edges are clipping skeleton data
        /// </summary>
        /// <param name="skeleton">skeleton to draw clipping information for</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private static void RenderClippedEdges(Skeleton skeleton, DrawingContext drawingContext)
        {
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, RenderHeight - ClipBoundsThickness, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, RenderHeight));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(RenderWidth - ClipBoundsThickness, 0, ClipBoundsThickness, RenderHeight));
            }
        }

        /// <summary>
        /// Execute startup tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // Create the drawing group we'll use for drawing
            this.drawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image control
            this.imageSource = new DrawingImage(this.drawingGroup);


        }

        // Since the timer resolution defaults to about 10ms precisely, we need to
        // increase the resolution to get framerates above between 50fps with any
        // consistency.
        [DllImport("Winmm.dll", EntryPoint = "timeBeginPeriod")]
        private static extern int TimeBeginPeriod(uint period);

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowClosing(object sender, CancelEventArgs e)
        {
            sensorChooser.Stop();
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            this.KinectSensorManager.KinectSensor = null;
        }
        #endregion render window

        #region render Skeleton

        /// <summary>
        /// Event handler for Kinect sensor's SkeletonFrameReady event
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
            }

            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));

                if (skeletons.Length != 0)
                {
                    foreach (Skeleton skel in skeletons)
                    {
                        
                    }
                }

                // prevent drawing outside of our render area
                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, RenderWidth, RenderHeight));
            }
        }

        /// <summary>
        /// Draws a skeleton's bones and joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawBonesAndJoints(Skeleton skeleton, DrawingContext drawingContext)
        {
            // Render Torso
            this.DrawBone(skeleton, drawingContext, JointType.Head, JointType.ShoulderCenter);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderRight);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.Spine);
            this.DrawBone(skeleton, drawingContext, JointType.Spine, JointType.HipCenter);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipLeft);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipRight);

            // Left Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderLeft, JointType.ElbowLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);

            // Right Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderRight, JointType.ElbowRight);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(skeleton, drawingContext, JointType.WristRight, JointType.HandRight);

            // Left Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipLeft, JointType.KneeLeft);
            this.DrawBone(skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);

            // Right Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipRight, JointType.KneeRight);
            this.DrawBone(skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);

            // Render Joints
            foreach (Joint joint in skeleton.Joints)
            {
                Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), JointThickness, JointThickness);
                }
            }

        }

        /// <summary>
        /// Maps a SkeletonPoint to lie within our render space and converts to Point
        /// </summary>
        /// <param name="skelpoint">point to map</param>
        /// <returns>mapped point</returns>
        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensorChooser.Kinect.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }

        /// <summary>
        /// Draws a bone line between two joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw bones from</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="jointType0">joint to start drawing from</param>
        /// <param name="jointType1">joint to end drawing at</param>
        private void DrawBone(Skeleton skeleton, DrawingContext drawingContext, JointType jointType0, JointType jointType1)
        {
            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = this.trackedBonePen;
            }

            drawingContext.DrawLine(drawPen, this.SkeletonPointToScreen(joint0.Position), this.SkeletonPointToScreen(joint1.Position));
        }

        /// <summary>
        /// Handles the checking or unchecking of the seated mode combo box
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void CheckBoxSeatedModeChanged(object sender, RoutedEventArgs e)
        {
            if (null != this.sensorChooser.Kinect)
            {
                if (this.checkBoxSeatedMode.IsChecked.GetValueOrDefault())
                {
                    this.sensorChooser.Kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                }
                else
                {
                    this.sensorChooser.Kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
                }
            }
        }
        #endregion render Skeleton

        #region data transformation

        private void SaveSkeletonToRaw(Skeleton[] skeletons)
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
        #endregion data transformation

        #region button actions
        private Boolean clicked = false;

        private void Button_Start_Stop(object sender, RoutedEventArgs e)
        {
            if (!clicked)
            {
                startRecording();
                var uriSource = new Uri(@"/Kinect_D2_v1;component/Images/stop.png", UriKind.Relative);
                this.imgStartStop.Source = new BitmapImage(uriSource);
                clicked = true;

            }
            else
            {
                stopRecording();
                var uriSource = new Uri(@"/Kinect_D2_v1;component/Images/record.png", UriKind.Relative);
                this.imgStartStop.Source = new BitmapImage(uriSource);
                clicked = false;
            }
        }

        private void startRecording()
        {
            sensorChooser.Start();

            // Bind the KinectSensor from the sensorChooser to the KinectSensor on the KinectSensorManager
            var kinectSensorBinding = new Binding("Kinect") { Source = this.sensorChooser };
            BindingOperations.SetBinding(this.KinectSensorManager, KinectSensorManager.KinectSensorProperty, kinectSensorBinding);

            //Create an entity version
            //vals = new Kinect_D2_v1.KinectDatabaseEntities1().Set<JointValue>();
            raw = new Kinect_D2_v1.KinectDatabaseEntities1().Set<RawData>();


            if (null == sensorChooser.Kinect)
            {
                this.statusBarText.Text = Properties.Resources.KinectNotFound;
            }
            else
            {
                colorStreamFile = File.Create(@"C:\Users\Marc\Desktop\color.txt");
                Console.Write(colorStreamFile.Name);
                this.statusBarText.Text = Properties.Resources.KinectRecording;
            }
        }

        private void stopRecording()
        {
            if (null != this.sensorChooser.Kinect)
            {
                //stop the recording

                this.sensorChooser.Stop();

                MessageBox.Show("Sensor Stopped");

                this.statusBarText.Text = Properties.Resources.KinectOutPutToDatabase;

                KinectDatabaseEntities1 ent = new KinectDatabaseEntities1();
                foreach (RawData rawdata in raw.Local)
                {
                    ent.RawDatas.Add(rawdata);
                }
                //ent.RawDatas = raw;
                ent.SaveChanges();
                MessageBox.Show("Records saved to database");
            }
            else
            {
                MessageBox.Show("No Kinect Detected.");
            }
            this.statusBarText.Text = Properties.Resources.KinectFound;
        }
        #endregion button actions

        private void openReplay(object sender, RoutedEventArgs e)
        {
            WriteAvi write = new WriteAvi();
            write.testWrite(bitmaps);
            //if (this.sensorChooser != null)
            //{
            //    ReplayWindow replay = new ReplayWindow(640, 480);
            //    replay.Show();
            //}
            //else
            //{
            //    this.statusBarText.Text = Properties.Resources.KinectNotFound;
            //}
        }
    }
}