using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aktitic.HrProject.BL.Managers;

public static class EntitySizeCalculator
 {
     
         // Dictionary to map types to their corresponding sizes
         private static readonly Dictionary<Type, int> TypeSizeMap = new Dictionary<Type, int>
         {
             { typeof(int), sizeof(int) },
             { typeof(long), sizeof(long) },
             { typeof(bool), sizeof(bool) },
             { typeof(double), sizeof(double) },
             { typeof(float), sizeof(float) },
             { typeof(char), sizeof(char) },
             { typeof(short), sizeof(short) },
             { typeof(byte), sizeof(byte) },
             { typeof(decimal), sizeof(decimal) },
             // Add more types as needed
         };

         public static long GetEstimatedSize<T>(this T entity) where T : class
         {
             long size = 0;

             // Iterate through all the properties of the entity
             foreach (var property in typeof(T).GetProperties())
             {
                 var propertyType = property.PropertyType;
                 var value = property.GetValue(entity);

                 if (TypeSizeMap.ContainsKey(propertyType))
                 {
                     // Add size based on the type
                     size += TypeSizeMap[propertyType];
                 }
                 else if (propertyType == typeof(string))
                 {
                     // For strings, estimate size based on the length (2 bytes per character)
                     var stringValue = value as string;
                     size += string.IsNullOrEmpty(stringValue) ? 0 : stringValue.Length * sizeof(char);
                 }
                 else if (propertyType.IsEnum)
                 {
                     // Estimate size for enums as their underlying type (usually int)
                     size += sizeof(int);
                 }
                 else if (propertyType.IsClass && value != null)
                 {
                     // For complex types, recursively calculate the size
                     size += GetEstimatedSize(value);
                 }
             }

             return size;
         }
 }
    
