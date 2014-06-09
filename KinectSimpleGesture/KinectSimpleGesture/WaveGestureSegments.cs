using Microsoft.Kinect;
using System;

namespace KinectSimpleGesture
{
    /// <summary>
    /// Represents a single gesture segment which uses relative positioning of body parts to detect a gesture.
    /// </summary>
    public interface IGestureSegment
    {
        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        GesturePartResult Update(Skeleton skeleton);

    }

    public class ArmSegment : IGestureSegment
    {

        public enum Arm { Right, Left };

        protected Arm m_arm;
        protected JointType m_handJoint;
        protected JointType m_elbowJoint;

        public ArmSegment(Arm armside)
        {
            m_arm = armside;
            m_handJoint = (m_arm == Arm.Right) ? JointType.HandRight : JointType.HandLeft;
            m_elbowJoint = (m_arm == Arm.Right) ? JointType.ElbowRight : JointType.ElbowLeft;
        }

        GesturePartResult IGestureSegment.Update(Skeleton skeleton) { return update(skeleton); }

        public virtual GesturePartResult update(Skeleton skeleton) { return GesturePartResult.Failed; }

    }

    public class RightOfElbowSegment : ArmSegment
    {
        public RightOfElbowSegment(Arm armside) : base(armside) { }

        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public override GesturePartResult update(Skeleton skeleton)
        {
            // Hand above elbow
            if (skeleton.Joints[m_handJoint].Position.Y > skeleton.Joints[m_elbowJoint].Position.Y)
            {   
                // Hand right of elbow
                if ((m_arm == Arm.Right && skeleton.Joints[m_handJoint].Position.X > skeleton.Joints[m_elbowJoint].Position.X)
                    || (m_arm == Arm.Left && skeleton.Joints[m_handJoint].Position.X < skeleton.Joints[m_elbowJoint].Position.X))
                {
                   // Console.WriteLine("Right Elbow Success");
                    return GesturePartResult.Succeeded;
                }
            }

            // Hand dropped
            return GesturePartResult.Failed;
        }
    }

    public class LeftOfElbowSegment : ArmSegment
    {
        public LeftOfElbowSegment(Arm armside) : base(armside) { }

        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>A GesturePartResult based on whether the gesture part has been completed.</returns>
        public override GesturePartResult update(Skeleton skeleton)
        {
            // Hand above elbow
            if (skeleton.Joints[m_handJoint].Position.Y > skeleton.Joints[m_elbowJoint].Position.Y)
            {
                // Hand left of elbow
                if ((m_arm == Arm.Right && skeleton.Joints[m_handJoint].Position.X > skeleton.Joints[m_elbowJoint].Position.X)
                    || (m_arm == Arm.Left && skeleton.Joints[m_handJoint].Position.X < skeleton.Joints[m_elbowJoint].Position.X))
                {
                    //Console.WriteLine("Left Elbow Success");
                    return GesturePartResult.Succeeded;
                }
            }

            // Hand dropped
            return GesturePartResult.Failed;
        }
    }
}
