﻿using System.Runtime.Serialization;

namespace vindinium.Models.DTOs
{
  [DataContract]
  internal class Pos
  {
    [DataMember]
    internal int x;

    [DataMember]
    internal int y;
  }
}