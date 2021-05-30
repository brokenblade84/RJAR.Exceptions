using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace RJAR.Exceptions.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class SerializationHelper
    {
        /// <summary>
        /// Default settings for serialization.
        /// </summary>
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DefaultValueHandling = DefaultValueHandling.Include,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        /// <summary>
        /// Extension to return the serialization configuration.
        /// </summary>
        /// <returns>JsonSerializerSettings</returns>
        public static JsonSerializerSettings GetSerializationSettings() =>
            _jsonSerializerSettings;
    }
}
