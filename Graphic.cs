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
            // Створення нової форми для відображення графіка
            Form graphForm = new Form
            {
                Text = "Polynomial Graph",
                Width = 800,
                Height = 600
            };

            // Створення елемента PlotView для розміщення графіка
            var plotView = new PlotView
            {
                Dock = DockStyle.Fill
            };

            // Створення моделі графіка для визначення його зовнішнього вигляду та поведінки
            var plotModel = new PlotModel { Title = "Polynomial Graph" };

            // Визначення осі X
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

            // Визначення осі Y
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

            // Додавання осей до моделі графіка
            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            // Створення серії ліній для представлення кривої полінома
            var lineSeries = new LineSeries();

            // Визначення діапазону для осі X на основі коренів
            double startX = findMin(roots) - 1;
            double endX = findMax(roots) + 1;
            double step = 0.1;

            // Обчислення значень полінома у визначеному діапазоні та додавання їх до серії
            for (double x = startX; x <= endX; x += step)
            {
                double y = Polynom(coefficients, x);
                lineSeries.Points.Add(new DataPoint(x, y));
            }

            // Додавання серії ліній до моделі графіка
            plotModel.Series.Add(lineSeries);
            plotView.Model = plotModel;

            // Додавання PlotView до форми та її відображення
            graphForm.Controls.Add(plotView);
            graphForm.ShowDialog();
        }
    }
}
