using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Dacodelaac.DebugUtils;
using UnityEngine;

namespace Dacodelaac.DataStorage
{
    [JsonConverter(typeof(Dictionary<string, object>))]
    public class DictionaryStringObjectConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Dictionary<string, object>);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer
        )
        {
            var dictionaryTypes = objectType.GetGenericArguments();
            var isStringKey = dictionaryTypes[0] == typeof(string);

            var result = new Dictionary<string, object>();
            object key = null;
            object value = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject) break;
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    key = isStringKey
                        ? reader.Value
                        : serializer.Deserialize(reader, dictionaryTypes[0]);
                }
                else
                {
                    value = serializer.Deserialize(reader, dictionaryTypes[1]);
                    result.Add(key.ToString(), value);
                    key = null;
                    value = null;
                }
            }

            var resultDict = new Dictionary<string, object>();
            foreach (var item in result)
            {
                var itemValue =
                    JsonConvert
                        .DeserializeObject<Dictionary<string, string>>(item.Value.ToString());
                var type = Type.GetType(itemValue["Type"]);
                itemValue["Value"] = itemValue["Value"].Replace(',', '.');
                if (type != null && type != typeof(Dacodelaac.DataType.ShortDouble))
                {
                    var converted = Convert.ChangeType(itemValue["Value"], type, CultureInfo.InvariantCulture);
                    resultDict.Add(item.Key, converted);
                    continue;
                }

                if (double.TryParse(itemValue["Value"], NumberStyles.Any, CultureInfo.InvariantCulture, out var dValue))
                {
                    resultDict.Add(item.Key, new Dacodelaac.DataType.ShortDouble(dValue));
                    continue;
                }

                Dacoder.LogErrorFormat("Error try parse - {0} - {1}", item.Key, itemValue["Value"]);
            }

            return resultDict;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value.GetType();
            var keys = (IEnumerable)type.GetProperty("Keys").GetValue(value, null);
            var values = (IEnumerable)type.GetProperty("Values").GetValue(value, null);
            var valueEnumerator = values.GetEnumerator();

            writer.WriteStartObject();

            foreach (var key in keys)
            {
                valueEnumerator.MoveNext();
                var valueCurrent = valueEnumerator.Current;

                writer.WritePropertyName(key.ToString());
                writer.WriteStartObject();
                writer.WritePropertyName("Type");
                writer.WriteValue(valueCurrent.GetType().ToString());
                writer.WritePropertyName("Value");
                var valueString = valueCurrent.ToString();
                var typeCurrent = valueCurrent.GetType();
                if (typeCurrent == typeof(Dacodelaac.DataType.ShortDouble))
                {
                    var shortDoubleValue = ((Dacodelaac.DataType.ShortDouble)valueCurrent);
                    valueString = shortDoubleValue.Value.ToString(CultureInfo.InvariantCulture);
                }
                else if (
                    typeCurrent.IsEnum
                    && Enum.TryParse(valueCurrent.GetType(), valueCurrent.ToString(), false, out var enumVal)
                )
                {
                    var enumValue = Convert.ToInt32(enumVal, CultureInfo.InvariantCulture);
                    valueString = enumValue.ToString(CultureInfo.InvariantCulture);
                }

                valueString = valueString.Replace(',', '.');
                writer.WriteValue(valueString);
                writer.WriteEndObject();
            }

            writer.WriteEndObject();
        }
    }
}