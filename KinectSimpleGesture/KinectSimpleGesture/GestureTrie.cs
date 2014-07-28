using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Kinect;

namespace KinectSimpleGesture
{

    public class TrieNode
    {
        GestureSegment m_segment;
        Dictionary<GestureSegment, TrieNode> m_children;
        string m_name;

		public TrieNode()
		{
			// This is necessary to instantiate the first trienode, when you don't have a Gesture Segment to add yet.
			// It's basically the root node.
		}
		public TrieNode(GestureSegment s)
        {
            m_segment = s;
            m_children = new Dictionary<GestureSegment,TrieNode>();
        }
        public bool isTerminal { get; set; } // if isTerminal, the entire gesture so far is a match

        // Returns string if valid gesture
        // Returns null/empty string if not
        public string isGesture(List<GestureSegment> segments)
        {
            TrieNode root = this;
            for (int i = 0; i < segments.Count; i++)
            {
                root = root.findChild(segments[i]);
                if (root == null)
                {
                    break;
                }
            }
            if (root != null && root.isTerminal)
            {
                return root.m_name;
            }
            else
            {
                return "";
            }
        }

        public TrieNode findChild(GestureSegment s)
        {
            TrieNode child;
            // TODO: RETURN NULL IF NOT PRESENT. ELSE RETURN ACTUAL VALUE STORED IN CHILD.
            m_children.TryGetValue(s, out child);
            return child;
            // does the given segment match a sweet child of mine?
            // returm child value or return null
        }

		public TrieNode addChild(GestureSegment newChild)
        {
            TrieNode newNode = new TrieNode(newChild);
            m_children.Add(newChild, newNode);
			return newNode;
        }

        public void addSegments(List<GestureSegment> segments, string name)
        {
            TrieNode root = this;
            for (int i = 0; i < segments.Count; i++)
            {
                TrieNode next = root.findChild(segments[i]);
                if (next == null)
                {
                    root.addChild(segments[i]);
                    next = root.findChild(segments[i]);
                }
                root = next;
            }
            root.isTerminal = true;
            root.m_name = name;
        }

    }
}
