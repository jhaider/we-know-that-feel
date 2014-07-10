using System;
using Microsoft.Kinect;

namespace KinectSimpleGesture
{
	public class GestureDetect
	{
		readonly int WINDOW_SIZE = 50;

		TrieNode currentNode = null;
		int m_segmentCount = 0;
		int m_FrameCount = 0;

		public event EventHandler GestureRecognized;

		public GestureDetect () { }

		/// <summary>
		/// Updates the current gesture.
		/// </summary>
		/// <param name="skeleton">The skeleton data.</param>
		public void Update(Skeleton skeleton)
		{
			// Create a gesture segment and generate it
		}

		public bool DetectGesture(GestureSegment segment, ref int segmentCount, ref int frameCount)
		{
			// Check the trie to see if the segment is found
			// Null is returned if nothing is found
			TrieNode result = currentNode.findChild(segment);
			if (result != null) {
				if (result.isTerminal) {
					GestureRecognized(this, new GestureEventArgs());
					segmentCount = 0;
					frameCount = 0;
					return true;
				} else if (GestureRecognized != null) {
					segmentCount++;
					frameCount = 0;
				}
			} else if (frameCount == WINDOW_SIZE){
				segmentCount = 0;
				frameCount = 0;
			} else {
				frameCount++;
			}

			return false;
		}


	}

	public class GestureEventArgs : EventArgs {
		TrieNode m_gesture;
		public GestureEventArgs (TrieNode gesture) {
			m_gesture = gesture;
		}
	}

}

