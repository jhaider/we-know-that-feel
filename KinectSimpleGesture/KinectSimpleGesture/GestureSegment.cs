using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectSimpleGesture {
    class JointData {
        public enum Axis { x, y, z };

        JointType m_joint;
        double m_angleX, m_angleY, m_angleZ;

        const double DEFAULT_TOLERANCE = 10;

        double m_Xmin, m_Ymin, m_Zmin;
        double m_Xmax, m_Ymax, m_Zmax;

        public JointType DaJoint {
            get { return m_joint; }
        }

        public JointData(JointType joint, double xAngle, double yAngle, double zAngle) {
            m_joint = joint;
            m_angleX = xAngle;
            m_angleY = yAngle;
            m_angleZ = zAngle;
            m_Xmin = m_Ymin = m_Zmin = DEFAULT_TOLERANCE;
            m_Xmax = m_Ymax = m_Zmax = DEFAULT_TOLERANCE;
        }

        public bool InRange(Axis axis, double angle, double minRange, double maxRange) {
            double maxAngle = angle + maxRange;
            double minAngle = angle - minRange;
            switch (axis) {
                case Axis.x:
                    return (m_angleX - m_Xmin <= maxAngle && minAngle <= m_angleX + m_Xmax);
                case Axis.y:
                    return (m_angleY - m_Ymin <= maxAngle && minAngle <= m_angleY + m_Ymax);
                case Axis.z:
                    return (m_angleZ - m_Zmin <= maxAngle && minAngle <= m_angleZ + m_Zmax);
                default:
                    return false;
            }
        }

        public bool Overlap(JointData data) {
            if (data.DaJoint != DaJoint) {
                return false;
            }
            return InRange(Axis.x, data.m_angleX, data.m_Xmin, data.m_Xmax) &&
                    InRange(Axis.y, data.m_angleY, data.m_Ymin, data.m_Ymax) &&
                    InRange(Axis.z, data.m_angleZ, data.m_Zmin, data.m_Zmax);
        }
    }

    class GestureSegment {

        GestureSegment m_next;
        Dictionary<JointType, JointData> m_joints;

        public GestureSegment(GestureSegment nextSegment, Dictionary<JointType, JointData> joints) {
            m_next = nextSegment;
            m_joints = new Dictionary<JointType, JointData>(joints);
        }

        public GestureSegment NextGesture {
            get { return m_next; }
        }

        public Dictionary<JointType, JointData> Joints {
            get { return m_joints; }
        }

        // TODO possibility that the range will overlap!!!!!!
        // Have a method that calculates how close you are to the gesture


        public bool Match(object obj) {
            if (obj == null) {
                return false;
            }

            GestureSegment segment = obj as GestureSegment;
            if (m_joints.Count != segment.m_joints.Count) {
                return false;
            }

            // Check for each joint to see if they are in range
            foreach (KeyValuePair<JointType, JointData> entry in m_joints) {
                // do something with entry.Value or entry.Key
                JointData data;
                if (!segment.m_joints.TryGetValue(entry.Key, out data) || !entry.Value.Overlap(data)) {
                    return false;
                }
            }
            return true;
        }


   
    }
}
