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
        public const float Rad2Deg = 57.29578f;
        public const float Deg2Rad = 0.01745329f;

        /// <summary>
        /// Angles to 0-360
        /// </summary>
        /// <returns>The format.</returns>
        /// <param name="angle">Angle.</param>
        public static float AngleFormat(float angle)
        {
            var a = (double)angle % 360.0;
            if (a <-Epsilon)
            {
                return (float)( 360.0 + a);
            }

            return (float)a;
        }
    }
}
