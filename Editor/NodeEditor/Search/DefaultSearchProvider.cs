using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dropecho.Graph.Editor {
  public class DefaultSearchProvider : ScriptableObject, ISearchWindowProvider {

    public DEGraphView view;

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context) {
      var root = new SearchTreeGroupEntry(new GUIContent("Add Node")) { level = 0 };
      var tree = new List<SearchTreeEntry>();
      tree.Add(root);


      var nodeTypes = TypeUtils.GetSubClassesOf<NodeData>();
      foreach (var nodeType in nodeTypes) {
        tree.Add(new SearchTreeEntry(new GUIContent(nodeType.Name)) { level = 1, userData = nodeType });
      }
      return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context) {
      view.AddNode(entry.userData as Type);
      return true;
    }
  }
}