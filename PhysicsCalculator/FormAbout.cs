// ================================================================
// DIALOG FORM: About  –  .NET 10
// ================================================================
using System.Drawing;
using System.Windows.Forms;

namespace PhysicsCalculator;

public sealed class FormAbout : Form
{
    public FormAbout()
    {
        // ── Form settings ─────────────────────────────────────────
        Text            = "About";
        ClientSize      = new Size(380, 280);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition   = FormStartPosition.CenterParent;
        MaximizeBox     = false;
        MinimizeBox     = false;
        BackColor       = ColorScheme.MainBg;
        Font            = new Font("Consolas", 9F);

        // ── Blue header bar ───────────────────────────────────────
        var pnlHeader = new Panel
        {
            Location  = new Point(0, 0),
            Size      = new Size(380, 56),
            BackColor = ColorScheme.HeaderBg
        };

        var lblAppIcon = new Label
        {
            Text      = "⚛",
            Font      = new Font("Segoe UI", 26F),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            Location  = new Point(16, 8),
            Size      = new Size(44, 44),
            TextAlign = ContentAlignment.MiddleCenter
        };

        var lblAppName = new Label
        {
            Text      = "Physics Scientific Calculator",
            Font      = new Font("Consolas", 12F, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.Transparent,
            Location  = new Point(66, 8),
            Size      = new Size(300, 22),
            TextAlign = ContentAlignment.MiddleLeft
        };

        var lblAppSub = new Label
        {
            Text      = "Scientific calculator for physics students",
            Font      = new Font("Consolas", 8F),
            ForeColor = Color.FromArgb(186, 212, 255),
            BackColor = Color.Transparent,
            Location  = new Point(66, 32),
            Size      = new Size(300, 16),
            TextAlign = ContentAlignment.MiddleLeft
        };

        pnlHeader.Controls.AddRange(new Control[] { lblAppIcon, lblAppName, lblAppSub });

        // ── Separator ─────────────────────────────────────────────
        var sep1 = new Label
        {
            Location  = new Point(0, 56),
            Size      = new Size(380, 1),
            BackColor = ColorScheme.Border,
            Text      = string.Empty
        };

        // ── Info rows ─────────────────────────────────────────────
        static Label MakeCaption(string t, int y) => new()
        {
            Text      = t,
            Font      = new Font("Consolas", 8F, FontStyle.Bold),
            ForeColor = ColorScheme.DimText,
            Location  = new Point(24, y),
            Size      = new Size(110, 18),
            BackColor = Color.Transparent,
            TextAlign = ContentAlignment.MiddleLeft
        };

        static Label MakeValue(string t, int y, Color? col = null) => new()
        {
            Text      = t,
            Font      = new Font("Consolas", 9F),
            ForeColor = col ?? ColorScheme.BrightText,
            Location  = new Point(140, y),
            Size      = new Size(220, 18),
            BackColor = Color.Transparent,
            TextAlign = ContentAlignment.MiddleLeft
        };

        var lblCaptionAuthor  = MakeCaption("Author",   76);
        var lblValueAuthor    = MakeValue("Vio Gustian Nur Alamsyah", 76, ColorScheme.BrightBlue);

        var lblCaptionYear    = MakeCaption("Year",     104);
        var lblValueYear      = MakeValue("2025", 104);

        var lblCaptionVersion = MakeCaption("Version",  132);
        var lblValueVersion   = MakeValue("1.0.0", 132);

        var lblCaptionRuntime = MakeCaption("Runtime",  160);
        var lblValueRuntime   = MakeValue(".NET 10  ·  Windows Forms", 160);

        var lblCaptionLang    = MakeCaption("Language", 188);
        var lblValueLang      = MakeValue("C#  13", 188);

        // ── Separator ─────────────────────────────────────────────
        var sep2 = new Label
        {
            Location  = new Point(24, 218),
            Size      = new Size(332, 1),
            BackColor = ColorScheme.Border,
            Text      = string.Empty
        };

        // ── Constants info ────────────────────────────────────────
        var lblConstants = new Label
        {
            Text      = "Built-in constants: CODATA 2022 recommended values",
            Font      = new Font("Consolas", 7F),
            ForeColor = ColorScheme.DimText,
            Location  = new Point(24, 226),
            Size      = new Size(332, 14),
            BackColor = Color.Transparent,
            TextAlign = ContentAlignment.MiddleLeft
        };

        // ── Close button ──────────────────────────────────────────
        var btnClose = new Button
        {
            Text                    = "Close",
            Location                = new Point(270, 244),
            Size                    = new Size(86, 28),
            BackColor               = ColorScheme.BtnDigit,
            ForeColor               = ColorScheme.BrightText,
            FlatStyle               = FlatStyle.Flat,
            Cursor                  = Cursors.Hand,
            UseVisualStyleBackColor = false
        };
        btnClose.FlatAppearance.BorderColor = ColorScheme.Border;
        btnClose.FlatAppearance.BorderSize  = 1;
        btnClose.Click += (_, _) => Close();

        // ── Add all controls ──────────────────────────────────────
        Controls.AddRange(new Control[]
        {
            pnlHeader, sep1,
            lblCaptionAuthor,  lblValueAuthor,
            lblCaptionYear,    lblValueYear,
            lblCaptionVersion, lblValueVersion,
            lblCaptionRuntime, lblValueRuntime,
            lblCaptionLang,    lblValueLang,
            sep2, lblConstants,
            btnClose
        });

        AcceptButton = btnClose;
        CancelButton = btnClose;
    }
}
