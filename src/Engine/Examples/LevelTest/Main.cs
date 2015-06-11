using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Fusee.Engine;
using Fusee.Engine.SimpleScene;
using Fusee.Math;
using Fusee.Serialization;


namespace Examples.LevelTest
{
    public class LevelTest : RenderCanvas
    {
        //server varialbles
        private ThreadPoolTcpSrvr _tpts;

        //gui variables
        private GUIText _guiSubText;
        private GUIText _serverText;
        private IFont _guiLatoBlack;
        private GUIHandler _guiHandler;

        private GUI _gui;

        // angle variables
        private static float _angleHorz, _angleVert, _angleVelHorz, _angleVelVert;

        private const float RotationSpeed = 1f;
        private const float Damping = 0.92f;

        // model variables
        private SceneRenderer _srSky;
        private SceneContainer _sceneSky;

        private SceneRenderer _srLevel1;
        private SceneContainer _sceneLevel1;

        private SceneRenderer _srDeko;
        private SceneContainer _sceneDeko;

        private SceneRenderer _srFire;
        private SceneContainer _sceneFire;
        float3 rot;

        private SceneRenderer _srWater;
        private SceneContainer _sceneWater;

        private SceneRenderer _srAir;
        private SceneContainer _sceneAir;

        private SceneRenderer _srEarth;
        private SceneContainer _sceneEarth;

        private SceneRenderer _srBorder;
        private SceneContainer _sceneBorder;

       // variables for shader
        private ShaderProgram _spTexture;
        private ShaderProgram _spColor;

        private IShaderParam _colorParam;
        private IShaderParam _textureParam;

        private ITexture _iTex;

        // For now using a Dictionary is the best I came up with. Feel freeto correct me.
        private Dictionary<string, List<float>> _playerOrientations = new Dictionary<string, List<float>>();

        private int _aa;
        private int _bb;
        private int _cc = 300;
        private int _dd;
        private int _ee = 600;
        private int _ff;

        // some logic
        private bool isEmpty;


        private float3 averageNewPos;

        // is called on startup
        public override void Init()
        {
            //creates thread for TcpServer, sets it as backgroundthread, starts the thread
            var tcpServer = new Thread(StartTcpServer);
            tcpServer.IsBackground = true;
            tcpServer.Start(this);


            
            
            

            _gui = new GUI(RC);

            RC.ClearColor = new float4(0.1f, 0.1f, 0.1f, 1);
            
            //Border
            var serBorder = new Serializer();
            using (var file = File.OpenRead(@"Assets/border1.fus"))
            {
                _sceneBorder = serBorder.Deserialize(file, null, typeof(SceneContainer)) as SceneContainer;
            }

            _srBorder = new SceneRenderer(_sceneBorder, "Assets");


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
                _sceneFire = serFire.Deserialize(file, null, typeof (SceneContainer)) as SceneContainer;
            }
            _srFire = new SceneRenderer(_sceneFire, "Assets");


            //rotation of firesphere
            foreach (SceneNodeContainer node in _sceneFire.Children.FindNodes(node => node.Name.Equals("Fire")))
            {
                TransformComponent transform = node.GetTransform();
                //rotation component
                rot = transform.Rotation;                
            }               


            //Water
            var serWater = new Serializer();
            using (var file = File.OpenRead(@"Assets/Player_water.fus"))
            {
                _sceneWater = serWater.Deserialize(file, null, typeof(SceneContainer)) as SceneContainer;
            }
            _srWater = new SceneRenderer(_sceneWater, "Assets");

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

        }

        // is called once a frame
        public override void RenderAFrame()
        {

            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            //GUI
            float fps = Time.Instance.FramePerSecond;
            _gui.RenderFps(fps);

            isEmpty = !_tpts.GetConnections().Any();
            if (isEmpty)
            {
                Console.WriteLine("Awaiting Connections");
            }
            else
            {
                var playerOrientation = ExtractNumbers(_tpts.GetConnections().First().Message);
                if (playerOrientation != null)
                    //Console.WriteLine("P: {0}, R: {1}", playerOrientation[0], playerOrientation[1]);
                foreach (var connection in _tpts.GetConnections())
                {
                    try
                    {
                        var ip = connection.Address.ToString();
                        List<float> value = new List<float>();
                        if (!_playerOrientations.TryGetValue(ip, out value))
                            _playerOrientations.Add(ip, ExtractNumbers(connection.Message));
                        else
                        {
                            _playerOrientations.TryGetValue(ip, out value);
                            _playerOrientations.Remove(ip);
                            _playerOrientations.Add(ip, ExtractNumbers(connection.Message));
                            Console.WriteLine(_playerOrientations[ip]);
                        }
                    }
                    catch (NullReferenceException)
                    {
                        return;
                    }
                    
                    
                    //
                    //
                }
            }

            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (TcpConnection connection in _tpts.GetConnections())
                {
                    sb.Append(connection.Message);
                    sb.Append("// ");
                    //Console.WriteLine(ExtractNumbers(connection.Message).Length);
                }
                _gui.RenderMsg(sb.ToString());

            }
            catch (NullReferenceException)
            {
                _gui.RenderMsg("Nichts empfangen!");
            }

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


          //GAMEMODE 0
            // move per keyboard (arrow keys) in gamemode 0
            /*if (Input.Instance.IsKey(KeyCodes.Left))
            {
                inputA = 30;
                _aa -= (int)inputA;
            }
            if (Input.Instance.IsKey(KeyCodes.Right))
            {
                inputA = 30;
                _aa += (int)inputA;
            }
            if (Input.Instance.IsKey(KeyCodes.Up))
            {
                inputB = 30;
                _bb += (int)inputB;
            }
            if (Input.Instance.IsKey(KeyCodes.Down))
            {
                inputB = 30;
                _bb -= (int)inputB;
            }*/
                if (_playerOrientations.Count != 0)
                {
                    var controllArray = _playerOrientations.First().Value;
                    inputA = -(int)controllArray[1];
                    _aa += inputA;

                    inputB = -(int)controllArray[0];
                    _bb += inputB;
                }

            // move per keyboard (W A S D) in gamemode 0

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
                var mtxCam = float4x4.CreateTranslation(averageNewPos.x, 0, averageNewPos.z) * float4x4.CreateRotationY(-_angleHorz) * float4x4.CreateRotationX(-_angleVert) * float4x4.CreateTranslation(0, 0, -3500);
                mtxCam.Invert();
                //Console.WriteLine(_angleVert + " " + _angleHorz);

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
                // border
                var mtxR = float4x4.CreateTranslation(averageNewPos.x, -101, averageNewPos.z+200);
                RC.ModelView = mtxCam * mtxRot * mtxR;
                _srBorder.Render(RC);

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

           
           

            
            Present();
        }

        private List<float> ExtractNumbers(string message)
        {
            List<float> orientation = new List<float>();
            //string pitch = string.Empty;
            //string roll = string.Empty;
            if (message.Length == 0) return orientation;

            string[] split = message.Split(new char[] {':', ' ', ',', ';'});

            float tempNumber = 0;

            foreach(char numChar in message.ToCharArray())
            {
               // if (Char.IsNumber(numChar)) figure += numChar.ToString();
            }
            //if (figure == string.Empty) return "";
            tempNumber = float.Parse(split[1]);
            split[2] = "0," + split[2];
            orientation.Add(float.Parse(split[2]) + tempNumber);
            tempNumber = float.Parse(split[6]);
            split[7] = "0," + split[7];
            orientation.Add(float.Parse(split[7]) + tempNumber);

            return orientation;
        }

        // is called when the window was resized
        public override void Resize()
        {
            RC.Viewport(0, 0, Width, Height);

            var aspectRatio = Width / (float)Height;
            RC.Projection = float4x4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1, 100000);
        }

        public static void StartTcpServer(object self)
        {
            ((LevelTest)self)._tpts = new ThreadPoolTcpSrvr();
            ((LevelTest)self)._tpts.StartListening();
        }

        public static void Main()
        {
            var app = new LevelTest();
            app.Run();
        }
    }
}