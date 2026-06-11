// ================================================================
// DIALOG FORM: Add / Edit Physics Constant  –  .NET 10
// ================================================================
using System.Drawing;
using System.Windows.Forms;

namespace PhysicsCalculator;

public sealed class FormConstant : Form
{
    // ── Output ────────────────────────────────────────────────────
    public PhysicsConstant? NewConstant { get; private set; }

    // ── Controls ──────────────────────────────────────────────────
    private readonly TextBox txtSymbol;
    private readonly TextBox txtName;
    private readonly TextBox txtValue;
    private readonly TextBox txtUnit;
    private readonly Button  btnOK;
    private readonly Button  btnCancel;
    private readonly Button  btnDelete;
    private readonly Label   lblTitle;
    private readonly bool    _isEdit;

    // ── Constructor: Add mode ──────────────────────────────────────
    public FormConstant() : this(null) { }

    // ── Constructor: Edit mode ─────────────────────────────────────
    public FormConstant(PhysicsConstant? existing)
    {
        _isEdit = existing is not null;

        // ── Form settings ─────────────────────────────────────────
        Text            = _isEdit ? "Edit Constant" : "Add New Constant";
        ClientSize      = new Size(400, 300);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition   = FormStartPosition.CenterParent;
        MaximizeBox     = false;
        MinimizeBox     = false;
        BackColor       = ColorScheme.MainBg;
        Font            = new Font("Consolas", 9F);

        // ── Header ────────────────────────────────────────────────
        lblTitle = new Label
        {
            Text      = _isEdit ? "\u270E  EDIT PHYSICS CONSTANT" : "+  NEW PHYSICS CONSTANT",
            Font      = new Font("Consolas", 10F, FontStyle.Bold),
            ForeColor = _isEdit ? ColorScheme.BrightAmber : ColorScheme.BrightGreen,
            Location  = new Point(12, 12),
            Size      = new Size(376, 22),
            BackColor = Color.Transparent
        };

        // ── Field helpers ─────────────────────────────────────────
        static Label Lbl(string t, int y) => new()
        {
            Text      = t,
            Font      = new Font("Consolas", 8F, FontStyle.Bold),
            ForeColor = ColorScheme.DimText,
            Location  = new Point(12, y),
            Size      = new Size(376, 16),
            BackColor = Color.Transparent
        };

        static TextBox Txt(int y, int w = 376) => new()
        {
            Location    = new Point(12, y),
            Size        = new Size(w, 26),
            BackColor   = Color.White,
            ForeColor   = ColorScheme.BrightText,
            Font        = new Font("Consolas", 10F),
            BorderStyle = BorderStyle.FixedSingle
        };

        // ── Symbol ────────────────────────────────────────────────
        var lblSymbol = Lbl("SYMBOL (e.g. k_B, m_e, G_F):", 44);
        txtSymbol = Txt(62, 120);
        txtSymbol.CharacterCasing = CharacterCasing.Normal;

        // ── Name ──────────────────────────────────────────────────
        var lblName = Lbl("CONSTANT NAME:", 96);
        txtName = Txt(114, 376);

        // ── Value ─────────────────────────────────────────────────
        var lblValue = Lbl("VALUE (use notation: 1.23E-10 or 6.674e-11):", 148);
        txtValue = Txt(166, 260);

        // ── Unit ──────────────────────────────────────────────────
        var lblUnit = Lbl("UNIT (e.g. m/s, J\u00B7s, mol\u207B\u00B9):", 200);
        txtUnit = Txt(218, 376);

        // ── OK button ─────────────────────────────────────────────
        btnOK = new Button
        {
            Text                    = _isEdit ? "\u2713  Save Changes" : "+  Add",
            Location                = new Point(12, 258),
            Size                    = new Size(150, 30),
            BackColor               = _isEdit ? ColorScheme.BtnSpecial   : ColorScheme.BtnConstant,
            ForeColor               = _isEdit ? ColorScheme.BrightAmber  : ColorScheme.BrightGreen,
            FlatStyle               = FlatStyle.Flat,
            Cursor                  = Cursors.Hand,
            UseVisualStyleBackColor = false
        };
        btnOK.FlatAppearance.BorderColor = ColorScheme.Border;
        btnOK.FlatAppearance.BorderSize  = 1;
        btnOK.Click += BtnOK_Click;

        // ── Cancel button ─────────────────────────────────────────
        btnCancel = new Button
        {
            Text                    = "Cancel",
            Location                = new Point(170, 258),
            Size                    = new Size(80, 30),
            BackColor               = ColorScheme.BtnDigit,
            ForeColor               = ColorScheme.DimText,
            FlatStyle               = FlatStyle.Flat,
            Cursor                  = Cursors.Hand,
            UseVisualStyleBackColor = false
        };
        btnCancel.FlatAppearance.BorderColor = ColorScheme.Border;
        btnCancel.FlatAppearance.BorderSize  = 1;
        btnCancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };

        // ── Delete button (edit mode, custom constants only) ──────
        btnDelete = new Button
        {
            Text                    = "\u2715  Delete",
            Location                = new Point(258, 258),
            Size                    = new Size(130, 30),
            BackColor               = ColorScheme.BtnUtility,
            ForeColor               = ColorScheme.BrightRed,
            FlatStyle               = FlatStyle.Flat,
            Cursor                  = Cursors.Hand,
            UseVisualStyleBackColor = false,
            Visible                 = _isEdit && (existing?.IsCustom ?? false)
        };
        btnDelete.FlatAppearance.BorderColor = ColorScheme.Border;
        btnDelete.FlatAppearance.BorderSize  = 1;
        btnDelete.Click += (_, _) =>
        {
            var conf = MessageBox.Show(
                "Delete this constant?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (conf == DialogResult.Yes)
            {
                DialogResult = DialogResult.Abort;
                Close();
            }
        };

        // ── Pre-fill fields in edit mode ──────────────────────────
        if (existing is not null)
        {
            txtSymbol.Text = existing.Symbol;
            txtName.Text   = existing.Name;
            txtValue.Text  = existing.Value.ToString("R", System.Globalization.CultureInfo.InvariantCulture);
            txtUnit.Text   = existing.Unit;

            // Mark note if editing a built-in constant
            if (!existing.IsCustom)
            {
                var noteBuiltIn = new Label
                {
                    Text      = "\u26A0  Built-in constant: changes will be saved as a custom copy.",
                    Font      = new Font("Consolas", 7F),
                    ForeColor = ColorScheme.BrightAmber,
                    Location  = new Point(12, 238),
                    Size      = new Size(376, 16),
                    BackColor = Color.Transparent
                };
                Controls.Add(noteBuiltIn);
            }
        }

        // ── Add controls ──────────────────────────────────────────
        Controls.AddRange(new Control[] {
            lblTitle,
            lblSymbol, txtSymbol,
            lblName,   txtName,
            lblValue,  txtValue,
            lblUnit,   txtUnit,
            btnOK, btnCancel, btnDelete
        });

        AcceptButton = btnOK;
        CancelButton = btnCancel;
    }

    // ── Validate & save ───────────────────────────────────────────
    private void BtnOK_Click(object? sender, EventArgs e)
    {
        string symbol   = txtSymbol.Text.Trim();
        string name     = txtName.Text.Trim();
        string valueStr = txtValue.Text.Trim().Replace(",", ".");
        string unit     = txtUnit.Text.Trim();

        if (string.IsNullOrWhiteSpace(symbol))
        {
            MessageBox.Show("Symbol cannot be empty.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtSymbol.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Constant name cannot be empty.", "Validation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtName.Focus();
            return;
        }

        if (!double.TryParse(valueStr,
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture,
            out double value))
        {
            MessageBox.Show(
                "Invalid value format.\nUse a dot as decimal separator, e.g. 6.674E-11",
                "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtValue.Focus();
            return;
        }

        NewConstant  = new PhysicsConstant(symbol, name, value, unit, IsCustom: true);
        DialogResult = DialogResult.OK;
        Close();
    }
}
