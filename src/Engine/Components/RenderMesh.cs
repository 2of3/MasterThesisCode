﻿using System;
using System.Collections.Generic;
using System.Text;
using Fusee.Engine;

namespace Fusee.SceneManagement
{
    public class RenderMesh : RenderJob
    {
        private Mesh _mesh;

        public RenderMesh(Mesh mesh)
        {
            _mesh = mesh;
        }
        public override Mesh GetMesh()
        {
            return _mesh;
        }
    }
}
