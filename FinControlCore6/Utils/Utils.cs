using System.Reflection;

namespace FinControlCore6.Utils
{
	public class Utils
	{
		public static IEnumerable<PropertyInfo> GetProperties(Type type)
		{
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
		}
		public static IEnumerable<string> GetPropertiesNames(Type type)
		{
			return GetPropertiesNames(GetProperties(type));
        }
        public static IEnumerable<string> GetPropertiesNames(IEnumerable<PropertyInfo> properties)
        {
            return properties.Select(p => (p.Name[0].ToString().ToLower()+p.Name.Substring(1)));
        }

		public static object? GetPropertyValue(PropertyInfo property, object obj)
		{
			return property.GetValue(obj);
		}
    }
}
