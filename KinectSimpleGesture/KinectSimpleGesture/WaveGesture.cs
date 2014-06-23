using Microsoft.Kinect;
using System;
using System.Collections;

namespace KinectSimpleGesture
{
    public class WaveGesture
    {
        readonly int WINDOW_SIZE = 50;

        public static readonly string DIRECTION_RIGHT = "right";
        public static readonly string DIRECTION_LEFT = "left";

        IGestureSegment[] _leftWaveSegments;
        int _leftSegmentCount = 0;
        int _leftFrameCount = 0;

        IGestureSegment[] _rightWaveSegments;
        int _rightSegmentCount = 0;
        int _rightFrameCount = 0;

        string _direction;
        string _hand;

        public event EventHandler GestureRecognized;

        public WaveGesture(ArmSegment.Arm side)
        {
            RightOfElbowSegment rightElbowSegment = new RightOfElbowSegment(side);
            LeftOfElbowSegment leftElbowSegment = new LeftOfElbowSegment(side);
            _hand = (side == ArmSegment.Arm.Left) ? DIRECTION_LEFT : DIRECTION_RIGHT;

            _leftWaveSegments = new IGestureSegment[]
            {
                rightElbowSegment,
                leftElbowSegment,

            };

            _rightWaveSegments = new IGestureSegment[]
            {
                leftElbowSegment,
                rightElbowSegment,
            };
        }

        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton data.</param>
        public void Update(Skeleton skeleton)
        {
            DetectLeftWave(skeleton);
            DetectRightWave(skeleton);
        }

        public bool DetectLeftWave(Skeleton skeleton) 
        {
            _direction = DIRECTION_LEFT;
            return DetectWave(_leftWaveSegments, skeleton, ref _leftSegmentCount, ref _leftFrameCount);
        }

        public bool DetectRightWave(Skeleton skeleton) 
        {
            _direction = DIRECTION_RIGHT;
            return DetectWave(_rightWaveSegments, skeleton, ref _rightSegmentCount, ref _rightFrameCount);
        }

        public bool DetectWave(IGestureSegment[] segments, Skeleton skeleton, ref int segmentCount, ref int frameCount)
        {
            
            GesturePartResult result = segments[segmentCount].Update(skeleton);

            if (result == GesturePartResult.Succeeded)
            {
                if (segmentCount + 1 < segments.Length)
                {
                    segmentCount++;
                    frameCount = 0;
                }
                else
                {
                    if (GestureRecognized != null)
                    {
                        GestureRecognized(this, new EventArgs());
                        segmentCount = 0;
                        frameCount = 0;
                        return true;
                    }
                }
            }
            else if (result == GesturePartResult.Failed || frameCount == WINDOW_SIZE)
            {
                segmentCount = 0;
                frameCount = 0;
            }
            else
            {
                frameCount++;
            }

            return false;
        }

        public string Direction
        {
            get { return _direction; }
        }

    }
}
