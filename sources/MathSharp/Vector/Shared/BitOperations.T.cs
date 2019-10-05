﻿using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
// ReSharper disable InconsistentNaming

namespace MathSharp
{
    public static partial class Vector
    {
        [MethodImpl(MaxOpt)]
        public static Vector128<T> SelectWhereTrue<T, U>(Vector128<T> vector, Vector128<U> selector)
            where T : struct where U : struct
            => And(selector.As<U, T>(), vector);

        [MethodImpl(MaxOpt)]
        public static Vector128<T> SelectWhereFalse<T, U>(Vector128<T> vector, Vector128<U> selector)
            where T : struct where U : struct
            => AndNot(selector.As<U, T>(), vector);

        [MethodImpl(MaxOpt)]
        public static Vector128<float> SelectEqual(Vector128<float> left, Vector128<float> right, Vector128<float> vector)
            => And(CompareEqual(left, right), vector);

        [MethodImpl(MaxOpt)]
        public static Vector128<float> SelectNotEqual(Vector128<float> left, Vector128<float> right, Vector128<float> vector)
            => And(CompareNotEqual(left, right), vector);

        [MethodImpl(MaxOpt)]
        public static Vector128<float> SelectLessThanOrEqual(Vector128<float> left, Vector128<float> right, Vector128<float> vector)
            => And(CompareLessThanOrEqual(left, right), vector);

        [MethodImpl(MaxOpt)]
        public static Vector128<T> And<T>(Vector128<T> left, Vector128<T> right) where T : struct
        {
            if (typeof(T) == typeof(float))
            {
                if (Sse.IsSupported)
                {
                    return Sse.And(left.AsSingle(), right.AsSingle()).As<float, T>();
                }
            }
            
            if (typeof(T) == typeof(double))
            {
                if (Sse2.IsSupported)
                {
                    return Sse2.And(left.AsDouble(), right.AsDouble()).As<double, T>();
                }
                if (Sse.IsSupported)
                {
                    return Sse.And(left.AsSingle(), right.AsSingle()).As<float, T>();
                }
            }

            if (Sse2.IsSupported)
            {
                return Sse2.And(left.AsByte(), right.AsByte()).As<byte, T>();
            }
            if (Sse.IsSupported)
            {
                return Sse.And(left.AsSingle(), right.AsSingle()).As<float, T>();
            }

            return SoftwareFallbacks.And_Software(left, right);
        }

        [MethodImpl(MaxOpt)]
        public static Vector128<T> Or<T>(Vector128<T> left, Vector128<T> right) where T : struct
        {
            if (typeof(T) == typeof(float))
            {
                if (Sse.IsSupported)
                {
                    return Sse.Or(left.AsSingle(), right.AsSingle()).As<float, T>();
                }
            }

            if (typeof(T) == typeof(double))
            {
                if (Sse2.IsSupported)
                {
                    return Sse2.Or(left.AsDouble(), right.AsDouble()).As<double, T>();
                }
                if (Sse.IsSupported)
                {
                    return Sse.Or(left.AsSingle(), right.AsSingle()).As<float, T>();
                }
            }

            if (Sse2.IsSupported)
            {
                return Sse2.Or(left.AsByte(), right.AsByte()).As<byte, T>();
            }
            if (Sse.IsSupported)
            {
                return Sse.Or(left.AsSingle(), right.AsSingle()).As<float, T>();
            }

            return SoftwareFallbacks.Or_Software(left, right);
        }

        [MethodImpl(MaxOpt)]
        public static Vector128<T> Xor<T>(Vector128<T> left, Vector128<T> right) where T : struct
        {
            if (typeof(T) == typeof(float))
            {
                if (Sse.IsSupported)
                {
                    return Sse.Xor(left.AsSingle(), right.AsSingle()).As<float, T>();
                }
            }

            if (typeof(T) == typeof(double))
            {
                if (Sse2.IsSupported)
                {
                    return Sse2.Xor(left.AsDouble(), right.AsDouble()).As<double, T>();
                }
                if (Sse.IsSupported)
                {
                    return Sse.Xor(left.AsSingle(), right.AsSingle()).As<float, T>();
                }
            }

            if (Sse2.IsSupported)
            {
                return Sse2.Xor(left.AsByte(), right.AsByte()).As<byte, T>();
            }
            if (Sse.IsSupported)
            {
                return Sse.Xor(left.AsSingle(), right.AsSingle()).As<float, T>();
            }

            return SoftwareFallbacks.Xor_Software(left, right);
        }

        [MethodImpl(MaxOpt)]
        public static Vector128<T> AndNot<T>(Vector128<T> left, Vector128<T> right) where T : struct
        {
            if (typeof(T) == typeof(float))
            {
                if (Sse.IsSupported)
                {
                    return Sse.AndNot(left.AsSingle(), right.AsSingle()).As<float, T>();
                }
            }

            if (typeof(T) == typeof(double))
            {
                if (Sse2.IsSupported)
                {
                    return Sse2.AndNot(left.AsDouble(), right.AsDouble()).As<double, T>();
                }
                if (Sse.IsSupported)
                {
                    return Sse.AndNot(left.AsSingle(), right.AsSingle()).As<float, T>();
                }
            }

            if (Sse2.IsSupported)
            {
                return Sse2.AndNot(left.AsByte(), right.AsByte()).As<byte, T>();
            }
            if (Sse.IsSupported)
            {
                return Sse.AndNot(left.AsSingle(), right.AsSingle()).As<float, T>();
            }

            return SoftwareFallbacks.AndNot_Software(left, right);
        }

        [MethodImpl(MaxOpt)]
        public static Vector128<T> Not<T>(Vector128<T> vector) where T : struct
        {
            return Xor(vector, SingleConstants.AllBitsSet.As<float, T>());
        }

        [MethodImpl(MaxOpt)]
        public static Vector256<T> SelectWhereTrue<T, U>(Vector256<T> vector, Vector256<U> selector)
            where T : struct where U : struct
            => And(selector.As<U, T>(), vector);

        [MethodImpl(MaxOpt)]
        public static Vector256<T> SelectWhereFalse<T, U>(Vector256<T> vector, Vector256<U> selector)
            where T : struct where U : struct
            => AndNot(selector.As<U, T>(), vector);

        [MethodImpl(MaxOpt)]
        public static Vector256<T> And<T>(Vector256<T> left, Vector256<T> right) where T : struct
        {
            if (typeof(T) == typeof(float))
            {
                if (Avx.IsSupported)
                {
                    return Avx.And(left.AsSingle(), right.AsSingle()).As<float, T>();
                }
            }

            if (typeof(T) == typeof(double))
            {
                if (Avx.IsSupported)
                {
                    return Avx.And(left.AsDouble(), right.AsDouble()).As<double, T>();
                }
            }

            if (Avx.IsSupported)
            {
                return Avx.And(left.AsSingle(), right.AsSingle()).As<float, T>();
            }

            return SoftwareFallbacks.And_Software(left, right);
        }

        [MethodImpl(MaxOpt)]
        public static Vector256<T> Or<T>(Vector256<T> left, Vector256<T> right) where T : struct
        {
            if (typeof(T) == typeof(float))
            {
                if (Avx.IsSupported)
                {
                    return Avx.Or(left.AsSingle(), right.AsSingle()).As<float, T>();
                }
            }

            if (typeof(T) == typeof(double))
            {
                if (Avx.IsSupported)
                {
                    return Avx.Or(left.AsDouble(), right.AsDouble()).As<double, T>();
                }
            }

            if (Avx.IsSupported)
            {
                return Avx.Or(left.AsSingle(), right.AsSingle()).As<float, T>();
            }

            return SoftwareFallbacks.Or_Software(left, right);
        }

        [MethodImpl(MaxOpt)]
        public static Vector256<T> Xor<T>(Vector256<T> left, Vector256<T> right) where T : struct
        {
            if (typeof(T) == typeof(float))
            {
                if (Avx.IsSupported)
                {
                    return Avx.Xor(left.AsSingle(), right.AsSingle()).As<float, T>();
                }
            }

            if (typeof(T) == typeof(double))
            {
                if (Avx.IsSupported)
                {
                    return Avx.Xor(left.AsDouble(), right.AsDouble()).As<double, T>();
                }
            }

            if (Avx.IsSupported)
            {
                return Avx.Xor(left.AsSingle(), right.AsSingle()).As<float, T>();
            }

            return SoftwareFallbacks.Xor_Software(left, right);
        }

        [MethodImpl(MaxOpt)]
        public static Vector256<T> AndNot<T>(Vector256<T> left, Vector256<T> right) where T : struct
        {
            if (typeof(T) == typeof(float))
            {
                if (Avx.IsSupported)
                {
                    return Avx.AndNot(left.AsSingle(), right.AsSingle()).As<float, T>();
                }
            }

            if (typeof(T) == typeof(double))
            {
                if (Avx.IsSupported)
                {
                    return Avx.AndNot(left.AsDouble(), right.AsDouble()).As<double, T>();
                }
            }

            if (Avx.IsSupported)
            {
                return Avx.AndNot(left.AsSingle(), right.AsSingle()).As<float, T>();
            }

            return SoftwareFallbacks.AndNot_Software(left, right);
        }

        [MethodImpl(MaxOpt)]
        public static Vector256<T> Not<T>(Vector256<T> vector) where T : struct
        {
            return Xor(vector, DoubleConstants.AllBitsSet.As<double, T>());
        }
    }
}
