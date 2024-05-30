using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace krylov_faddeev
{
    public partial class MainForm : Form
    {
        private double[,] inputMatrix;
        private double[,]? outputMatrix;
        private double[] coefficients;
        private double[,] eigenVectors;
        private double[] eigenValues;
        private List<TextBox> Matrix;
        private List<Label> eigenValueLabels;
        private List<Label> eigenVectorLabels;
        public static int difficulty;

        public MainForm()
        {
            InitializeComponent();
            Matrix = new List<TextBox>();
            eigenValueLabels = new List<Label>();
            eigenVectorLabels = new List<Label>();
            SolutionMethod.SelectedIndexChanged += SoluionMethod_SelectedIndexChanged;
            MinValueTextBox.Validating += Validation.MinValueTextBox_Validating;
            MaxValueTextBox.Validating += Validation.MaxValueTextBox_Validating;

            MinValueTextBox.TextChanged += MinValueTextBox_TextChanged;
            MaxValueTextBox.TextChanged += MaxValueTextBox_TextChanged;
        }

        private void SoluionMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckSolveButtonState();
        }

        private void SolutionMethod_ComboBox(object sender, EventArgs e)
        {

        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            try
            {
                difficulty = 0;
                outputMatrix = null;
                ReadMatrix();
                switch (SolutionMethod.Text)
                {
                    case "Krylov method":
                        coefficients = KrylovCoefficients.CalculateKrylovCoefficients(inputMatrix);
                        eigenValues = SolveCharacteristicPolynom.EigenvaluesCalculating(coefficients);
                        eigenVectors = KrylovEigenvectors.CalculateKrylovVectors(inputMatrix, eigenValues, coefficients);
                        break;

                    case "Leverrier-Faddeev method":
                        coefficients = LeverrierFaddeevCoefficients.CalculatingFaddeevCoefficients(inputMatrix);
                        eigenValues = SolveCharacteristicPolynom.EigenvaluesCalculating(coefficients);
                        eigenVectors = LeverrierFaddeevEigenvectors.CalculateFaddeevEigenvectors(inputMatrix, eigenValues);
                        break;

                    default:
                        throw new ArgumentException("Invalid solution method selected.");
                        break;
                }

                DifficultyLabel.Visible = true;
                DifficultyLabel.Text = $"Difficulty: {difficulty}";
                InitializeOutputLabels();
                Graphic.DrawGraph(coefficients, eigenValues);
                outputMatrix = inputMatrix;
                SaveInFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveInFile()
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileName = $"Matrix_{timestamp}.txt";

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, fileName);

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Input Matrix:");
                    for (int i = 0; i < outputMatrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < outputMatrix.GetLength(1); j++)
                        {
                            writer.Write($"{outputMatrix[i, j]} ");
                        }
                        writer.WriteLine();
                    }

                    writer.WriteLine("\nEigenvalues:");
                    foreach (double eigenValue in eigenValues)
                    {
                        writer.WriteLine(eigenValue);
                    }

                    writer.WriteLine("\nEigenvectors:");
                    for (int i = 0; i < eigenVectors.GetLength(0); i++)
                    {
                        for (int j = 0; j < eigenVectors.GetLength(1); j++)
                        {
                            writer.Write($"{eigenVectors[i, j]} ");
                        }
                        writer.WriteLine();
                    }

                    writer.WriteLine($"\nDifficulty: {difficulty}");

                    writer.WriteLine($"\nSolution Method: {SolutionMethod.Text}");
                }

                MessageBox.Show("Data successfully saved to file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InputForm_Load(object sender, EventArgs e)
        {

        }

        private void MatrixSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeMatrix();
            CheckSolveButtonState();
        }

        private void CheckSolveButtonState()
        {
            bool allFieldsFilled = Matrix.All(tb => !string.IsNullOrWhiteSpace(tb.Text));
            bool methodSelected = SolutionMethod.SelectedItem != null;

            SolveButton.Enabled = allFieldsFilled && methodSelected;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            foreach (TextBox tb in Matrix)
            {
                tb.Text = string.Empty;
            }
            CheckSolveButtonState();
        }

        private double[,] ReadMatrix()
        {
            int size = Int32.Parse(MatrixSize.Text);
            inputMatrix = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int index = i * size + j;
                    if (double.TryParse(Matrix[index].Text, out double value))
                    {
                        inputMatrix[i, j] = value;
                    }
                    else
                    {
                        throw new FormatException($"Invalid input at position ({i}, {j})." +
                            $" Please ensure all inputs are valid numbers.");
                    }
                }
            }

            return inputMatrix;
        }

        private void MinValueTextBox_TextChanged(object sender, EventArgs e)
        {
            CheckGenerateMatrixButtonState();
        }

        private void MaxValueTextBox_TextChanged(object sender, EventArgs e)
        {
            CheckGenerateMatrixButtonState();
        }

        private void GenerateMatrixButton_Click(object sender, EventArgs e)
        {
            var randomMatrix = MatrixVectorMath.GenerateRandomMatrix(Int32.Parse(MatrixSize.Text),
                double.Parse(MinValueTextBox.Text), double.Parse(MaxValueTextBox.Text));
            FillEmptyMatrixFields(randomMatrix);
        }

        private void CheckGenerateMatrixButtonState()
        {
            double minValue, maxValue;
            bool isMinValid = double.TryParse(MinValueTextBox.Text, out minValue);
            bool isMaxValid = double.TryParse(MaxValueTextBox.Text, out maxValue);

            if (isMinValid && isMaxValid && minValue < maxValue && maxValue - minValue > 1 && AreMatrixFieldsEmpty())
            {
                GenerateMatrixButton.Enabled = true;
            }
            else
            {
                GenerateMatrixButton.Enabled = false;
            }
        }


        private bool AreMatrixFieldsEmpty()
        {
            foreach (TextBox tb in Matrix)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    return true;
                }
            }
            return false;
        }

        private void FillEmptyMatrixFields(double[] randomMatrix)
        {
            foreach (TextBox tb in Matrix)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = randomMatrix[Matrix.IndexOf(tb)].ToString();
                }
            }
        }
    }
}
