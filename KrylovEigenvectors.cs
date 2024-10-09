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
            tempVector = MultiplyMatrixByVector(matrix, tempVector);
            vectors.Add(tempVector);
        }

        return vectors;
    }

    private static double[] CalculateQ(double eigenvalue, double[] coefficients, int n)
    {
        double[] result = new double[n];
        result[0] = 1;

        // Починаємо з 1, а попередній результат множимо на власне значення і додаємо чи
        // віднімаємо коефіцієнт в залежності від парності вхідної матриці
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
        // як для коефіцієнтів знаходимо ті ж самі вектори СЛАР
        List<double[]> vectorsY = CalculateVectorsY(A);
        List<double[]> eigenVectors = new List<double[]>();

        
        for (int i = 0; i < eigenvalues.Length; i++)
        {
            double[] tempEigenVector = new double[n];
            // Для кожного власного вектора рахуємо масив допоміжних значень q
            double[] q = CalculateQ(eigenvalues[i], coefficients, n);

            // За рекурентною формулою рахуємо ненормований власний вектор
            // Просто множимо передостанній вектор СЛАР на перше значення q і так по колу
            for (int j = 1; j <= n; j++)
            {
                tempEigenVector = SumOfVectors(tempEigenVector, MultiplyVectorByScalar(vectorsY[^(j + 1)], q[j - 1]));
            }

            // Нормуємо вектор на його довжину
            eigenVectors.Add(NormalizeVector(tempEigenVector));
        }

        return VectorsToMatrix(eigenVectors);
    }
}

