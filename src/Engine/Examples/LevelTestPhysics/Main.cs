using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Fusee.Engine;
using Fusee.Engine.SimpleScene;
using Fusee.Math;
using Fusee.Serialization;

namespace Examples.LevelTestPhysics
{
    public class LevelTestPhysics : RenderCanvas
    {
        // angle variables
        private static float _angleHorz, _angleVert, _angleVelHorz, _angleVelVert;

        private const float RotationSpeed = 1f;
        private const float Damping = 0.92f;

        // model variables
        private Mesh _Rechteck;

        private SceneRenderer _srSky;
        private SceneContainer _sceneSky;

        private SceneRenderer _srLevel1;
        private SceneContainer _sceneLevel1;

        private SceneRenderer _srDeko;
        private SceneContainer _sceneDeko;

        private SceneRenderer _srFire;
        private SceneContainer _sceneElement1;

        private SceneRenderer _srWater;
        private SceneContainer _sceneWater;

        private SceneRenderer _srAir;
        private SceneContainer _sceneAir;

        private SceneRenderer _srEarth;
        private SceneContainer _sceneEarth;

       // variables for shader
        private ShaderProgram _spTexture;
        private ShaderProgram _spColor;

        private IShaderParam _colorParam;
        private IShaderParam _textureParam;

        private ITexture _iTex;

        //Physic
        private Physic _physic;
        private Mesh _meshWater;
        private Mesh _meshBall;
        float3 _impulse;

        private int _aa;
        private int _bb;
        private int _cc = 300;
        private int _dd;
        private int _ee = 600;
        private int _ff;


        private float3 averageNewPos;

        // is called on startup
        public override void Init()
        {
            RC.ClearColor = new float4(0.1f, 0.1f, 0.5f, 1);
            _Rechteck = MeshReader.LoadMesh(@"Assets/Rechteck.obj.model");


            //Scene Skybox
            var serSky = new Serializer();
            using (var file = File.OpenRead(@"Assets/Skybox1.fus"))
            {
                _sceneSky = serSky.Deserialize(file, null, typeof(SceneContainer)) as SceneContainer;
            }

            _srSky = new SceneRenderer(_sceneSky, "Assets");

            //Scene Level1
            var serLevel1 = new Serializer();
            using (var file = File.OpenRead(@"Assets/Island_split.fus"))
            {
                _sceneLevel1 = serLevel1.Deserialize(file, null, typeof(SceneContainer)) as SceneContainer;
            }

            _srLevel1 = new SceneRenderer(_sceneLevel1, "Assets");


            var serDeko = new Serializer();
            using (var file = File.OpenRead(@"Assets/assets_split.fus"))
            {
                _sceneDeko = serDeko.Deserialize(file, null, typeof(SceneContainer)) as SceneContainer;
            }

            _srDeko = new SceneRenderer(_sceneDeko, "Assets");

            //Scene Spheres
            //Fire
            var serFire = new Serializer();
            using (var file = File.OpenRead(@"Assets/player_fire.fus"))
            {
                _sceneElement1 = serFire.Deserialize(file, null, typeof (SceneContainer)) as SceneContainer;
            }
            _srFire = new SceneRenderer(_sceneElement1, "Assets");

            //Water
            /*
            var serWater = new Serializer();
            using (var file = File.OpenRead(@"Assets/Player_water.fus"))
            {
                var sceneWater = serWater.Deserialize(file, null, typeof(SceneContainer)) as SceneContainer;
                MeshComponent mc = _sceneWater.Children.FindComponents<MeshComponent>(comp => true).First();
                _meshWater = SceneRenderer.MakeMesh(mc);
            }
            //_srWater = new SceneRenderer(_sceneWater, "Assets");
            */
            var ser = new Serializer();
            using (var file = File.OpenRead(@"Assets/Sphere.fus"))
            {
                var scene = ser.Deserialize(file, null, typeof(SceneContainer)) as SceneContainer;
                MeshComponent mc = scene.Children.FindComponents<MeshComponent>(comp => true).First();
                _meshBall = SceneRenderer.MakeMesh(mc);
            }

            //Earth
            var serEarth = new Serializer();
            using (var file = File.OpenRead(@"Assets/player_earth.fus"))
            {
                _sceneEarth = serEarth.Deserialize(file, null, typeof(SceneContainer)) as SceneContainer;
            }
            _srEarth = new SceneRenderer(_sceneEarth, "Assets");

            //Air
            var serAir = new Serializer();
            using (var file = File.OpenRead(@"Assets/Player_Air.fus"))
            {
                _sceneAir = serAir.Deserialize(file, null, typeof(SceneContainer)) as SceneContainer;
            }
            _srAir = new SceneRenderer(_sceneAir, "Assets");


            _spColor = MoreShaders.GetDiffuseColorShader(RC);
            
            _colorParam = _spColor.GetShaderParam("color");
            
            _spTexture = MoreShaders.GetTextureShader(RC);

            _textureParam = _spTexture.GetShaderParam("texture1");




            //Central Position of all Players
            var averageNewPos = new float3(0, 0, 0);

            _physic = new Physic();

            _physic.InitScene();

        }

        // is called once a frame
        public override void RenderAFrame()
        {
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            //Physics
            RigidBody rbSphere = _physic.GetWaterSphere();
            //Debug.WriteLine("POSITION START " + rbSphere.Position);

           // _physic.World.StepSimulation((float)Time.Instance.DeltaTime, (Time.Instance.FramePerSecondSmooth / 60), 1 / 60);//???
           
            //Array for Players Position 
            float3[] playerPos = new float3[3];

            //Array for new Players Position
            float3[] newPlayerPos = new float3[3];

            //Array for input
            float3[] move = new float3[3];

            

            //Camera Minimum and Maximum
            var camMin = new float3(0, 0, 0);
            var camMax = new float3(0, 0, 0);

            playerPos[0].x = _aa;
            playerPos[0].y = 0;
            playerPos[0].z = _bb;

            playerPos[1].x = _cc;
            playerPos[1].y = 0;
            playerPos[1].z = _dd;

            playerPos[2].x = _ee;
            playerPos[2].y = 0;
            playerPos[2].z = _ff;

            var inputA = 0;
            var inputB = 0;
            var inputC = 0;
            var inputD = 0;
            var inputE = 0;
            var inputF = 0;

            // move per mouse
            if (Input.Instance.IsButton(MouseButtons.Left))
            {
                _angleVelHorz = -RotationSpeed * Input.Instance.GetAxis(InputAxis.MouseX);
                _angleVelVert = -RotationSpeed * Input.Instance.GetAxis(InputAxis.MouseY);
            }
            else
            {
                var curDamp = (float)Math.Exp(-Damping * Time.Instance.DeltaTime);

                _angleVelHorz *= curDamp;
                _angleVelVert *= curDamp;
            }

            _angleHorz += _angleVelHorz;
            _angleVert += _angleVelVert;


            
            if (averageNewPos.x > -2000) { //GAMEMODE 0
            // move per keyboard (arrow keys) in gamemode 0
            if (Input.Instance.IsKeyUp(KeyCodes.Left))
            {
                inputA = 0;// 30;
                _aa -= (int)inputA;
                _impulse = new float3(-30, 0, 0);
                rbSphere.ApplyCentralImpulse = new float3(-30, 0, 0);
            }
            if (Input.Instance.IsKeyUp(KeyCodes.Right))
            {
                inputA = 0;//30;
                _aa += (int)inputA;
                _impulse = new float3(30, 0, 0);
            }
            if (Input.Instance.IsKeyUp(KeyCodes.Up))
            {
                inputB = 0;// 30;
                _bb += (int)inputB;
                _impulse = new float3(0, 0, 30);
            }
            if (Input.Instance.IsKeyUp(KeyCodes.Down))
            {
                inputB = 0;// 30;
                _bb -= (int)inputB;
                _impulse = new float3(0, 0, -30);
            }

            // move per keyboard (W A S D) in gamemode 0

            if (Input.Instance.IsKey(KeyCodes.A))
            {
                inputC = 30;
                _cc -= (int)inputC;
            }


            if (Input.Instance.IsKey(KeyCodes.D))
            {
                inputC = 30;
                _cc += (int)inputC;
            }


            if (Input.Instance.IsKey(KeyCodes.W))
            {
                inputD = 30;
                _dd += (int)inputD;
                
            }


            if (Input.Instance.IsKey(KeyCodes.S))
            {
                inputD = 30;
                _dd -= (int)inputD;

            }


            //move per keybord U H J K in gamemode 0

            if (Input.Instance.IsKey(KeyCodes.H))
            {

                inputE = 30;
                _ee -= (int)inputE;
            }
            if (Input.Instance.IsKey(KeyCodes.K))
            {
                inputE = 30;
                _ee += (int)inputE;
            }


            if (Input.Instance.IsKey(KeyCodes.U))
            {
                inputF = 30;
                _ff += (int)inputF;
            }
            if (Input.Instance.IsKey(KeyCodes.J))
            {
                inputF = 30;
                _ff -= (int)inputF;

            }
            move[0].x = inputA;
            move[0].z = inputB;
            move[1].x = inputC;
            move[1].z = inputD;
            move[2].x = inputE;
            move[2].z = inputF;
            //Console.WriteLine( "Bin im Gamemode 0");
            averageNewPos = new float3(0, 0, 0); 
            
            for (int i = 0; i < playerPos.Length; i++)
            {
                newPlayerPos[i].x = playerPos[i].x + move[i].x;
                newPlayerPos[i].z = playerPos[i].z + move[i].z;
                averageNewPos += newPlayerPos[i];

                //Console.WriteLine("PLAYERPOS#######################" + playerPos[i]);
            }

            averageNewPos *= (float)(1.0 / playerPos.Length);
           //  Console.WriteLine(averageNewPos);

            camMin = new float3(averageNewPos.x - 750, 0, averageNewPos.z - 550);
            camMax = new float3(averageNewPos.x + 750, 0, averageNewPos.z + 950);

            var mtxRot = float4x4.CreateRotationX(_angleVert) * float4x4.CreateRotationY(_angleHorz);
            var mtxCam = float4x4.LookAt(averageNewPos.x, 400, averageNewPos.z - 2500, averageNewPos.x, 0, averageNewPos.z, 0, 1, 0);

            for (int i = 0; i < playerPos.Length; i++)
            {
                if (newPlayerPos[i].x <= camMin.x)
                {
                    playerPos[i].x = camMin.x;

                }
                else
                {
                    if (newPlayerPos[i].x >= camMax.x)
                    {
                        playerPos[i].x = camMax.x;

                    }
                    else
                    {
                        playerPos[i].x = newPlayerPos[i].x;
                        if (i == 0)
                        {
                            rbSphere.ApplyCentralImpulse = _impulse;
                        }
                        
                    }
                }

                if (newPlayerPos[i].z <= camMin.z)
                {
                    playerPos[i].z = camMin.z;

                }
                else
                {
                    if (newPlayerPos[i].z >= camMax.z)
                    {
                        playerPos[i].z = camMax.z;

                    }
                    else
                    {
                        playerPos[i].z = newPlayerPos[i].z;

                        if (i == 0)
                        {
                            rbSphere.ApplyCentralImpulse = _impulse;
                        }
                    }
                }
            }

            
            RC.SetShader(_spColor);
           // Rechteck Boden
            var mtxR = float4x4.CreateTranslation(averageNewPos.x, -101, averageNewPos.z + 200);
            RC.ModelView = mtxCam * mtxRot * mtxR;
            RC.Render(_Rechteck);

            //Fire
            var mtxM2 = float4x4.CreateTranslation(playerPos[1].x, 0, playerPos[1].z);
            var mtxScalePlayer = float4x4.CreateScale(5);
            RC.ModelView = mtxCam * mtxRot * mtxM2 * mtxScalePlayer;
            _srFire.Render(RC);

            //Water
            //var mtxM1 = float4x4.CreateTranslation(playerPos[0].x, 0, playerPos[0].z);
            if (rbSphere.CollisionShape is SphereShape)
            {
                //RC.ModelView = mtxCam * mtxRot * mtxScalePlayer; //* mtxM1
                Debug.WriteLine("#######rbSphere Position: " + rbSphere.Position);
                var shape = (SphereShape)rbSphere.CollisionShape;
                var pos = float4x4.Transpose(rbSphere.WorldTransform);
                Debug.WriteLine("Wolrdtransform WATER SPHERE " + pos );
                RC.ModelView = mtxCam  * mtxScalePlayer * pos;
                RC.SetRenderState(new RenderStateSet { AlphaBlendEnable = false, ZEnable = true });
               // _srWater.Render(RC);
                RC.Render(_meshBall);
                
            }
            

            //Earth
            var mtxM3 = float4x4.CreateTranslation(playerPos[2].x, 0, playerPos[2].z);
            RC.ModelView = mtxCam * mtxRot * mtxM3 * mtxScalePlayer;
            _srEarth.Render(RC);

            //Air
            var mtxM4 = float4x4.CreateTranslation(300, 0, 300);
            RC.ModelView = mtxCam * mtxRot * mtxM4 * mtxScalePlayer;
            _srAir.Render(RC);
            
            //Skybox
            var mtxScale = float4x4.CreateScale(1.5f);
            RC.ModelView = mtxCam * mtxRot * mtxR * mtxScale;
            _srSky.Render(RC);

            //Level1
            var mtxTranslLevel = float4x4.CreateTranslation(0, -101, 0);
            var mtxScaleLevel = float4x4.CreateScale(0.7f);
            RC.ModelView = mtxCam * mtxRot * mtxTranslLevel * mtxScaleLevel;
            _srLevel1.Render(RC);

            _srDeko.Render(RC);

            }
            else  //JETZT IM GAMEMODE 1
            {
                // move per keyboard (arrow keys) in gamemode 1
                if (Input.Instance.IsKey(KeyCodes.Left))
                {
                    inputB = 30;
                    _bb -= (int)inputB;
                }
                if (Input.Instance.IsKey(KeyCodes.Right))
                {
                    inputB = 30;
                    _bb += (int)inputB;
                }
                if (Input.Instance.IsKey(KeyCodes.Up))
                {
                    inputA = 30;
                    _aa -= (int)inputA;
                }
                if (Input.Instance.IsKey(KeyCodes.Down))
                {
                    inputA = 30;
                    _aa += (int)inputA;
                }

                // move per keyboard (W A S D) in gamemode 1

                if (Input.Instance.IsKey(KeyCodes.A))
                {
                    inputD = 30;
                    _dd -= (int)inputD;
                }


                if (Input.Instance.IsKey(KeyCodes.D))
                {
                    inputD = 30;
                    _dd += (int)inputD;
                }


                if (Input.Instance.IsKey(KeyCodes.W))
                {
                    inputC = 30;
                    _cc -= (int)inputC;

                }


                if (Input.Instance.IsKey(KeyCodes.S))
                {
                    inputC = 30;
                    _cc += (int)inputC;

                }


                //move per keybord U H J K in gamemode 1

                if (Input.Instance.IsKey(KeyCodes.H))
                {

                    inputF = 30;
                    _ff -= (int)inputF;
                }
                if (Input.Instance.IsKey(KeyCodes.K))
                {
                    inputF = 30;
                    _ff += (int)inputF;
                }


                if (Input.Instance.IsKey(KeyCodes.U))
                {
                    inputE = 30;
                    _ee -= (int)inputE;
                }
                if (Input.Instance.IsKey(KeyCodes.J))
                {
                    inputE = 30;
                    _ee += (int)inputE;

                }
                move[0].x = inputA;
                move[0].z = inputB;
                move[1].x = inputC;
                move[1].z = inputD;
                move[2].x = inputE;
                move[2].z = inputF;

                Console.WriteLine("bin im Gamemode 1");
                averageNewPos = new float3(0, 0, 0); 
                for (int i = 0; i < playerPos.Length; i++)
                {
                    newPlayerPos[i].x = playerPos[i].x + move[i].x;
                    newPlayerPos[i].z = playerPos[i].z + move[i].z;
                    averageNewPos += newPlayerPos[i];

                    //  Console.WriteLine(move[i]);
                }

                averageNewPos *= (float)(1.0 / playerPos.Length);
              //  Console.WriteLine(averageNewPos);

                camMin = new float3(averageNewPos.x - 750, 0, averageNewPos.z - 550);
                camMax = new float3(averageNewPos.x + 750, 0, averageNewPos.z + 950);
                _angleHorz = 1.56f;
                _angleVert = -0.445f;
                var mtxRot = float4x4.Identity;
                var mtxCam = float4x4.CreateTranslation(averageNewPos.x, 0, averageNewPos.z) * float4x4.CreateRotationY(-_angleHorz) * float4x4.CreateRotationX(-_angleVert) * float4x4.CreateTranslation(0, 0, -2500);
                mtxCam.Invert();
                Console.WriteLine(_angleVert + " " + _angleHorz);

                // var mtxRot = float4x4.CreateRotationX(_angleVert) * float4x4.CreateRotationY(_angleHorz);
                // var mtxCam = float4x4.LookAt(averageNewPos.x, 400, averageNewPos.z - 2500, averageNewPos.x, 0, averageNewPos.z, 0, 1, 0);
               // Console.WriteLine(newPlayerPos[0]);
                for (int i = 0; i < playerPos.Length; i++)
                {
                    if (newPlayerPos[i].x <= camMin.x)
                    {
                        playerPos[i].x = camMin.x;

                    }
                    else
                    {
                        if (newPlayerPos[i].x >= camMax.x)
                        {
                            playerPos[i].x = camMax.x;

                        }
                        else
                        {
                            playerPos[i].x = newPlayerPos[i].x;
                        }
                    }

                    if (newPlayerPos[i].z <= camMin.z)
                    {
                        playerPos[i].z = camMin.z;

                    }
                    else
                    {
                        if (newPlayerPos[i].z >= camMax.z)
                        {
                            playerPos[i].z = camMax.z;

                        }
                        else
                        {
                            playerPos[i].z = newPlayerPos[i].z;
                        }
                    }
                }

               // Console.WriteLine(averageNewPos + " " +playerPos[0]);
                RC.SetShader(_spColor);
                // Rechteck Boden
                var mtxR = float4x4.CreateTranslation(averageNewPos.x, -101, averageNewPos.z + 200);
                RC.ModelView = mtxCam * mtxRot * mtxR;
                RC.Render(_Rechteck);

                //Fire
                var mtxM2 = float4x4.CreateTranslation(playerPos[1].x, 0, playerPos[1].z);
                var mtxScalePlayer = float4x4.CreateScale(5);
                RC.ModelView = mtxCam * mtxRot * mtxM2 * mtxScalePlayer;
                _srFire.Render(RC);

                //Water
                var mtxM1 = float4x4.CreateTranslation(playerPos[0].x, 0, playerPos[0].z);
                RC.ModelView = mtxCam * mtxRot * mtxM1 * mtxScalePlayer;
                _srWater.Render(RC);

                //Earth
                var mtxM3 = float4x4.CreateTranslation(playerPos[2].x, 0, playerPos[2].z);
                RC.ModelView = mtxCam * mtxRot * mtxM3 * mtxScalePlayer;
                _srEarth.Render(RC);

                //Air
                var mtxM4 = float4x4.CreateTranslation(300, 0, 300);
                RC.ModelView = mtxCam * mtxRot * mtxM4 * mtxScalePlayer;
                _srAir.Render(RC);

                //Skybox
                var mtxScale = float4x4.CreateScale(1.5f);
                RC.ModelView = mtxCam * mtxRot * mtxR * mtxScale;
                _srSky.Render(RC);

                //Level1
                var mtxTranslLevel = float4x4.CreateTranslation(0, -101, 0);
                var mtxScaleLevel = float4x4.CreateScale(0.7f);
                RC.ModelView = mtxCam * mtxRot * mtxTranslLevel * mtxScaleLevel;
                _srLevel1.Render(RC);

                _srDeko.Render(RC);
            }
            
            Present();
        }

        // is called when the window was resized
        public override void Resize()
        {
            RC.Viewport(0, 0, Width, Height);

            var aspectRatio = Width / (float)Height;
            RC.Projection = float4x4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1, 100000);
        }

        public static void Main()
        {
            var app = new LevelTestPhysics();
            app.Run();
        }
    }
}
