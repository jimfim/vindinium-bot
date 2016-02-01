using System.Runtime.Serialization;

namespace vindinium.Models.DTOs
{
  [DataContract]
    class Board
    {
        [DataMember]
        internal int size;

        [DataMember]
        internal string tiles;
    }
}
