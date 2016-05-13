﻿using System;
using Fusee.Math;

namespace Fusee.Engine
{
    public class StereoCameraRig : Stereo3D
    {
       // public float4x4 CurrentProjection { get; private set; }

       // public float3 CurrentOffest { get; private set; }
        private float4x4 _leftFrustum, _rightFrustum;

        public float Iod
        {
            get { return Stereo3DParams.EyeDistance; }
            set { Stereo3DParams.EyeDistance = value; }
        }

        public StereoCameraRig(Stereo3DMode mode, int width, int height, float iod = 0.065f) : base(mode, width, height)
        {
            Iod = iod;
        }

        public override void Prepare(Stereo3DEye eye)
        {
            _rc.Projection = (eye == Stereo3DEye.Left) ? _leftFrustum : _rightFrustum;
            base.Prepare(eye);
        }

        //LookAt3D -Shift -- override/hide
        public override float4x4 LookAt3D(Stereo3DEye eye, float3 eyeV, float3 target, float3 up)
        {
            //shifttranslation
            var x = (eye == Stereo3DEye.Left)
                ? eyeV.x -Iod/2
                : eyeV.x + Iod/2;

            //set frustum
            //_rc.Projection = eye == Stereo3DEye.Left ? _leftFrustum : _rightFrustum;
        
            var newEye = new float3(x, eyeV.y, eyeV.z);
            var newTarget = new float3(x, target.y, target.z);

            return float4x4.LookAt(newEye, newTarget, up);
        }

        private float4x4 LookAt3D(float3 eye, float3 target, float3 up)
        {
            var n = float3.Normalize(target  -eye);//z
            var u = float3.Normalize(float3.Cross(up, n));//x
            var v = float3.Cross(n, u);//y

            float3 d = new float3(float3.Dot(-eye, u), float3.Dot(-eye, v), float3.Dot(-eye, n));
            return new float4x4(u.x, u.y, u.z, d.x,
                                v.x, v.y, v.z, d.y,
                                n.x, n.y, n.z, d.z,
                                0, 0, 0, 1);
        }

        public void SetFrustums(RenderContext rc, float fovy, float aspectRatio, float zNear, float zFar, float screenZero)
        {
            _leftFrustum = ViewFrustumShifted(fovy, aspectRatio, zNear, zFar,screenZero, true);
            _rightFrustum = ViewFrustumShifted(fovy, aspectRatio, zNear, zFar, screenZero, false);
            rc.Projection = _leftFrustum;
        }


        /// <summary>
        ///     Creates a left handed perspective projection matrix when using SteroCameraRig -> Frustrum shift / Off-axis.
        /// </summary>
        /// <param name="fovy">Angle of the field of view in the y direction (in radians)</param>
        /// <param name="aspect">Aspect ratio of the view (width / height)</param>
        /// <param name="zNear">Distance to the near clip plane</param>
        /// <param name="zFar">Distance to the far clip plane</param>
        /// <param name="IOD">intraocular distance</param>
        /// <param name="screenZero">distance to convergence plane</param>
        /// <param name="lefteye">defines if frustum is created for left odr right camera</param>
        /// <returns>A projection matrix that transforms camera space to raster space</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     Thrown under the following conditions:
        ///     <list type="bullet">
        ///         <item>fovy is zero, less than zero or larger than Math.PI</item>
        ///         <item>aspect is negative or zero</item>
        ///         <item>zNear is negative or zero</item>
        ///         <item>zFar is negative or zero</item>
        ///         <item>zNear is larger than zFar</item>
        ///     </list>
        /// </exception>
        private float4x4 ViewFrustumShifted(float fovy, float aspect, float zNear, float zFar, float screenZero,
            bool lefteye)
        {
            //Allgemein
            if (fovy <= 0 || fovy > System.Math.PI)
                throw new ArgumentOutOfRangeException("fovy");
            if (aspect <= 0)
                throw new ArgumentOutOfRangeException("aspect");
            if (zNear <= 0)
                throw new ArgumentOutOfRangeException("zNear");
            if (zFar <= 0)
                throw new ArgumentOutOfRangeException("zFar");
            if (zNear >= zFar)
                throw new ArgumentOutOfRangeException("zNear");


            var top = (float) System.Math.Tan(fovy*0.5f)*zNear;
            var bottom = -top;

            var shiftLr = lefteye ? -1 : 1;
            var shiftOffset = (Iod * 0.5f) * (zNear / screenZero);
            var left = -aspect*top + shiftOffset*shiftLr;
            var right = aspect*top + shiftOffset*shiftLr;

            float4x4 result;
            float4x4.CreatePerspectiveOffCenter(left, right, bottom, top, zNear, zFar, out result);
            return result;
        }
    }
}
