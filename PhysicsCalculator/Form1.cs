// ================================================================
// MAIN FORM: Physics Calculator  –  .NET 10
// ================================================================
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PhysicsCalculator;

public sealed partial class Form1 : Form
{
    // ── Calculator state ──────────────────────────────────────────
    private string _expression = string.Empty;
    private string _lastExpression = string.Empty;
    private bool _angleRad = false;
    private readonly List<string> _history = [];
    private const int MAX_HISTORY = 10;

    public Form1()
    {
        InitializeComponent();
        BuildCalculatorButtons();
        BuildConstantsPanel();
        LoadDefaultConstants();
        SetupFormLayout();
        UpdateAngleLabel();
        UpdateExpressionLabel();
        UpdateMainDisplay("0");
    }

    // ============================================================
    // FORM LAYOUT SETUP
    // ============================================================
    private void SetupFormLayout()
    {
        // Blue header bar
        lblHeader.Text = "⚛  PHYSICS SCIENTIFIC CALCULATOR";
        lblHeader.Font = new Font("Consolas", 11F, FontStyle.Bold);
        lblHeader.ForeColor = Color.White;
        lblHeader.BackColor = ColorScheme.HeaderBg;
        lblHeader.Location = new Point(0, 0);
        lblHeader.Size = new Size(720, 32);
        lblHeader.AutoSize = false;
        lblHeader.Padding = new Padding(12, 0, 0, 0);
        lblHeader.TextAlign = ContentAlignment.MiddleLeft;

        lblAngle.Font = new Font("Consolas", 9F, FontStyle.Bold);
        lblAngle.ForeColor = Color.FromArgb(251, 191, 36);
        lblAngle.BackColor = ColorScheme.HeaderBg;
        lblAngle.Location = new Point(650, 0);
        lblAngle.Size = new Size(60, 32);
        lblAngle.TextAlign = ContentAlignment.MiddleRight;

        lblExpression.Font = new Font("Consolas", 9F);
        lblExpression.ForeColor = ColorScheme.DimText;
        lblExpression.BackColor = ColorScheme.DisplayBg;
        lblExpression.Location = new Point(0, 32);
        lblExpression.Size = new Size(720, 20);
        lblExpression.TextAlign = ContentAlignment.MiddleRight;
        lblExpression.Padding = new Padding(0, 0, 12, 0);
        lblExpression.AutoSize = false;

        txtDisplay.ReadOnly = true;
        txtDisplay.BackColor = ColorScheme.DisplayBg;
        txtDisplay.ForeColor = ColorScheme.BrightText;
        txtDisplay.BorderStyle = BorderStyle.None;
        txtDisplay.Location = new Point(0, 52);
        txtDisplay.Size = new Size(720, 48);
        txtDisplay.TextAlign = HorizontalAlignment.Right;
        txtDisplay.Padding = new Padding(0, 0, 12, 0);

        lblInfo.Font = new Font("Consolas", 8F);
        lblInfo.ForeColor = ColorScheme.BrightGreen;
        lblInfo.BackColor = ColorScheme.DisplayBg;
        lblInfo.Location = new Point(0, 100);
        lblInfo.Size = new Size(720, 18);
        lblInfo.TextAlign = ContentAlignment.MiddleRight;
        lblInfo.Padding = new Padding(0, 0, 12, 0);
        lblInfo.AutoSize = false;

        // Separator line below display
        var panelDisplay = new Panel
        {
            Location = new Point(0, 118),
            Size = new Size(720, 1),
            BackColor = ColorScheme.Border
        };
        Controls.Add(panelDisplay);

        tabControl.Location = new Point(0, 119);
        tabControl.Size = new Size(720, 580);
        tabControl.Font = new Font("Consolas", 9F);
        tabControl.BackColor = ColorScheme.MainBg;

        tabCalculator.Text = "  CALCULATOR  ";
        tabCalculator.BackColor = ColorScheme.MainBg;
        tabConstants.Text = "  PHYSICS CONSTANTS  ";
        tabConstants.BackColor = ColorScheme.MainBg;

        lblHistory.Text = "HISTORY:";
        lblHistory.Font = new Font("Consolas", 7F, FontStyle.Bold);
        lblHistory.ForeColor = ColorScheme.DimText;
        lblHistory.BackColor = Color.Transparent;
        lblHistory.Location = new Point(500, 6);
        lblHistory.Size = new Size(200, 14);

        listHistory.BackColor = ColorScheme.DisplayBg;
        listHistory.ForeColor = ColorScheme.HistoryText;
        listHistory.Font = new Font("Consolas", 8F);
        listHistory.BorderStyle = BorderStyle.FixedSingle;
        listHistory.Location = new Point(500, 22);
        listHistory.Size = new Size(205, 510);
        listHistory.DrawMode = DrawMode.Normal;

        ClientSize = new Size(720, 700);
        this.BackColor = ColorScheme.MainBg;
    }

    // ============================================================
    // UPDATE DISPLAY
    // ============================================================
    private void UpdateMainDisplay(string text)
    {
        txtDisplay.Text = text;
        txtDisplay.Font = text.Length switch
        {
            > 16 => new Font("Consolas", 16F, FontStyle.Bold),
            > 12 => new Font("Consolas", 20F, FontStyle.Bold),
            _ => new Font("Consolas", 26F, FontStyle.Bold),
        };
    }

    private void UpdateExpressionLabel() => lblExpression.Text = _lastExpression;
    private void UpdateAngleLabel() => lblAngle.Text = _angleRad ? "RAD" : "DEG";

    private void AddHistory(string entry)
    {
        _history.Insert(0, entry);
        if (_history.Count > MAX_HISTORY)
            _history.RemoveAt(_history.Count - 1);
        RenderHistory();
    }

    private void RenderHistory()
    {
        listHistory.BeginUpdate();
        listHistory.Items.Clear();
        foreach (var r in _history)
            listHistory.Items.Add(r);
        listHistory.EndUpdate();
    }

    // ============================================================
    // CHARACTER & FUNCTION INPUT
    // ============================================================
    private void AppendCharacter(string ch)
    {
        if (_expression is "0" or "Error") _expression = string.Empty;
        _expression += ch;
        UpdateMainDisplay(_expression);
    }

    private void AppendFunction(string fn)
    {
        if (_expression is "0" or "Error") _expression = string.Empty;
        if (_expression.Length > 0 && !IsEndingWithOperator(_expression))
            _expression += "*";
        _expression += fn;
        UpdateMainDisplay(_expression);
    }

    private static bool IsEndingWithOperator(string s) =>
        s.Length == 0 || "+-*/(^,".Contains(s[^1]);

    // ============================================================
    // DIGIT BUTTONS
    // ============================================================
    private void DigitButton_Click(object? sender, EventArgs e)
    {
        if (sender is Button btn)
            AppendCharacter(btn.Text);
    }

    private void btnDecimal_Click(object? sender, EventArgs e)
    {
        var parts = Regex.Split(_expression, @"[\+\-\*\/\(\)\^]");
        string lastPart = parts.Length > 0 ? parts[^1] : string.Empty;
        if (!lastPart.Contains('.'))
            AppendCharacter(".");
    }

    // ============================================================
    // SCIENTIFIC FUNCTION BUTTONS
    // ============================================================
    private void btnSin_Click(object? sender, EventArgs e) => AppendFunction("sin(");
    private void btnCos_Click(object? sender, EventArgs e) => AppendFunction("cos(");
    private void btnTan_Click(object? sender, EventArgs e) => AppendFunction("tan(");
    private void btnAsin_Click(object? sender, EventArgs e) => AppendFunction("asin(");
    private void btnAcos_Click(object? sender, EventArgs e) => AppendFunction("acos(");
    private void btnAtan_Click(object? sender, EventArgs e) => AppendFunction("atan(");
    private void btnLog_Click(object? sender, EventArgs e) => AppendFunction("log(");
    private void btnLn_Click(object? sender, EventArgs e) => AppendFunction("ln(");
    private void btnSqrt_Click(object? sender, EventArgs e) => AppendFunction("sqrt(");
    private void btnCbrt_Click(object? sender, EventArgs e) => AppendFunction("cbrt(");
    private void btnExp_Click(object? sender, EventArgs e) => AppendFunction("exp(");
    private void btnAbs_Click(object? sender, EventArgs e) => AppendFunction("abs(");
    private void btnFact_Click(object? sender, EventArgs e) => AppendFunction("fact(");

    private void btnSquare_Click(object? sender, EventArgs e)
    {
        if (_expression.Length > 0) { _expression += "^2"; UpdateMainDisplay(_expression); }
    }

    private void btnCube_Click(object? sender, EventArgs e)
    {
        if (_expression.Length > 0) { _expression += "^3"; UpdateMainDisplay(_expression); }
    }

    private void btnPower_Click(object? sender, EventArgs e) => AppendCharacter("^(");

    private void btnReciprocal_Click(object? sender, EventArgs e)
    {
        if (_expression.Length > 0 && _expression != "0")
        {
            _expression = $"1/({_expression})";
            UpdateMainDisplay(_expression);
        }
    }

    private void btnPi_Click(object? sender, EventArgs e)
    {
        if (_expression is "0" or "Error") _expression = string.Empty;
        if (_expression.Length > 0 && !IsEndingWithOperator(_expression)) _expression += "*";
        _expression += "3.14159265358979";
        UpdateMainDisplay(_expression);
        lblInfo.Text = "π = 3.14159265358979...";
    }

    private void btnE_Click(object? sender, EventArgs e)
    {
        if (_expression is "0" or "Error") _expression = string.Empty;
        if (_expression.Length > 0 && !IsEndingWithOperator(_expression)) _expression += "*";
        _expression += "2.71828182845905";
        UpdateMainDisplay(_expression);
        lblInfo.Text = "e = 2.71828182845905...";
    }

    private void btnDegRad_Click(object? sender, EventArgs e)
    {
        _angleRad = !_angleRad;
        UpdateAngleLabel();
        lblInfo.Text = "Angle mode: " + (_angleRad ? "Radians" : "Degrees");
    }

    // ============================================================
    // OPERATOR BUTTONS (+, -, *, /, (, ))
    // ============================================================
    private void OperatorButton_Click(object? sender, EventArgs e)
    {
        if (sender is Button btn)
            AppendCharacter(btn.Tag?.ToString() ?? btn.Text);
    }

    private void btnOpenParen_Click(object? sender, EventArgs e)
    {
        if (_expression is "0" or "Error") _expression = string.Empty;
        _expression += _expression.Length > 0 && !IsEndingWithOperator(_expression) ? "*(" : "(";
        UpdateMainDisplay(_expression);
    }

    private void btnCloseParen_Click(object? sender, EventArgs e) => AppendCharacter(")");

    private void btnPercent_Click(object? sender, EventArgs e)
    {
        if (double.TryParse(_expression, out double number))
        {
            double result = number / 100;
            _expression = result.ToString();
            UpdateMainDisplay(_expression);
        }
    }

    // ============================================================
    // UTILITIES (AC, ⌫, +/-)
    // ============================================================
    private void btnClear_Click(object? sender, EventArgs e)
    {
        _expression = string.Empty;
        _lastExpression = string.Empty;
        UpdateMainDisplay("0");
        UpdateExpressionLabel();
        lblInfo.Text = string.Empty;
    }

    private void btnBackspace_Click(object? sender, EventArgs e)
    {
        _expression = _expression.Length > 1
            ? _expression[..^1]
            : string.Empty;
        UpdateMainDisplay(_expression.Length > 0 ? _expression : "0");
    }

    private void btnNegate_Click(object? sender, EventArgs e)
    {
        if (_expression.StartsWith('-'))
            _expression = _expression[1..];
        else if (_expression.Length > 0 && _expression != "0")
            _expression = "-" + _expression;
        UpdateMainDisplay(_expression.Length > 0 ? _expression : "0");
    }

    // ============================================================
    // EQUALS – EVALUATE EXPRESSION
    // ============================================================
    private void btnEquals_Click(object? sender, EventArgs e)
    {
        if (_expression.Length == 0) return;
        try
        {
            string snap = _expression;
            double result = EvaluateExpression(_expression);
            string resultStr = FormatResult(result);

            _lastExpression = snap + " =";
            UpdateExpressionLabel();
            UpdateMainDisplay(resultStr);
            AddHistory(snap + " = " + resultStr);
            _expression = resultStr;
            lblInfo.Text = string.Empty;
        }
        catch (Exception ex)
        {
            UpdateMainDisplay("Error");
            lblInfo.Text = ex.Message;
            _expression = string.Empty;
        }
    }

    // ============================================================
    // EXPRESSION EVALUATION
    // ============================================================
    private double EvaluateExpression(string expr) =>
        ComputeExpression(PreprocessExpression(expr));

    private static string PreprocessExpression(string expr)
    {
        string s = expr.Trim();
        s = Regex.Replace(s, @"(\d)(\()", "$1*$2");
        s = Regex.Replace(s, @"(\))(\d)", "$1*$2");
        s = Regex.Replace(s, @"(\))(\()", "$1*$2");
        return s;
    }

    private double ComputeExpression(string expr)
    {
        expr = expr.Trim();

        if (double.TryParse(expr,
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture,
            out double directNumber))
            return directNumber;

        while (true)
        {
            var match = Regex.Match(expr,
                @"(sin|cos|tan|asin|acos|atan|log|ln|sqrt|cbrt|exp|abs|fact)\(([^()]+)\)");
            if (!match.Success) break;

            double arg = ComputeExpression(match.Groups[2].Value);
            double result = ApplyFunction(match.Groups[1].Value, arg);
            expr = expr[..match.Index]
                 + result.ToString("R", System.Globalization.CultureInfo.InvariantCulture)
                 + expr[(match.Index + match.Length)..];
        }

        while (true)
        {
            var match = Regex.Match(expr,
                @"(-?\d+\.?\d*(?:E[+-]?\d+)?)\^(-?\d+\.?\d*(?:E[+-]?\d+)?)");
            if (!match.Success) break;

            double baseVal = double.Parse(match.Groups[1].Value, System.Globalization.CultureInfo.InvariantCulture);
            double exponent = double.Parse(match.Groups[2].Value, System.Globalization.CultureInfo.InvariantCulture);
            double result = Math.Pow(baseVal, exponent);
            expr = expr[..match.Index]
                 + result.ToString("R", System.Globalization.CultureInfo.InvariantCulture)
                 + expr[(match.Index + match.Length)..];
        }

        try
        {
            var table = new DataTable();
            var result2 = table.Compute(expr, string.Empty);
            return Convert.ToDouble(result2);
        }
        catch
        {
            throw new InvalidOperationException("Invalid expression: " + expr);
        }
    }

    private double ApplyFunction(string fn, double arg)
    {
        double toRad = _angleRad ? arg : arg * Math.PI / 180.0;
        double fromRad(double r) => _angleRad ? r : r * 180.0 / Math.PI;

        return fn switch
        {
            "sin" => Math.Sin(toRad),
            "cos" => Math.Cos(toRad),
            "tan" => Math.Abs(Math.Cos(toRad)) < 1e-10
                        ? throw new InvalidOperationException("tan is undefined at 90° or its multiples")
                        : Math.Tan(toRad),
            "asin" => arg is < -1 or > 1
                        ? throw new InvalidOperationException("asin: argument must be -1 ≤ x ≤ 1")
                        : fromRad(Math.Asin(arg)),
            "acos" => arg is < -1 or > 1
                        ? throw new InvalidOperationException("acos: argument must be -1 ≤ x ≤ 1")
                        : fromRad(Math.Acos(arg)),
            "atan" => fromRad(Math.Atan(arg)),
            "log" => arg <= 0
                        ? throw new InvalidOperationException("log: argument must be > 0")
                        : Math.Log10(arg),
            "ln" => arg <= 0
                        ? throw new InvalidOperationException("ln: argument must be > 0")
                        : Math.Log(arg),
            "sqrt" => arg < 0
                        ? throw new InvalidOperationException("sqrt: argument cannot be negative")
                        : Math.Sqrt(arg),
            "cbrt" => Math.Cbrt(arg),
            "exp" => Math.Exp(arg),
            "abs" => Math.Abs(arg),
            "fact" => arg < 0 || arg != Math.Floor(arg)
                        ? throw new InvalidOperationException("fact: must be a non-negative integer")
                        : Factorial((int)arg),
            _ => throw new InvalidOperationException("Unknown function: " + fn)
        };
    }

    private static double Factorial(int n)
    {
        if (n > 170) return double.PositiveInfinity;
        double result = 1;
        for (int i = 2; i <= n; i++) result *= i;
        return result;
    }

    private static string FormatResult(double value)
    {
        if (double.IsNaN(value)) return "Error";
        if (double.IsPositiveInfinity(value)) return "∞";
        if (double.IsNegativeInfinity(value)) return "-∞";

        double abs = Math.Abs(value);
        if (abs != 0 && (abs >= 1e10 || abs < 1e-4))
            return value.ToString("E6", System.Globalization.CultureInfo.InvariantCulture);

        return value.ToString("G10", System.Globalization.CultureInfo.InvariantCulture);
    }

    // ============================================================
    // HISTORY
    // ============================================================
    private void listHistory_DoubleClick(object? sender, EventArgs e)
    {
        if (listHistory.SelectedIndex < 0) return;
        string item = listHistory.SelectedItem?.ToString() ?? string.Empty;
        int idx = item.LastIndexOf(" = ", StringComparison.Ordinal);
        if (idx >= 0)
        {
            _expression = item[(idx + 3)..];
            UpdateMainDisplay(_expression);
        }
    }

    // ============================================================
    // PHYSICS CONSTANTS MANAGEMENT
    // ============================================================
    private void LoadDefaultConstants()
    {
        ConstantsManager.Instance.LoadDefaults();
        RefreshConstantsGrid();
    }

    private void RefreshConstantsGrid()
    {
        dataGridConstants.SuspendLayout();
        dataGridConstants.Rows.Clear();
        foreach (var c in ConstantsManager.Instance.AllConstants())
        {
            string valueStr = c.Value.ToString("E4", System.Globalization.CultureInfo.InvariantCulture);
            dataGridConstants.Rows.Add(c.Symbol, c.Name, valueStr, c.Unit, c.IsCustom ? "✎" : "—");
        }
        dataGridConstants.ResumeLayout();
    }

    private void btnUseConstant_Click(object? sender, EventArgs e)
    {
        if (dataGridConstants.SelectedRows.Count == 0) return;
        int idx = dataGridConstants.SelectedRows[0].Index;
        var constant = ConstantsManager.Instance.AllConstants().ElementAt(idx);

        if (_expression is "0" or "Error") _expression = string.Empty;
        if (_expression.Length > 0 && !IsEndingWithOperator(_expression)) _expression += "*";
        _expression += constant.Value.ToString("R", System.Globalization.CultureInfo.InvariantCulture);
        UpdateMainDisplay(_expression);
        lblInfo.Text = $"{constant.Symbol} = {constant.Value:E6} {constant.Unit}";
        tabControl.SelectedIndex = 0;
    }

    private void btnAddConstant_Click(object? sender, EventArgs e)
    {
        using var form = new FormConstant();
        if (form.ShowDialog() == DialogResult.OK && form.NewConstant is not null)
        {
            ConstantsManager.Instance.Add(form.NewConstant);
            RefreshConstantsGrid();
        }
    }

    private void btnEditConstant_Click(object? sender, EventArgs e)
    {
        if (dataGridConstants.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a constant to edit.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        int idx = dataGridConstants.SelectedRows[0].Index;
        var constant = ConstantsManager.Instance.AllConstants().ElementAt(idx);

        using var form = new FormConstant(constant);
        var result = form.ShowDialog();

        if (result == DialogResult.OK && form.NewConstant is not null)
        {
            ConstantsManager.Instance.Update(idx, form.NewConstant);
            RefreshConstantsGrid();
        }
        else if (result == DialogResult.Abort && constant.IsCustom)
        {
            ConstantsManager.Instance.Delete(idx);
            RefreshConstantsGrid();
        }
    }

    private void btnDeleteConstant_Click(object? sender, EventArgs e)
    {
        if (dataGridConstants.SelectedRows.Count == 0) return;

        int idx = dataGridConstants.SelectedRows[0].Index;
        var constant = ConstantsManager.Instance.AllConstants().ElementAt(idx);

        if (!constant.IsCustom)
        {
            MessageBox.Show(
                "Built-in constants cannot be deleted.\nUse Edit to customize their values.",
                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirm = MessageBox.Show(
            $"Delete constant '{constant.Symbol} - {constant.Name}'?",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirm == DialogResult.Yes)
        {
            ConstantsManager.Instance.Delete(idx);
            RefreshConstantsGrid();
        }
    }

    // ============================================================
    // KEYBOARD SUPPORT
    // ============================================================
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        switch (keyData)
        {
            case Keys.D0 or Keys.NumPad0: AppendCharacter("0"); return true;
            case Keys.D1 or Keys.NumPad1: AppendCharacter("1"); return true;
            case Keys.D2 or Keys.NumPad2: AppendCharacter("2"); return true;
            case Keys.D3 or Keys.NumPad3: AppendCharacter("3"); return true;
            case Keys.D4 or Keys.NumPad4: AppendCharacter("4"); return true;
            case Keys.D5 or Keys.NumPad5: AppendCharacter("5"); return true;
            case Keys.D6 or Keys.NumPad6: AppendCharacter("6"); return true;
            case Keys.D7 or Keys.NumPad7: AppendCharacter("7"); return true;
            case Keys.D8 or Keys.NumPad8: AppendCharacter("8"); return true;
            case Keys.D9 or Keys.NumPad9: AppendCharacter("9"); return true;
            case Keys.OemPeriod or Keys.Decimal: btnDecimal_Click(null, EventArgs.Empty); return true;
            case Keys.Add: AppendCharacter("+"); return true;
            case Keys.Subtract: AppendCharacter("-"); return true;
            case Keys.Multiply: AppendCharacter("*"); return true;
            case Keys.Divide: AppendCharacter("/"); return true;
            case Keys.Enter or Keys.Return: btnEquals_Click(null, EventArgs.Empty); return true;
            case Keys.Back: btnBackspace_Click(null, EventArgs.Empty); return true;
            case Keys.Escape: btnClear_Click(null, EventArgs.Empty); return true;
            case Keys.OemOpenBrackets: AppendCharacter("("); return true;
            case Keys.OemCloseBrackets: AppendCharacter(")"); return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        BuildMenuBar();
    }

    // ============================================================
    // MENU BAR
    // ============================================================
    private void BuildMenuBar()
    {
        var menuStrip = new MenuStrip  
        {
            BackColor = ColorScheme.HeaderBg,
            ForeColor = Color.White,
            Font = new Font("Consolas", 9F),
            Dock = DockStyle.None,
        };

        var aboutItem = new ToolStripMenuItem("About")
        {
            ForeColor = Color.White,
            BackColor = ColorScheme.HeaderBg,
        };
        aboutItem.Click += (_, _) =>
        {
            var dlg = new FormAbout();
            dlg.ShowDialog(this);
            dlg.Dispose();
        };

        menuStrip.Items.Add(aboutItem);
        menuStrip.RenderMode = ToolStripRenderMode.Professional;
        menuStrip.Renderer = new DarkMenuRenderer();

        Controls.Add(menuStrip);
        menuStrip.BringToFront();
        menuStrip.Size = new Size(720, 32);
        menuStrip.Location = new Point(0, 0);
        MainMenuStrip = menuStrip;
    }
    private void btnAbout_Click(object? sender, EventArgs e)
    {
        using var dlg = new FormAbout();
        dlg.ShowDialog(this);
    }

    // ============================================================
    // DARK MENU RENDERER (matches blue header)
    // ============================================================
    private sealed class DarkMenuRenderer : ToolStripProfessionalRenderer
    {
        public DarkMenuRenderer()
            : base(new DarkMenuColorTable()) { }

        protected override void OnRenderMenuItemBackground(
            ToolStripItemRenderEventArgs e)
        {
            var item = e.Item;
            var g    = e.Graphics;
            var rect = new Rectangle(Point.Empty, item.Size);

            Color bg = item.Selected
                ? Color.FromArgb(50, 120, 240)   // hover: lighter blue
                : ColorScheme.HeaderBg;           // normal: header blue

            using var brush = new SolidBrush(bg);
            g.FillRectangle(brush, rect);
        }

        protected override void OnRenderToolStripBackground(
            ToolStripRenderEventArgs e)
        {
            using var brush = new SolidBrush(ColorScheme.HeaderBg);
            e.Graphics.FillRectangle(brush, e.AffectedBounds);
        }

        protected override void OnRenderToolStripBorder(
            ToolStripRenderEventArgs e) { /* no border */ }
    }

    private sealed class DarkMenuColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected          => Color.FromArgb(50, 120, 240);
        public override Color MenuItemBorder            => Color.FromArgb(80, 140, 255);
        public override Color MenuBorder                => ColorScheme.Border;
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(50, 120, 240);
        public override Color MenuItemSelectedGradientEnd   => Color.FromArgb(50, 120, 240);
        public override Color MenuItemPressedGradientBegin  => Color.FromArgb(26,  86, 219);
        public override Color MenuItemPressedGradientEnd    => Color.FromArgb(26,  86, 219);
        public override Color ToolStripDropDownBackground   => Color.FromArgb(30,  50,  80);
        public override Color ImageMarginGradientBegin      => Color.FromArgb(30,  50,  80);
        public override Color ImageMarginGradientMiddle     => Color.FromArgb(30,  50,  80);
        public override Color ImageMarginGradientEnd        => Color.FromArgb(30,  50,  80);
    }
}
