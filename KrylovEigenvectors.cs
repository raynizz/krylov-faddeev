using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static krylov_faddeev.MatrixVectorMath;

namespace krylov_faddeev;

public class KrylovEigenvectors
{
    private static List<double[]> CalculateVectorsY(double[,] matrix)
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

        return vectors;
    }

    private static double[] CalculateQ(double eigenvalue, double[] coefficients, int n)
    {
        double[] result = new double[n];
        result[0] = 1;

        for (int i = 1; i < n; i++)
        {
            MainForm.difficulty++;
            result[i] = result[i - 1] * eigenvalue;
            if (n % 2 == 0)
            {
                result[i] += coefficients[i];
            }
            else
            {
                result[i] -= coefficients[i];
            }
        }

        return result;
    }

    public static double[,] CalculateKrylovVectors(double[,] A, double[] eigenvalues, double[] coefficients)
    {
        int n = A.GetLength(0);
        List<double[]> vectorsY = CalculateVectorsY(A);
        List<double[]> eigenVectors = new List<double[]>();

        for (int i = 0; i < eigenvalues.Length; i++)
        {
            double[] tempEigenVector = new double[n];
            double[] q = CalculateQ(eigenvalues[i], coefficients, n);
            for (int j = 1; j <= n; j++)
            {
                //MainForm.difficulty++;
                tempEigenVector = SumOfVectors(tempEigenVector, MultiplyVectorByScalar(vectorsY[^(j + 1)], q[j - 1]));
            }

            eigenVectors.Add(NormalizeVector(tempEigenVector));
        }

        return VectorsToMatrix(eigenVectors);
    }
}

