﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using BulletSharp;
using BulletSharp.MultiThreaded;
using Fusee.Engine;
using Fusee.Math;
using BulletSharp.Serialize;



namespace Fusee.Engine
{

    public class DynamicWorldImp : IDynamicWorldImp
    {
        internal Translater Translater = new Translater();

        internal DynamicsWorld BtWorld;
        internal CollisionConfiguration BtCollisionConf;
        internal CollisionDispatcher BtDispatcher;
        internal BroadphaseInterface BtBroadphase;
        internal ConstraintSolver BtSolver;
        //internal AlignedCollisionShapeArray BtCollisionShapes { get; private set; }
        List<CollisionShape> BtCollisionShapes = new List<CollisionShape>();


        internal DynamicWorldImp()
        {
            Debug.WriteLine("DynamicWorldImp");

            // collision configuration contains default setup for memory, collision setup
            BtCollisionConf = new DefaultCollisionConfiguration();
            BtDispatcher = new CollisionDispatcher(BtCollisionConf);
            BtBroadphase = new DbvtBroadphase();
            BtSolver = new SequentialImpulseConstraintSolver();
            //BtCollisionShapes = new AlignedCollisionShapeArray();

            
            /*BtWorld = new DiscreteDynamicsWorld(BtDispatcher, BtBroadphase, BtSolver, BtCollisionConf)
            {
                Gravity = new Vector3(0, -9.81f   *10.0f , 0)
            };*/
            BtWorld = new DiscreteDynamicsWorld(BtDispatcher, BtBroadphase, BtSolver, BtCollisionConf)
            {
                Gravity = new Vector3(0, -9.81f * 10.0f, 0)
            };
            BtWorld.SolverInfo.NumIterations = 8;

            BtWorld.PerformDiscreteCollisionDetection();
            //GImpactCollisionAlgorithm.RegisterAlgorithm(BtDispatcher);
           //BtWorld.SetInternalTickCallback(MyTickCallBack);


        }

        private void MyTickCallBack(DynamicsWorld world, float timeStep)
        {
            Debug.WriteLine("MyTickCallBack");
        }


        public IRigidBodyImp AddRigidBody(float mass, float3 worldTransform, Fusee.Math.Quaternion orientation,ICollisionShapeImp colShape/*, float3 intertia*/)
        {
            // Use bullet to do what needs to be done:

            var btMatrixPosition = Matrix.Translation(worldTransform.x, worldTransform.y, worldTransform.z);
            var btMatrixOrientation = Translater.QuaternionToBtQuaternion(orientation);
            var qm = Matrix.RotationAxis(Translater.Float3ToBtVector3(orientation.xyz), orientation.w);
            var btMotionstatematrix = Matrix.RotationQuaternion(btMatrixOrientation) * Matrix.Translation(worldTransform.x, worldTransform.y, worldTransform.z);
            //Matrix.RotationQuaternion(BulletSharp.Quaternion.Identity));
           // btMatrixOrientation = Matrix.RotationQuaternion(Translater.QuaternionToBtQuaternion(orientation));
            var btMotionState = new DefaultMotionState(btMotionstatematrix); 
          
           
               // new DefaultMotionState(//(Transform(btQuaternion(0, 0, 0, 1), btVector3(0, -1, 0)));

            //var btMotionState = new DefaultMotionState(btMatrix); 
            
            var shapeType = colShape.GetType().ToString();
            
            CollisionShape btColShape;
            
            var isStatic = false;
            switch (shapeType)
            {
                //Primitives
                case "Fusee.Engine.BoxShapeImp":
                    var box = (BoxShapeImp) colShape;
                    var btBoxHalfExtents = Translater.Float3ToBtVector3(box.HalfExtents);
                    btColShape = new BoxShape(btBoxHalfExtents);
                    break;
                case "Fusee.Engine.CapsuleShapeImp":
                    var capsule = (CapsuleShapeImp) colShape;
                    btColShape = new CapsuleShape(capsule.Radius, capsule.HalfHeight);
                    break;
                case "Fusee.Engine.ConeShapeImp":
                    var cone = (ConeShapeImp) colShape;
                    btColShape = new ConeShape(cone.Radius, cone.Height);
                    break;
                case "Fusee.Engine.CylinderShapeImp":
                    var cylinider = (CylinderShapeImp) colShape;
                    var btCylinderHalfExtents = Translater.Float3ToBtVector3(cylinider.HalfExtents);
                    btColShape = new CylinderShape(btCylinderHalfExtents);
                    break;
                case "Fusee.Engine.MultiSphereShapeImp":
                    var multiSphere = (MultiSphereShapeImp) colShape;
                    var btPositions = new Vector3[multiSphere.SphereCount];
                    var btRadi = new float[multiSphere.SphereCount];
                    for (int i = 0; i < multiSphere.SphereCount; i++)
                    {
                        var pos = Translater.Float3ToBtVector3(multiSphere.GetSpherePosition(i));
                        btPositions[i] = pos;
                        btRadi[i] = multiSphere.GetSphereRadius(i);
                    }
                    btColShape = new MultiSphereShape(btPositions, btRadi);
                    break;
                case "Fusee.Engine.SphereShapeImp":
                    var sphere = (SphereShapeImp) colShape;
                    var btRadius = sphere.Radius;
                    btColShape = new SphereShape(btRadius);
                    break;
                
                //Misc
                case "Fusee.Engine.CompoundShapeImp":
                    var compShape = (CompoundShapeImp) colShape;
                    btColShape = new CompoundShape();
                    break;
                case "Fusee.Engine.EmptyShapeImp":
                    btColShape = new EmptyShape();
                    break;

                //Meshes
                case "Fusee.Engine.ConvexHullShapeImp":
                    var convHull = (ConvexHullShapeImp) colShape;
                    var btPoints= new Vector3[convHull.GetNumPoints()];
                    for (int i = 0; i < convHull.GetNumPoints(); i++)
                    {
                        var point = convHull.GetScaledPoint(i);
                        btPoints[i] = Translater.Float3ToBtVector3(point);
                    }
                    btColShape = new ConvexHullShape(btPoints);
                    break;
                case "Fusee.Engine.StaticPlaneShapeImp":  
                    var staticPlane = (StaticPlaneShapeImp) colShape;
                    Debug.WriteLine("staticplane: " + staticPlane.Margin);
                    var btNormal = Translater.Float3ToBtVector3(staticPlane.PlaneNormal);
                    btColShape = new StaticPlaneShape(btNormal, staticPlane.PlaneConstant);
                    isStatic = true;
                    //btColShape.Margin = 0.04f;
                    //Debug.WriteLine("btColshape" + btColShape.Margin);
                    break;               
                case "Fusee.Engine.GImpactMeshShapeImp":
                    var gImpMesh = (GImpactMeshShapeImp)colShape;
                    //gImpMesh.BtGImpactMeshShape.UpdateBound();
                    var btGimp = new GImpactMeshShape(gImpMesh.BtGImpactMeshShape.MeshInterface);
                    
                    btGimp.UpdateBound();
                    btColShape = btGimp;
                    
                    break;
                //Default
                default:
                    Debug.WriteLine("defaultImp");
                    btColShape = new EmptyShape();
                    break;
            }
            var btLocalInertia = btColShape.CalculateLocalInertia(mass) * 10;
            RigidBodyConstructionInfo btRbcInfo = new RigidBodyConstructionInfo(mass * 10, btMotionState, btColShape, btLocalInertia);
            
            var btRigidBody = new RigidBody(btRbcInfo);
            BtWorld.AddRigidBody(btRigidBody);
            btRbcInfo.Dispose();

            var retval = new RigidBodyImp();
            retval._rbi = btRigidBody;
            btRigidBody.UserObject = retval;
            return retval;
        }

        public int StepSimulation(float timeSteps, int maxSubSteps, float fixedTimeSteps)
        {
            return BtWorld.StepSimulation(timeSteps, maxSubSteps);//, maxSubSteps, fixedTimeSteps);  
        }

        public bool CallbackFunc(ManifoldPoint cp, CollisionObject obj1, int id1, int index1, CollisionObject obj2,
            int id2, int index2)
        {
            throw new NotImplementedException();
        }


        public void CheckCollisions()
        {
            Debug.WriteLine("CheckCollisions()");
            int numManifolds = BtWorld.Dispatcher.NumManifolds;
            for (int i = 0; i < numManifolds; i++)
            {
                PersistentManifold contactManifold = BtWorld.Dispatcher.GetManifoldByIndexInternal(i);
                CollisionObject obA = contactManifold.Body0 as CollisionObject;
                CollisionObject obB = contactManifold.Body1 as CollisionObject;

                int numContacts = contactManifold.NumContacts;
                for (int j = 0; j < numContacts; j++)
                {
                    ManifoldPoint pt = contactManifold.GetContactPoint(j);
                    if (pt.Distance < 0.0f)
                    {
                        Vector3 ptA = pt.PositionWorldOnA;
                        Vector3 ptB = pt.PositionWorldOnB;
                        Vector3 normalOnB = pt.NormalWorldOnB;
                        Debug.WriteLine(obA.CollisionShape);
                    }
                }
            }
            
        }

        public IRigidBodyImp GetRigidBody(int i)
        {
            var colisionObject = BtWorld.CollisionObjectArray[i];
            var btRigidBody = (RigidBody) colisionObject;
            return (RigidBodyImp) btRigidBody.UserObject;
        }

        public int NumberRigidBodies()
        {
            var number = BtWorld.CollisionObjectArray.Count;
            return number;
        }

        #region Constraints
        //Point2point
        public IPoint2PointConstraintImp AddPoint2PointConstraint(IRigidBodyImp rigidBodyA, float3 pivotInA)
        {
            var rigidBodyAImp = (RigidBodyImp)rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;

            var btP2PConstraint = new Point2PointConstraint(btRigidBodyA, new Vector3(pivotInA.x, pivotInA.y, pivotInA.z));
            BtWorld.AddConstraint(btP2PConstraint);

            var retval = new Point2PointConstraintImp();
            retval._p2pci = btP2PConstraint;
            btP2PConstraint.UserObject = retval;
            return retval;
        }
        public IPoint2PointConstraintImp AddPoint2PointConstraint(IRigidBodyImp rigidBodyA, IRigidBodyImp rigidBodyB, float3 pivotInA,float3 pivotInB)
        {
            var rigidBodyAImp = (RigidBodyImp) rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;

            var rigidBodyBImp = (RigidBodyImp) rigidBodyB;
            var btRigidBodyB = rigidBodyBImp._rbi;

            var btP2PConstraint = new Point2PointConstraint(btRigidBodyA, btRigidBodyB,
                new Vector3(pivotInA.x, pivotInA.y, pivotInA.z), new Vector3(pivotInB.x, pivotInB.y, pivotInB.z));
            BtWorld.AddConstraint(btP2PConstraint);
            var retval = new Point2PointConstraintImp();
            retval._p2pci = btP2PConstraint;
            btP2PConstraint.UserObject = retval;
            return retval;
        }

        //TODO: What about inheritance problems -> should return any constraint type
        public IPoint2PointConstraintImp GetConstraint(int i)
        {
            return (Point2PointConstraintImp)BtWorld.GetConstraint(0).UserObject;
        }


        //HingeConstraint
        public IHingeConstraintImp AddHingeConstraint(IRigidBodyImp rigidBodyA, float4x4 frameInA, bool useReferenceFrameA)
        {
            var rigidBodyAImp = (RigidBodyImp)rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;
            var btFframeInA = Translater.Float4X4ToBtMatrix(frameInA);
            var btHingeConstraint = new HingeConstraint(btRigidBodyA, btFframeInA, useReferenceFrameA);
            BtWorld.AddConstraint(btHingeConstraint);

            var retval = new HingeConstraintImp();
            retval._hci = btHingeConstraint;
            btHingeConstraint.UserObject = retval;
            return retval;
        }
        public IHingeConstraintImp AddHingeConstraint(IRigidBodyImp rigidBodyA, float3 pivotInA, float3 axisInA, bool useReferenceFrameA)
        {
            var rigidBodyAImp = (RigidBodyImp)rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;

            var btHingeConstraint = new HingeConstraint(btRigidBodyA, new Vector3(pivotInA.x, pivotInA.y, pivotInA.z), new Vector3(axisInA.x, axisInA.y, axisInA.z), useReferenceFrameA);
            BtWorld.AddConstraint(btHingeConstraint);

            var retval = new HingeConstraintImp();
            retval._hci = btHingeConstraint;
            btHingeConstraint.UserObject = retval;
            return retval;
        }
        public IHingeConstraintImp AddHingeConstraint(IRigidBodyImp rigidBodyA, IRigidBodyImp rigidBodyB, float3 pivotInA, float3 pivotInB, float3 axisInA, float3 AxisInB, bool useReferenceFrameA)
        {
            var rigidBodyAImp = (RigidBodyImp)rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;

            var rigidBodyBImp = (RigidBodyImp)rigidBodyB;
            var btRigidBodyB = rigidBodyBImp._rbi;

            var btHingeConstraint = new HingeConstraint(btRigidBodyA, btRigidBodyB, new Vector3(pivotInA.x, pivotInA.y, pivotInA.z), new Vector3(pivotInB.x, pivotInB.y, pivotInB.z), new Vector3(axisInA.x, axisInA.y, axisInA.z), new Vector3(AxisInB.x, AxisInB.y, AxisInB.z), useReferenceFrameA);
            BtWorld.AddConstraint(btHingeConstraint);

            var retval = new HingeConstraintImp();
            retval._hci = btHingeConstraint;
            btHingeConstraint.UserObject = retval;
            return retval;
        }
        public IHingeConstraintImp AddHingeConstraint(IRigidBodyImp rigidBodyA, IRigidBodyImp rigidBodyB, float4x4 brAFrame, float4x4 brBFrame, bool useReferenceFrameA)
        {
            var rigidBodyAImp = (RigidBodyImp)rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;

            var rigidBodyBImp = (RigidBodyImp)rigidBodyB;
            var btRigidBodyB = rigidBodyBImp._rbi;

            var btRbAFrame = Translater.Float4X4ToBtMatrix(brAFrame);
            var btRbBFrame = Translater.Float4X4ToBtMatrix(brBFrame);

            var btHingeConstraint = new HingeConstraint(btRigidBodyA, btRigidBodyB,btRbAFrame, btRbBFrame, useReferenceFrameA);
            BtWorld.AddConstraint(btHingeConstraint);

            var retval = new HingeConstraintImp();
            retval._hci = btHingeConstraint;
            btHingeConstraint.UserObject = retval;
            return retval;
        }


        //SliderConstraint
        public ISliderConstraintImp AddSliderConstraint(IRigidBodyImp rigidBodyA, IRigidBodyImp rigidBodyB, float4x4 frameInA, float4x4 frameInB, bool useLinearReferenceFrameA = false)
        {
            var rigidBodyAImp = (RigidBodyImp)rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;

            var rigidBodyBImp = (RigidBodyImp)rigidBodyB;
            var btRigidBodyB = rigidBodyBImp._rbi;

            var btFrameInA = Translater.Float4X4ToBtMatrix(frameInA);
            var btFrameInB = Translater.Float4X4ToBtMatrix(frameInB);

            var btSliderConstraint = new SliderConstraint(btRigidBodyA, btRigidBodyB, btFrameInA, btFrameInB, useLinearReferenceFrameA);

            BtWorld.AddConstraint(btSliderConstraint);

            var retval = new SliderConstraintImp();
            retval._sci = btSliderConstraint;
            btSliderConstraint.UserObject = retval;
            return retval;
        }
        public ISliderConstraintImp AddSliderConstraint(IRigidBodyImp rigidBodyA, float4x4 frameInA, bool useLinearReferenceFrameA)
        {
            var rigidBodyAImp = (RigidBodyImp)rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;
            var btFrameInA = Translater.Float4X4ToBtMatrix(frameInA);
            var btSliderConstraint = new SliderConstraint(btRigidBodyA, btFrameInA, useLinearReferenceFrameA);
            BtWorld.AddConstraint(btSliderConstraint);

            var retval = new SliderConstraintImp();
            retval._sci = btSliderConstraint;
            btSliderConstraint.UserObject = retval;
            return retval;
        }

        //GearConstraint
        public IGearConstraintImp AddGearConstraint(IRigidBodyImp rigidBodyA, IRigidBodyImp rigidBodyB, float3 axisInA, float3 axisInB, float ratio)
        {
            var rigidBodyAImp = (RigidBodyImp)rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;

            var rigidBodyBImp = (RigidBodyImp)rigidBodyB;
            var btRigidBodyB = rigidBodyBImp._rbi;

            var btAxisInA = Translater.Float3ToBtVector3(axisInA);
            var btAxisInB = Translater.Float3ToBtVector3(axisInB);

            var btGearConstraint = new GearConstraint(btRigidBodyA, btRigidBodyB, btAxisInA, btAxisInB, ratio);

            BtWorld.AddConstraint(btGearConstraint);

            var retval = new GearConstraintImp();
            retval._gci = btGearConstraint;
            btGearConstraint.UserObject = retval;
            return retval;
        }

        //ConeTwistConstraint
        public IConeTwistConstraintImp AddConeTwistConstraint(IRigidBodyImp rigidBodyA, float4x4 rbAFrame)
        {
            var rigidBodyAImp = (RigidBodyImp)rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;
            var btRbAFrame = Translater.Float4X4ToBtMatrix(rbAFrame);

            var btCTConstraint = new ConeTwistConstraint(btRigidBodyA, btRbAFrame);
            
            BtWorld.AddConstraint(btCTConstraint);

            var retval = new ConeTwistConstraintImp();
            retval._cti = btCTConstraint;
            btCTConstraint.UserObject = retval;
            return retval;
        }
        public IConeTwistConstraintImp AddConeTwistConstraint(IRigidBodyImp rigidBodyA, IRigidBodyImp rigidBodyB, float4x4 rbAFrame,float4x4 rbBFrame)
        {
            var rigidBodyAImp = (RigidBodyImp)rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;

            var rigidBodyBImp = (RigidBodyImp)rigidBodyB;
            var btRigidBodyB = rigidBodyBImp._rbi;

            var btRbAFrame = Translater.Float4X4ToBtMatrix(rbAFrame);
            var btRbBFrame = Translater.Float4X4ToBtMatrix(rbBFrame);

            var btCTConstraint = new ConeTwistConstraint(btRigidBodyA, btRigidBodyB, btRbAFrame, btRbBFrame);

            BtWorld.AddConstraint(btCTConstraint);

            var retval = new ConeTwistConstraintImp();
            retval._cti = btCTConstraint;
            btCTConstraint.UserObject = retval;
            return retval;
        }


        //GenericoDofConstraint
        public IGeneric6DofConstraintImp AddGeneric6DofConstraint(IRigidBodyImp rigidBodyA, float4x4 frameInA, bool useReferenceFrameA)
        {
            var rigidBodyAImp = (RigidBodyImp)rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;
            var btFframeInA = Translater.Float4X4ToBtMatrix(frameInA);
            var btGeneric6DofConstraint = new Generic6DofConstraint(btRigidBodyA, btFframeInA, useReferenceFrameA);
            BtWorld.AddConstraint(btGeneric6DofConstraint);

            var retval = new Generic6DofConstraintImp();
            retval._g6dofci = btGeneric6DofConstraint;
            btGeneric6DofConstraint.UserObject = retval;
            return retval;
        }
        public IGeneric6DofConstraintImp AddGeneric6DofConstraint(IRigidBodyImp rigidBodyA, IRigidBodyImp rigidBodyB, float4x4 frameInA, float4x4 frameInB, bool useReferenceFrameA = false)
        {
            var rigidBodyAImp = (RigidBodyImp)rigidBodyA;
            var btRigidBodyA = rigidBodyAImp._rbi;
            var rigidBodyBImp = (RigidBodyImp)rigidBodyB;
            var btRigidBodyB = rigidBodyAImp._rbi;

            var btGeneric6DofConstraint = new Generic6DofConstraint(btRigidBodyA, btRigidBodyB, Translater.Float4X4ToBtMatrix(frameInA), Translater.Float4X4ToBtMatrix(frameInB), useReferenceFrameA);
            BtWorld.AddConstraint(btGeneric6DofConstraint);

            var retval = new Generic6DofConstraintImp();
            retval._g6dofci = btGeneric6DofConstraint;
            btGeneric6DofConstraint.UserObject = retval;
            return retval;
        }
        #endregion Constraints

        #region CollisionShapes
        //CollisionShapes
        //Primitives
        //BoxShape
        public IBoxShapeImp AddBoxShape(float boxHalfExtents)
        {
            var btBoxShape = new BoxShape(boxHalfExtents);
            BtCollisionShapes.Add(btBoxShape);
            
            var retval = new BoxShapeImp();
            retval.BtBoxShape = btBoxShape;
            btBoxShape.UserObject = retval;
            return retval;
        }
        public IBoxShapeImp AddBoxShape(float3 boxHalfExtents)
        {
            var btBoxShape = new BoxShape(Translater.Float3ToBtVector3(boxHalfExtents));
            BtCollisionShapes.Add(btBoxShape);

            var retval = new BoxShapeImp();
            retval.BtBoxShape = btBoxShape;
            btBoxShape.UserObject = retval;
            return retval;
        }
        public IBoxShapeImp AddBoxShape(float boxHalfExtentsX, float boxHalfExtentsY, float boxHalfExtentsZ)
        {
            var btBoxShape = new BoxShape(boxHalfExtentsX, boxHalfExtentsY, boxHalfExtentsZ);
            BtCollisionShapes.Add(btBoxShape);

            var retval = new BoxShapeImp();
            retval.BtBoxShape = btBoxShape;
            btBoxShape.UserObject = retval;
            return retval;
        }

        //SphereShape
        public ISphereShapeImp AddSphereShape(float radius)
        {
            var btSphereShape = new SphereShape(radius);
            BtCollisionShapes.Add(btSphereShape);
            
            var retval = new SphereShapeImp();
            retval.BtSphereShape = btSphereShape;
            btSphereShape.UserObject = retval;
            return retval;
        }

        //CapsuleShape
        public ICapsuleShapeImp AddCapsuleShape(float radius, float height)
        {
            var btCapsuleShape = new CapsuleShape(radius, height);
            BtCollisionShapes.Add(btCapsuleShape);

            var retval = new CapsuleShapeImp();
            retval.BtCapsuleShape = btCapsuleShape;
            btCapsuleShape.UserObject = retval;
            return retval;
        }

        //CylinderShape
        public ICylinderShapeImp AddCylinderShape(float halfExtents)
        {
            var btCylinderShape = new CylinderShape(halfExtents);
            BtCollisionShapes.Add(btCylinderShape);

            var retval = new CylinderShapeImp();
            retval.BtCylinderShape = btCylinderShape;
            btCylinderShape.UserObject = retval;
            return retval;
        }
        public ICylinderShapeImp AddCylinderShape(float3 halfExtents)
        {
            var btCylinderShape = new CylinderShape(Translater.Float3ToBtVector3(halfExtents));
            BtCollisionShapes.Add(btCylinderShape);

            var retval = new CylinderShapeImp();
            retval.BtCylinderShape = btCylinderShape;
            btCylinderShape.UserObject = retval;
            return retval;
        }
        public ICylinderShapeImp AddCylinderShape(float halfExtentsX, float halfExtentsY, float halfExtentsZ)
        {
            var btCylinderShape = new CylinderShape(halfExtentsX, halfExtentsY, halfExtentsZ);
            BtCollisionShapes.Add(btCylinderShape);

            var retval = new CylinderShapeImp();
            retval.BtCylinderShape = btCylinderShape;
            btCylinderShape.UserObject = retval;
            return retval;
        }
  

        //ConeShape
        public IConeShapeImp AddConeShape(float radius, float height)
        {
            var btConeShape = new ConeShape(radius, height);
            BtCollisionShapes.Add(btConeShape);

            var retval = new ConeShapeImp();
            retval.BtConeShape = btConeShape;
            btConeShape.UserObject = retval;
            return retval;
        }

        //MultiSphereShape
        public IMultiSphereShapeImp AddMultiSphereShape(float3[] positions, float[] radi)
        {
            var btPositions = new Vector3[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                btPositions[i] = new Vector3(positions[i].x, positions[i].y, positions[i].z);
            }
            var btMultiSphereShape = new MultiSphereShape(btPositions, radi);
            BtCollisionShapes.Add(btMultiSphereShape);

            var retval = new MultiSphereShapeImp();
            retval.BtMultiSphereShape = btMultiSphereShape;
            btMultiSphereShape.UserObject = retval;
            return retval;
        }

        public ICompoundShapeImp AddCompoundShape(bool enableDynamicAabbTree)
        {
            var btCompoundShape = new CompoundShape(enableDynamicAabbTree);
            BtCollisionShapes.Add(btCompoundShape);

            var retval = new CompoundShapeImp();
            retval.BtCompoundShape = btCompoundShape;
            btCompoundShape.UserObject = retval;
            return retval;
        }

        public IEmptyShapeImp AddEmptyShape()
        {
            var btEmptyShape = new EmptyShape();
            BtCollisionShapes.Add(btEmptyShape);

            var retval = new EmptyShapeImp();
            retval.BtEmptyShape = btEmptyShape;
            btEmptyShape.UserObject = retval;
            return retval;
        }

        public IConvexHullShapeImp AddConvexHullShape()
        {
            var btConvexHullShape = new ConvexHullShape();
            BtCollisionShapes.Add(btConvexHullShape);

            var retval = new ConvexHullShapeImp();
            retval.BtConvexHullShape = btConvexHullShape;
            btConvexHullShape.UserObject = retval;
            return retval;
        }

        public IConvexHullShapeImp AddConvexHullShape(float3[] points)
        {
            var btPoints = new Vector3[points.Count()];
            for (int i = 0; i < btPoints.Count(); i++)
            {
                var point = Translater.Float3ToBtVector3(points[i]);
                btPoints[i] = point;
            }
            var btConvexHullShape = new ConvexHullShape(btPoints);
            BtCollisionShapes.Add(btConvexHullShape);

            var retval = new ConvexHullShapeImp();
            retval.BtConvexHullShape = btConvexHullShape;
            btConvexHullShape.UserObject = retval;
            return retval;
        }

        public IStaticPlaneShapeImp AddStaticPlaneShape(float3 planeNormal, float planeConstant)
        {
            var btPlaneNormal = Translater.Float3ToBtVector3(planeNormal);
            var btStaticPlaneShape = new StaticPlaneShape(btPlaneNormal, planeConstant);
            btStaticPlaneShape.Margin = 0.04f;
            BtCollisionShapes.Add(btStaticPlaneShape);
            Debug.WriteLine("btStaticPlaneShape.Margin" + btStaticPlaneShape.Margin);
            var retval = new StaticPlaneShapeImp();
            retval.BtStaticPlaneShape = btStaticPlaneShape;
            btStaticPlaneShape.UserObject = retval;
            return retval;
        }

        public IGImpactMeshShapeImp AddGImpactMeshShape(int[] meshTriangles, float3[] meshVertices)
        {
            Vector3[] btMeshVertices = new Vector3[meshVertices.Length];
            for (int i = 0; i < meshVertices.Length; i++)
            {
                btMeshVertices[i].X = meshVertices[i].x;
                btMeshVertices[i].Y = meshVertices[i].y;
                btMeshVertices[i].Z = meshVertices[i].z;
            }
            var btTriangleIndexVertexArray = new TriangleIndexVertexArray(meshTriangles, btMeshVertices);
            var btGimpactMeshShape = new GImpactMeshShape(btTriangleIndexVertexArray);
            //btGimpactMeshShape.UpdateBound();
            BtCollisionShapes.Add(btGimpactMeshShape);
            var retval = new GImpactMeshShapeImp();
            retval.BtGImpactMeshShape = btGimpactMeshShape;
            btGimpactMeshShape.UserObject = retval;
            return retval;
        }
        #endregion CollisionShapes


        public int NumberConstraints()
        {
            return BtWorld.NumConstraints;
        }

        /*This Funcion is called at:
         * public static void Main()
           {
               var app = new BulletTest();
                app.Run();
                _physic.World.Dispose();
           }
         * definetly the wrong place!!!!!!!!
         * TODO: call it at the right place
         */
        public void Dispose()
        {
            if (BtWorld != null)
            {
                //remove/dispose constraints
                int i;
                for (i = BtWorld.NumConstraints - 1; i >= 0; i--)
                {
                    TypedConstraint constraint = BtWorld.GetConstraint(i);
                    BtWorld.RemoveConstraint(constraint);
                    constraint.Dispose(); 
                }

                //remove the rigidbodies from the dynamics world and delete them
                for (i = BtWorld.NumCollisionObjects - 1; i >= 0; i--)
                {
                    CollisionObject obj = BtWorld.CollisionObjectArray[i];
                    RigidBody body = obj as RigidBody;
                    if (body != null && body.MotionState != null)
                    {
                        body.MotionState.Dispose();
                    }
                    BtWorld.RemoveCollisionObject(obj);
                    obj.Dispose();
                }

                //delete collision shapes
                foreach (CollisionShape shape in BtCollisionShapes)
                    shape.Dispose();
                BtCollisionShapes.Clear();

                BtWorld.Dispose();
                BtBroadphase.Dispose();
                BtDispatcher.Dispose();
                BtCollisionConf.Dispose();
            }

            if (BtBroadphase != null)
            {
                BtBroadphase.Dispose();
            }
            if (BtDispatcher != null)
            {
                BtDispatcher.Dispose();
            }
            if (BtCollisionConf != null)
            {
                BtCollisionConf.Dispose();
            }
        }
    }
}
