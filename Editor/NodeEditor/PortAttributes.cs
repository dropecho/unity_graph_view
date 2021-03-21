using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PortAttribute : PropertyAttribute {
  public string name;
  public Direction direction;
  public Port.Capacity capacity;
  public PortAttribute(string name = "", Direction direction = Direction.Input, Port.Capacity capacity = Port.Capacity.Single) {
    this.name = name;
    this.direction = direction;
    this.capacity = capacity;
  }
}

public class InputPortAttribute : PortAttribute {
  public InputPortAttribute(string name = "", Port.Capacity capacity = Port.Capacity.Single) : base(name, Direction.Input, capacity) { }
}

public class OutputPortAttribute : PortAttribute {
  public OutputPortAttribute(string name = "", Port.Capacity capacity = Port.Capacity.Single) : base(name, Direction.Output, capacity) { }
}