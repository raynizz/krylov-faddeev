using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace krylov_faddeev
{
    public class GaussMethod
    {
        public class NoSolutionException : ArgumentException
        {
            public NoSolutionException() : base("No solution exists.")
            {
            }

            public NoSolutionException(string message) : base(message)
            {
            }

            public NoSolutionException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }

        public class WrongDataFormatException : ApplicationException
        {
            public WrongDataFormatException()
            {
            }
        }

        private static void swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        private class GeneralElement
        {
            private int _row; //row index [0, ..., n)
            private int _col; // column index [0, ..., n)
            private double _val = 0; //value
            private bool _isSet = false;
            public void setPoint(int row, int col, double val)
            {
                this._col = col;
                this._row = row;
                this._val = val;
                this._isSet = true;
            }
            public int row { get { return this._row; } }
            public int col { get { return this._col; } }
            public double val { get { return this._val; } }
            public double absVal { get { return Math.Abs(this.val); } }
            public bool isSet { get { return this._isSet; } }
            public override string ToString()
            {
                return this._isSet ? String.Format("General element: a[{0},{1}] = {2:0.0}", this.row + 1, this.col + 1, this.val) :
                    "General element is not found";
            }
        }

        public static double[] GaussianElimination(double[,] data)
        {
            data = (double[,])data.Clone(); //The method mustn't change an argument

            // varOrder - array for variables' order
            int[] varOrder = new int[data.GetLength(1) - 1];
            for (int i = 0; i < varOrder.Length; ++i)
                varOrder[i] = i;

            // Main loop
            for (int i = 0; i < data.GetLength(0); ++i)
            {
                //Looking for the general element
                GeneralElement generalElement = new GeneralElement();
                for (int j = i; j < data.GetLength(1) - 1; ++j)
                    if (Math.Abs(data[i, j]) - generalElement.absVal > 1e-10)
                        generalElement.setPoint(i, j, data[i, j]);

                if (!generalElement.isSet) // Its mean every a-coefficient in row = 0
                    throw new NoSolutionException("It is smpossible to find solution by Kryliv method");

                // Row reduction
                for (int k = 0; k < data.GetLength(0); ++k)
                    if (k != generalElement.row)
                    {
                        double m = -data[k, generalElement.col] / generalElement.val;
                        for (int j = 0; j < data.GetLength(1); ++j)
                            data[k, j] += data[generalElement.row, j] * m;
                    }

                // Change variables' order
                if (i != generalElement.col)
                {
                    swap<int>(ref varOrder[generalElement.col], ref varOrder[i]);
                    for (int k = 0; k < data.GetLength(0); ++k)
                        swap<double>(ref data[k, generalElement.col], ref data[k, i]);
                }
            }
            // Getting variables' values
            double[] answers = new double[data.GetLength(0)];
            for (int i = 0; i < data.GetLength(0); ++i)
                answers[varOrder[i]] = data[i, data.GetLength(1) - 1] / data[i, i];
            return answers;
        }
    }

}
