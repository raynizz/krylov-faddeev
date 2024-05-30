using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static krylov_faddeev.MatrixVectorMath;

namespace krylov_faddeev;

public class LeverrierFaddeevCoefficients
{
    public static double[] CalculatingFaddeevCoefficients(double[,] matrix)
    {
        int n = matrix.GetLength(0);
        double[] coefficients = new double[n + 1];

        coefficients[0] = -1;


        double[,] tempMatrix = matrix;
        double tempC = Sp(matrix);
        coefficients[1] = tempC;
        double[,] matrixB = SubtractionMatrix(tempMatrix,
            MultiplyMatrixByScalar(GetMatrixE(n), tempC));

        for (int i = 2; i <= n; i++)
        {
            tempMatrix = MultiplyMatrixByMatrix(matrix, matrixB);
            tempC = Sp(tempMatrix) / (double)i;
            coefficients[i] = tempC;

            matrixB = SubtractionMatrix(tempMatrix,
                MultiplyMatrixByScalar(GetMatrixE(n), tempC));
        }

        if (n % 2 == 0)
        {
            for (int i = 0; i < coefficients.Length; i++)
            {
                MainForm.difficulty++;
                coefficients[i] = -coefficients[i];
            }
        }

        return coefficients;
    }
}