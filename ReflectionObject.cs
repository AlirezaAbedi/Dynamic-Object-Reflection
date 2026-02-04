using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompareTwoClassObjects
{
    public enum CompareResult
    {
        NewItem,
        UpdatedItem
    }

    public static class ReflectionObject
    {

    public static Dictionary<CompareResult, List<T>> CompareByPropertyValue<T>(
        List<T> source,
        List<T> destination,
        string propertyName
    ) where T : class
    {
        var result = new Dictionary<CompareResult, List<T>>
        {
            [CompareResult.NewItem] = new List<T>(),
            [CompareResult.UpdatedItem] = new List<T>()
        };

        var property = typeof(T).GetProperty(
            propertyName,
            BindingFlags.Public | BindingFlags.Instance
        );

        if (property == null)
            throw new ArgumentException(
                $"Property '{propertyName}' not found on type {typeof(T).Name}");

            var properties = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead)
            .ToArray();

            foreach (var src in source)
            {
                var srcValue = property.GetValue(src);

                bool existsInDestination = destination.Any(dest =>
                    Equals(property.GetValue(dest), srcValue));

                    if (!existsInDestination)
                    {
                        result[CompareResult.NewItem].Add(src);
                    }
                    else
                    {
                        bool exists = destination.Any(dest =>
                            PropertiesEqual(src, dest, properties));

                        if (!exists)
                            result[CompareResult.UpdatedItem].Add(src);
                    }
            }

            return result;
        }
        private static bool PropertiesEqual<T>(T a, T b, PropertyInfo[] properties)
        {
            foreach (var prop in properties)
            {
                if (!Equals(prop.GetValue(a), prop.GetValue(b)))
                    return false;
            }
            return true;
        }

    }
}
