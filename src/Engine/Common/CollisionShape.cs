﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusee.Engine
{
    public abstract class CollisionShape
    {
        internal ICollisionShapeImp ICollisionShapeImp;

        public abstract float Margin { get; set; }
    }
}
