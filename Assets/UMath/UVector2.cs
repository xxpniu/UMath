using System;

namespace UMath
{
    public struct UVector2
    {
        public float x,y;

        public UVector2(float x,float y)
        {
            this.x = x;
            this.y = y;
        }


        #region Operator
        public static UVector2 operator +(UVector2 v1, UVector2 v2)
        {
            return new UVector2(v1.x + v2.x, v1.y + v2.y);
        }
        #endregion
    }
}

