using System.Diagnostics.CodeAnalysis;

namespace FinControlCore6.Utils
{
    public class PropertyToColumnComparer : IEqualityComparer<string>
    {
        public bool Equals(string? propertyStr, string? column)
        {
            if (propertyStr ==null && column==null)
                return true;
            else if (propertyStr == null || column == null)
                return false;
            return propertyStr.ToUpperInvariant() == column.ToUpperInvariant();
        }

        public int GetHashCode([DisallowNull] string obj)
        {
            return obj.ToUpperInvariant().GetHashCode();
        }
    }
}
