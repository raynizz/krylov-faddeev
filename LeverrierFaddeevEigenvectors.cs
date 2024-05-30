using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static krylov_faddeev.MatrixVectorMath;

namespace krylov_faddeev;

public class LeverrierFaddeevEigenvectors
{
    private static List<double[]> GetFaddeevVectorB(double[,] matrix, int colmIndex)
    {
        // Знаходимо вектори B за рекурентною формулою аналогічно пошуку коефіцієнтів
        int n = matrix.GetLength(0);
        List<double[]> result = new List<double[]>();

        double[,] tempMatrix = matrix;
        double tempC = Sp(matrix);
        double[,] matrixB = SubtractionMatrix(tempMatrix, MultiplyMatrixByScalar(GetMatrixE(n), tempC));
        result.Add(GetVector(matrixB, colmIndex));

        for (int i = 2; i < n; i++)
        {
            tempMatrix = MultiplyMatrixByMatrix(matrix, matrixB);
            tempC = Sp(tempMatrix) / (double)i;

            matrixB = SubtractionMatrix(tempMatrix, MultiplyMatrixByScalar(GetMatrixE(n), tempC));
            result.Add(GetVector(matrixB, colmIndex));
        }

        return result;
    }

    private static bool IsVectorZero(double[] vector)
    {
        int numZero = 0;
        for (int i = 0; i < vector.Length; i++)
        {
            MainForm.difficulty++;
            if (vector[i] == 0)
            {
                numZero++;
            }
        }

        return (numZero == vector.Length);
    }

    public static double[,] CalculateFaddeevEigenvectors(double[,] matrixA, double[] eigenvalues)
    {
        List<double[]> result = new List<double[]>();

        // Для кожного власного значення знаходимо власний вектор
        for (int i = 0; i < eigenvalues.Length; i++)
        {
            double[] eigenvector;
            int tempColm = 0;
            do
            {
                // Знаходимо стовпці матриць B за тіє самою рекурентною формулою, що і в пошуку коефіцієнтів
                List<double[]> vectorB = GetFaddeevVectorB(matrixA, tempColm);
                eigenvector = GetVectorE(vectorB[0].Length, tempColm);
                // За рекурентною формулою знаходимо ненормований власний вектор
                foreach (var tempVectorB in vectorB)
                {
                    eigenvector = MultiplyVectorByScalar(eigenvector, eigenvalues[i]);
                    eigenvector = SumOfVectors(eigenvector, tempVectorB);
                }

                // Якщо вектор нульовий, то переходимо до наступного стовпця, і так поки не знайдемо ненульовий
                tempColm++;
            } while (IsVectorZero(eigenvector) && tempColm < matrixA.GetLength(0));

            // Нормалізуємо власний вектор
            result.Add(NormalizeVector(eigenvector));
        }

        return VectorsToMatrix(result);
    }
}
