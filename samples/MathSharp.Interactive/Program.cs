using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using MathSharp.Interactive.Benchmarks.MatrixTests.Single;
using MathSharp.Interactive.Benchmarks.Vector.Single;

namespace MathSharp.Interactive
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Vector128<float> a = Vector128.Create(1f, 2f, 3f, 4f);
            Vector128<float> b = Vector128.Create(-1f, -2f, -3f, -4f);
            const byte control = 0b_11_00_10_01;

            var intrinsic = Sse.Shuffle(a, b, control);
            var software = SoftwareFallbacks.Shuffle_Software(a, b, control);

            Console.WriteLine($"Intrinsic: {intrinsic}");
            Console.WriteLine($"Software: {software}");
        }

        private static void Example()
        {
            Vector4 x = new Vector4(1f, 2f, 3f, 4f);
            Vector4 z = new Vector4(-2f, -3f, -4f, -5f);

            Vector128<float> v1 = x.Load();
            Vector128<float> v2 = z.Load();

            v1 = Vector.Add(v1, v2);
            Vector128<float> dp = Vector.DotProduct4D(v1, v1);
            Vector128<float> len = Vector.Sqrt(dp);
            v1 = Vector.Multiply(v1, len);

            v1.Store(out x);

            Console.WriteLine(x);
        }
    }
}