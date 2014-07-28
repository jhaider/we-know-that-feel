using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectSimpleGesture
{
    class Program
    {
		static GestureDetect _gesture = new GestureDetect(null);

        static void Main(string[] args)
        {
            var sensor = KinectSensor.KinectSensors.Where(s => s.Status == KinectStatus.Connected).FirstOrDefault();

            if (sensor != null)
            {
                sensor.SkeletonStream.Enable();
                sensor.SkeletonFrameReady += Sensor_SkeletonFrameReady;

                _gesture.GestureRecognized += Gesture_GestureRecognized;

                sensor.Start();
            }

            while(true)
            {
                Console.ReadKey();
                Console.Clear();  
            }
        }

        static void Sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    Skeleton[] skeletons = new Skeleton[frame.SkeletonArrayLength];

                    frame.CopySkeletonDataTo(skeletons);

                    if (skeletons.Length > 0)
                    {
                        var user = skeletons.Where(u => u.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();

                        if (user != null)
                        {
                            _gesture.Update(user);
                        }
                    }               
                }
            }
        }

        static void Gesture_GestureRecognized(object sender, EventArgs e)
        {
            Console.Write("You just waved ");
			//   Console.WriteLine(waveGesture.Direction);
        }
    }
}
