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
        protected JointType m_wristJoint;
        protected JointType m_shoulderJoint;

        //testing purposes: will be refined as we do more testing
        protected const int m_minWristToElbowAngle = 25;

        public ArmSegment(Arm armside)
        { 
            m_arm = armside;
            m_handJoint = (m_arm == Arm.Right) ? JointType.HandRight : JointType.HandLeft;
            m_elbowJoint = (m_arm == Arm.Right) ? JointType.ElbowRight : JointType.ElbowLeft;
            m_wristJoint = (m_arm == Arm.Right) ? JointType.WristRight : JointType.WristLeft;
            m_shoulderJoint = (m_arm == Arm.Right) ? JointType.ShoulderRight : JointType.ShoulderLeft;
        }

        GesturePartResult IGestureSegment.Update(Skeleton skeleton) { return update(skeleton); }
        public virtual GesturePartResult update(Skeleton skeleton) { return GesturePartResult.Failed; }

        protected double GetWristToElbowAngle(float z, float y)
        {
            return (Math.Atan2(y, z) * 180) / Math.PI;
        }

        protected bool IsWristAboveElbowEnough(Skeleton skeleton)
        {
            float y = skeleton.Joints[m_wristJoint].Position.Y - skeleton.Joints[m_elbowJoint].Position.Y;
            float z = skeleton.Joints[m_elbowJoint].Position.Z - skeleton.Joints[m_wristJoint].Position.Z;
            return (GetWristToElbowAngle(z, y) >= m_minWristToElbowAngle);
        }

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
            // Hand above elbow and infront of body
            if (IsWristAboveElbowEnough(skeleton)
                && skeleton.Joints[m_wristJoint].Position.Z < skeleton.Joints[m_shoulderJoint].Position.Z)
            {   
                // Hand right of elbow
                if ((m_arm == Arm.Right && skeleton.Joints[m_handJoint].Position.X > skeleton.Joints[m_elbowJoint].Position.X)
                    || (m_arm == Arm.Left && skeleton.Joints[m_handJoint].Position.X < skeleton.Joints[m_elbowJoint].Position.X))
                {
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
            // Hand above elbow and infront of body (shoulder)
            if (IsWristAboveElbowEnough(skeleton)
                && skeleton.Joints[m_wristJoint].Position.Z < skeleton.Joints[m_shoulderJoint].Position.Z)
            {
                // Hand left of elbow
                if ((m_arm == Arm.Right && skeleton.Joints[m_handJoint].Position.X > skeleton.Joints[m_elbowJoint].Position.X)
                    || (m_arm == Arm.Left && skeleton.Joints[m_handJoint].Position.X < skeleton.Joints[m_elbowJoint].Position.X))
                {
                    return GesturePartResult.Succeeded;
                }
            }

            // Hand dropped
            return GesturePartResult.Failed;
        }
    }
}
