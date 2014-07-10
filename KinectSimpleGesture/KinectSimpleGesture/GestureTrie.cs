using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TrieNode {
    private GestureSegment segment { get; set; }
    private Dictionary<GestureSegment, TrieNode> children;
    private TrieNode parent { get; set; }
    private string name { get; set; } 

    public TrieNode(GestureSegment s) {
        segment.set(s);
    }
    public bool isTerminal { get; set; } // if isTerminal, the entire gesture so far is a match
    
    // Returns string if valid gesture
    // Returns null/empty string if not
    public string isGesture(List<GestureSegments> gesture) {
        TrieNode root = this;
        for (int i = 0; i < gesture.size(); i++) {
            root = root.findChild(gesture[i]);
            if (!root) {
                return "";
            }
        }
        if (root.isTerminal) {
            return root.name;
        }
        return "";
    }
    
    public TrieNode findChild(GestureSegment s) {
        TrieNode child;
        // TODO: RETURN NULL IF NOT PRESENT. ELSE RETURN ACTUAL VALUE STORED IN CHILD.
        children.TryGetValue(s, child);
        return child;
        // does the given segment match a sweet child of mine?
        // returm child value or return null
    }

    public void addChild(GestureSegment newChild) {
        children.add(newChild, TrieNode(newChild));
    }

    public void addSegments(List<GestureSegment> segments, string name) {
        TrieNode root = this;
        for (int i = 0; i < segments.size(); i++) {
            TrieNode next = root.findChild(segments[i]);
            if (!next) {
                root.addChild(segments[i]);
                next = root.findChild(segments[i]);
            }
            root = next;
        }
        root.isTerminal = true;
        root.name = name;
    }

}
