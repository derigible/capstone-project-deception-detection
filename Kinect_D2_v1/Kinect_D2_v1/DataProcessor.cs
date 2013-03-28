using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_D2_v1
{
    class DataProcessor
    {
        KinectDatabaseEntities1 ent = new KinectDatabaseEntities1();

        public void getData()
        {
            using (var context = new KinectDatabaseEntities1())
            {
                foreach (var destination in context.RawDatas)
                {
                    //Console.WriteLine(destination.AnkleLeft_X.Value);
                    //Console.WriteLine("Hand Left: " + destination.HandLeft_X.Value);
                    //Console.WriteLine("Hand Right: " + destination.HandRight_X.Value);
                    double distance = Math.Sqrt(Math.Pow(destination.HandLeft_X.Value - destination.HandRight_X.Value
                        , 2) + Math.Pow(destination.HandLeft_Y.Value - destination.HandRight_Y.Value
                        , 2));
                    Console.WriteLine(distance);
                }
            }
        }
    }
}
