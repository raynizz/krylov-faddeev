using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace krylov_faddeev
{
    public class Graphic
    {
        private static double Polynom(double[] coefficients, double x)
        {
            int degree = coefficients.Length - 1;
            double res = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                res += coefficients[i] * Math.Pow(x, degree - i);
            }

            return res;
        }

        private static double findMax(double[] roots)
        {
            double max = double.NegativeInfinity;
            for (int i = 0; i < roots.Length; i++)
            {
                if (roots[i] > max)
                {
                    max = roots[i];
                }
            }

            return max;
        }

        private static double findMin(double[] roots)
        {
            double min = double.PositiveInfinity;
            for (int i = 0; i < roots.Length; i++)
            {
                if (roots[i] < min)
                {
                    min = roots[i];
                }
            }

            return min;
        }

        public static void DrawGraph(double[] coefficients, double[] roots)
        {
            Form graphForm = new Form
            {
                Text = "Polynomial Graph",
                Width = 800,
                Height = 600
            };

            var plotView = new PlotView
            {
                Dock = DockStyle.Fill
            };

            var plotModel = new PlotModel { Title = "Polynomial Graph" };

            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "X Axis",
                PositionAtZeroCrossing = true,
                AxislineStyle = LineStyle.Solid,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                IsZoomEnabled = true,
                IsPanEnabled = true
            };

            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Y Axis",
                PositionAtZeroCrossing = true,
                AxislineStyle = LineStyle.Solid,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                IsZoomEnabled = true,
                IsPanEnabled = true

            };

            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            var lineSeries = new LineSeries();

            double startX = findMin(roots) - 1;
            double endX = findMax(roots) + 1;
            double step = 0.1;

            for (double x = startX; x <= endX; x += step)
            {
                double y = Polynom(coefficients, x);
                lineSeries.Points.Add(new DataPoint(x, y));
            }

            plotModel.Series.Add(lineSeries);
            plotView.Model = plotModel;

            graphForm.Controls.Add(plotView);
            graphForm.ShowDialog();
        }
    }
}
