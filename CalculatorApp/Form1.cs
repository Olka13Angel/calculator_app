using System.CodeDom;
using System.Text.RegularExpressions;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        private enum Operation
        {
            ADD,
            SUBTRACT,
            MULTIPLY,
            DIVIDE,
            CALCULATE,
            EMPTY
        }

        private enum Action
        {
            NUMBER_ACTION,
            OPERATION_ACTION,
            EMPTY
        }

        Dictionary<Operation, string> operationsAsText = new()
        {
            {Operation.ADD, " + "},
            {Operation.SUBTRACT, " - " },
            {Operation.MULTIPLY, " * " },
            {Operation.DIVIDE, " / " },
            {Operation.EMPTY, "" }
        };

        string first = "";
        string second = "";
        string userInput = "";
        Operation function = Operation.EMPTY;
        Action lastAction = Action.EMPTY;
        double result = 0.0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void NumberAction(string s)
        {
            if (first.Length != 0 && function == Operation.EMPTY && lastAction == Action.NUMBER_ACTION)
            {
                ClearAll();
            }
            userInput += s;
            lastAction = Action.NUMBER_ACTION;
            updateCalculatorDisplay();
        }

        private void OperationAction(Operation operation)
        {
            if (operation == Operation.CALCULATE)
            {
                if (function == Operation.EMPTY)
                {
                    userInput = CreateValidString(userInput);
                    updateCalculatorDisplay();
                    return;
                }
                calculateAndShowResult();
                lastAction = Action.NUMBER_ACTION;
                return;
            }

            if (lastAction == Action.OPERATION_ACTION)
            {
                if (operation == function) { return; }

                function = operation;
            } else if (lastAction == Action.NUMBER_ACTION) 
            {
                if (first.Length == 0)
                {
                    first = CreateValidString(userInput);
                    userInput = "";
                    lastAction = Action.OPERATION_ACTION;
                    function = operation;
                    updateCalculatorDisplay();
                } else
                {
                    if (userInput.Length == 0)
                    {
                        function = operation;
                        updateCalculatorDisplay();
                    }
                    else
                    {
                        calculateAndShowResult();
                        function = operation;
                        userInput = "";
                    }
                }
            }
        }


        private void num1_Click(object sender, EventArgs e)
        {
            NumberAction("1");
        }

        private void num2_Click(object sender, EventArgs e)
        {
            NumberAction("2");
        }

        private void num3_Click(object sender, EventArgs e)
        {
            NumberAction("3");
        }

        private void num4_Click(object sender, EventArgs e)
        {
            NumberAction("4");
        }

        private void num5_Click(object sender, EventArgs e)
        {
            NumberAction("5");
        }

        private void num6_Click(object sender, EventArgs e)
        {
            NumberAction("6");
        }

        private void num7_Click(object sender, EventArgs e)
        {
            NumberAction("7");
        }

        private void num8_Click(object sender, EventArgs e)
        {
           NumberAction("8");
        }

        private void num9_Click(object sender, EventArgs e)
        {
            NumberAction("9");
        }

        private void zeroButton_Click(object sender, EventArgs e)
        {
            if (userInput.StartsWith("0")) return;

            NumberAction("0");
        }

        private void ClearAll()
        {
            first = "";
            second = "";
            userInput = "";
            function = Operation.EMPTY;
            lastAction = Action.EMPTY;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearAll();

            updateCalculatorDisplay();
        }

        private void calculateAndShowResult()
        {
            double firstAsDouble = Double.Parse(CreateValidString(first));
            double secondAsDouble = Double.Parse(CreateValidString(userInput));

            double result = Double.NaN;
            switch (function)
            {
                case Operation.ADD:
                    result = firstAsDouble + secondAsDouble;
                    break;
                case Operation.SUBTRACT:
                    result = firstAsDouble - secondAsDouble;
                    break;
                case Operation.MULTIPLY:
                    result = firstAsDouble * secondAsDouble;
                    break;
                case Operation.DIVIDE:
                    if (secondAsDouble != 0.0) 
                    { 
                        result = firstAsDouble / secondAsDouble;
                    }
                    break;
                case Operation.EMPTY:
                    break;
            }

            if (!Double.IsNaN(result))
            {
                first = result.ToString();
                userInput = "";
                function = Operation.EMPTY;
                updateCalculatorDisplay();
            } else
            {
                ClearAll();
                calculatorDisplay.Text = "ERROR!";
            }
        }

        private string CreateValidString(string text)
        {
            return Regex.Replace(text, "\\.0*$", "");
        }

        private void divideButton_Click(object sender, EventArgs e)
        {
            OperationAction(Operation.DIVIDE);
        }

        private void multiplyButton_Click(object sender, EventArgs e)
        {
            OperationAction(Operation.MULTIPLY);
        }

        private void minusButton_Click(object sender, EventArgs e)
        {
            OperationAction(Operation.SUBTRACT);
        }

        private void plusButton_Click(object sender, EventArgs e)
        {
            OperationAction(Operation.ADD);
        }

        private void equalButton_Click(object sender, EventArgs e)
        {
            OperationAction(Operation.CALCULATE);
        }

        private void decimalButton_Click(object sender, EventArgs e)
        {
            if (userInput.Contains(',')) return;
            if (userInput.Length == 0)
            {
                NumberAction("0");
            }

            NumberAction(",");
        }

        private void updateCalculatorDisplay()
        {
            calculatorDisplay.Text = first + operationsAsText[function] + userInput;
        }
    }
}
