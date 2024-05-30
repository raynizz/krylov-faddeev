using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krylov_faddeev;

public class SolveCharacteristicPolynom
{
    public static double[] EigenvaluesCalculating(double[] coefficients)
    {
        List<double> roots = new List<double>();
        double[] quotient = coefficients;
        bool hasRoots = true;
        while (hasRoots)
        {
            double root = PolyRoot(quotient);
            if (double.IsNegativeInfinity(root))
            {
                hasRoots = false;
            }
            else
            {
                roots.Add(root);
                quotient = PolyDiv(quotient, root);
            }
        }

        double[] result = roots.ToArray();
        if (result.Length == 0)
            throw new Exception("There is no real solution.");

        return result;
    }

    private static double EvaluatingFunction(double[] coefficients, double x)
    {
        int degree = coefficients.Length - 1;
        double res = 0;
        for (int i = 0; i < coefficients.Length; i++)
        {
            res += coefficients[i] * Math.Pow(x, degree - i);
        }

        return res;
    }

    private static double[] EvaluatingDerivative(double[] coefficients)
    {
        int degree = coefficients.Length - 1;
        double[] derivativeCoefficients = new double[degree];
        for (int i = 0; i < degree; i++)
        {
            derivativeCoefficients[i] = (degree - i) * coefficients[i];
        }

        return derivativeCoefficients;
    }

    private static double PolyRoot(double[] coefficients)
    {
        double x0 = 0.151231;
        const double error = 0.00000001;
        const int maxIterations = 1000000;

        for (int i = 0; i < maxIterations; i++)
        {
            double x1 = x0 - EvaluatingFunction(coefficients, x0) /
                EvaluatingFunction(EvaluatingDerivative(coefficients), x0);
            if (Math.Abs(x1 - x0) <= error) return x1;
            x0 = x1;
        }

        return double.NegativeInfinity;
    }

    private static double[] PolyDiv(double[] coefficients, double xi)
    {
        double[] quotient = new double[coefficients.Length - 1];
        double prevCoeff = 0;
        for (int i = 0; i < quotient.Length; i++)
        {
            double result = coefficients[i] + prevCoeff;
            quotient[i] = result;
            prevCoeff = xi * result;
        }

        return quotient;
    }
}