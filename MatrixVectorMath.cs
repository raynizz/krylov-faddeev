using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krylov_faddeev
{
    public static class MatrixVectorMath
    {
        public static double[,] MultiplyMatrixByMatrix(double[,] matrixA, double[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int colsB = matrixB.GetLength(1);

            if (colsA != matrixB.GetLength(0))
            {
                throw new ArgumentException(
                    "Number of columns in Matrix A must be equal to the number of rows in Matrix B.");
            }

            double[,] result = new double[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        MainForm.difficulty++;
                        sum += matrixA[i, k] * matrixB[k, j];
                    }

                    result[i, j] = sum;
                }
            }

            return result;
        }

        public static double[] MultiplyMatrixByVector(double[,] matrix, double[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (cols != vector.Length)
            {
                throw new ArgumentException(
                    "Number of columns in Matrix must be equal to the number of elements in Vector.");
            }

            double[] result = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                double sum = 0;
                for (int j = 0; j < cols; j++)
                {
                    MainForm.difficulty++;
                    sum += matrix[i, j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }

        public static double[,] MultiplyMatrixByScalar(double[,] matrix, double scalar)
        {
            int n = matrix.GetLength(0);

            double[,] result = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    MainForm.difficulty++;
                    result[i, j] = matrix[i, j] * scalar;
                }
            }

            return result;
        }


        public static double[,] SubtractionMatrix(double[,] matrixA, double[,] matrixB)
        {
            double[,] result = new double[matrixA.GetLength(0), matrixA.GetLength(1)];
            for (int i = 0; i < matrixA.GetLength(0); i++)
            {
                for (int j = 0; j < matrixA.GetLength(1); j++)
                {
                    MainForm.difficulty++;
                    result[i, j] = matrixA[i, j] - matrixB[i, j];
                }
            }

            return result;
        }

        public static double[,] GetMatrixE(int n)
        {
            double[,] result = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (i == j)
                    {
                        result[i, i] = 1;
                    }
                    else
                    {
                        result[i, j] = 0;
                    }
            return result;
        }

        public static double Sp(double[,] matrix)
        {
            double result = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                MainForm.difficulty++;
                result += matrix[i, i];
            }

            return result;
        }

        public static double[] GetVector(double[,] matrix, int colmIndex)
        {
            int n = matrix.GetLength(0);
            double[] result = new double[n];

            for (int i = 0; i < n; i++)
            {
                MainForm.difficulty++;
                result[i] = matrix[i, colmIndex];
            }

            return result;
        }

        public static double[] MultiplyVectorByScalar(double[] vector, double scalar)
        {
            double[] result = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                MainForm.difficulty++;
                result[i] = vector[i] * scalar;
            }

            return result;
        }

        public static double[] SumOfVectors(double[] vectorA, double[] vectorB)
        {
            double[] result = new double[vectorA.Length];
            for (int i = 0; i < vectorA.Length; i++)
            {
                MainForm.difficulty++;
                result[i] = vectorA[i] + vectorB[i];
            }

            return result;
        }

        public static double[] GetVectorE(int size, int index)
        {
            double[] result = new double[size];
            for (int i = 0; i < size; i++)
            {
                MainForm.difficulty++;
                result[i] = i == index ? 1 : 0;
            }

            return result;
        }


        public static double[,] VectorsToMatrix(List<double[]> vectors)
        {
            int n = vectors[0].Length;
            double[,] result = new double[n, vectors.Count];
            for (int i = 0; i < vectors.Count; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    MainForm.difficulty++;
                    result[j, i] = vectors[i][j];
                }
            }

            return result;
        }

        public static double[] NormalizeVector(double[] vector)
        {
            double[] result = new double[vector.Length];
            double magnitude = 0;

            for (int i = 0; i < vector.Length; i++)
            {
                
                magnitude += vector[i] * vector[i];
            }

            magnitude = Math.Sqrt(magnitude);
            if (magnitude == 0) return vector;

            for (int i = 0; i < vector.Length; i++)
            {
                result[i] = vector[i] / magnitude;
            }

            return result;
        }

        public static double[] GenerateRandomMatrix(int n, double min, double max)
        {
            double[] result = new double[n * n];
            Random random = new Random();

            for (int i = 0; i < n * n; i++)
            {
                MainForm.difficulty++;
                double randomNumber = random.NextDouble() * (max - min) + min;
                string formattedNumber = randomNumber.ToString("F6");
                double parsedNumber = double.Parse(formattedNumber.Substring(0, Math.Min(7, formattedNumber.Length)));
                result[i] = parsedNumber;
            }

            return result;
        }
    }
}
