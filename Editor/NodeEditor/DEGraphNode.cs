
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

[Serializable]
public class DeGraphNode<T> : Node where T : NodeData {
  public T data;

  public DeGraphNode() : this(default) { }

  public DeGraphNode(T data = default) {
    title = ObjectNames.NicifyVariableName(typeof(T).Name);
    this.data = data ?? Activator.CreateInstance<T>();
    AddPorts();
  }


  void AddPorts() {
    var portFields = typeof(T).GetFieldsWithAttribute<PortAttribute>();

    foreach (var portField in portFields) {
      var attr = portField.GetCustomAttribute<PortAttribute>();
      var port = InstantiatePort(Orientation.Horizontal, attr.direction, attr.capacity, portField.FieldType);
      port.portName = portField.Name;
      if (attr.direction == Direction.Input) {
        inputContainer.Add(port);
      } else {
        outputContainer.Add(port);
      }
    }

    RefreshExpandedState();
    RefreshPorts();
  }

  public static explicit operator DeGraphNode<NodeData>(DeGraphNode<T> v) {
    return new DeGraphNode<NodeData>(v.data);
  }
}

