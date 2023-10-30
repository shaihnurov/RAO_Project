﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;

namespace Models.JSON.TopTableData;

[JsonConverter(typeof(ExecutorDataConverter))]
public class ExecutorData
{
    [JsonProperty("email")]
    public string ExecEmail { get; set; }

    [JsonProperty("phone")]
    public string ExecPhone { get; set; }

    [JsonProperty("position")]
    public string GradeExecutor { get; set; }

    [JsonProperty("full_name")]
    public string FIOexecutor { get; set; }

    #region ContractResolver

    private class ExecutorDataConverterSpecifiedConcreteClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(ExecutorData).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
            return base.ResolveContractConverter(objectType);
        }
    }

    #endregion

    #region TableDataConverter

    private class ExecutorDataConverter : JsonConverter
    {
        private static readonly JsonSerializerSettings SpecifiedSubclassConversion = new()
        {
            ContractResolver = new ExecutorDataConverterSpecifiedConcreteClassConverter()
        };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(JsonForm));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);

            return objectType.Name switch
            {
                "ExecutorData" => JsonConvert.DeserializeObject<ExecutorData>(jo.ToString(), SpecifiedSubclassConversion),
                "ExecutorData28" => JsonConvert.DeserializeObject<ExecutorData28>(jo.ToString(), SpecifiedSubclassConversion),
                _ => null
            };
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }
    }

    #endregion
}