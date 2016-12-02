using System;

namespace UMath
{
    /// <summary>
    /// column-major left-hand 
    /// </summary>
    [Serializable]
    public struct UMatrix4x4
    {
        public float m11, m12, m13, m14,
            m21, m22, m23, m24,
            m31, m32, m33, m34,
            m41, m42, m43, m44;

        public UMatrix4x4(float m11, float m12, float m13,float m14,
            float m21,float m22, float m23,float m24,
            float m31,float m32,float m33,float m34,
            float m41,float m42,float m43,float m44)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m14 = m14;

            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m24 = m24;

            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
            this.m34 = m34;

            this.m41 = m41;
            this.m42 = m42;
            this.m43 = m43;
            this.m44 = m44;
        }


        #region public 

        public UVector3 Translate
        {
            get
            {
                return new UVector3(m14, m24, m34);
            }
        }

        public UVector3 Scale
        {
            get
            {
                var x = new UVector3(m11, m21, m31).magnitude;
                var y = new UVector3(m12, m22, m32).magnitude;
                var z = new UVector3(m13, m23, m33).magnitude;
                return new UVector3(x, y, z);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="UMath.UMatrix4x4"/> with the specified row colnum.
        /// </summary>
        /// <param name="row">Row.</param>
        /// <param name="colnum">Colnum.</param>
        public float this [int row, int colnum]
        {
            set
            {
                #region set values
                switch (row)
                {
                    case 1:
                        switch (colnum)
                        {
                            case 1:
                                m11 = value;
                                break;
                            case 2:
                                m12 = value;
                                break;
                            case 3:
                                m13 = value;
                                break;
                            case 4:
                                m14 = value;
                                break;
                            default:
                                throw new IndexOutOfRangeException("colnum out of index:" + colnum);
                                break;
                        }
                        break;
                    case 2:
                        switch (colnum)
                        {
                            case 1:
                                m21 = value;
                                break;
                            case 2:
                                m22 = value;
                                break;
                            case 3:
                                m23 = value;
                                break;
                            case 4:
                                m24 = value;
                                break;
                            default:
                                throw new IndexOutOfRangeException("colnum out of index:" + colnum);
                                break;
                        }
                        break;
                    case 3:
                        switch (colnum)
                        {
                            case 1:
                                m31 = value;
                                break;
                            case 2:
                                m32 = value;
                                break;
                            case 3:
                                m33 = value;
                                break;
                            case 4:
                                m34 = value;
                                break;
                            default:
                                throw new IndexOutOfRangeException("colnum out of index:" + colnum);
                                break;
                        }
                        break;
                    case 4:
                        switch (colnum)
                        {
                            case 1:
                                m41 = value;
                                break;
                            case 2:
                                m42 = value;
                                break;
                            case 3:
                                m43 = value;
                                break;
                            case 4:
                                m44 = value;
                                break;
                            default:
                                throw new IndexOutOfRangeException("colnum out of index:" + colnum);
                                break;
                        }
                        break;
                    default:
                        throw new IndexOutOfRangeException("row out of index:" + row);
                        break;
                }
                #endregion
            }

            get
            {
                #region get
                switch (row)
                {
                    case 1:
                        switch (colnum)
                        {
                            case 1:
                                return  m11;

                            case 2:
                                return m12;

                            case 3:
                                return m13;

                            case 4:
                                return m14;
                            default:
                                throw new IndexOutOfRangeException("colnum out of index:" + colnum);
                               
                        }
                        break;
                    case 2:
                        switch (colnum)
                        {
                            case 1:
                                return m21;
                            case 2:
                                return m22;
                            case 3:
                                return m23;
                            case 4:
                                return m24;
                            default:
                                throw new IndexOutOfRangeException("colnum out of index:" + colnum);

                        }
                        break;
                    case 3:
                        switch (colnum)
                        {
                            case 1:
                                return m31;
                            case 2:
                                return m32;
                            case 3:
                                return m33;
                            case 4:
                                return m34;
                            default:
                                throw new IndexOutOfRangeException("colnum out of index:" + colnum);
                               
                        }
                        break;
                    case 4:
                        switch (colnum)
                        {
                            case 1:
                                return  m41;
                            case 2:
                                return  m42;
                            case 3:
                                return  m43;
                            case 4:
                                return m44;
                            default:
                                throw new IndexOutOfRangeException("colnum out of index:" + colnum);

                        }
                        break;
                    default:
                        throw new IndexOutOfRangeException("row out of index:" + row);

                }
                #endregion
            }
        }              
        /// <summary>
        /// Invert the specified mat.
        /// </summary>
        /// <param name="mat">Mat.</param>
        public UMatrix4x4 Invert( UMatrix4x4 mat)
        {
            var result = UMatrix4x4.zero;
            int[] colIdx = { 0, 0, 0, 0 };
            int[] rowIdx = { 0, 0, 0, 0 };
            int[] pivotIdx = { -1, -1, -1, -1 };

            // convert the matrix to an array for easy looping
            float[,] inverse =  {
                {m11, m12, m13, m14},
                {m21, m22, m23, m24},
                {m31, m32, m33, m34},
                {m41, m42, m43, m44} 
            };
            int icol = 0;
            int irow = 0;
            for (int i = 0; i < 4; i++)
            {
                // Find the largest pivot value
                float maxPivot = 0.0f;
                for (int j = 0; j < 4; j++)
                {
                    if (pivotIdx[j] != 0)
                    {
                        for (int k = 0; k < 4; ++k)
                        {
                            if (pivotIdx[k] == -1)
                            {
                                float absVal = System.Math.Abs(inverse[j, k]);
                                if (absVal > maxPivot)
                                {
                                    maxPivot = absVal;
                                    irow = j;
                                    icol = k;
                                }
                            }
                            else if (pivotIdx[k] > 0)
                            {
                                result = mat;
                                return result;
                            }
                        }
                    }
                }

                ++(pivotIdx[icol]);

                // Swap rows over so pivot is on diagonal
                if (irow != icol)
                {
                    for (int k = 0; k < 4; ++k)
                    {
                        float f = inverse[irow, k];
                        inverse[irow, k] = inverse[icol, k];
                        inverse[icol, k] = f;
                    }
                }

                rowIdx[i] = irow;
                colIdx[i] = icol;

                float pivot = inverse[icol, icol];
                // check for singular matrix
                if (pivot == 0.0f)
                {
                    throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
                }

                // Scale row so it has a unit diagonal
                float oneOverPivot = 1.0f / pivot;
                inverse[icol, icol] = 1.0f;
                for (int k = 0; k < 4; ++k)
                    inverse[icol, k] *= oneOverPivot;

                // Do elimination of non-diagonal elements
                for (int j = 0; j < 4; ++j)
                {
                    // check this isn't on the diagonal
                    if (icol != j)
                    {
                        float f = inverse[j, icol];
                        inverse[j, icol] = 0.0f;
                        for (int k = 0; k < 4; ++k)
                            inverse[j, k] -= inverse[icol, k] * f;
                    }
                }
            }

            for (int j = 3; j >= 0; --j)
            {
                int ir = rowIdx[j];
                int ic = colIdx[j];
                for (int k = 0; k < 4; ++k)
                {
                    float f = inverse[k, ir];
                    inverse[k, ir] = inverse[k, ic];
                    inverse[k, ic] = f;
                }
            }

            result.m11 = inverse[0, 0];
            result.m12 = inverse[0, 1];
            result.m13 = inverse[0, 2];
            result.m14 = inverse[0, 3];
            result.m21 = inverse[1, 0];
            result.m22 = inverse[1, 1];
            result.m23 = inverse[1, 2];
            result.m24 = inverse[1, 3];
            result.m31 = inverse[2, 0];
            result.m32 = inverse[2, 1];
            result.m33 = inverse[2, 2];
            result.m34 = inverse[2, 3];
            result.m41 = inverse[3, 0];
            result.m42 = inverse[3, 1];
            result.m43 = inverse[3, 2];
            result.m44 = inverse[3, 3];
            return result;
        }     
        #endregion

        #region static
        /// <summary>
        /// The identity.
        /// </summary>
        public static UMatrix4x4 identity = new 
            UMatrix4x4(1, 0, 0, 0,
                       0, 1, 0, 0,
                       0, 0, 1, 0,
                       0, 0, 0, 1);
        /// <summary>
        /// The zero.
        /// </summary>
        public static UMatrix4x4 zero = new UMatrix4x4(0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);

        /// <summary>
        /// Creates the TRS.
        /// </summary>
        /// <returns>The TR.</returns>
        /// <param name="trans">Trans.</param>
        /// <param name="rot">Rot.</param>
        /// <param name="scale">Scale.</param>
        public static UMatrix4x4 TRS(UVector3 trans,UQuaternion rot, UVector3 scale)
        {
            var mTrasn = identity;
            mTrasn.m14 = trans.x;
            mTrasn.m24 = trans.y;
            mTrasn.m34 = trans.z;
            var mRot = rot.ToMartix();
            var mscale = identity;
            mscale.m11 = scale.x;
            mscale.m22 = scale.y;
            mscale.m33 = scale.z;
            return mTrasn * mRot * mscale;
        }
        #endregion

        #region operator

        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        public static UMatrix4x4 operator *(UMatrix4x4 left, UMatrix4x4 right)
        {
            var result = zero;
            for (int row = 1; row <= 4; row++)
            {
                for (var colnum = 1; colnum <= 4; colnum++)
                {
                    for (var k = 1; k <= 4; k++)
                    {
                        result[row, colnum] += left[row, k] * right[k, colnum];
                    }
                }
            }
            return result;
        }

        /// <param name="matrix">Matrix.</param>
        /// <param name="v">V.</param>
        public static UVector4 operator *(UMatrix4x4 matrix, UVector4 v)
        {
            var res = UVector4.zero;
            for (var index = 1; index <= 4; index++)
            {
                for (var k = 1; k <= 4; k++)
                {
                    res[index] += v[k] * matrix[k, index];
                }
            }
            return res;
        }

        #endregion
    }
}

