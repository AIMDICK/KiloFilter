using System;
using System.Drawing;
using System.Windows.Forms;
using KiloFilter.Core;

namespace KiloFilter.Forms
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            LoadIcon();
            RefreshLanguage();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form
            this.Text = Localization.Get("HELP_TITLE");
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ForeColor = Color.FromArgb(230, 230, 230);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // RichTextBox for help content
            RichTextBox rtbHelp = new RichTextBox
            {
                Location = new Point(10, 10),
                Size = new Size(770, 540),
                ReadOnly = true,
                BackColor = Color.FromArgb(37, 37, 38),
                ForeColor = Color.FromArgb(230, 230, 230),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.Fixed3D,
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                Text = Localization.Get("HELP_CONTENT")
            };

            this.Controls.Add(rtbHelp);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadIcon()
        {
            // Icono incrustado
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        public void RefreshLanguage()
        {
            this.Text = Localization.Get("HELP_TITLE");
            if (this.Controls.Count > 0 && this.Controls[0] is RichTextBox rtb)
            {
                rtb.Text = Localization.Get("HELP_CONTENT");
            }
        }
    }
}
