using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace krylov_faddeev;

public static class Validation
{
    public static void TextBox_Validating(object sender, CancelEventArgs e)
    {
        TextBox textBox = sender as TextBox;
        if (textBox != null)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                e.Cancel = true;
                textBox.BackColor = System.Drawing.Color.LightCoral;
                MessageBox.Show("This field cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox.Text.Length > 7)
            {
                e.Cancel = true;
                textBox.BackColor = System.Drawing.Color.LightCoral;
                MessageBox.Show("Input cannot exceed 7 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!double.TryParse(textBox.Text, out double result))
            {
                e.Cancel = true;
                textBox.BackColor = System.Drawing.Color.LightCoral;
                MessageBox.Show("Input must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result < -1000 || result > 1000)
            {
                e.Cancel = true;
                textBox.BackColor = System.Drawing.Color.LightCoral;
                MessageBox.Show("Input must be a number between -1000 and 1000.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                textBox.BackColor = System.Drawing.Color.White;
            }
        }
    }

    public static void MinValueTextBox_Validating(object sender, EventArgs e)
    {
        ValidateMinMax(sender as TextBox, "Minimum");
    }

    public static void MaxValueTextBox_Validating(object sender, EventArgs e)
    {
        ValidateMinMax(sender as TextBox, "Maximum");
    }

    private static void ValidateMinMax(TextBox textBox, string valueType)
    {
        if (textBox != null)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BackColor = System.Drawing.Color.LightCoral;
                MessageBox.Show($"{valueType} value cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox.Text.Length > 7)
            {
                textBox.BackColor = System.Drawing.Color.LightCoral;
                MessageBox.Show($"{valueType} value cannot exceed 7 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!double.TryParse(textBox.Text, out double result))
            {
                textBox.BackColor = System.Drawing.Color.LightCoral;
                MessageBox.Show($"{valueType} value must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result < -1000 || result > 1000)
            {
                textBox.BackColor = System.Drawing.Color.LightCoral;
                MessageBox.Show($"{valueType} value must be a number between -1000 and 1000.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                textBox.BackColor = System.Drawing.Color.White;
                ValidateMinMaxDifference();
            }
        }
    }

    private static void ValidateMinMaxDifference()
    {
        Form form = Application.OpenForms.Cast<Form>().FirstOrDefault();
        if (form != null)
        {
            TextBox minTextBox = form.Controls.Find("MinValueTextBox", true).FirstOrDefault() as TextBox;
            TextBox maxTextBox = form.Controls.Find("MaxValueTextBox", true).FirstOrDefault() as TextBox;

            if (minTextBox != null && maxTextBox != null)
            {
                if (double.TryParse(minTextBox.Text, out double minValue) && double.TryParse(maxTextBox.Text, out double maxValue))
                {
                    if (minValue >= maxValue)
                    {
                        minTextBox.BackColor = System.Drawing.Color.LightCoral;
                        maxTextBox.BackColor = System.Drawing.Color.LightCoral;
                        MessageBox.Show("Minimum value must be less than maximum value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (maxValue - minValue <= 1)
                    {
                        minTextBox.BackColor = System.Drawing.Color.LightCoral;
                        maxTextBox.BackColor = System.Drawing.Color.LightCoral;
                        MessageBox.Show("The difference between minimum and maximum value must be greater than 1.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        minTextBox.BackColor = System.Drawing.Color.White;
                        maxTextBox.BackColor = System.Drawing.Color.White;
                    }
                }
            }
        }
    }
}