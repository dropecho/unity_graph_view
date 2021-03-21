using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DEGraphEditorWindow : EditorWindow {
  private DEGraphView _graphView;
  private DEGraphData _graphData;

  [MenuItem("Dropecho/Graph View")]
  public static void Open() => GetWindow<DEGraphEditorWindow>();

  void OnEnable() {
    var graph = RenderGraphView();
    rootVisualElement.Add(graph);
    graph.Add(RenderToolbar());
  }

  void OnDisable() {
    rootVisualElement.Clear();
  }

  Toolbar RenderToolbar() {
    var toolbar = new Toolbar();

    var graphDatas = Dropecho.Utils.GetAssetsOfType<DEGraphData>().ToList();

    var def = graphDatas.Count() > 0 ? graphDatas.First() : new DEGraphData() { name = "New Graph Data" };
    if (!graphDatas.Contains(def)) {
      graphDatas.Add(def);
    }
    _graphData = def;
    LoadData(_graphData);
    var dropdown = new PopupField<DEGraphData>(graphDatas, _graphData, (data) => data.name, (data) => data.name);
    dropdown.RegisterValueChangedCallback(evt => {
      _graphData = evt.newValue;
      LoadData(_graphData);
    });

    var menu = new ToolbarMenu();
    foreach (var nt in TypeUtils.GetSubClassesOf<NodeData>()) {
      var niceName = ObjectNames.NicifyVariableName(nt.Name);
      menu.menu.AppendAction($"Create {niceName}", (act) => _graphView.AddNode(nt));
    }
    toolbar.Add(dropdown);
    toolbar.Add(new Button(() => SaveData()) { text = "Save" });
    // toolbar.Add(new Button(() => ClearGraph()) { text = "CLEAR" });
    toolbar.Add(menu);

    return toolbar;
  }

  private void SaveData() {
    _graphData.BuildFromGraphView(_graphView);
    EditorUtility.SetDirty(_graphData);
    AssetDatabase.SaveAssets();
  }

  public void LoadData(DEGraphData data) {
    ClearGraph();
    CreateNodes();
    CreateEdges();
  }

  private void CreateEdges() {
    var nodes = _graphView.nodes.ToList();
    foreach (var edge in _graphData.Edges) {
      var outPort = nodes
        .Find(x => x.viewDataKey == edge.fromGuid).outputContainer.Query<Port>().ToList()
        .Find(x => x.portName == edge.fromPortName);
      var inPort = nodes
        .Find(x => x.viewDataKey == edge.toGuid).inputContainer.Query<Port>().ToList()
        .Find(x => x.portName == edge.toPortName);

      var link = new Edge {
        input = inPort,
        output = outPort
      };

      outPort.Connect(link);
      inPort.Connect(link);
      _graphView.AddElement(link);
    }
  }

  private void CreateNodes() {
    foreach (var node in _graphData.Nodes) {
      _graphView.AddNode(node);
    }
  }

  private void ClearGraph() {
    _graphView.nodes.ToList().ForEach(x => _graphView.RemoveElement(x));
    _graphView.edges.ToList().ForEach(x => _graphView.RemoveElement(x));
  }

  DEGraphView RenderGraphView() {
    _graphView = new DEGraphView();
    _graphView.StretchToParentSize();
    return _graphView;
  }
}