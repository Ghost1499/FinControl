using System.Reflection;

namespace FinControlCore6.Utils
{
	public class ReflectionUtils
	{
		public static IEnumerable<PropertyInfo> GetProperties(Type type)
		{
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanRead && x.CanWrite && (!x.GetGetMethod()?.IsVirtual ?? false));
		}
        public static IEnumerable<string> GetPropertiesNames(IEnumerable<PropertyInfo> properties)
        {
            return properties.Select(p => p.Name/*[0].ToString().ToLower()+p.Name.Substring(1)*/);
        }
		public static IEnumerable<string> GetPropertiesNames(Type type)
		{
			return GetPropertiesNames(GetProperties(type));
        }

		public static object? GetPropertyValue(PropertyInfo property, object obj)
		{
			return property.GetValue(obj);
		}

		public static bool ModelPropertyComparer(PropertyInfo property, string columnName)
		{
			return property.Name.ToUpperInvariant() == columnName.ToUpperInvariant();
		}
    }
}
