using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation.DoubleAnimation;

namespace Kinect_D2_v1
{
    class AVIWriter1
    {
        public void test()
        {
            DoubleAnimation anim = new DoubleAnimation(50, 400, TimeSpan.FromSeconds(10), System.Windows.Media.Animation.FillBehavior.HoldEnd);
            var clock = anim.CreateClock();
            this.ellipse1.ApplyAnimationClock(Canvas.LeftProperty, clock);
            clock.Controller.Pause();

            int dpi = 96;
            int fps = 24;
            int anim_length_in_secs = 5;
            int num_total_frames = fps * anim_length_in_secs;

            var secs = Enumerable.Range(0, num_total_frames).Select(t => (((double)t) / fps));
            AviManager aviManager = new AviFile.AviManager(filename, false);
            VideoStream aviStream = null;
            foreach (var sec in secs)
            {
                clock.Controller.SeekAlignedToLastTick(TimeSpan.FromSeconds(sec),
                                                       System.Windows.Media.Animation.TimeSeekOrigin.BeginTime);
                this.canvas1.UpdateLayout();

                string temp_bitmap = "d:\\canvas_frame.png";
                util.SaveCanvas(this, this.canvas1, dpi, temp_bitmap);

                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(temp_bitmap);
                if (aviStream == null)
                {
                    aviStream = aviManager.AddVideoStream(compress, fps, bm);
                }
                else
                {
                    aviStream.AddFrame(bm);

                }
                bm.Dispose();

            }
            aviManager.Close();
        }
    }
}
