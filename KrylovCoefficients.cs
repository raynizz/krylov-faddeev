using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static krylov_faddeev.MatrixVectorMath;
using static krylov_faddeev.GaussMethod;

namespace krylov_faddeev;

public class KrylovCoefficients
{
    private static List<double[]> VectorsSwap(List<double[]> vectors)
    {
        List<double[]> result = new List<double[]>();

        for (int i = vectors.Count - 2; i >= 0; i--)
        {
            MainForm.difficulty++;
            result.Add(vectors[i]);
        }

        result.Add(vectors[^1]);
        return result;
    }

    private static double[] NormalizeCoefficients(double[] coefficients, double[,] matrix)
    {
        const double firstCoefficient = -1;

        coefficients = Enumerable.Repeat(firstCoefficient, 1).Concat(coefficients).ToArray();

        if (matrix.GetLength(0) % 2 == 0)
        {
            for (int i = 0; i < coefficients.Length; i++)
            {
                MainForm.difficulty++;
                coefficients[i] = -coefficients[i];
            }
        }

        return coefficients;
    }

    public static double[] CalculateKrylovCoefficients(double[,] matrix)
    {
        List<double[]> vectors = new List<double[]>();
        double[] tempVector = GetVectorE(matrix.GetLength(0), 0);
        vectors.Add(tempVector);

        for (int i = 1; i <= tempVector.Length; i++)
        {
            //MainForm.difficulty++;
            tempVector = MultiplyMatrixByVector(matrix, tempVector);
            vectors.Add(tempVector);
        }

        double[] coefficients = GaussianElimination(VectorsToMatrix(VectorsSwap(vectors)));

        return NormalizeCoefficients(coefficients, matrix);

    }
}