using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RJAR.Exceptions.Helpers
{
    public static class SerializationHelper
    {
        private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DefaultValueHandling = DefaultValueHandling.Include,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static JsonSerializerSettings GetSerializationSettings() =>
            _jsonSerializerSettings;
    }
}
