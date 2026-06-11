// ================================================================
// DESIGNER & COLOR SCHEME  –  .NET 10
// ================================================================
using System.Drawing;
using System.Windows.Forms;

namespace PhysicsCalculator;

// ── Color palette (Light Theme) ───────────────────────────────
public static class ColorScheme
{
    public static readonly Color MainBg       = Color.FromArgb(240, 242, 245);
    public static readonly Color DisplayBg    = Color.FromArgb(255, 255, 255);
    public static readonly Color BtnDigit     = Color.FromArgb(255, 255, 255);
    public static readonly Color BtnOperator  = Color.FromArgb(239, 246, 255);
    public static readonly Color BtnFunction  = Color.FromArgb(239, 246, 255);
    public static readonly Color BtnEquals    = Color.FromArgb(26,  86,  219);
    public static readonly Color BtnConstant  = Color.FromArgb(236, 253, 245);
    public static readonly Color BtnUtility   = Color.FromArgb(254, 242, 242);
    public static readonly Color BtnSpecial   = Color.FromArgb(255, 251, 235);
    public static readonly Color Border       = Color.FromArgb(226, 232, 240);
    public static readonly Color BrightText   = Color.FromArgb(30,  41,  59);
    public static readonly Color DimText      = Color.FromArgb(100, 116, 139);
    public static readonly Color BrightBlue   = Color.FromArgb(29,  78,  216);
    public static readonly Color BrightGreen  = Color.FromArgb(5,   150, 105);
    public static readonly Color BrightAmber  = Color.FromArgb(146, 64,  14);
    public static readonly Color BrightRed    = Color.FromArgb(220, 38,  38);
    public static readonly Color HistoryText  = Color.FromArgb(71,  85,  105);
    public static readonly Color HeaderBg     = Color.FromArgb(26,  86,  219);
}

partial class Form1
{
    private System.ComponentModel.IContainer? components;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    // ── Control declarations ───────────────────────────────────
    private Label   lblHeader     = null!;
    private Label   lblAngle      = null!;
    private Label   lblExpression = null!;
    private TextBox txtDisplay    = null!;
    private Label   lblInfo       = null!;
    private TabControl tabControl    = null!;
    private TabPage    tabCalculator = null!;
    private TabPage    tabConstants  = null!;

    // Scientific function buttons
    private Button btnSin = null!, btnCos = null!, btnTan = null!;
    private Button btnAsin = null!, btnAcos = null!, btnAtan = null!;
    private Button btnLog = null!, btnLn = null!, btnSqrt = null!, btnCbrt = null!;
    private Button btnSquare = null!, btnCube = null!, btnPower = null!;
    private Button btnExp = null!, btnAbs = null!, btnFact = null!;
    private Button btnPi = null!, btnE = null!, btnDegRad = null!;
    private Button btnReciprocal = null!;

    // Digit buttons
    private Button btn0 = null!, btn1 = null!, btn2 = null!, btn3 = null!, btn4 = null!;
    private Button btn5 = null!, btn6 = null!, btn7 = null!, btn8 = null!, btn9 = null!;
    private Button btnDecimal = null!;

    // Operator buttons
    private Button btnAdd = null!, btnSubtract = null!, btnMultiply = null!, btnDivide = null!;
    private Button btnEquals    = null!;
    private Button btnOpenParen = null!, btnCloseParen = null!;
    private Button btnPercent = null!, btnNegate = null!;

    // Utility
    private Button btnClear = null!, btnBackspace = null!;

    // History
    private Label   lblHistory  = null!;
    private ListBox listHistory = null!;

    // Constants tab
    private DataGridView dataGridConstants    = null!;
    private Button       btnUseConstant       = null!;
    private Button       btnAddConstant       = null!;
    private Button       btnEditConstant      = null!;
    private Button       btnDeleteConstant    = null!;
    private Label        lblConstantsCaption  = null!;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        lblHeader     = new Label();
        lblAngle      = new Label();
        lblExpression = new Label();
        txtDisplay    = new TextBox();
        lblInfo       = new Label();
        tabControl    = new TabControl();
        tabCalculator = new TabPage();
        tabConstants  = new TabPage();
        lblHistory    = new Label();
        listHistory   = new ListBox();

        tabControl.SuspendLayout();
        tabCalculator.SuspendLayout();
        SuspendLayout();

        // Form settings
        ClientSize      = new Size(720, 700);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        KeyPreview      = true;
        MaximizeBox     = false;
        Name            = "Form1";
        StartPosition   = FormStartPosition.CenterScreen;
        Text            = "⚛  Physics Scientific Calculator";
        BackColor       = ColorScheme.MainBg;
        Font            = new Font("Segoe UI", 9F);
        Load           += Form1_Load;

        // tabControl
        tabControl.Controls.Add(tabCalculator);
        tabControl.Controls.Add(tabConstants);
        tabControl.SelectedIndex = 0;

        // tabCalculator
        tabCalculator.Controls.Add(lblHistory);
        tabCalculator.Controls.Add(listHistory);
        tabCalculator.Name     = "tabCalculator";
        tabCalculator.TabIndex = 0;

        // tabConstants
        tabConstants.Name     = "tabConstants";
        tabConstants.TabIndex = 1;

        // listHistory event
        listHistory.DoubleClick += listHistory_DoubleClick;

        // Add main controls to form
        Controls.Add(lblHeader);
        Controls.Add(lblAngle);
        Controls.Add(lblExpression);
        Controls.Add(txtDisplay);
        Controls.Add(lblInfo);
        Controls.Add(tabControl);

        tabControl.ResumeLayout(false);
        tabCalculator.ResumeLayout(false);
        ResumeLayout(false);
    }

    // ─────────────────────────────────────────────────────────────
    // HELPER: Build calculator buttons
    // ─────────────────────────────────────────────────────────────
    private void BuildCalculatorButtons()
    {
        const int bw = 62, bh = 38, gapX = 4, gapY = 4;
        int x = 4, y = 6;

        // ── Row 1: Trigonometry ───────────────────────────────────
        btnSin  = MakeButton("sin",          x + 0*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue,  btnSin_Click);
        btnCos  = MakeButton("cos",          x + 1*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue,  btnCos_Click);
        btnTan  = MakeButton("tan",          x + 2*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue,  btnTan_Click);
        btnAsin = MakeButton("sin\u207B\u00B9", x + 3*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue,  btnAsin_Click);
        btnAcos = MakeButton("cos\u207B\u00B9", x + 4*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue,  btnAcos_Click);
        btnAtan = MakeButton("tan\u207B\u00B9", x + 5*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue,  btnAtan_Click);
        btnDegRad = MakeButton("DEG/RAD",    x + 6*(bw+gapX), y, bw, bh, ColorScheme.BtnSpecial,  ColorScheme.BrightAmber, btnDegRad_Click);

        // ── Row 2: Other functions ────────────────────────────────
        y += bh + gapY;
        btnLog    = MakeButton("log",         x + 0*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue, btnLog_Click);
        btnLn     = MakeButton("ln",          x + 1*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue, btnLn_Click);
        btnSqrt   = MakeButton("\u221Ax",     x + 2*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue, btnSqrt_Click);
        btnCbrt   = MakeButton("\u221Bx",     x + 3*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue, btnCbrt_Click);
        btnExp    = MakeButton("e\u02E3",     x + 4*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue, btnExp_Click);
        btnAbs    = MakeButton("|x|",         x + 5*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue, btnAbs_Click);
        btnFact   = MakeButton("n!",          x + 6*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue, btnFact_Click);

        // ── Row 3: Powers & constants ────────────────────────────
        y += bh + gapY;
        btnSquare     = MakeButton("x\u00B2", x + 0*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue,  btnSquare_Click);
        btnCube       = MakeButton("x\u00B3", x + 1*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue,  btnCube_Click);
        btnPower      = MakeButton("x\u207F", x + 2*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue,  btnPower_Click);
        btnReciprocal = MakeButton("1/x",     x + 3*(bw+gapX), y, bw, bh, ColorScheme.BtnFunction, ColorScheme.BrightBlue,  btnReciprocal_Click);
        btnPi         = MakeButton("\u03C0",  x + 4*(bw+gapX), y, bw, bh, ColorScheme.BtnSpecial,  ColorScheme.BrightAmber, btnPi_Click);
        btnE          = MakeButton("e",       x + 5*(bw+gapX), y, bw, bh, ColorScheme.BtnSpecial,  ColorScheme.BrightAmber, btnE_Click);

        // ── Separator line ────────────────────────────────────────
        y += bh + gapY + 4;
        var sep = new Label
        {
            Location  = new Point(x, y),
            Size      = new Size(7 * (bw + gapX), 1),
            BackColor = ColorScheme.Border,
            Text      = string.Empty
        };
        y += 6;

        // ── Utilities ─────────────────────────────────────────────
        btnClear     = MakeButton("AC",       x + 0*(bw+gapX), y, bw, bh, ColorScheme.BtnUtility,  ColorScheme.BrightRed,   btnClear_Click);
        btnBackspace = MakeButton("\u232B",   x + 1*(bw+gapX), y, bw, bh, ColorScheme.BtnOperator, ColorScheme.BrightBlue,  btnBackspace_Click);
        btnNegate    = MakeButton("+/\u2212", x + 2*(bw+gapX), y, bw, bh, ColorScheme.BtnOperator, ColorScheme.BrightBlue,  btnNegate_Click);
        btnPercent   = MakeButton("%",        x + 3*(bw+gapX), y, bw, bh, ColorScheme.BtnOperator, ColorScheme.BrightBlue,  btnPercent_Click);
        btnOpenParen = MakeButton("(",        x + 4*(bw+gapX), y, bw, bh, ColorScheme.BtnOperator, ColorScheme.BrightBlue,  btnOpenParen_Click);
        btnCloseParen= MakeButton(")",        x + 5*(bw+gapX), y, bw, bh, ColorScheme.BtnOperator, ColorScheme.BrightBlue,  btnCloseParen_Click);
        btnDivide    = MakeButton("\u00F7",   x + 6*(bw+gapX), y, bw, bh, ColorScheme.BtnOperator, ColorScheme.BrightBlue,  OperatorButton_Click, "/");

        // ── 7-8-9-× ───────────────────────────────────────────────
        y += bh + gapY;
        btn7     = MakeButton("7",        x + 0*(bw+gapX), y, bw, bh, ColorScheme.BtnDigit,    ColorScheme.BrightText, DigitButton_Click);
        btn8     = MakeButton("8",        x + 1*(bw+gapX), y, bw, bh, ColorScheme.BtnDigit,    ColorScheme.BrightText, DigitButton_Click);
        btn9     = MakeButton("9",        x + 2*(bw+gapX), y, bw, bh, ColorScheme.BtnDigit,    ColorScheme.BrightText, DigitButton_Click);
        btnMultiply = MakeButton("\u00D7",x + 3*(bw+gapX), y, bw, bh, ColorScheme.BtnOperator, ColorScheme.BrightBlue, OperatorButton_Click, "*");

        // ── 4-5-6-− ───────────────────────────────────────────────
        y += bh + gapY;
        btn4     = MakeButton("4",        x + 0*(bw+gapX), y, bw, bh, ColorScheme.BtnDigit,    ColorScheme.BrightText, DigitButton_Click);
        btn5     = MakeButton("5",        x + 1*(bw+gapX), y, bw, bh, ColorScheme.BtnDigit,    ColorScheme.BrightText, DigitButton_Click);
        btn6     = MakeButton("6",        x + 2*(bw+gapX), y, bw, bh, ColorScheme.BtnDigit,    ColorScheme.BrightText, DigitButton_Click);
        btnSubtract = MakeButton("\u2212",x + 3*(bw+gapX), y, bw, bh, ColorScheme.BtnOperator, ColorScheme.BrightBlue, OperatorButton_Click, "-");

        // ── 1-2-3-+ ───────────────────────────────────────────────
        y += bh + gapY;
        btn1   = MakeButton("1", x + 0*(bw+gapX), y, bw, bh, ColorScheme.BtnDigit,    ColorScheme.BrightText, DigitButton_Click);
        btn2   = MakeButton("2", x + 1*(bw+gapX), y, bw, bh, ColorScheme.BtnDigit,    ColorScheme.BrightText, DigitButton_Click);
        btn3   = MakeButton("3", x + 2*(bw+gapX), y, bw, bh, ColorScheme.BtnDigit,    ColorScheme.BrightText, DigitButton_Click);
        btnAdd = MakeButton("+", x + 3*(bw+gapX), y, bw, bh, ColorScheme.BtnOperator, ColorScheme.BrightBlue, OperatorButton_Click, "+");

        // ── 0-decimal-= ───────────────────────────────────────────
        y += bh + gapY;
        btn0       = MakeButton("0", x + 0*(bw+gapX), y, bw*2+gapX, bh, ColorScheme.BtnDigit,   ColorScheme.BrightText, DigitButton_Click);
        btnDecimal = MakeButton(".", x + 2*(bw+gapX), y, bw,         bh, ColorScheme.BtnDigit,   ColorScheme.BrightText, btnDecimal_Click);
        btnEquals  = MakeButton("=", x + 3*(bw+gapX), y, bw, bh*2+gapY, ColorScheme.BtnEquals,  Color.White,            btnEquals_Click);
        btnEquals.Font = new Font("Consolas", 20F, FontStyle.Bold);

        tabCalculator.Controls.AddRange(new Control[] {
            btnSin, btnCos, btnTan, btnAsin, btnAcos, btnAtan, btnDegRad,
            btnLog, btnLn, btnSqrt, btnCbrt, btnExp, btnAbs, btnFact,
            btnSquare, btnCube, btnPower, btnReciprocal, btnPi, btnE,
            sep,
            btnClear, btnBackspace, btnNegate, btnPercent,
            btnOpenParen, btnCloseParen, btnDivide,
            btn7, btn8, btn9, btnMultiply,
            btn4, btn5, btn6, btnSubtract,
            btn1, btn2, btn3, btnAdd,
            btn0, btnDecimal, btnEquals
        });
    }

    // ─────────────────────────────────────────────────────────────
    // HELPER: Build constants panel
    // ─────────────────────────────────────────────────────────────
    private void BuildConstantsPanel()
    {
        var lbl = new Label
        {
            Text      = "Manage Physics Constants  \u2014  Double-click a row to use it",
            Font      = new Font("Consolas", 8F),
            ForeColor = ColorScheme.DimText,
            Location  = new Point(8, 8),
            Size      = new Size(690, 16),
            BackColor = Color.Transparent
        };

        dataGridConstants = new DataGridView
        {
            Location     = new Point(8, 30),
            Size         = new Size(690, 480),
            BackgroundColor = ColorScheme.DisplayBg,
            GridColor    = ColorScheme.Border,
            BorderStyle  = BorderStyle.None,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            SelectionMode   = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect     = false,
            ReadOnly        = true,
            AllowUserToAddRows    = false,
            AllowUserToDeleteRows = false,
            AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
            RowHeadersVisible     = false,
            Font          = new Font("Consolas", 9F),
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
            ForeColor     = ColorScheme.BrightText,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor          = ColorScheme.DisplayBg,
                ForeColor          = ColorScheme.BrightText,
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = ColorScheme.BrightText,
                Font               = new Font("Consolas", 9F)
            },
            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = ColorScheme.DimText,
                Font      = new Font("Consolas", 8F, FontStyle.Bold)
            },
            AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = ColorScheme.BrightText
            }
        };

        dataGridConstants.Columns.Add("Symbol", "SYMBOL");
        dataGridConstants.Columns.Add("Name",   "NAME");
        dataGridConstants.Columns.Add("Value",  "VALUE");
        dataGridConstants.Columns.Add("Unit",   "UNIT");
        dataGridConstants.Columns.Add("Status", "");
        dataGridConstants.Columns["Symbol"]!.FillWeight = 80;
        dataGridConstants.Columns["Name"]!.FillWeight   = 240;
        dataGridConstants.Columns["Value"]!.FillWeight  = 160;
        dataGridConstants.Columns["Unit"]!.FillWeight   = 130;
        dataGridConstants.Columns["Status"]!.FillWeight = 40;

        const int by = 522, bh = 32, bw2 = 130, gap = 8;
        btnUseConstant    = MakeActionButton("\u25B6  Use in Calculator",  8,                                  by, bw2+30, bh, ColorScheme.BtnOperator, ColorScheme.BrightBlue,  btnUseConstant_Click);
        btnAddConstant    = MakeActionButton("+  Add Constant",            8 + bw2+30 + gap,                  by, bw2,    bh, ColorScheme.BtnConstant, ColorScheme.BrightGreen, btnAddConstant_Click);
        btnEditConstant   = MakeActionButton("\u270E  Edit / Copy",        8 + (bw2+30+gap) + (bw2+gap),      by, bw2,    bh, ColorScheme.BtnSpecial,  ColorScheme.BrightAmber, btnEditConstant_Click);
        btnDeleteConstant = MakeActionButton("\u2715  Delete",             8 + (bw2+30+gap) + (bw2+gap)*2,    by, 100,    bh, ColorScheme.BtnUtility,  ColorScheme.BrightRed,   btnDeleteConstant_Click);

        lblConstantsCaption = new Label
        {
            Text      = "\u2014 = built-in constant  |  \u270E = custom constant",
            Font      = new Font("Consolas", 7F),
            ForeColor = ColorScheme.DimText,
            Location  = new Point(8, 560),
            Size      = new Size(690, 14),
            BackColor = Color.Transparent
        };

        tabConstants.Controls.AddRange(new Control[] {
            lbl, dataGridConstants,
            btnUseConstant, btnAddConstant, btnEditConstant, btnDeleteConstant,
            lblConstantsCaption
        });
    }

    // ─────────────────────────────────────────────────────────────
    // FACTORY: Create calculator button
    // ─────────────────────────────────────────────────────────────
    private static Button MakeButton(
        string text, int x, int y, int w, int h,
        Color bg, Color fg, EventHandler? handler, string? tag = null)
    {
        var btn = new Button
        {
            Text      = text,
            Location  = new Point(x, y),
            Size      = new Size(w, h),
            BackColor = bg,
            ForeColor = fg,
            FlatStyle = FlatStyle.Flat,
            Font      = new Font("Consolas", 11F, FontStyle.Bold),
            Cursor    = Cursors.Hand,
            UseVisualStyleBackColor = false,
            Tag       = tag ?? text
        };
        btn.FlatAppearance.BorderColor         = ColorScheme.Border;
        btn.FlatAppearance.BorderSize          = 1;
        btn.FlatAppearance.MouseOverBackColor  = ControlPaint.Light(bg, 0.25f);
        btn.FlatAppearance.MouseDownBackColor  = ControlPaint.Dark(bg, 0.1f);
        if (handler is not null) btn.Click    += handler;
        return btn;
    }

    private static Button MakeActionButton(
        string text, int x, int y, int w, int h,
        Color bg, Color fg, EventHandler? handler)
    {
        var btn = new Button
        {
            Text      = text,
            Location  = new Point(x, y),
            Size      = new Size(w, h),
            BackColor = bg,
            ForeColor = fg,
            FlatStyle = FlatStyle.Flat,
            Font      = new Font("Segoe UI", 9F),
            Cursor    = Cursors.Hand,
            UseVisualStyleBackColor = false
        };
        btn.FlatAppearance.BorderColor        = ColorScheme.Border;
        btn.FlatAppearance.BorderSize         = 1;
        btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(bg, 0.3f);
        if (handler is not null) btn.Click   += handler;
        return btn;
    }
}
