using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[Serializable]
public class NodeEdge {
  public string fromGuid;
  public string fromPortName;
  public string toGuid;
  public string toPortName;
}

[CreateAssetMenu(fileName = "Graph", menuName = "Dropecho/Graph Data", order = 1)]
public class DEGraphData : ScriptableObject {
  [SerializeReference]
  public List<NodeData> Nodes = new List<NodeData>();
  public List<NodeEdge> Edges = new List<NodeEdge>();

  public void BuildFromGraphView(GraphView view) {
    SetNodes(view);
    SetEdges(view);
  }

  public void SetEdges(GraphView view) {
    Edges = view.edges.ToList().Where(x => {
      return x.input.node != null && x.output.node != null;
    }).Select(x => {
      var inGuid = x.input.node.viewDataKey;
      var outGuid = x.output.node.viewDataKey;
      var edge = new NodeEdge {
        fromGuid = outGuid,
        fromPortName = x.output.portName,
        toGuid = inGuid,
        toPortName = x.input.portName
      };

      return edge;
    }).ToList();
  }

  public void SetNodes(GraphView view) {
    Nodes = view.nodes.ToList()
          .Select(x => {
            var data = x.GetType().GetField("data").GetValue(x) as NodeData;
            data.guid = x.viewDataKey;
            data.position = x.GetPosition();
            return data;
          })
          .ToList();
  }
}
