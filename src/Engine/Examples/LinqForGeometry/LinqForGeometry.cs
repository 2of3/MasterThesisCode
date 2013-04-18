﻿using Fusee.Engine;
using Fusee.Math;

using hsfurtwangen.dsteffen.lfg;
using Geometry = hsfurtwangen.dsteffen.lfg.Geometry;

namespace Examples.LinqForGeometry
{
    public class LinqForGeometry : RenderCanvas
    {
        #region Shader
        // pixel and vertex shader
        protected string _vs = @"
            #ifndef GL_ES
               #version 120
            #endif

            /* Copies incoming vertex color without change.
             * Applies the transformation matrix to vertex position.
             */

            attribute vec4 fuColor;
            attribute vec3 fuVertex;
            attribute vec3 fuNormal;
            attribute vec2 fuUV;
            
        
            varying vec4 vColor;
            varying vec3 vNormal;
            varying vec2 vUV;
        
            uniform mat4 FUSEE_MVP;
            uniform mat4 FUSEE_ITMV;

            void main()
            {
                gl_Position = FUSEE_MVP * vec4(fuVertex, 1.0);
                // vColor = vec4(fuNormal * 0.5 + 0.5, 1.0);
                // vec4 norm4 = FUSEE_MVP * vec4(fuNormal, 0.0);
                // vNormal = norm4.xyz;
                vNormal = mat3(FUSEE_ITMV) * fuNormal;
                vUV = fuUV;
            }";

        protected string _ps = @"
           #ifndef GL_ES
              #version 120
           #endif

            /* Copies incoming fragment color without change. */
            #ifdef GL_ES
                precision highp float;
            #endif

         
            uniform sampler2D texture1;
            uniform sampler2D texture2;
            uniform vec4 vColor;
            varying vec3 vNormal;
            varying vec2 vUV;

            void main()
            {     
                vec4 tex1 = texture2D(texture1,vUV);
                vec4 tex2 = texture2D(texture2,vUV);
                //gl_FragColor = texture2D(texture1, vUV);        
                gl_FragColor = mix(tex1, tex2, 0.6);  /* *dot(vNormal, vec3(0, 0, 1))*/;
                //gl_FragColor = vColor;
            }";

        #endregion Shader

        // angle variables
        private static float _angleHorz, _angleVert, _angleVelHorz, _angleVelVert;

        private const float RotationSpeed = 1f;
        private const float Damping = 0.92f;

        // model variable
        private Mesh _lfgmesh;

        // variables for color and texture
        private IShaderParam _vColorParam;
        private IShaderParam _vTextureParam;

        private ImageData _imgData;
        private ITexture _tex;

        // LFG Geo
        private Geometry _Geo;

        // 'Gamedesign'
        private float _MovementSpeed = 10.0f;

        public override void Init()
        {
            // initialize the variables
            //_lfgmesh = MeshReader.LoadMesh("Assets/cube_quadrangle_1_textured.obj.model");
            //_lfgmesh = MeshReader.LoadMesh("Assets/sphere_quadrangle_1.obj.model");
            //_lfgmesh = MeshReader.LoadMesh("Assets/Teapot.obj.model");
            //_lfgmesh = MeshReader.LoadMesh("Assets/Teapot_textured.obj.model");


            // This would be a solution to step over the MeshReader Class
            // TODO: Important for now to use the transformation methods.
            _Geo = new Geometry();
            _Geo.LoadAsset("Assets/cube_quadrangle_1_textured.obj.model");
            //_Geo.LoadAsset("Assets/sphere_quadrangle_1.obj.model");
            //_Geo.LoadAsset("Assets/Teapot_textured.obj.model");
            _lfgmesh = _Geo.ToMesh();


            ShaderProgram sp = RC.CreateShader(_vs, _ps);
            RC.SetShader(sp);

            //_vColorParam = sp.GetShaderParam("vColor");
            // Texture stuff
            _vTextureParam = sp.GetShaderParam("texture1");
            //_imgData = RC.LoadImage("Assets/world_map.jpg");
            _imgData = RC.LoadImage("Assets/checkerboard_tex.jpg");
            _tex = RC.CreateTexture(_imgData);


            RC.ClearColor = new float4(1, 1, 1, 0.8f);
        }

        public override void RenderAFrame()
        {
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Pull the users input
            PullInput();

            // A crash happens here with the color ... (_vColorParam is null)
            //RC.SetShaderParam(_vColorParam, new float4(0.5f, 0.8f, 0, 1));
            RC.SetShaderParamTexture(_vTextureParam, _tex);
            RC.Render(_lfgmesh);

            // swap buffers
            Present();
        }

        // Pull the users input
        public void PullInput()
        {
            #region Mouse
            if (Input.Instance.IsButtonDown(MouseButtons.Left))
            {
                _angleVelHorz = RotationSpeed * Input.Instance.GetAxis(InputAxis.MouseX);
                _angleVelVert = RotationSpeed * Input.Instance.GetAxis(InputAxis.MouseY);
            }
            else
            {
                var curDamp = (float)System.Math.Exp(-Damping * Time.Instance.DeltaTime);

                _angleVelHorz *= curDamp;
                _angleVelVert *= curDamp;
            }

            // Scale the model up with the middle mouse button
            if (Input.Instance.IsButtonDown(MouseButtons.Middle))
            {
                if (_Geo.Scale(1.1f, 1.1f, 1.1f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }

            // Reset the model with the right mouse button
            if (Input.Instance.IsButtonDown(MouseButtons.Right))
            {
                if (_Geo.ResetGeometryToDefault())
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            #endregion Mouse

            #region Scaling
            if (Input.Instance.IsKeyDown(KeyCodes.Q))
            {
                if (_Geo.Scale(1.1f, 1.1f, 1.1f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            else if (Input.Instance.IsKeyDown(KeyCodes.A))
            {
                if (_Geo.Scale(0.9f, 0.9f, 0.9f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            // Scale only x direction
            if (Input.Instance.IsKeyDown(KeyCodes.W))
            {
                if (_Geo.Scale(1.1f, 1.0f, 1.0f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            else if (Input.Instance.IsKeyDown(KeyCodes.S))
            {
                if (_Geo.Scale(0.9f, 1.0f, 1.0f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }

            // Scale only y direction
            if (Input.Instance.IsKeyDown(KeyCodes.E))
            {
                if (_Geo.Scale(1.0f, 1.1f, 1.0f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            else if (Input.Instance.IsKeyDown(KeyCodes.D))
            {
                if (_Geo.Scale(1.0f, 0.9f, 1.0f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }

            // Scale only z direction
            if (Input.Instance.IsKeyDown(KeyCodes.R))
            {
                if (_Geo.Scale(1.0f, 1.0f, 1.1f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            else if (Input.Instance.IsKeyDown(KeyCodes.F))
            {
                if (_Geo.Scale(1.0f, 1.0f, 0.9f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }

            if (Input.Instance.IsKeyDown(KeyCodes.T))
            {
                if (_Geo.ResetGeometryToDefault())
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            #endregion Scaling

            #region Translation
            if (Input.Instance.IsKeyDown(KeyCodes.U))
            {
                if (_Geo.Translate(0f, (_MovementSpeed * (float)Time.Instance.DeltaTime) * 2, 0f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            else if (Input.Instance.IsKeyDown(KeyCodes.J))
            {
                if (_Geo.Translate(0f, (-_MovementSpeed * (float)Time.Instance.DeltaTime) * 2, 0f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            if (Input.Instance.IsKeyDown(KeyCodes.H))
            {
                if (_Geo.Translate(-(_MovementSpeed * (float)Time.Instance.DeltaTime) * 2, 0f, 0f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            else if (Input.Instance.IsKeyDown(KeyCodes.K))
            {
                if (_Geo.Translate((_MovementSpeed * (float)Time.Instance.DeltaTime) * 2, 0f, 0f))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            if (Input.Instance.IsKeyDown(KeyCodes.Z))
            {
                if (_Geo.Translate(0f, 0f, -(_MovementSpeed * (float)Time.Instance.DeltaTime) * 2))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            else if (Input.Instance.IsKeyDown(KeyCodes.I))
            {
                if (_Geo.Translate(0f, 0f, (_MovementSpeed * (float)Time.Instance.DeltaTime) * 2))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            #endregion Translation

            #region Rotation
            if (Input.Instance.IsKeyDown(KeyCodes.Up))
            {
                if (_Geo.RotateX(1f * (float)Time.Instance.DeltaTime))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            else if (Input.Instance.IsKeyDown(KeyCodes.Down))
            {
                if (_Geo.RotateX(-1f * (float)Time.Instance.DeltaTime))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }

            if (Input.Instance.IsKeyDown(KeyCodes.Left))
            {
                if (_Geo.RotateY(1f * (float)Time.Instance.DeltaTime))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            else if (Input.Instance.IsKeyDown(KeyCodes.Right))
            {
                if (_Geo.RotateY(-1f * (float)Time.Instance.DeltaTime))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }

            if (Input.Instance.IsKeyDown(KeyCodes.O))
            {
                if (_Geo.RotateZ(1f * (float)Time.Instance.DeltaTime))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            else if (Input.Instance.IsKeyDown(KeyCodes.P))
            {
                if (_Geo.RotateZ(-1f * (float)Time.Instance.DeltaTime))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            #endregion Rotation

            if (Input.Instance.IsKeyDown(KeyCodes.NumPad8))
            {
                if (_Geo.RotateLocalX(_MovementSpeed * (float)Time.Instance.DeltaTime))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }
            else if (Input.Instance.IsKeyDown(KeyCodes.NumPad2))
            {
                if (_Geo.RotateLocalX(-_MovementSpeed * (float)Time.Instance.DeltaTime))
                {
                    _lfgmesh = _Geo.ToMesh();
                }
            }

            var mtxRot = float4x4.CreateRotationY(_angleHorz) * float4x4.CreateRotationX(_angleVert);
            _angleHorz += _angleVelHorz;
            _angleVert += _angleVelVert;

            var mtxCam = float4x4.LookAt(0, 200, 400, 0, 0, 0, 0, 1, 0);

            // first mesh
            RC.ModelView = mtxRot * float4x4.CreateTranslation(0, 0, 0) * mtxCam;
            //RC.ModelView = float4x4.CreateTranslation(0, 0, 0) * mtxCam;
        }

        public override void Resize()
        {
            RC.Viewport(0, 0, Width, Height);

            var aspectRatio = Width / (float)Height;
            RC.Projection = float4x4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1, 5000);
        }

        public static void Main()
        {
            var app = new LinqForGeometry();
            app.Run();
        }

    }
}