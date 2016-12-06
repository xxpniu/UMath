using System.Collections;
using System.Collections.Generic;
using System;

namespace UMath
{
    /// <summary>
    /// Math helper.
    /// </summary>
    public sealed class MathHelper
    {
        /// <summary>
        /// The epsilon.
        /// </summary>
        public const float Epsilon = 0.0000001f;
        /// <summary>
        /// The rad2 deg.
        /// </summary>
        public const float Rad2Deg = 57.29578f;
        /// <summary>
        /// The deg2 RAD.
        /// </summary>
        public const float Deg2Rad = 0.01745329f;

        /// <summary>
        /// Angles to 0-360
        /// </summary>
        /// <returns>The format.</returns>
        /// <param name="angle">Angle.</param>
        public static float AngleFormat(float angle)
        {
            var a = (double)angle % 360.0;
            if (a <Epsilon)
            {
                a = (float)( 360.0 + a);
            }

            if (Math.Abs(360 - a) < Epsilon)
                return 0;
            return (float)a;
        }

        /// <summary>
        /// Cals the angle with axis y.
        /// </summary>
        /// <returns>The angle with axis y.</returns>
        /// <param name="forward">Forward.</param>
        public static float CalAngleWithAxisY(UVector3 forward)
        {
            forward.y = 0;
            forward.Normalized();
            var acos = Math.Acos(forward.z) * MathHelper.Rad2Deg;
            if (forward.x > 0)
                return (float)acos;
            else
                return 360f - (float)(acos);

        }

        /// <summary>
        /// Deltas the angle.
        /// </summary>
        /// <returns>The angle.</returns>
        /// <param name="a">The alpha component.</param>
        /// <param name="b">The blue component.</param>
        public static float DeltaAngle(float a, float b)
        {
            var r =(AngleFormat(b) - AngleFormat(a)) % 180;
            return r;
        }
    }
}
