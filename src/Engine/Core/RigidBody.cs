﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Fusee.Engine;
using Fusee.Math;
using Microsoft.CSharp.RuntimeBinder;

namespace Fusee.Engine
{
    public class RigidBody
    {
        internal IRigidBodyImp _iRigidBodyImp;

       // public Mesh Mesh { get; set; }

        public float3 Gavity
        {
            get
            {
                var retval = _iRigidBodyImp.Gravity;
                return retval;
            }
            set
            {
                var o = (RigidBody)_iRigidBodyImp.UserObject;
                _iRigidBodyImp.Gravity = value;
            }
        }

        public float Mass
        {
            get
            {
                return _iRigidBodyImp.Mass;
            }
            set
            {
                _iRigidBodyImp.Mass = value;
            }
        }

        public float3 Inertia
        {
            get
            {
                return _iRigidBodyImp.Inertia;
            }
            set
            {
                var o = (RigidBody)_iRigidBodyImp.UserObject;
                o._iRigidBodyImp.Inertia = value;
            }
        }   

        public float4x4 WorldTransform
        {
            get
            {
                return _iRigidBodyImp.WorldTransform;
            }
            set
            {
                var o = (RigidBody) _iRigidBodyImp.UserObject;
                o._iRigidBodyImp.WorldTransform = value;
                
            }
        }

        public float3 Position
        {
            get
            {
                return _iRigidBodyImp.Position;
            }
            set
            {
                var o = (RigidBody)_iRigidBodyImp.UserObject;
                o._iRigidBodyImp.Position = value;

            }
        }

        
        public void ApplyForce(float3 force, float3 relPos)
        {
            var o = (RigidBody)_iRigidBodyImp.UserObject;
            o._iRigidBodyImp.ApplyForce(force, relPos);
        }

        public float3 ApplyTorque
        {
            get
            {
                return _iRigidBodyImp.ApplyTorque;
            }
            set
            {
                var o = (RigidBody)_iRigidBodyImp.UserObject;
                o._iRigidBodyImp.ApplyTorque = value;
            }
        }

        public void ApplyImpulse(float3 impulse, float3 relPos)
        {
            var o = (RigidBody)_iRigidBodyImp.UserObject;
            o._iRigidBodyImp.ApplyImpulse(impulse, relPos);
        }

        public float3 ApplyTorqueImpulse
        {
            get
            {
                return _iRigidBodyImp.ApplyTorqueImpulse;
            }
            set
            {
                var o = (RigidBody)_iRigidBodyImp.UserObject;
                o._iRigidBodyImp.ApplyTorqueImpulse = value;
            }
        }

        public float3 ApplyCentralForce
        {
            get
            {
                return _iRigidBodyImp.ApplyCentralForce;
            }
            set
            {
                var o = (RigidBody)_iRigidBodyImp.UserObject;
                o._iRigidBodyImp.ApplyCentralForce = value;
            }
        }

        public float3 ApplyCentralImpulse
        {
            get
            {
                return _iRigidBodyImp.ApplyCentralImpulse;
            }
            set
            {
                var o = (RigidBody)_iRigidBodyImp.UserObject;
                o._iRigidBodyImp.ApplyCentralImpulse = value;
            }
        }

        public float3 LinearVelocity
        {
            get
            {
                return _iRigidBodyImp.LinearVelocity;
            }
            set
            {
                var o = (RigidBody)_iRigidBodyImp.UserObject;
                o._iRigidBodyImp.LinearVelocity = value;
            }
        }

        public float3 AngularVelocity
        {
            get
            {
                return _iRigidBodyImp.AngularVelocity;
            }
            set
            {
                var o = (RigidBody)_iRigidBodyImp.UserObject;
                o._iRigidBodyImp.AngularVelocity = value;
            }
        }


        public float3 LinearFactor
        {
            get
            {
                return _iRigidBodyImp.LinearFactor;
            }
            set
            {
                var o = (RigidBody) _iRigidBodyImp.UserObject;
                o._iRigidBodyImp.LinearFactor = value;
            }
        }

        public float3 AngularFactor
        {
            get
            {
                return _iRigidBodyImp.LinearFactor;
            }
            set
            {
                var o = (RigidBody)_iRigidBodyImp.UserObject;
                o._iRigidBodyImp.AngularFactor = value;
            }
        }

        public float Bounciness
        {
            get
            {
                return _iRigidBodyImp.Bounciness;
            }
            set
            {
                var o = (RigidBody)_iRigidBodyImp.UserObject;
                o._iRigidBodyImp.Bounciness = value;
            }
        }

        //CollisionShapes

        //BoxShape
        public BoxShape AddBoxShape(float boxHalfExtents)
        {
            IBoxShapeImp iBoxShapeImp = _iRigidBodyImp.AddBoxShape(boxHalfExtents);
            var retval = new BoxShape();
            retval.BoxShapeImp = iBoxShapeImp;
            iBoxShapeImp.UserObject = retval;
            return retval;
        }
        public BoxShape AddBoxShape(float boxHalfExtentsX, float boxHalfExtentsY, float boxHalfExtentsZ)
        {
            IBoxShapeImp iBoxShapeImp = _iRigidBodyImp.AddBoxShape(boxHalfExtentsX, boxHalfExtentsY, boxHalfExtentsZ);
            var retval = new BoxShape();
            retval.BoxShapeImp = iBoxShapeImp;
            iBoxShapeImp.UserObject = retval;
            return retval;
        }
        public BoxShape AddBoxShape(float3 boxHalfExtents)
        {
            IBoxShapeImp iBoxShapeImp = _iRigidBodyImp.AddBoxShape(boxHalfExtents);
            var retval = new BoxShape();
            retval.BoxShapeImp = iBoxShapeImp;
            iBoxShapeImp.UserObject = retval;
            return retval;
        }

        //SphereShape
        public SphereShape AddSphereShape(float radius)
        {
            ISphereShapeImp iSphereShapeImp = _iRigidBodyImp.AddSphereShape(radius);
            var retval = new SphereShape();
            retval.SphereShapeImp = iSphereShapeImp;
            iSphereShapeImp.UserObject = retval;
            return retval;
        }

        //CapsuleShape
        public CapsuleShape AddCapsuleShape(float radius, float height)
        {
            ICapsuleShapeImp iCapsuleShapeImp = _iRigidBodyImp.AddCapsuleShape(radius, height);
            var retval = new CapsuleShape();
            retval.CapsuleShapeImp = iCapsuleShapeImp;
            iCapsuleShapeImp.UserObject = retval;
            return retval;
        }



        /*public void SetCollisionShape(CollisionShape colShape)
        {
            var o = (RigidBody)_iRigidBodyImp.UserObject;
            o._iRigidBodyImp.SetCollisionShape(colShape);  
        }*/


    }
}
