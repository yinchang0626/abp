﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp.Json.Microsoft.JsonConverters;
using Volo.Abp.ObjectExtending;

namespace Volo.Abp.EntityFrameworkCore.ValueConverters
{
    public class ExtraPropertiesValueConverter : ValueConverter<Dictionary<string, object>, string>
    {
        public ExtraPropertiesValueConverter(Type entityType)
            : base(
                d => SerializeObject(d, entityType),
                s => DeserializeObject(s, entityType))
        {

        }

        private static string SerializeObject(Dictionary<string, object> extraProperties, Type entityType)
        {
            var copyDictionary = new Dictionary<string, object>(extraProperties);

            if (entityType != null)
            {
                var objectExtension = ObjectExtensionManager.Instance.GetOrNull(entityType);
                if (objectExtension != null)
                {
                    foreach (var property in objectExtension.GetProperties())
                    {
                        if (property.IsMappedToFieldForEfCore())
                        {
                            copyDictionary.Remove(property.Name);
                        }
                    }
                }
            }

            return JsonSerializer.Serialize(copyDictionary);
        }

        private static Dictionary<string, object> DeserializeObject(string extraPropertiesAsJson, Type entityType)
        {
            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new ObjectToInferredTypesConverter());
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(extraPropertiesAsJson, deserializeOptions) ?? new Dictionary<string, object>();
            if (entityType != null)
            {
                var objectExtension = ObjectExtensionManager.Instance.GetOrNull(entityType);
                if (objectExtension != null)
                {
                    foreach (var property in objectExtension.GetProperties())
                    {
                        dictionary[property.Name] = GetNormalizedValue(dictionary, property);
                    }
                }
            }

            return dictionary;
        }

        private static object GetNormalizedValue(
            Dictionary<string, object> dictionary,
            ObjectExtensionPropertyInfo property)
        {
            var value = dictionary.GetOrDefault(property.Name);
            if (value == null)
            {
                return property.GetDefaultValue();
            }

            try
            {
                if (property.Type.IsEnum)
                {
                    return Enum.Parse(property.Type, value.ToString(), true);
                }

                //return Convert.ChangeType(value, property.Type);
                return value;
            }
            catch
            {
                return value;
            }
        }
    }
}
