using System;
using System.Drawing;
using AForge.Video;
using Fusee.Engine;
using Fusee.SceneManagement;
using Fusee.Math;
using AForge.Video.FFMPEG;
using AForge.Video.VFW;

namespace Examples.VideoTexture
{
    public class VideoTexture : RenderCanvas
    {

        // angle variables
        private static float _angleHorz, _angleVert, _angleVelHorz, _angleVelVert;

        private const float RotationSpeed = 1f;
        private const float Damping = 0.92f;

        // model variables
        private Mesh _meshCube;


        // variables for shader
        private ShaderProgram _spTexture;

        private IShaderParam _textureParam;

        private ITexture _iTex;
        private VideoFileSource _videoSource;
        // is called on startup
        public override void Init()
        {
            RC.ClearColor = new float4(0.1f, 0.1f, 0.5f, 1);

            _meshCube = MeshReader.LoadMesh(@"Assets/Cube.obj.model");
          
            _spTexture = MoreShaders.GetTextureShader(RC);

            _textureParam = _spTexture.GetShaderParam("texture1");

            // create video source
            _videoSource = new VideoFileSource(@"Assets/video.avi");
            _videoSource.Start();
            _videoSource.FrameIntervalFromSource = false;
            _videoSource.FrameInterval = 100;
            _videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            // start the video source            
            var imgData = RC.LoadImage(@"Assets/world_map.jpg");
            _iTex = RC.CreateTexture(imgData);


        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // get new frame
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            ImageData imgData = RC.LoadVideoTexture(bitmap);
            _iTex = RC.CreateTexture(imgData);
            Random rand = new Random();
            System.Diagnostics.Debug.WriteLine(bitmap.GetPixel(rand.Next(0, bitmap.Width), rand.Next(0, bitmap.Height)));
        }


        // is called once a frame
        public override void RenderAFrame()
        {
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            
            // move per mouse
            if (Input.Instance.IsButton(MouseButtons.Left))
            {
                _angleVelHorz = RotationSpeed * Input.Instance.GetAxis(InputAxis.MouseX);
                _angleVelVert = RotationSpeed * Input.Instance.GetAxis(InputAxis.MouseY);
            }
            else
            {
                var curDamp = (float)Math.Exp(-Damping * Time.Instance.DeltaTime);

                _angleVelHorz *= curDamp;
                _angleVelVert *= curDamp;
            }

            _angleHorz += _angleVelHorz;
            _angleVert += _angleVelVert;

            // move per keyboard
            if (Input.Instance.IsKey(KeyCodes.Left))
                _angleHorz -= RotationSpeed * (float)Time.Instance.DeltaTime;

            if (Input.Instance.IsKey(KeyCodes.Right))
                _angleHorz += RotationSpeed * (float)Time.Instance.DeltaTime;

            if (Input.Instance.IsKey(KeyCodes.Up))
                _angleVert -= RotationSpeed * (float)Time.Instance.DeltaTime;

            if (Input.Instance.IsKey(KeyCodes.Down))
                _angleVert += RotationSpeed * (float)Time.Instance.DeltaTime;

            var mtxRot = float4x4.CreateRotationY(_angleHorz) * float4x4.CreateRotationX(_angleVert);
            var mtxCam = float4x4.LookAt(0, 200, 500, 0, 0, 0, 0, 1, 0);

            // second mesh
            RC.ModelView = mtxRot * float4x4.CreateTranslation(0, 0, 0) * mtxCam;

            RC.SetShader(_spTexture);
            RC.SetShaderParamTexture(_textureParam, _iTex);

            RC.Render(_meshCube);

            Present();
        }

        // is called when the window was resized
        public override void Resize()
        {
            RC.Viewport(0, 0, Width, Height);

            var aspectRatio = Width / (float)Height;
            RC.Projection = float4x4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1, 10000);
        }

        public static void Main()
        {
            var app = new VideoTexture();
            app.Run();
        }
    }
}