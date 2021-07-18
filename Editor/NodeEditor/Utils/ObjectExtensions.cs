using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class ObjectExtensions {
  public static object GetFieldValue(this Object obj, string fieldName) {
    return obj.GetType().GetField(fieldName).GetValue(obj);
  }

  public static T GetFieldValue<T>(this Object obj, string fieldName) where T : class {
    return obj.GetType().GetField(fieldName).GetValue(obj) as T;
  }
}