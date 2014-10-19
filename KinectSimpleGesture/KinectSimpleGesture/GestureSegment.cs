using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectSimpleGesture {

    public class JointData {
        public enum Axis { x, y, z };
        public enum Direction { POS, NEG };

        JointType m_joint;
		double m_angleY, m_angleX;
		double m_xDistance;

        const double DEFAULT_TOLERANCE = 10;
        const int AGGREGATE_TOLERANCE = 30;

        double m_Xmin, m_Ymin;
        double m_Xmax, m_Ymax;

        Direction x_dir;
        Direction y_dir;

        public JointType DaJoint {
            get { return m_joint; }
        }

        public void print()
        {
            Console.WriteLine("data = new JointData(JointType."+m_joint+", " + m_angleX + ", " + m_angleY + ", 10);");
        }

		public JointData(JointType joint, double xAngle, double yAngle, double xDistance) {
            m_joint = joint;
			m_angleY = yAngle;
			m_angleX = xAngle;
            m_Xmax = m_Xmin = m_Ymax = m_Ymin = DEFAULT_TOLERANCE;
            //m_Xmin = xAngle - DEFAULT_TOLERANCE;
            //m_Xmax = xAngle + DEFAULT_TOLERANCE;
            //m_Ymin = yAngle - DEFAULT_TOLERANCE;
            //m_Ymax = yAngle + DEFAULT_TOLERANCE;
			m_xDistance = xDistance;
        }

        public bool InRange(Axis axis, double angle, int tolerance = 10) 
        {
            double maxAngle = Math.Abs(angle + tolerance) * ((angle < 0) ? -1 : 1);
            double minAngle = Math.Abs(angle - tolerance) * ((angle < 0)? -1 : 1);
			// TODO: use xDistance somehow?
            switch (axis) 
            {
                case Axis.x:
                    Console.WriteLine(m_angleX + ", " + m_Xmin + ", " + maxAngle + ", ");
                    return (m_angleX - tolerance <= maxAngle && minAngle <= m_angleX + tolerance);
                case Axis.y:
                    return (m_angleY - tolerance <= maxAngle && minAngle <= m_angleY + tolerance);
                default:
                    return false;
            }
        }

		public double PercentOverlap(Axis axis, double angle) 
        {
			if (!InRange(axis, angle))
				return 0;
			
            int tolerance = 15;
			double maxAngle = angle + tolerance;
			double minAngle = angle - tolerance;
			double numLeft, numRight, denLeft, denRight;
			// to find the intersection, choose the largest on the left and the smallest on the right.
			// Denominator would be the sum of the segments
			switch (axis) 
            {
				case Axis.x:
					numLeft = m_angleX - m_Xmin <= minAngle ?  minAngle : m_angleX - m_Xmin;
					numRight = maxAngle <= m_angleX + m_Xmax ?  maxAngle : m_angleX + m_Xmax;
					denLeft = m_angleX - m_Xmin <= minAngle ? m_angleX - m_Xmin : minAngle;
					denRight = maxAngle <= m_angleX + m_Xmax ? m_angleX + m_Xmax :  maxAngle;
                    break;
				case Axis.y:
					numLeft = m_angleY - m_Ymin <= minAngle ?  minAngle : m_angleY - m_Ymin;
					numRight = maxAngle <= m_angleY + m_Ymax ?  maxAngle : m_angleY + m_Ymax;
					denLeft = m_angleY - m_Ymin <= minAngle ? m_angleY - m_Ymin : minAngle;
					denRight = maxAngle <= m_angleY + m_Ymax ? m_angleY + m_Ymax :  maxAngle;
                    break;
				default:
					return 0;
			}
			return Math.Abs(((numRight - numLeft)/(denRight - denLeft)) * 100);
		}

		// May need to refine this.
		// TODO: Return true if at least 50% overlap
        public bool Overlap(JointData data) 
        {
            if (data.DaJoint != DaJoint)
                return false;
            
			return (PercentOverlap(Axis.x, data.m_angleX) >= 50) &&
				(PercentOverlap(Axis.y, data.m_angleY) >= 50);
        }

        public bool InAggregateRange(JointData data)
        {
            if (data.DaJoint != DaJoint)
                return false;

            Console.WriteLine("Tol axis.x " + data.m_angleX + " vs " + m_angleX);
            Console.WriteLine("Tol axis.y " + data.m_angleY + " vs " + m_angleY);

            return InRange(Axis.x, data.m_angleX, AGGREGATE_TOLERANCE) &&
                InRange(Axis.y, data.m_angleY, AGGREGATE_TOLERANCE);
        }

        public bool InSameDirection(JointData data, JointData prevData)
        {
            if (data.DaJoint != DaJoint)
                return false;

            x_dir = ((data.m_angleX - m_angleX) > 0) ? Direction.POS : Direction.NEG;
            y_dir = ((data.m_angleY - m_angleY) > 0) ? Direction.POS : Direction.NEG;

            Console.WriteLine("xDir " + x_dir + " y_dir " + y_dir);

            if (prevData == null)
            {
                Console.WriteLine("prev Data was null");
                return true;
            }

            Console.WriteLine("PREV: xDir " + prevData.x_dir + "y_dir " + prevData.y_dir);
            return (prevData.x_dir == x_dir && prevData.y_dir == y_dir);
        }
    }

    public class GestureSegment {

        static JointType[] jointArray = {
            JointType.ElbowLeft,
            JointType.ElbowRight,
            JointType.HandLeft,
            JointType.HandRight,
            JointType.ShoulderLeft,
            JointType.ShoulderRight,
            JointType.WristLeft,
            JointType.WristRight
           /* JointType.KneeLeft,
            JointType.KneeRight,
            JointType.AnkleLeft,
            JointType.AnkleRight,
            JointType.HipLeft,
            JointType.HipRight,
            JointType.FootLeft,
            JointType.FootRight*/
        };

        Dictionary<JointType, JointData> m_joints;

        public GestureSegment(Dictionary<JointType, JointData> joints) {
            m_joints = new Dictionary<JointType, JointData>(joints);
        }

        public Dictionary<JointType, JointData> Joints {
            get { return m_joints; }
        }


        public static GestureSegment generateSegmentFromSkeleton(Skeleton skeleton) {
            Dictionary<JointType, JointData> dictionary = new Dictionary<JointType,JointData>();

            //Console.WriteLine("GestureSegment segment = new GestureSegment(dict1)");
            //Console.WriteLine("segments.Add(segment)"); ;

            foreach (JointType joint in jointArray) {

                // Calculate the value and add into dictionary
                float x = skeleton.Joints[joint].Position.X;
                float y = skeleton.Joints[joint].Position.Y;
                float z = skeleton.Joints[joint].Position.Z;

                double x_angle = (Math.Atan(x / z) * 180) / Math.PI;
                double y_angle = (Math.Atan(y / x) * 180) / Math.PI;

                JointData data = new JointData(joint, x_angle, y_angle, x);
                dictionary.Add(joint, data);

                //Console.WriteLine(joint + " " + x_angle + " " + y_angle);

                //Console.WriteLine("data = new JointData(JointType."+joint+", " + x_angle + ", " + y_angle + ", 10);");
                //Console.WriteLine("dict1.Add(JointType." + joint + ", data);");
            }

            return new GestureSegment(dictionary);
            //return null;
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

        //returns true if the segment passed in and self belong in the same aggregate
        public bool InAggregate(GestureSegment segment, GestureSegment prevSegment)
        {
            if (segment == null)
            {
                return false;
            }

            if (m_joints.Count != segment.m_joints.Count)
            {
                return false;
            }

            foreach (KeyValuePair<JointType, JointData> entry in m_joints)
            {
                JointData data; 
                segment.m_joints.TryGetValue(entry.Key, out data);

                JointData prevData = null;
                if (prevSegment != null) {
                    prevSegment.m_joints.TryGetValue(entry.Key, out prevData);
                }
 
                //first we check to see if they are in the same direction, if not return false
                if (!entry.Value.InSameDirection(data, prevData)) {
                    Console.WriteLine("not the same direction");
                    return false;
                }
           
                //next we check to see if they are within the aggregate tolerance range
                if (!entry.Value.InAggregateRange(data))
                {
                    Console.WriteLine("outside the aggregation tolerance");
                    return false;
                }
            }
            return true;
        }

   
    }
}
