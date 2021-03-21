using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[Serializable]
public abstract class NodeData {
  [HideInInspector]
  public string guid;
  [HideInInspector]
  public Rect position;
}

public class TestClass : NodeData {
  [InputPort, NonSerialized]
  public float someNumber;
  [OutputPort, NonSerialized]
  public float someoutput;
  [OutputPort, NonSerialized]
  public float someoutput2;
  [OutputPort, NonSerialized]
  public float someoutput3;
}

public class TestClass2 : NodeData {
  [InputPort, NonSerialized]
  public float someNumber;
  [InputPort(capacity: Port.Capacity.Multi), NonSerialized]
  public float someNumber2;
  [OutputPort, NonSerialized]
  public float someoutput3;
}