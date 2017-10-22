using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PNM.Visualization
{
    public class Sit1D
    {
        /// <param name="l">delka struny</param>
        /// <param name="n">pocet prvku site</param>
        public static (IList<float> X, IList<float> Y) Calculate(float l, int n, Func<float, float> func, float u0 = 0, float un = 0)
        {
            int p = n + 1;
            float h = l / (n + 1); // krok site
            var a = Matrix<float>.Build.Sparse(p, p, (i, j) =>
            {
                if (i == j) return 2;
                if (i - 1 == j || i + 1 == j) return -1;
                return 0;
            });
            var pointsX = Enumerable.Range(0, p).Select(t => t * h).ToArray();
            var pointsF = pointsX.Select(t => h * h * func(t)).ToArray();
            var b = Vector<float>.Build.DenseOfArray(pointsF);
            b[0] += u0;
            b[n] += un;
            var u = a.Solve(b);
            u[0] = u0;
            u[n] = un;

            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(u);

            return (pointsX, u);
        }
    }
}
