using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMath;

namespace Extands
{
    
    public static class Extands
    {
        public static UVector3 ToUV3(this Vector3 v)
        {
            return new UVector3(v.x, v.y, v.z);
        }

        public static UQuaternion ToUQ(this Quaternion u)
        {
            return new UQuaternion(u.x, u.y, u.z, u.w);
        }

        public static Vector3 ToV3(this UVector3 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        public static Quaternion ToQ(this UQuaternion u)
        {
            return new Quaternion(u.x, u.y, u.z, u.w);
        }

    }
}