﻿using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;
using Fusee.Engine;
using Fusee.Math;

namespace Examples.DepthVideo
{
    internal struct VideoFrames
    {
        public Image<Bgr, byte> CurrentLeftFrame;
        public Image<Bgr, byte> CurrentRightFrame;
        public Image<Gray, byte> CurrentLeftDepthFrame;
        public Image<Gray, byte> CurrentRightDepthFrame;

        public ImageData ImgDataLeft;
        public ImageData ImgDataRight;
        public ImageData ImgDataDepthLeft;
        public ImageData ImgDataDepthRight;
    }

    

    public class ScreenS3D
    {

        #region S3D-Shader + Depth
        // GLSL
        private const string VsS3dDepth = @"
            attribute vec4 fuColor;
            attribute vec3 fuVertex;
            attribute vec3 fuNormal;
            attribute vec2 fuUV;
        
            varying vec4 vColor;
            varying vec3 vNormal;
            varying vec2 vUV;
            varying vec4 camPos;
            varying vec4 clip;
        
            uniform mat4 FUSEE_MV;
            uniform mat4 FUSEE_P;
            uniform mat4 FUSEE_IMV;
            uniform mat4 FUSEE_ITMV;
            uniform mat4 FUSEE_MVP;
            uniform mat4 FUSEE_V;
            uniform mat4 FUSEE_IP;

            void main()
            {               
               
                gl_Position = FUSEE_MVP * vec4(fuVertex, 1.0);
                clip = FUSEE_P*FUSEE_MV * vec4(fuVertex, 1.0);
                
                
                 camPos =  FUSEE_MV[3];//distance to cam
                
                vNormal = mat3(FUSEE_ITMV[0].xyz, FUSEE_ITMV[1].xyz, FUSEE_ITMV[2].xyz) * fuNormal;
                vUV = fuUV;
            }";

        private const string PsS3dDepth = @"
            #ifdef GL_ES
                precision highp float;
            #endif
           
            uniform sampler2D vTexture;
            uniform sampler2D textureDepth;
            uniform vec4 vColor;
            uniform mat4 FUSEE_MV;
            uniform mat4 FUSEE_MVP;
            uniform mat4 FUSEE_P;
            uniform mat4 FUSEE_V;
            uniform mat4 FUSEE_IP;
            uniform mat4 FUSEE_ITMV;

            varying vec3 vNormal;
            varying vec2 vUV;
            varying vec4 camPos;
            varying vec4 pos;
            varying vec4 clip;
            vec4 temp;
            float zFar = 50;
            float zNear = 1;

            //// depthSample from depthTexture.r, for instance
            //float linearDepth(float depthSample)
            //{
            //    depthSample = 2.0 * depthSample - 1.0;
            //    float zLinear = 2.0 * zNear * zFar / (zFar + zNear - (depthSample-0.5) * (zFar - zNear));
            //    return zLinear;
            //}

            //// result suitable for assigning to gl_FragDepth
            //float depthSample(float linearDepth)
            //{
            //    float nonLinearDepth = (zFar + zNear - 2.0 * zNear * zFar / linearDepth) / (zFar - zNear);
            //    nonLinearDepth = (nonLinearDepth + 1.0) / 2.0;
            //    return nonLinearDepth;
            //}

             float ndcPosZ (float d) 
            {
                  return  ((2*d)-gl_DepthRange.far-gl_DepthRange.near)/(gl_DepthRange.far-gl_DepthRange.near);
            }
//-------------------------------------------------------
            //step 1
            float prepareSample(float samlpe)
            {    
                return ((samlpe*2)-1)*0.1; //[0,1]->[-1,1]
            }

            //step 2
            float ndcZ(float fragZ)
            {
                float zLin = (fragZ*2)-1; //[0,1]->[-1,1] 
                zLin = (2*gl_DepthRange.near+gl_DepthRange.far)/(gl_DepthRange.far+gl_DepthRange.near-zLin*(gl_DepthRange.far-gl_DepthRange.near)); // linearize
                return zLin;
            }

            //step3 merge
            float merge(float fragZLin, float preparedSample)
            {
                return fragZLin+preparedSample;
            }

            //step 4 back to nonLin [0,1]
            float fragCoordZ(float linZ)
            {
                float nonLin = (gl_DepthRange.far+gl_DepthRange.near-2*gl_DepthRange.near*gl_DepthRange.far)/linZ/(gl_DepthRange.far-gl_DepthRange.near);
                return (nonLin+1)/2;
            }
//--------------------------------------------------------
           
            float coordZ;
            void main()
            {
               
                vec4 colTex = vColor * texture2D(vTexture, vUV);               
             
                float depthTexValue = 1-texture(textureDepth, vUV);
                
               // colTex *= depthTexValue;
    
                if(depthTexValue >0.9)          
                {
                    //float ndcDepth = (clip.z/clip.w);  
                    //float coordZ = (gl_DepthRange.far-gl_DepthRange.near)*0.5*ndcDepth+(gl_DepthRange.far-gl_DepthRange.near)*0.5; 
                    //gl_FragDepth =  coordZ;  
                  //  gl_FragDepth = gl_FragCoord.z ;
                    discard;
                }
                else
                {                   

                    vec4 eye = clip; 
                 //   eye = FUSEE_P*(eye+vec4(0,0,depthTexValue-1,1));
                    
                    float ndcDepth = (eye.z/eye.w);
                   // ndcDepth += prepareSample(depthTexValue);
                   coordZ  = (gl_DepthRange.far-gl_DepthRange.near)*0.5*ndcDepth+(gl_DepthRange.far-gl_DepthRange.near)*0.5; 
                   // gl_FragDepth =  coordZ;              
                }

                vec4 temp = vec4(1,1,1,1);
                if(coordZ == gl_FragCoord.z)
                {
                    temp = vec4(0,1,0,1);
                }  
                if(clip.z == 35)
                {
                    temp = vec4(0,0,1,1);
                }  
                gl_FragColor =  dot(vColor, vec4(0, 0, 0, 1)) *temp *colTex * dot(vNormal, vec3(0, 0, -1));        
        
                
                

            }";

        #endregion


        private readonly RenderContext _rc;
        private readonly Stereo3D _stereo3D;
        private ShaderProgram _stereo3DShaderProgram;
        private IShaderParam _colorShaderParam;
        private IShaderParam _colorTextureShaderParam;
        private IShaderParam _depthTextureShaderParam;
        private IShaderParam _depthShaderParamZ;


        public Mesh ScreenMesh { get; set; }
    

        private List<Image<Bgr, byte>> _framesListLeft = new List<Image<Bgr, byte>>();
        private List<Image<Bgr, byte>> _framesListRight = new List<Image<Bgr, byte>>();
        private List<Image<Gray, byte>> _framesListDepthLeft = new List<Image<Gray, byte>>();
        private List<Image<Gray, byte>> _framesListDepthRight = new List<Image<Gray, byte>>();


        private IEnumerator<Image<Bgr, byte>> _framesListLeftEnumerator;
        private IEnumerator<Image<Bgr, byte>> _framesListRightEnumerator;
        private IEnumerator<Image<Gray, byte>> _framesListDepthLeftEnumerator;
        private IEnumerator<Image<Gray, byte>> _framesListDepthRightEnumerator;


        private List<ITexture> _iTexturesListLeft = new List<ITexture>();
        private List<ITexture> _iTexturesListRight = new List<ITexture>();
        private List<ITexture> _iTexturesListDepthLeft = new List<ITexture>();
        private List<ITexture> _iTexturesListDepthRight = new List<ITexture>();

        private IEnumerator<ITexture> _iTexturesListLeftEnumerator;
        private IEnumerator<ITexture> _iTexturesListRightEnumerator;
        private IEnumerator<ITexture> _iTexturesListDepthLeftEnumerator;
        private IEnumerator<ITexture> _iTexturesListDepthRightEnumerator;

        private struct CurrentVideoTextrures
        {
            public ITexture ITextureLeft;
            public ITexture ITextureRight;
            public ITexture ITextureDepthLeft;
            public ITexture ITextureDepthRight;
        }

        private CurrentVideoTextrures _currentVideoTextures = new CurrentVideoTextrures();


        public ITexture _iTextureLeft;
        public ITexture _iTextureRight;
        public ITexture _iTextureDepthLeft;
        public ITexture _iTextureDepthRight;

        public float3 Position { get; set; }
        private float3 _scaleFactor;

        public float Hit { get; private set; }

        private StereoBM _stereoSolver = new StereoBM();
        private ITexture iTexLeft;
        private ITexture iTexRight;

        public ScreenS3D(RenderContext rc, Stereo3D s3D,  float3 pos)
        {
            ScreenMesh = new Mesh();
            Hit = 0.065f*2f;
            _rc = rc;
            _stereo3D = s3D;
            _stereo3DShaderProgram = _rc.CreateShader(VsS3dDepth, PsS3dDepth);
            _colorShaderParam = _stereo3DShaderProgram.GetShaderParam("vColor");
            _colorTextureShaderParam = _stereo3DShaderProgram.GetShaderParam("vTexture");
            _depthTextureShaderParam = _stereo3DShaderProgram.GetShaderParam("textureDepth");
            //_depthShaderParamZ = _stereo3DShaderProgram.GetShaderParam("z");

            iTexLeft = _rc.CreateTexture(_rc.LoadImage("Assets/imL.png"));
            iTexRight = _rc.CreateTexture(_rc.LoadImage("Assets/imR.png"));

            Position = pos;
            var faktor = 10;
            _scaleFactor = new float3(0.64f* faktor, 0.48f* faktor, 1f);
            CreatePlaneMesh();

        //    CalcEpipolar();
        }

       
        /// <summary>
        /// Creates the Mesh where the Video is mapt on as a texture
        /// </summary>
        private void CreatePlaneMesh()
        {

            var vertecies = new[]
            {
                new float3 {x = +0.5f, y = -0.5f, z = +0.5f},
                new float3 {x = +0.5f, y = +0.5f, z = +0.5f},
                new float3 {x = -0.5f, y = +0.5f, z = +0.5f},
                new float3 {x = -0.5f, y = -0.5f, z = +0.5f}
            };

            var triangles = new ushort[]
            {
                // front face
                1,2,0,2,3,0
            };

            var normals = new[]
            {
                new float3(0, 0, -1),
                new float3(0, 0, -1),
                new float3(0, 0, -1),
                new float3(0, 0, -1)
            };
            var uVs = new[]
            {

                new float2(1, 0),
                new float2(1, 1),
                new float2(0, 1),
                new float2(0, 0)
            };


            ScreenMesh.Vertices = vertecies;
            ScreenMesh.Triangles = triangles;
            ScreenMesh.Normals = normals;
            ScreenMesh.UVs = uVs;
        }


        /// <summary>
        /// Imort the videos to the ScreenS3D object
        /// </summary>
        /// <param name="pathLeftVideo">Path to left Video</param>
        /// <param name="pathRightVideo">Path to right Video</param>
        /// <param name="pathDepthVideLeft">Path to depth Video (left)</param>
        /// <param name="pathDepthVideRight">Path to depth Video (right)</param>
        /// <param name="videoLength">Length of the videos in frames. (All three videos must have the same amount of frames and recorded with the same frame rate)</param>
        public void SetVideo(string pathLeftVideo, string pathRightVideo, string pathDepthVideLeft, string pathDepthVideRight, int videoLength)
        {

            ImportVideo(_framesListLeft, pathLeftVideo, ref _framesListLeftEnumerator, videoLength);
            ImportVideo(_framesListRight, pathRightVideo, ref _framesListRightEnumerator, videoLength);
            ImportVideo(_framesListDepthLeft, pathDepthVideLeft, ref _framesListDepthLeftEnumerator, videoLength);
            ImportVideo(_framesListDepthRight, pathDepthVideRight, ref _framesListDepthRightEnumerator, videoLength);

            VideoFrames _videoFrames = new VideoFrames();
            for (int i = 0; i < _framesListLeft.Count; i++)
            {
                _videoFrames = GetVideoFrames();
                CrateTextures(_videoFrames);
            }
            _iTexturesListLeftEnumerator = _iTexturesListLeft.GetEnumerator();
            _iTexturesListRightEnumerator = _iTexturesListRight.GetEnumerator();
            _iTexturesListDepthLeftEnumerator = _iTexturesListDepthLeft.GetEnumerator();
            _iTexturesListDepthRightEnumerator = _iTexturesListDepthRight.GetEnumerator();


            _framesListLeft = null;
            _framesListRight = null;
            _framesListDepthLeft = null;
            _framesListDepthRight = null;

            _framesListLeftEnumerator = null;
            _framesListRightEnumerator = null;
            _framesListDepthLeftEnumerator = null;
            _framesListDepthRightEnumerator = null;
        }

        /// <summary>
        ///  Imports video (Color) file and stores frames into a List
        /// </summary>
        /// <param name="frameList"></param>
        /// <param name="path"></param>
        /// <param name="frameListEnumerator"></param>
        /// <param name="videoLength"></param>
        private void ImportVideo(List<Image<Bgr, byte>> frameList, string path, ref IEnumerator<Image<Bgr, byte>> frameListEnumerator, int videoLength)
        {
            using (Capture capture = new Capture(path))
            { 
                var tempFrame = capture.QueryFrame().ToImage<Bgr, byte>();

            var framecounter = 0;
            while (tempFrame != null)
            {
                frameList.Add(tempFrame);
                tempFrame = capture.QueryFrame().ToImage<Bgr, byte>();
                framecounter++;
                if (framecounter >= videoLength)
                {
                    break;
                }
            }
  
            frameListEnumerator = frameList.GetEnumerator();
            }
        }

        /// <summary>
        ///  Imports video (gray) file and stores frames into a List
        /// </summary>
        /// <param name="frameList"></param>
        /// <param name="path"></param>
        /// <param name="frameListEnumerator"></param>
        /// <param name="videoLength"></param>
        private void ImportVideo(List<Image<Gray, byte>> frameList, string path, ref IEnumerator<Image<Gray, byte>> frameListEnumerator, int videoLength)
        {
            using (Capture captureDepth = new Capture(path))
            {
                var tempFrame = captureDepth.QueryFrame().ToImage<Gray, byte>();


                var framecounter = 0;
                while (tempFrame != null)
                {
                    frameList.Add(tempFrame);
                    tempFrame = captureDepth.QueryFrame().ToImage<Gray, byte>();
                    framecounter++;
                    if (framecounter >= videoLength)
                    {
                        break;
                    }
                }

                frameListEnumerator = frameList.GetEnumerator();
            }
        }



        // looping videos and returning  current fram of color and depth video
        private VideoFrames GetVideoFrames()
        {
            VideoFrames vf;
            //Iterating over the frames List of the Color Video
            if (!_framesListLeftEnumerator.MoveNext())
            {
                _framesListLeftEnumerator.Reset();
                _framesListLeftEnumerator.MoveNext();
            }
            vf.CurrentLeftFrame = _framesListLeftEnumerator.Current;
            var imgDataLeft = new ImageData();
            imgDataLeft.Width = vf.CurrentLeftFrame.Width;
            imgDataLeft.Height = vf.CurrentLeftFrame.Height;
            imgDataLeft.PixelFormat = ImagePixelFormat.RGB;
            imgDataLeft.PixelData = vf.CurrentLeftFrame.Bytes;
            vf.ImgDataLeft = imgDataLeft;

            //Iterating over the frames List - Right
            if (!_framesListRightEnumerator.MoveNext())
            {
                _framesListRightEnumerator.Reset();
                _framesListRightEnumerator.MoveNext();
            }
            vf.CurrentRightFrame = _framesListRightEnumerator.Current;
            var imgDataRight = imgDataLeft;
            imgDataRight.PixelData = vf.CurrentRightFrame.Bytes;
            vf.ImgDataRight = imgDataRight;

            //Iterating over the frames List - Depth Left
            if (!_framesListDepthLeftEnumerator.MoveNext())
            {
                _framesListDepthLeftEnumerator.Reset();
                _framesListDepthLeftEnumerator.MoveNext();
            }
            vf.CurrentLeftDepthFrame = _framesListDepthLeftEnumerator.Current;
            var imgDataDepthLeft = imgDataLeft;
            imgDataDepthLeft.PixelFormat = ImagePixelFormat.Gray;
            imgDataDepthLeft.PixelData = vf.CurrentLeftDepthFrame.Bytes;
            vf.ImgDataDepthLeft = imgDataDepthLeft;


            //Iterating over the frames List - Depth Right
            if (!_framesListDepthRightEnumerator.MoveNext())
            {
                _framesListDepthRightEnumerator.Reset();
                _framesListDepthRightEnumerator.MoveNext();
            }
            vf.CurrentRightDepthFrame = _framesListDepthRightEnumerator.Current;
            var imgDataDepthRight= imgDataLeft;
            imgDataDepthRight.PixelFormat = ImagePixelFormat.Gray;
            imgDataDepthRight.PixelData = vf.CurrentRightDepthFrame.Bytes;
            vf.ImgDataDepthRight = imgDataDepthRight;


            //CreateDisparityMap(vf.CurrentLeftDepthFrame.Mat, vf.CurrentRightDepthFrame.Mat);
            return vf;
        }


        // looping videos and returning  current fram of color and depth video
        private CurrentVideoTextrures GetCurrentVideoITextures()
        {
           

            CurrentVideoTextrures cvt;
            //Iterating over the ITexture List of the Color Video
            if (!_iTexturesListLeftEnumerator.MoveNext())
            {
                _iTexturesListLeftEnumerator.Reset();
                _iTexturesListLeftEnumerator.MoveNext();
            }
            cvt.ITextureLeft = _iTexturesListLeftEnumerator.Current;

            //Iterating over the frames List - Right
            if (!_iTexturesListRightEnumerator.MoveNext())
            {
                _iTexturesListRightEnumerator.Reset();
                _iTexturesListRightEnumerator.MoveNext();
            }
            cvt.ITextureRight = _iTexturesListRightEnumerator.Current;

            //Iterating over the frames List - Depth Left
            if (!_iTexturesListDepthLeftEnumerator.MoveNext())
            {
                _iTexturesListDepthLeftEnumerator.Reset();
                _iTexturesListDepthLeftEnumerator.MoveNext();
            }
            cvt.ITextureDepthLeft = _iTexturesListDepthLeftEnumerator.Current;

            //Iterating over the frames List - Depth Right
            if (!_iTexturesListDepthRightEnumerator.MoveNext())
            {
                _iTexturesListDepthRightEnumerator.Reset();
                _iTexturesListDepthRightEnumerator.MoveNext();
            }
            cvt.ITextureDepthRight = _iTexturesListDepthRightEnumerator.Current;

            

            return cvt;
        }

        private void CalcEpipolar()
        {

            Image<Bgr, byte> left = CvInvoke.Imread("Assets/imL.png", LoadImageType.Color).ToImage<Bgr, byte>();
            Image<Bgr, byte> right = CvInvoke.Imread("Assets/imR.png", LoadImageType.Color).ToImage<Bgr, byte>();
            //  var diff = left.AbsDiff(right);

            VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch();
            SURF surfCPU = new SURF(300);
            int k = 2;
            UMat leftDescriptors = new UMat();
            UMat rightDescriptors = new UMat();
            VectorOfKeyPoint leftKeyPoints = new VectorOfKeyPoint();
            VectorOfKeyPoint rightKeyPoints = new VectorOfKeyPoint();
            surfCPU.DetectAndCompute(left, null, leftKeyPoints, leftDescriptors, false);
            surfCPU.DetectAndCompute(right, null, rightKeyPoints, rightDescriptors, false);

            BFMatcher matcher = new BFMatcher(DistanceType.L2);
            matcher.Add(leftDescriptors);

            matcher.KnnMatch(rightDescriptors, matches, k, null);
            Mat mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
            mask.SetTo(new MCvScalar(255));

              CvInvoke.Imshow("Diff", mask);

            Mat _left = CvInvoke.Imread("Assets/imL.png", LoadImageType.Color);
            Mat _right = CvInvoke.Imread("Assets/imR.png", LoadImageType.Color);
            
            // computeCorrespondEpilines
            Feature2D f2d = new FastDetector();
            var kpL = f2d.Detect(_left);
            var kpR = f2d.Detect(_right);

            for (int i = 0; i < kpL.Length; i++)
            {

                
                var dx = kpL[i].Point.X - kpR[i].Point.X;
                var dy = kpL[i].Point.Y - kpR[i].Point.Y;

                if (dy != 0)
                {
                    Console.WriteLine(i+": L: "+ kpL[i].Point + ",  R: "+ kpR[i].Point + ", diff: "+ dx);
                }
            }
            
        }


        private void CreateDisparityMap(Mat left, Mat right)
        {
            Mat _left = CvInvoke.Imread("Assets/imL.png", LoadImageType.Color);
            Mat _right = CvInvoke.Imread("Assets/imR.png", LoadImageType.Color);
            UMat leftGray = new UMat();
            UMat rightGray = new UMat();

            
            //CvInvoke.CvtColor(left, leftGray, ColorConversion.Bgr2Gray);
            //CvInvoke.CvtColor(right, rightGray, ColorConversion.Bgr2Gray);
            Mat disparityMap = new Mat();

            Disparity(left, right, disparityMap);
            
            CvInvoke.Imshow("Disp", disparityMap.ToImage<Gray,byte>());
        }

        public void Disparity(IInputArray left, IInputArray right,  Mat outputDisparityMap)
        {
            using (StereoBM stereoSolver = new StereoBM())
            {
  
                
                stereoSolver.Compute(left, right, outputDisparityMap);
                
            }

        }

      

        private void CrateTextures(VideoFrames vf)
        {
            ITexture left, right, depthleft, depthright;
            //iTexture left
            if (vf.ImgDataLeft.PixelData != null)
            {
              //  if (left == null)
                    left = _rc.CreateTexture(vf.ImgDataLeft);
                _iTexturesListLeft.Add(left);
               // _rc.UpdateTextureRegion(left, vf.ImgDataLeft, 0, 0, vf.ImgDataLeft.Width, vf.ImgDataLeft.Height);
            }

            //iTexture right
            if (vf.ImgDataRight.PixelData != null)
            {
                // if (_iTextureRight == null)
                right = _rc.CreateTexture(vf.ImgDataRight);
                _iTexturesListRight.Add(right);
                //_rc.UpdateTextureRegion(right, vf.ImgDataRight, 0, 0, vf.ImgDataRight.Width, vf.ImgDataRight.Height);
            }

          
            //depth texture left
            if (vf.ImgDataDepthLeft.PixelData != null)
            {
                //if (_iTextureDepthLeft == null)
                depthleft = _rc.CreateTexture(vf.ImgDataDepthLeft);
                _iTexturesListDepthLeft.Add(depthleft);
                //_rc.UpdateTextureRegion(depthleft, vf.ImgDataDepthLeft, 0, 0, vf.ImgDataDepthLeft.Width, vf.ImgDataDepthLeft.Height);
            }

            //depth texture right
            if (vf.ImgDataDepthRight.PixelData != null)
            {
                //if (_iTextureDepthRight == null)
                depthright = _rc.CreateTexture(vf.ImgDataDepthRight);
                _iTexturesListDepthRight.Add(depthright);
                //_rc.UpdateTextureRegion(depthright, vf.ImgDataDepthRight, 0, 0, vf.ImgDataDepthRight.Width, vf.ImgDataDepthRight.Height);
            }
        }

        public void Update()
        {
            // _videoFrames = GetCurrentVideoFrames();
            // CrateTextures(_videoFrames);

            // _videoFrames = GetCurrentVideoITextures();


            //preloaded videos
            _currentVideoTextures = GetCurrentVideoITextures();

            if (Input.Instance.IsKey(KeyCodes.W))
                Position += new float3(0, 0, 0.5f);

            if (Input.Instance.IsKey(KeyCodes.S))
                Position += new float3(0, 0, -0.5f);

            //Hit
            if (Input.Instance.IsKeyUp(KeyCodes.Add))
                Hit += 0.01f;
                
            if (Input.Instance.IsKeyUp(KeyCodes.Subtract))
                Hit -= 0.01f;



            if (Input.Instance.IsKey(KeyCodes.H))
                Console.WriteLine("Hit: "+ Hit);
        }

        public float offsetFix = 6.5f*0.15f;

        private float3 CalcOffset(float4x4 rot, float4x4 lookat, string side)
        {
            
            //var modelView = lookat*float4x4.CreateTranslation(Position);
            //var offsetVector =  _rc.Projection * modelView;
            ////Console.WriteLine(side+": "+res);
            //var ret = new float3(offsetVector.Column3.x, offsetVector.Column3.y, offsetVector.Column3.z);
            return float3.One;
        }

        public void Render3DScreen(float4x4 lookat, float4x4 rot)
        {
            float4x4 offset;
            float hit = 0;
            ITexture textureColor = null;
            ITexture textureDepth = null; 
            switch (_stereo3D.CurrentEye)
            {
                case Stereo3DEye.Left:
                    textureColor = _currentVideoTextures.ITextureLeft;
                    textureDepth = _currentVideoTextures.ITextureDepthLeft;
                    hit = (-Hit/2);
                    break;
                case Stereo3DEye.Right:
                    textureColor = _currentVideoTextures.ITextureRight;
                    textureDepth = _currentVideoTextures.ITextureDepthRight;
                    hit = (Hit/2);
                    break;
            }

            //var temp = lookat*rot;
            //float p = temp.Column3.z;
            //float dist = 0;
            //if (p>=0)
            //{
            //    dist = p + Position.z;
            //}
            //else
            //{
            //    dist = Math.Abs(Position.z + p);
            //}
            if (textureColor != null && textureDepth != null)
            {
                _rc.SetShader(_stereo3DShaderProgram);
                _rc.SetShaderParam(_colorShaderParam, new float4(new float3(1, 1, 1), 1));
                _rc.SetShaderParamTexture(_colorTextureShaderParam, textureColor);
                _rc.SetShaderParamTexture(_depthTextureShaderParam, textureDepth);
                
                  var mv= lookat* rot *float4x4.CreateTranslation(Position) * float4x4.CreateTranslation(hit, 0, 0) * float4x4.CreateScale(_scaleFactor);
               // _rc.SetShaderParam(_depthShaderParamZ, mv.Column3.z);
                _rc.ModelView = mv;
                _rc.Render(ScreenMesh);
                if (Input.Instance.IsKeyUp(KeyCodes.D))
                {
                    Console.WriteLine("Distance: ");
                    Console.WriteLine(lookat);
                    Console.WriteLine(float4x4.CreateTranslation(Position));
                    Console.WriteLine(mv);

                }
            }

           
             
        }

        #region alt
        //public void RenderLeft(float4x4 rot, float4x4 lookat)
        //{
        //    //left
        //    _rc.SetShader(_stereo3DShaderProgram);
        //    _rc.SetShaderParam(_colorShaderParam, new float4(new float3(1,1,1), 1.0f));
        //    _rc.SetShaderParamTexture(_colorTextureShaderParam, _iTextureLeft);
        //    _rc.SetShaderParamTexture(_depthTextureShaderParam, _iTextureDepthLeft);
        //    _rc.ModelView = lookat * rot * float4x4.CreateTranslation(Position)  * float4x4.CreateTranslation(0.15f, 0,0) * float4x4.CreateScale(_scaleFactor);
        //    _rc.Render(ScreenMesh);
        //}

        //public void RenderRight(float4x4 rot, float4x4 lookat)
        //{

        //    _rc.SetShader(_stereo3DShaderProgram);
        //    _rc.SetShaderParam(_colorShaderParam, new float4(new float3(1,1,1), 1.0f));
        //    _rc.SetShaderParamTexture(_colorTextureShaderParam, _iTextureRight);
        //    _rc.SetShaderParamTexture(_depthTextureShaderParam, _iTextureDepthRight);
        //    _rc.ModelView = lookat * rot * float4x4.CreateTranslation(Position) * float4x4.CreateTranslation(-0.15f, 0, 0)  *float4x4.CreateScale(_scaleFactor);
        //    _rc.Render(ScreenMesh);
        //}
        #endregion
    }
}
