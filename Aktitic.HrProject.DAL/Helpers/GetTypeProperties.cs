namespace Aktitic.HrProject.DAL.Helpers;

public static class GetTypeProperties
{
    public static string GetPropertyValue<T>(this T obj, string propertyName) where T : class
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return string.Empty;
        }

        // Capitalize the first letter if it's lowercase
        if (char.IsLower(propertyName[0]))
        {
            propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1);
        }

        // Retrieve property info
        var propertyInfo = typeof(T).GetProperty(propertyName);
        if (propertyInfo != null)
        {
            var value = propertyInfo.GetValue(obj)?.ToString();
            return value ?? string.Empty;
        }

        return string.Empty;
    }
}