using System.Runtime.Serialization;

namespace Project.Kafka.Common.Exceptions
{
    [Serializable]
    public class ConfigurationMissingException : Exception
    {
        public ConfigurationMissingException(string sectionName)
            : base($"The configuration section '{sectionName}' is missing or invalid.")
        {
        }

        protected ConfigurationMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
