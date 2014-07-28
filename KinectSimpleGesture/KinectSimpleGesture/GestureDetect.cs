﻿using System;
using Microsoft.Kinect;
using KinectSimpleGesture;

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

