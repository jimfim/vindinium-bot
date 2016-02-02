using System.Runtime.Serialization;

namespace vindinium.Infrastructure.DTOs
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
