using System.Runtime.Serialization;

namespace Client.Parameters
{
    [DataContract]
    public class ReadParameters
    {
        [DataMember]
        public string Key { get; set; }
    }
}