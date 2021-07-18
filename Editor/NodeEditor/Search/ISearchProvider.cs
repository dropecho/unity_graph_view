using System.Collections.Generic;

namespace Dropecho.Graph.Editor {
  /// <summary>
  /// Interface for a search provider that yields nodes that can be added to a graph.
  /// 
  /// All providers are instantiated the first time a graph is loaded in 
  /// the canvas editor and are reused for graphs that pass IsSupported().
  /// </summary>
  public interface ISearchProvider {
    /// <summary>
    /// Get results for the given search paramaeters
    /// </summary>
    IEnumerable<SearchResult> GetSearchResults(SearchFilter filter);

    // Node Instantiate(SearchResult result);
  }

  public class SearchResult {
    public string Name { get; set; }

    public IEnumerable<string> Path { get; set; }

    public object UserData { get; set; }

    public ISearchProvider Provider { get; set; }
  }

  public class SearchFilter {
  }
}