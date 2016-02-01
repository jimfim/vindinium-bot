using System.Runtime.Serialization;

namespace vindinium.Models.DTOs
{
  [DataContract]
  class GameResponse
  {
    [DataMember]
    internal Game game;

    [DataMember]
    internal Hero hero;

    [DataMember]
    internal string token;

    [DataMember]
    internal string viewUrl;

    [DataMember]
    internal string playUrl;
  }
}