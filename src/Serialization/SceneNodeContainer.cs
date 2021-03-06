﻿using System.Collections.Generic;
using ProtoBuf;

namespace Fusee.Serialization
{
    [ProtoContract]
    public class SceneNodeContainer
    {
        [ProtoMember(1)]
        public string Name;

        [ProtoMember(2, AsReference = true)]
        public List<SceneComponentContainer> Components;

        [ProtoMember(3, AsReference = true)]
        public List<SceneNodeContainer> Children;
     }
}
