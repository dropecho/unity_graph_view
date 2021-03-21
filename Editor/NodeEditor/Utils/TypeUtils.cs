
using System;
using System.Linq;

public static class TypeUtils {
  public static System.Collections.Generic.IEnumerable<System.Type> GetClassesImplementing<T>() {
    return AppDomain.CurrentDomain.GetAssemblies()
      .SelectMany(s => s.GetTypes())
      // where you can use T as an assignment, i.e. T mything = new type();
      // usually something like type implementing an interface T.
      .Where(type => typeof(T).IsAssignableFrom(type) && type.IsClass);
  }

  public static System.Collections.Generic.IEnumerable<System.Type> GetSubClassesOf<T>() {
    return AppDomain.CurrentDomain.GetAssemblies()
      .SelectMany(s => s.GetTypes())
      // Where type inherits from T.
      .Where(type => type.IsSubclassOf(typeof(T)) && type.IsClass);
  }
}