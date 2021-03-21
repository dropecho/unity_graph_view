using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DEGraphView : GraphView {
  Rect defaultPosition = new Rect(100, 200, 100, 200);

  public List<DeGraphNode<NodeData>> DeGraphNodes = new List<DeGraphNode<NodeData>>();

  public DEGraphView() {
    styleSheets.Add(Resources.Load<StyleSheet>("degraph"));

    SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
    this.AddManipulator(new ContentDragger());
    this.AddManipulator(new SelectionDragger());
    this.AddManipulator(new RectangleSelector());

    var grid = new GridBackground();
    Insert(0, grid);
    grid.StretchToParentSize();
  }

  public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter adapter) {
    return ports.ToList().Where(port => port != startPort && port.node != startPort.node).ToList();
  }

  public void AddNode(System.Type nodeDataType) {
    var node = CreateNode(nodeDataType);
    node.SetPosition(defaultPosition);
    AddElement(node);
  }

  public Node CreateNode(System.Type nodeDataType) {
    var nodeType = typeof(DeGraphNode<>).MakeGenericType(new System.Type[] { nodeDataType });
    return Activator.CreateInstance(nodeType) as Node;
  }

  public void AddNode(NodeData data) {
    var node = CreateNode(data);
    node.viewDataKey = data.guid;
    node.SetPosition(data.position);
    AddElement(node);
  }

  public Node CreateNode(NodeData data) {
    var dataType = data.GetType();
    var nodeType = typeof(DeGraphNode<>).MakeGenericType(new System.Type[] { dataType });
    return nodeType.GetConstructor(new System.Type[] { dataType }).Invoke(new object[] { data }) as Node;
  }
}