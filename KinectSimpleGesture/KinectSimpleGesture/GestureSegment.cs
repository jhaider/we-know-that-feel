using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectSimpleGesture {
    class JointData {
        public enum Axis { x, y, z };

        JointType m_joint;
		double m_angleY, m_angleX;
		double m_xDistance;

        const double DEFAULT_TOLERANCE = 10;

        double m_Xmin, m_Ymin, m_Zmin;
        double m_Xmax, m_Ymax, m_Zmax;

        public JointType DaJoint {
            get { return m_joint; }
        }

		public JointData(JointType joint, double xAngle, double yAngle, double xDistance) {
            m_joint = joint;
			m_angleY = yAngle;
			m_angleX = xAngle;
            m_Xmin = m_Ymin = m_Zmin = DEFAULT_TOLERANCE;
            m_Xmax = m_Ymax = m_Zmax = DEFAULT_TOLERANCE;
			m_xDistance = xDistance;
        }

        public bool InRange(Axis axis, double angle, double minRange, double maxRange) {
            double maxAngle = angle + maxRange;
            double minAngle = angle - minRange;
			// TODO: use xDistance somehow?
            switch (axis) {
                case Axis.x:
                    return (m_angleX - m_Xmin <= maxAngle && minAngle <= m_angleX + m_Xmax);
                case Axis.y:
                    return (m_angleY - m_Ymin <= maxAngle && minAngle <= m_angleY + m_Ymax);
                default:
                    return false;
            }
        }

		public double PercentOverlap(Axis axis, double angle, double minRange, double maxRange) {
			if (!InRange(axis, angle, minRange, maxRange)) {
				return 0;
			}
				double maxAngle = angle + minRange;
				double minAngle = angle - minRange;
				double numLeft, numRight, denLeft, denRight;
			// to find the intersection, choose the largest on the left and the smallest on the right.
			// Denominator would be the sum of the segments
			switch (axis) {
				case Axis.x:
					numLeft = m_angleX - m_Xmin <= minAngle ?  minAngle : m_angleX - m_Xmin;
					numRight = maxAngle <= m_angleX + m_Xmax ?  maxAngle : m_angleX + m_Xmax;
					denLeft = m_angleX - m_Xmin <= minAngle ? m_angleX - m_Xmin : minAngle;
					denRight = maxAngle <= m_angleX + m_Xmax ? m_angleX + m_Xmax :  maxAngle;
				case Axis.y:
					numLeft = m_angleY - m_Ymin <= minAngle ?  minAngle : m_angleY - m_Ymin;
					numRight = maxAngle <= m_angleY + m_Ymax ?  maxAngle : m_angleY + m_Ymax;
					denLeft = m_angleY - m_Ymin <= minAngle ? m_angleY - m_Ymin : minAngle;
					denRight = maxAngle <= m_angleY + m_Ymax ? m_angleY + m_Ymax :  maxAngle;
				default:
					return 0;
			}
			return Math.Abs(((numRight - numLeft)/(denRight - denLeft)) * 100);
		}

		// May need to refine this.
		// TODO: Return true if at least 50% overlap
        public bool Overlap(JointData data) {
            if (data.DaJoint != DaJoint) {
                return false;
            }
			return (PercentOverlap(Axis.x, data.m_angleX, data.m_Xmin, data.m_Xmax) >= 50) &&
				(PercentOverlap(Axis.y, data.m_angleY, data.m_Ymin, data.m_Ymax) >= 50);
        }
    }

    class GestureSegment {

        Dictionary<JointType, JointData> m_joints;

        public GestureSegment(Dictionary<JointType, JointData> joints) {
            m_joints = new Dictionary<JointType, JointData>(joints);
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
				JointData data; // data is an output variable
				segment.m_joints.TryGetValue (entry.Key, out data); 
				// if joint type does not exist or if there is no overlap
                if (!entry.Value.Overlap(data)) {
                    return false;
                }
            }
            return true;
        }


   
    }
}
