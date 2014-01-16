//----------------------------------------------
//            Xffect Editor
// Copyright © 2012- Shallway Studio
// http://shallway.net
//----------------------------------------------

using UnityEngine;
using System.Collections;
using Xft;

namespace Xft
{

    public enum GAFTTYPE
    {
        Planar,
        Spherical
    }

    public class GravityAffector : Affector
    {
        protected GAFTTYPE GType;
        protected MAGTYPE MType;
        protected float Magnitude;
        protected AnimationCurve MagCurve;
        protected Vector3 Dir;
        protected Transform GravityObj;
        protected bool IsAccelerate = true;

        public void SetAttraction(Transform goal)
        {
            GravityObj = goal;
        }

        public GravityAffector(Transform obj, GAFTTYPE gtype, MAGTYPE mtype,bool isacc, Vector3 dir, float mag,AnimationCurve curve,EffectNode node)
            : base(node, AFFECTORTYPE.GravityAffector)
        {
            GType = gtype;
            MType = mtype;
            Magnitude = mag;
            MagCurve = curve;
            Dir = dir;
            Dir.Normalize();
            GravityObj = obj;
            IsAccelerate = isacc;
        }

        public override void Update(float deltaTime)
        {
//             //新加功能:先转向,再
//             if (Node.Owner.SpriteType == (int)STYPE.BILLBOARD_SELF)
//             {
//                // Vector3.MoveTowards(Node.OriDirection,GravityObj.position-Node.Position,);
//                 Vector3.RotateTowards()
//             }


            Vector3 gDir = Node.OriDirection; 
            //
            float strength = 0f;

            if (MType == MAGTYPE.Fixed)
                strength = Magnitude;
            else
                strength = MagCurve.Evaluate(Node.GetElapsedTime());
   
            if (GType == GAFTTYPE.Planar)
            {
                Vector3 syncDir = Node.Owner.ClientTransform.rotation * Dir;
                gDir = syncDir;

                if (IsAccelerate)
                    Node.Velocity += syncDir * strength * deltaTime;
                else
                    Node.Position += syncDir * strength * deltaTime;
                 
            }
            else if (GType == GAFTTYPE.Spherical)
            {
                Vector3 dir;
                dir = GravityObj.position - Node.GetOriginalPos();
                gDir = dir;

                if (IsAccelerate)
                    Node.Velocity += dir * strength * deltaTime;
                else
                {
                    Node.Position += dir.normalized * strength * deltaTime;
                }  
            }

            if (Node.Owner.ChangeDirectionStrength != 0.0f)
            {
                //修正sprite的direction 
                if (strength < 0)
                {
                    gDir = -gDir;
                }
                Node.OriDirection = Vector3.RotateTowards(Node.OriDirection, gDir, Mathf.Abs(strength) * Node.Owner.ChangeDirectionStrength, 0.0f);
                Node.recalRotation(false); 
            }
             

        }
    }
}
