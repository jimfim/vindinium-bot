using System.Collections.Generic;
using System.Runtime.Serialization;

namespace vindinium.Models.DTOs
{
  [DataContract]
  class Game
  {
    [DataMember]
    internal string id;

    [DataMember]
    internal int turn;

    [DataMember]
    internal int maxTurns;

    [DataMember]
    internal List<Hero> heroes;

    [DataMember]
    internal Board board;

    [DataMember]
    internal bool finished;
  }
}