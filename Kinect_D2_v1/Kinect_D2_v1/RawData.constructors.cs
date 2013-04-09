using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Kinect_D2_v1
{
    public partial class Raw_Data
    {
        public Raw_Data()
        {
        }

        public void SetJoint(Joint joint)
        {
            switch (joint.JointType)
            {
                case JointType.Head: Head_X = joint.Position.X; Head_Y = joint.Position.Y; Head_Z = joint.Position.Z; break;
                case JointType.ShoulderCenter: ShoulderCenter_X = joint.Position.X; ShoulderCenter_Y = joint.Position.Y; ShoulderCenter_Z = joint.Position.Z; break;
                case JointType.ShoulderLeft: ShoulderLeft_X = joint.Position.X; ShoulderLeft_Y = joint.Position.Y; ShoulderLeft_Z = joint.Position.Z; break;
                case JointType.ShoulderRight: ShoulderRight_X = joint.Position.X; ShoulderRight_Y = joint.Position.Y; ShoulderRight_Z = joint.Position.Z; break;
                case JointType.Spine: Spine_X = joint.Position.X; Spine_Y = joint.Position.Y; Spine_Z = joint.Position.Z; break;
                case JointType.HipCenter: HipCenter_X = joint.Position.X; HipCenter_Y = joint.Position.Y; HipCenter_Z = joint.Position.Z; break;
                case JointType.HipLeft: HipLeft_X = joint.Position.X; HipLeft_Y = joint.Position.Y; HipLeft_Z = joint.Position.Z; break;
                case JointType.HipRight: HipRight_X = joint.Position.X; HipRight_Y = joint.Position.Y; HipRight_Z = joint.Position.Z; break;
                case JointType.ElbowLeft: ElbowLeft_X = joint.Position.X; ElbowLeft_Y = joint.Position.Y; ElbowLeft_Z = joint.Position.Z; break;
                case JointType.WristLeft: WristLeft_X = joint.Position.X; WristLeft_Y = joint.Position.Y; WristLeft_Z = joint.Position.Z; break;
                case JointType.HandLeft: HandLeft_X = joint.Position.X; HandLeft_Y = joint.Position.Y; HandLeft_Z = joint.Position.Z; break;
                case JointType.ElbowRight: ElbowRight_X = joint.Position.X; ElbowRight_Y = joint.Position.Y; ElbowRight_Z = joint.Position.Z; break;
                case JointType.WristRight: WristRight_X = joint.Position.X; WristRight_Y = joint.Position.Y; WristRight_Z = joint.Position.Z; break;
                case JointType.HandRight: HandRight_X = joint.Position.X; HandRight_Y = joint.Position.Y; HandRight_Z = joint.Position.Z; break;
                case JointType.KneeLeft: KneeLeft_X = joint.Position.X; KneeLeft_Y = joint.Position.Y; KneeLeft_Z = joint.Position.Z; break;
                case JointType.AnkleLeft: AnkleLeft_X = joint.Position.X; AnkleLeft_Y = joint.Position.Y; AnkleLeft_Z = joint.Position.Z; break;
                case JointType.FootLeft: FootLeft_X = joint.Position.X; FootLeft_Y = joint.Position.Y; FootLeft_Z = joint.Position.Z; break;
                case JointType.KneeRight: KneeRight_X = joint.Position.X; KneeRight_Y = joint.Position.Y; KneeRight_Z = joint.Position.Z; break;
                case JointType.AnkleRight: AnkleRight_X = joint.Position.X; AnkleRight_Y = joint.Position.Y; AnkleRight_Z = joint.Position.Z; break;
                case JointType.FootRight: FootRight_X = joint.Position.X; FootRight_Y = joint.Position.Y; FootRight_Z = joint.Position.Z; break;
            }
        }

        public void SetJoints(JointCollection joints)
        {
            foreach (Joint joint in joints)
            {
                SetJoint(joint);
            }
            this.timestamp = DateTime.Now;
        }
    }
}
