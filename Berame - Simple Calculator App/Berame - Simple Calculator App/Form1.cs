using System.Runtime.CompilerServices;

namespace Berame___Simple_Calculator_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string currentExpression = "";
        bool isDecimalAdded = false;
        bool isResultDisplayed = false;

        private void button_Click(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            if (button != null)
            {
                string buttonText = button.Text;
                HandleButtonClick(buttonText);
            }
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            CalculateResult();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteLastCharacter();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            txtTotal.Text = "";
            currentExpression = "";
            isDecimalAdded = false;
        }

        private void DeleteLastCharacter()
        {
            if (txtTotal.Text.Length > 0)
            {
                char lastChar = txtTotal.Text[txtTotal.Text.Length - 1];
                if (lastChar == '.')
                {
                    isDecimalAdded = false;
                }
                txtTotal.Text = txtTotal.Text.Remove(txtTotal.Text.Length - 1, 1);
                currentExpression = currentExpression.Remove(currentExpression.Length - 1, 1);
            }
        }

        private void HandleButtonClick(string buttonText)
        {
            if (isResultDisplayed && char.IsDigit(buttonText, 0))
            {
                txtTotal.Text = "";
                currentExpression = "";
                isDecimalAdded = false;
                isResultDisplayed = false;
            }

            if (char.IsDigit(buttonText, 0) || buttonText == ".")
            {
                AppendNumber(buttonText);
            }
            else
            {
                HandleOperation(buttonText);
            }
        }

        private void AppendNumber(string number)
        {
            if (number == ".")
            {
                if (isDecimalAdded)
                {
                    return;
                }
                isDecimalAdded = true;
            }

            txtTotal.Text += number;
            currentExpression += number;
        }

        private void HandleOperation(string operation)
        {
            if (txtTotal.Text.Length > 0 && !IsLastCharOperation())
            {
                txtTotal.Text += operation;
                currentExpression += operation;
                isDecimalAdded = false;
                isResultDisplayed = false;
            }
        }

        private bool IsLastCharOperation()
        {
            if (currentExpression.Length == 0)
                return false;

            char lastChar = currentExpression[currentExpression.Length - 1];
            return lastChar == '+' || lastChar == '-' || lastChar == '*' || lastChar == '/';
        }

        private void CalculateResult()
        {
            try
            {
                var dataTable = new System.Data.DataTable();
                var result = dataTable.Compute(currentExpression, "");
                txtTotal.Text = result.ToString();
                currentExpression = result.ToString()!;
                isDecimalAdded = false;
                isResultDisplayed = true;
            }
            catch
            {
                txtTotal.Text = "Error";
                currentExpression = "";
                isDecimalAdded = false;
                isResultDisplayed = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
