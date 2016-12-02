using System;

namespace UMath
{
    [Serializable]
    public struct UQuaternion
    {
        public float x,y,z,w;

        public UQuaternion(float x,float y,float z,float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        #region public 
        /// <summary>
        /// Gets or sets the xyzw.
        /// </summary>
        /// <value>The xyzw.</value>
        public UVector4 Xyzw
        {
            get
            { 
                return new UVector4(x, y, z, w);
            }
            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
                w = value.w;
            }
        }

        /// <summary>
        /// Inverts the Vector3 component of this Quaternion.
        /// </summary>
        public void Conjugate()
        {
            x = -x;
            y = -y;
            z = -z;
        }
        /// <summary>
        /// Invert this instance.
        /// </summary>
        public void Invert()
        {
            w = -w;
        }
        /// <summary>
        /// Tos the martix.
        /// </summary>
        /// <returns>The martix.</returns>
        public UMatrix4x4 ToMartix()
        {
            //var m = UMatrix4x4.identity;
            float x2 = x * x;
            float y2 = y * y;
            float z2 = z * z;
            float xy = x * y;
            float xz = x * z;
            float yz = y * z;
            float wx = w * x;
            float wy = w * y;
            float wz = w * z;

            return new UMatrix4x4(1.0f - 2.0f * (y2 + z2), 2.0f * (xy - wz), 2.0f * (xz + wy), 0.0f,
                2.0f * (xy + wz), 1.0f - 2.0f * (x2 + z2), 2.0f * (yz - wx), 0.0f,
                2.0f * (xz - wy), 2.0f * (yz + wx), 1.0f - 2.0f * (x2 + y2), 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f);
        }
        #endregion


        #region static

        public static UQuaternion identity =new  UQuaternion(0,0,0,0);

        public static UQuaternion AngleAxis(float angle,UVector3 axis)
        {
            if (Math.Abs(axis.sqrMagnitude) < MathHelper.Epsilon)
                return identity;
            
            axis.Normalized();
            var sin = (float)Math.Sin(angle * MathHelper.Deg2Rad * 0.5f);
            var cos = (float)Math.Cos(angle * MathHelper.Deg2Rad * 0.5f);
            float w = cos;
            float x = axis.x * sin;
            float y = axis.y * sin;
            float z = axis.z * sin;

            return new UQuaternion(x, y, z, w);
        }

        public static UQuaternion Euler(float x,float y,float z)
        {
            return AngleAxis(x, UVector3.right)
            * AngleAxis(y, UVector3.up)
            * AngleAxis(z, UVector3.forward);
        }

        public static UQuaternion Euler(UVector3 eulerAngles)
        {
            return Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
        }

        public static UQuaternion operator *(UQuaternion l,UQuaternion r)
        {
            return new UQuaternion(
                l.w * r.x + l.x * r.w + l.y * r.z - l.z * r.y,
                l.w * r.y + l.y * r.w + l.z * r.x - l.x * r.z,
                l.w * r.z + l.z * r.w + l.x * r.y - l.y * r.x,
                l.w * r.w - l.x * r.x - l.y * r.y - l.z * r.z);
        }
            
        #endregion
       
    }
}

