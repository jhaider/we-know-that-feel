using System;
using Microsoft.Kinect;
using KinectSimpleGesture;
using System;
using System.Collections.Generic;

namespace KinectSimpleGesture
{
	public class GestureDetect
	{
		readonly int WINDOW_SIZE = 50;

		TrieNode m_currentNode = null;
		int m_segmentCount = 0;
		int m_frameCount = 0;

		public event EventHandler GestureRecognized;

		public GestureDetect () {
            m_currentNode = new TrieNode();
            Dictionary<JointType, JointData> dict1 = new Dictionary<JointType, JointData>();
            Dictionary<JointType, JointData> dict2 = new Dictionary<JointType, JointData>();
            Dictionary<JointType, JointData> dict3 = new Dictionary<JointType, JointData>();
            Dictionary<JointType, JointData> dict4 = new Dictionary<JointType, JointData>();
			Dictionary<JointType, JointData> dict5 = new Dictionary<JointType, JointData>();
            
            // Hard code test data into the trie
            JointData data = new JointData(JointType.ElbowLeft, -13.8886118490198, 2.38573102788801, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 0.106122742254382, -76.2663496944735, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -14.5442959615442, 34.1279522697456, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 1.61748937279837, -80.2129274659732, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -11.373574684312, -33.594958750071, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, -1.59998662494679, -78.3427943947249, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -14.3813969738617, 27.9896822712673, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 1.37402396087154, -79.134973625391, 10);
            dict1.Add(JointType.WristRight, data);
            GestureSegment segment = new GestureSegment(dict1);
			TrieNode trie = new TrieNode(segment); // parent

            data = new JointData(JointType.ElbowLeft, -13.9888844123371, 2.21115891220704, 10);
            dict2.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 4.17213192378785, 33.8044823441846, 10);
            dict2.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -14.8216168105891, 33.4886923248334, 10);
            dict2.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 12.2081252315785, 30.6498011982772, 10);
            dict2.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -11.4024894705325, -33.6370778060487, 10);
            dict2.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, -1.57806897988996, -78.5381135236992, 10);
            dict2.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -14.7018796386593, 27.3269014147364, 10);
            dict2.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 10.1787335313979, 30.5393396193118, 10);
            dict2.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict2);
            TrieNode child = trie.addChild(segment);
            
            data = new JointData(JointType.ElbowLeft, -13.9836244839633, 1.71891913438868, 10);
			dict3.Add(JointType.ElbowLeft, data);
			data = new JointData(JointType.ElbowRight, 4.3049878323027, 43.4621989227762, 10);
			dict3.Add(JointType.ElbowRight, data);
			data = new JointData(JointType.HandLeft, -14.7151801589239, 33.2339854987523, 10);
			dict3.Add(JointType.HandLeft, data);
			data = new JointData(JointType.HandRight, 3.46824467371025, 76.7099660420278, 10);
			dict3.Add(JointType.HandRight, data);
			data = new JointData(JointType.ShoulderLeft, -11.1845941305872, -34.1881489323549, 10);
			dict3.Add(JointType.ShoulderLeft, data);
			data = new JointData(JointType.ShoulderRight, -1.38314797157102, -80.412653664972, 10);
			dict3.Add(JointType.ShoulderRight, data);
			data = new JointData(JointType.WristLeft, -14.632609630084, 26.9411859292547, 10);
			dict3.Add(JointType.WristLeft, data);
			data = new JointData(JointType.WristRight, 3.98281998920187, 70.7508683900332, 10);
			dict3.Add(JointType.WristRight, data);
			segment = new GestureSegment(dict3);
			TrieNode child2 = child.addChild(segment);

			data = new JointData(JointType.ElbowLeft, -14.0028558580185, 1.79803376615876, 10);
			dict4.Add(JointType.ElbowLeft, data);
			data = new JointData(JointType.ElbowRight, 4.90968971991051, 35.588217052244, 10);
			dict4.Add(JointType.ElbowRight, data);
			data = new JointData(JointType.HandLeft, -14.7223904525819, 33.2593668874657, 10);
			dict4.Add(JointType.HandLeft, data);
			data = new JointData(JointType.HandRight, 13.1459056125209, 37.7609138521562, 10);
			dict4.Add(JointType.HandRight, data);
			data = new JointData(JointType.ShoulderLeft, -11.3454599877182, -33.7682599948257, 10);
			dict4.Add(JointType.ShoulderLeft, data);
			data = new JointData(JointType.ShoulderRight, -1.51655913531167, -79.4708437301563, 10);
			dict4.Add(JointType.ShoulderRight, data);
			data = new JointData(JointType.WristLeft, -14.6397518392029, 26.9798967256435, 10);
			dict4.Add(JointType.WristLeft, data);
			data = new JointData(JointType.WristRight, 11.1063083347709, 37.2859755350544, 10);
			dict4.Add(JointType.WristRight, data);
			segment = new GestureSegment(dict4);
			TrieNode child3 = child2.addChild(segment);

            data = new JointData(JointType.ElbowLeft, -13.9022953696224, 1.35316570984196, 10);
			dict5.Add(JointType.ElbowLeft, data);
			data = new JointData(JointType.ElbowRight, 3.95160036770324, 49.6285706518732, 10);
			dict5.Add(JointType.ElbowRight, data);
			data = new JointData(JointType.HandLeft, -14.682648049446, 32.8898131784065, 10);
			dict5.Add(JointType.HandLeft, data);
			data = new JointData(JointType.HandRight, -1.6898189459153, -82.7667453028595, 10);
			dict5.Add(JointType.HandRight, data);
			data = new JointData(JointType.ShoulderLeft, -11.0641066611262, -34.0569025127242, 10);
			dict5.Add(JointType.ShoulderLeft, data);
			data = new JointData(JointType.ShoulderRight, -0.901948551797491, -84.4164358488472, 10);
			dict5.Add(JointType.ShoulderRight, data);
			data = new JointData(JointType.WristLeft, -14.6108300566225, 26.5385944815128, 10);
			dict5.Add(JointType.WristLeft, data);
			data = new JointData(JointType.WristRight, 0.0893234273827823, 89.5477381361585, 10);
			dict5.Add(JointType.WristRight, data);
			segment = new GestureSegment(dict5);
			TrieNode child4 = child3.addChild(segment);

        }

        public GestureDetect(TrieNode trieNode) {
            m_currentNode = trieNode;
        }

		/// <summary>
		/// Updates the current gesture.
		/// </summary>
		/// <param name="skeleton">The skeleton data.</param>
		public void Update(Skeleton skeleton) {
			// Create a gesture segment with the deltas compared to the axis
            // Get the difference from the current node to the generated gesture segment

            Console.Write("Update");

            GestureSegment segment = GestureSegment.generateSegmentFromSkeleton(skeleton);
            Console.Write("Update here ");

         //   DetectGesture(segment);
		}

		public bool DetectGesture(GestureSegment segment)
		{

            Console.Write("RAWR");
			// Check the trie to see if the segment is found
			// Null is returned if nothing is found
			TrieNode result = m_currentNode.findChild(segment);
            Console.Write("boo");
			if (result != null) {
				if (result.isTerminal) {
                    if (GestureRecognized != null) {
                        GestureRecognized(this, new GestureEventArgs());
                    }
					m_segmentCount = 0;
					m_frameCount = 0;
					return true;
				} else {
					m_segmentCount++;
					m_frameCount = 0;
				}
                m_currentNode = result;
			} else if (m_frameCount == WINDOW_SIZE){
				m_segmentCount = 0;
				m_frameCount = 0;
			} else {
				m_frameCount++;
			}

			return false;
		}


	}

	class GestureEventArgs : EventArgs {
		TrieNode m_gesture;
        public GestureEventArgs() {}
        public GestureEventArgs (TrieNode gesture) {
			m_gesture = gesture;
		}
	}

}

