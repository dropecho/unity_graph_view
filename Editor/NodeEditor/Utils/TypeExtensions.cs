using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class TypeExtensions {
  public static IEnumerable<FieldInfo> GetFieldsWithAttribute<T>(this System.Type type) {
    return type.GetFields()
        .Where(x => x.GetCustomAttributes(typeof(T), true).Length > 0);
  }

  public static TAttr GetCustomAttribute<TAttr>(this FieldInfo field) where TAttr : Attribute {
    return field.GetCustomAttributes(typeof(TAttr)).ToList().FirstOrDefault() as TAttr;
  }
}