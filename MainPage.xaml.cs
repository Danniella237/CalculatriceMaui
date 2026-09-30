namespace CalculatriceMaui;

public partial class MainPage : ContentPage
{
    private double _firstNumber = 0;
    private string _operator = "";
    private bool _isOperatorClicked = false;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnNumberClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        string pressed = button.Text;

        if (ResultLabel.Text == "0" || _isOperatorClicked || ResultLabel.Text == "Erreur")
        {
            ResultLabel.Text = pressed;
            _isOperatorClicked = false;
        }
        else
        {
            ResultLabel.Text += pressed;
        }
    }

    private void OnDecimalClicked(object sender, EventArgs e)
    {
        if (_isOperatorClicked || ResultLabel.Text == "Erreur")
        {
            ResultLabel.Text = "0,";
            _isOperatorClicked = false;
            return;
        }

        if (!ResultLabel.Text.Contains(","))
        {
            ResultLabel.Text += ",";
        }
    }

    private void OnOperationClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        if (double.TryParse(ResultLabel.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out double num))
        {
            _firstNumber = num;
            _operator = button.Text;
            CurrentOperationLabel.Text = $"{_firstNumber} {_operator}";
            _isOperatorClicked = true;
        }
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_operator)) return;

        if (double.TryParse(ResultLabel.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out double secondNumber))
        {
            CurrentOperationLabel.Text = $"{_firstNumber} {_operator} {secondNumber} =";
            double result = 0;

            switch (_operator)
            {
                case "+":
                    result = _firstNumber + secondNumber;
                    break;
                case "-":
                    result = _firstNumber - secondNumber;
                    break;
                case "×":
                    result = _firstNumber * secondNumber;
                    break;
                case "÷":
                    if (secondNumber != 0)
                    {
                        result = _firstNumber / secondNumber;
                    }
                    else
                    {
                        // Gestion explicite de la division par zéro sans plantage
                        ResultLabel.Text = "Erreur";
                        _operator = "";
                        return;
                    }
                    break;
            }

            ResultLabel.Text = result.ToString();
            _operator = "";
            _isOperatorClicked = true;
        }
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        ResultLabel.Text = "0";
        CurrentOperationLabel.Text = "";
        _firstNumber = 0;
        _operator = "";
        _isOperatorClicked = false;
    }

    private void OnBackspaceClicked(object sender, EventArgs e)
    {
        if (_isOperatorClicked || ResultLabel.Text == "Erreur") return;

        if (ResultLabel.Text.Length > 1)
        {
            ResultLabel.Text = ResultLabel.Text.Substring(0, ResultLabel.Text.Length - 1);
        }
        else
        {
            ResultLabel.Text = "0";
        }
    }

    private void OnToggleSignClicked(object sender, EventArgs e)
    {
        if (double.TryParse(ResultLabel.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out double number))
        {
            number = -number;
            ResultLabel.Text = number.ToString();
        }
    }

    private void OnPercentageClicked(object sender, EventArgs e)
    {
        if (double.TryParse(ResultLabel.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out double number))
        {
            number = number / 100;
            ResultLabel.Text = number.ToString();
        }
    }
}