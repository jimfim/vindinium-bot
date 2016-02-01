using System.Runtime.Serialization;

namespace vindinium.Models.DTOs
{
  [DataContract]
  public class Hero
  {
    [DataMember]
    internal int id;

    [DataMember]
    internal string name;

    [DataMember]
    internal int elo;

    [DataMember]
    internal Pos pos;

    [DataMember]
    internal int life;

    [DataMember]
    internal int gold;

    [DataMember]
    internal int mineCount;

    [DataMember]
    internal Pos spawnPos;

    [DataMember]
    internal bool crashed;
  }
}