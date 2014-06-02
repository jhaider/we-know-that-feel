using Microsoft.Kinect;
using System;
using System.Collections;

namespace KinectSimpleGesture
{
    public class WaveGesture
    {
        readonly int WINDOW_SIZE = 50;

        IGestureSegment[] _leftWaveSegments;
        int _leftSegmentCount = 0;
        int _leftFrameCount = 0;

        IGestureSegment[] _rightWaveSegments;
        int _rightSegmentCount = 0;
        int _rightFrameCount = 0;

        string _direction;

        public event EventHandler GestureRecognized;

        public WaveGesture()
        {
            RightOfElbowSegment rightElbowSegment = new RightOfElbowSegment();
            LeftOfElbowSegment leftElbowSegment = new LeftOfElbowSegment();

            _leftWaveSegments = new IGestureSegment[]
            {
                rightElbowSegment,
                leftElbowSegment,
                //rightElbowSegment,

            };

            _rightWaveSegments = new IGestureSegment[]
            {
                leftElbowSegment,
                rightElbowSegment,
                //leftElbowSegment,

            };
        }

        /// <summary>
        /// Updates the current gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton data.</param>
        public void Update(Skeleton skeleton)
        {
            //detections.Push(DetectRightWave(skeleton));
            //bool check2 = DetectLeftWave(skeleton);
            //if (check1 || check2)
            //{
            //    Console.WriteLine("whoo hoo");
            //}

            DetectLeftWave(skeleton);
            DetectRightWave(skeleton);
        }

        public bool DetectLeftWave(Skeleton skeleton) 
        {
            _direction = "left";
            return DetectWave(_leftWaveSegments, skeleton, ref _leftSegmentCount, ref _leftFrameCount);
        }

        public bool DetectRightWave(Skeleton skeleton) 
        {
            _direction = "right";
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

        /// <summary>
        /// Resets the current gesture.
        /// </summary>
        public void Reset()
        {
          
        }

        public string Direction
        {
            get { return _direction;  }
        }
    }
}
