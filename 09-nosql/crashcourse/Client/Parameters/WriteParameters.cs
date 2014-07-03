using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Client.Parameters
{
    [DataContract]
    public class Data : IComparable<Data>
    {
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public long Version { get; set; }

        public int CompareTo(Data other)
        {
            return Comparer<long>.Default.Compare(Version, other.Version);
        }
    }

    [DataContract]
    public class WriteParameters
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public Data Value { get; set; }
    }
}