using System;
using System.Diagnostics;

namespace Fusee.Math
{
    /// <summary>
    /// Class contining mainly static helper functions
    /// </summary>
    public class MathHelper
    {
        #region Fields

        /// <summary>
        /// Defines the value which represents the machine epsilon for <see cref="float"/> in C#.
        /// </summary>
        public const float EpsilonFloat = 1.192093E-07f;

        /// <summary>
        /// Defines the value which represents the machine epsilon for <see cref="double"/> in C#.
        /// </summary>
        public const double EpsilonDouble = 1.11022302462516E-16d;

        /// <summary>
        /// Defines the value of Pi as a <see cref="System.Single"/>.
        /// </summary>
        public const float Pi = 3.14159265358979f;

        /// <summary>
        /// Defines the value of Pi divided by two as a <see cref="System.Single"/>.
        /// </summary>
        public const float PiOver2 = Pi / 2;

        /// <summary>
        /// Defines the value of Pi divided by three as a <see cref="System.Single"/>.
        /// </summary>
        public const float PiOver3 = Pi / 3;

        /// <summary>
        /// Defines the value of  Pi divided by four as a <see cref="System.Single"/>.
        /// </summary>
        public const float PiOver4 = Pi / 4;

        /// <summary>
        /// Defines the value of Pi divided by six as a <see cref="System.Single"/>.
        /// </summary>
        public const float PiOver6 = Pi / 6;

        /// <summary>
        /// Defines the value of Pi multiplied by two as a <see cref="System.Single"/>.
        /// </summary>
        public const float TwoPi = 2 * Pi;

        /// <summary>
        /// Defines the value of Pi multiplied by 3 and divided by two as a <see cref="System.Single"/>.
        /// </summary>
        public const float ThreePiOver2 = 3 * Pi / 2;

        /// <summary>
        /// Defines the value of E as a <see cref="System.Single"/>.
        /// </summary>
        public const float E = 2.71828182845904523536f;

        /// <summary>
        /// Defines the base-10 logarithm of E.
        /// </summary>
        public const float Log10E = 0.434294482f;

        /// <summary>
        /// Defines the base-2 logarithm of E.
        /// </summary>
        public const float Log2E = 1.442695041f;

        #endregion

        #region Public Members

        #region Trigonometry

        /// <summary>
        /// Returns the Sin of the given value as float.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static float Sin(float val)
        {
            return (float)System.Math.Sin(val);
        }

        /// <summary>
        /// Returns the Sin of the given value as float.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static float Cos(float val)
        {
            return (float)System.Math.Cos(val);
        }

        #endregion

        #region NextPowerOfTwo

        /// <summary>
        /// Returns the next power of two that is larger than the specified number.
        /// </summary>
        /// <param name="n">The specified number.</param>
        /// <returns>The next power of two.</returns>
        public static long NextPowerOfTwo(long n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException("n", "Must be positive.");
            return (long)System.Math.Pow(2, System.Math.Ceiling(System.Math.Log((double)n, 2)));
        }

        /// <summary>
        /// Returns the next power of two that is larger than the specified number.
        /// </summary>
        /// <param name="n">The specified number.</param>
        /// <returns>The next power of two.</returns>
        public static int NextPowerOfTwo(int n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException("n", "Must be positive.");
            return (int)System.Math.Pow(2, System.Math.Ceiling(System.Math.Log((double)n, 2)));
        }

        /// <summary>
        /// Returns the next power of two that is larger than the specified number.
        /// </summary>
        /// <param name="n">The specified number.</param>
        /// <returns>The next power of two.</returns>
        public static float NextPowerOfTwo(float n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException("n", "Must be positive.");
            return (float)System.Math.Pow(2, System.Math.Ceiling(System.Math.Log((double)n, 2)));
        }

        /// <summary>
        /// Returns the next power of two that is larger than the specified number.
        /// </summary>
        /// <param name="n">The specified number.</param>
        /// <returns>The next power of two.</returns>
        public static double NextPowerOfTwo(double n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException("n", "Must be positive.");
            return System.Math.Pow(2, System.Math.Ceiling(System.Math.Log((double)n, 2)));
        }

        /// <summary>
        /// Determines whether the specified value is a power of two.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static bool IsPowerOfTwo(int val)
        {
            return (val & (val - 1)) == 0;
        }

        #endregion

        #region Factorial

        /// <summary>Calculates the factorial of a given natural number.
        /// </summary>
        /// <param name="n">The number.</param>
        /// <returns>n!</returns>
        public static long Factorial(int n)
        {
            long result = 1;

            for (; n > 1; n--)
                result *= n;

            return result;
        }

        #endregion

        #region BinomialCoefficient

        /// <summary>
        /// Calculates the binomial coefficient <paramref name="n"/> above <paramref name="k"/>.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="k">The k.</param>
        /// <returns>n! / (k! * (n - k)!)</returns>
        public static long BinomialCoefficient(int n, int k)
        {
            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }

        #endregion

        #region InverseSqrtFast

        /// <summary>
        /// Returns an approximation of the inverse square root of left number.
        /// </summary>
        /// <param name="x">A number.</param>
        /// <returns>An approximation of the inverse square root of the specified number, with an upper error bound of 0.001</returns>
        /// <remarks>
        /// This is an improved implementation of the the method known as Carmack's inverse square root
        /// which is found in the Quake III source code. This implementation comes from
        /// http://www.codemaestro.com/reviews/review00000105.html. For the history of this method, see
        /// http://www.beyond3d.com/content/articles/8/
        /// </remarks>
        public static float InverseSqrtFast(float x)
        {
            return (float) (1.0/System.Math.Sqrt(x));
            /*
            unsafe
            {
                float xhalf = 0.5f * x;
                int i = *(int*)&x;              // Read bits as integer.
                i = 0x5f375a86 - (i >> 1);      // Make an initial guess for Newton-Raphson approximation
                x = *(float*)&i;                // Convert bits back to float
                x = x * (1.5f - xhalf * x * x); // Perform left single Newton-Raphson step.
                return x;
            }
            */
        }

        /// <summary>
        /// Returns an approximation of the inverse square root of left number.
        /// </summary>
        /// <param name="x">A number.</param>
        /// <returns>An approximation of the inverse square root of the specified number, with an upper error bound of 0.001</returns>
        /// <remarks>
        /// This is an improved implementation of the the method known as Carmack's inverse square root
        /// which is found in the Quake III source code. This implementation comes from
        /// http://www.codemaestro.com/reviews/review00000105.html. For the history of this method, see
        /// http://www.beyond3d.com/content/articles/8/
        /// </remarks>
        public static double InverseSqrtFast(double x)
        {
            return InverseSqrtFast((float)x);
            // TODO: The following code is wrong. Fix it, to improve precision.
#if false
            unsafe
            {
                double xhalf = 0.5f * x;
                int i = *(int*)&x;              // Read bits as integer.
                i = 0x5f375a86 - (i >> 1);      // Make an initial guess for Newton-Raphson approximation
                x = *(float*)&i;                // Convert bits back to float
                x = x * (1.5f - xhalf * x * x); // Perform left single Newton-Raphson step.
                return x;
            }
#endif
        }

        #endregion

        #region DegreesToRadians

        /// <summary>
        /// Convert degrees to radians
        /// </summary>
        /// <param name="degrees">An angle in degrees</param>
        /// <returns>The angle expressed in radians</returns>
        public static float DegreesToRadians(float degrees)
        {
            const float degToRad = (float)System.Math.PI / 180.0f;
            return degrees * degToRad;
        }

        /// <summary>
        /// Convert radians to degrees
        /// </summary>
        /// <param name="radians">An angle in radians</param>
        /// <returns>The angle expressed in degrees</returns>
        public static float RadiansToDegrees(float radians)
        {
            const float radToDeg = 180.0f / (float)System.Math.PI;
            return radians * radToDeg;
        }

        #endregion

        #region Conversion

        /// <summary>
        /// Converts a float4 to an ABGR value (Int64).
        /// </summary>
        /// <param name="value">The float4 to convert.</param>
        /// <returns>The ABGR value.</returns>
        public static uint Float4ToABGR(float4 value)
        {
            var r = (uint)(255 * value.x);
            var g = (uint)(255 * value.y);
            var b = (uint)(255 * value.z);
            var a = (uint)(255 * value.w);

            return (a << 24) + (b << 16) + (g << 8) + r;
        }

        #endregion

        #region Swap

        /// <summary>
        /// Swaps two double values.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        public static void Swap(ref double a, ref double b)
        {
            double temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// Swaps two float values.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        public static void Swap(ref float a, ref float b)
        {
            float temp = a;
            a = b;
            b = temp;
        }

        #endregion

        #region Clamp

        /// <summary>
        /// Clamp a value to the given minimum and maximum vectors.
        /// </summary>
        /// <param name="val">Input value</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>
        /// The clamped value.
        /// </returns>
        public static double Clamp(double val, double min, double max)
        {
            return val < min ? min : val > max ? max : val;
        }

        /// <summary>
        /// Clamp a value to the given minimum and maximum vectors.
        /// </summary>
        /// <param name="val">Input value.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns>
        /// The clamped value.
        /// </returns>
        public static float Clamp(float val, float min, float max)
        {
            return val < min ? min : val > max ? max : val;
        }

        #endregion

        #region Equals

        /// <summary>
        /// Compares two double values for equality.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>True if the numbers are equal.</returns>
        public static bool Equals(double a, double b)
        {
            return (System.Math.Abs(a - b) < EpsilonDouble);
        }

        /// <summary>
        /// Compares two float values for equality.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>True if the numbers are equal.</returns>
        public static bool Equals(float a, float b)
        {
            return (System.Math.Abs(a - b) < EpsilonFloat);
        }

        #endregion

        /*
        #region PointIsInTri
        public static bool PointIsInTri(float2 p, float2 a, float2 b, float2 c, out float3 w)
        {
            return PointIsInTri(p, a, b, c, out w.x, out w.y, out w.z);
        }

        public static bool PointIsInTri(float2 p, float2 a, float2 b, float2 c, out float wa, out float wb, out float wc)
        {
            return !PointIsInTriCW(p, a, b, c, out wa, out wb, out wc) ? PointIsInTriCW(p, a, c, b, out wa, out wb, out wc) : PointIsInTriCW(p, a, b, c, out wa, out wb, out wc);
        }

        private static bool PointIsInTriCW(float2 p, float2 a, float2 b, float2 c, out float wa, out float wb, out float wc)
        {
            wa = (a.y * c.x - a.x * c.y + (c.y - a.y) * p.x + (a.x - c.x) * p.y);
            wb = (a.x * b.y - a.y * b.x + (a.y - b.y) * p.x + (b.x - a.x) * p.y);

            if (wa <= 0 || wb <= 0)
            {
                wa = wb = wc = 0;
                return false;
            }

            var A = (-b.y * c.x + a.y * (-b.x + c.x) + a.x * (b.y - c.y) + b.x * c.y);

            if ((wa + wb) > A)
            {
                wa = wb = wc = 0;
                return false;
            }

            float AInv = 1.0f/A;
            wa *= AInv;
            wb *= AInv;
            wc = 1 - (wa + wb);

            return true;
        }

        */

        #region IsPointInTri

        // Compute barycentric coordinates (u, v, w) for
        // point p with respect to triangle (a, b, c)
        public static bool IsPointInTriCCW(float2 p, float2 a, float2 b, float2 c, out float u, out float v, out float w)
        {
            return IsPointInTriCW(p, b, a, c, out u, out v, out w);
        }

        public static bool IsPointInTriCW(float2 p, float2 a, float2 b, float2 c, out float u, out float v, out float w)
        {
            float2 v0 = b - a, v1 = c - a, v2 = p - a;
            float vNum = (v2.x*v1.y - v1.x*v2.y);
            float wNum = (v0.x*v2.y - v2.x*v0.y);
            if (vNum > 0 || wNum > 0)
                goto fastExit;

            float den = (v0.x * v1.y - v1.x * v0.y);
            if (vNum + wNum < den)
                goto fastExit;

            float denInv = 1.0f / den;
            v = vNum * denInv;
            w = wNum * denInv;
            u = 1.0f - v - w;
            return true;
         fastExit:
            u = v = w = 0;
            return false;
        }
 
        public static bool IsPointInTri(float2 p, float2 a, float2 b, float2 c, out float u, out float v, out float w)
        {
            float2 v0 = b - a, v1 = c - a, v2 = p - a;
            float vNum = (v2.x * v1.y - v1.x * v2.y);
            float wNum = (v0.x * v2.y - v2.x * v0.y);
            if ((vNum < 0) != (wNum < 0))
                goto fastExit;

            float den = (v0.x * v1.y - v1.x * v0.y);
            if ((den < 0) != (vNum < 0) ||
                 ((den >= 0) && (vNum + wNum > den) || (den < 0) && (vNum + wNum < den)))
                goto fastExit;

            float denInv = 1.0f / den;
            v = vNum * denInv;
            w = wNum * denInv;
            u = 1.0f - v - w;
            return true;
         fastExit:
            u = v = w = 0;
            return false;
        }


        public static void Barycentric(float2 p, float2 a, float2 b, float2 c, out float u, out float v, out float w)
        {
            float2 v0 = b - a, v1 = c - a, v2 = p - a;
            float denInv = 1.0f / (v0.x * v1.y - v1.x * v0.y);
            v = (v2.x * v1.y - v1.x * v2.y) * denInv;
            w = (v0.x * v2.y - v2.x * v0.y) * denInv;
            u = 1.0f - v - w;
        }
        #endregion


        #endregion

    } 
}