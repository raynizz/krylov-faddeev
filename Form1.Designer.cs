using System.Drawing.Drawing2D;

namespace krylov_faddeev
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeOutputLabels()
        {
            // Remove existing labels
            foreach (Label lbl in eigenValueLabels)
            {
                Controls.Remove(lbl);
            }
            eigenValueLabels.Clear();

            foreach (Label lbl in eigenVectorLabels)
            {
                Controls.Remove(lbl);
            }
            eigenVectorLabels.Clear();

            int eigenValueLabelY = 200;  // Y-coordinate for eigenvalue labels
            int eigenVectorStartY = 230; // Starting Y-coordinate for eigenvector labels
            int eigenVectorSpacingX = 150; // X-spacing for eigenvector labels
            int eigenVectorSpacingY = 30;  // Y-spacing for eigenvector labels

            // Create and add eigenvalue labels
            for (int i = 0; i < eigenValues.Length; i++)
            {
                Label lbl = new Label
                {
                    Location = new Point(500 + eigenVectorSpacingX * i, eigenValueLabelY), // Adjust coordinates for eigenvalues in a row above eigenvectors
                    Size = new Size(100, 23),
                    Name = "EigenValueLabel" + i,
                    Text = $"λ{i + 1}: {eigenValues[i]:F4}"
                };
                Controls.Add(lbl);
                eigenValueLabels.Add(lbl);
            }

            // Create and add eigenvector labels
            for (int i = 0; i < eigenVectors.GetLength(1); i++)
            {
                for (int j = 0; j < eigenVectors.GetLength(0); j++)
                {
                    Label lbl = new Label
                    {
                        Location = new Point(500 + eigenVectorSpacingX * i, eigenVectorStartY + eigenVectorSpacingY * j), // Adjust coordinates for eigenvectors below eigenvalues
                        Size = new Size(100, 23),
                        Name = $"EigenVectorLabel{i}_{j}",
                        Text = $"v{i + 1}_{j + 1}: {eigenVectors[j, i]:F4}"
                    };
                    Controls.Add(lbl);
                    eigenVectorLabels.Add(lbl);
                }
            }
        }

        public void InitializeMatrix()
        {
            foreach (TextBox tb in Matrix)
            {
                Controls.Remove(tb);
            }
            Matrix.Clear();

            int size = Int32.Parse(MatrixSize.Text);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TextBox tb = new TextBox();
                    tb.Location = new Point(38 + 50 * j, 200 + 30 * i);
                    tb.Name = "InputMatrix" + i + j;
                    tb.Size = new Size(50, 27);
                    tb.TabIndex = 2;
                    tb.TextAlign = HorizontalAlignment.Center;
                    tb.Validating += Validation.TextBox_Validating;

                    tb.TextChanged += (s, e) => CheckSolveButtonState();

                    Matrix.Add(tb);
                    Controls.Add(tb);
                }
            }

            ClearButton.Visible = Matrix.Count > 0;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SolutionMethod = new ComboBox();
            SolveButton = new Button();
            ClearButton = new Button();
            MinValueTextBox = new TextBox();
            MaxValueTextBox = new TextBox();
            GenerateMatrixButton = new Button();
            MatrixSize = new ComboBox();
            DifficultyLabel = new Label();
            SizeLabel = new Label();
            SolutionMethodLabel = new Label();
            MinValueLabel = new Label();
            MaxValueLabel = new Label();
            SuspendLayout();
            // 
            // SolutionMethod
            // 
            SolutionMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            SolutionMethod.FormattingEnabled = true;
            SolutionMethod.Items.AddRange(new object[] { "Krylov method", "Leverrier-Faddeev method" });
            SolutionMethod.Location = new Point(122, 29);
            SolutionMethod.Name = "SolutionMethod";
            SolutionMethod.Size = new Size(206, 28);
            SolutionMethod.TabIndex = 0;
            SolutionMethod.SelectedIndexChanged += SolutionMethod_ComboBox;
            // 
            // SolveButton
            // 
            SolveButton.Enabled = false;
            SolveButton.Location = new Point(360, 29);
            SolveButton.Name = "SolveButton";
            SolveButton.Size = new Size(157, 29);
            SolveButton.TabIndex = 4;
            SolveButton.Text = "Solve";
            SolveButton.UseVisualStyleBackColor = true;
            SolveButton.Click += SolveButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(146, 471);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(144, 29);
            ClearButton.TabIndex = 6;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Visible = false;
            ClearButton.Click += ClearButton_Click;
            // 
            // MinValueTextBox
            // 
            MinValueTextBox.Location = new Point(639, 29);
            MinValueTextBox.Name = "MinValueTextBox";
            MinValueTextBox.Size = new Size(77, 27);
            MinValueTextBox.TabIndex = 7;
            MinValueTextBox.TextChanged += MinValueTextBox_TextChanged;
            // 
            // MaxValueTextBox
            // 
            MaxValueTextBox.Location = new Point(753, 29);
            MaxValueTextBox.Name = "MaxValueTextBox";
            MaxValueTextBox.Size = new Size(77, 27);
            MaxValueTextBox.TabIndex = 8;
            MaxValueTextBox.TextChanged += MaxValueTextBox_TextChanged;
            // 
            // GenerateMatrixButton
            // 
            GenerateMatrixButton.Enabled = false;
            GenerateMatrixButton.Location = new Point(892, 28);
            GenerateMatrixButton.Name = "GenerateMatrixButton";
            GenerateMatrixButton.Size = new Size(139, 29);
            GenerateMatrixButton.TabIndex = 9;
            GenerateMatrixButton.Text = "Generate matrix";
            GenerateMatrixButton.UseVisualStyleBackColor = true;
            GenerateMatrixButton.Click += GenerateMatrixButton_Click;
            // 
            // MatrixSize
            // 
            MatrixSize.DropDownStyle = ComboBoxStyle.DropDownList;
            MatrixSize.FormattingEnabled = true;
            MatrixSize.Items.AddRange(new object[] { "2", "3", "4", "5", "6", "7", "8" });
            MatrixSize.Location = new Point(24, 29);
            MatrixSize.Name = "MatrixSize";
            MatrixSize.Size = new Size(46, 28);
            MatrixSize.Sorted = true;
            MatrixSize.TabIndex = 5;
            MatrixSize.SelectedIndexChanged += MatrixSize_SelectedIndexChanged;
            // 
            // DifficultyLabel
            // 
            DifficultyLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            DifficultyLabel.Location = new Point(1063, 31);
            DifficultyLabel.Name = "DifficultyLabel";
            DifficultyLabel.Size = new Size(177, 28);
            DifficultyLabel.TabIndex = 11;
            DifficultyLabel.Text = "Difficulty:";
            DifficultyLabel.Visible = false;
            // 
            // SizeLabel
            // 
            SizeLabel.AutoSize = true;
            SizeLabel.Location = new Point(27, 6);
            SizeLabel.Name = "SizeLabel";
            SizeLabel.Size = new Size(36, 20);
            SizeLabel.TabIndex = 12;
            SizeLabel.Text = "Size";
            // 
            // SolutionMethodLabel
            // 
            SolutionMethodLabel.AutoSize = true;
            SolutionMethodLabel.Location = new Point(122, 6);
            SolutionMethodLabel.Name = "SolutionMethodLabel";
            SolutionMethodLabel.Size = new Size(120, 20);
            SolutionMethodLabel.TabIndex = 13;
            SolutionMethodLabel.Text = "Solution method";
            // 
            // MinValueLabel
            // 
            MinValueLabel.AutoSize = true;
            MinValueLabel.Location = new Point(658, 6);
            MinValueLabel.Name = "MinValueLabel";
            MinValueLabel.Size = new Size(34, 20);
            MinValueLabel.TabIndex = 14;
            MinValueLabel.Text = "min";
            // 
            // MaxValueLabel
            // 
            MaxValueLabel.AutoSize = true;
            MaxValueLabel.Location = new Point(772, 6);
            MaxValueLabel.Name = "MaxValueLabel";
            MaxValueLabel.Size = new Size(37, 20);
            MaxValueLabel.TabIndex = 15;
            MaxValueLabel.Text = "max";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1266, 530);
            Controls.Add(MaxValueLabel);
            Controls.Add(MinValueLabel);
            Controls.Add(SolutionMethodLabel);
            Controls.Add(SizeLabel);
            Controls.Add(DifficultyLabel);
            Controls.Add(GenerateMatrixButton);
            Controls.Add(MaxValueTextBox);
            Controls.Add(MinValueTextBox);
            Controls.Add(ClearButton);
            Controls.Add(MatrixSize);
            Controls.Add(SolveButton);
            Controls.Add(SolutionMethod);
            Name = "MainForm";
            Text = "Eigen problem solver";
            Load += InputForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox SolutionMethod;
        private Button SolveButton;
        private Button ClearButton;
        private TextBox MinValueTextBox;
        private TextBox MaxValueTextBox;
        private Button GenerateMatrixButton;
        private ComboBox MatrixSize;
        private Label DifficultyLabel;
        private Label SizeLabel;
        private Label SolutionMethodLabel;
        private Label MinValueLabel;
        private Label MaxValueLabel;
    }
}