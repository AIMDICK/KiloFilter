using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using KiloFilter.Core;

namespace KiloFilter.Forms
{
    public class MinFileSizeConfigForm : Form
    {
        private Dictionary<string, long> minFileSizes;
        private List<string> extensions;
        private Dictionary<string, NumericUpDown> sizeControls;

        public MinFileSizeConfigForm(List<string> exts, Dictionary<string, long> currentMinFileSizes) {
                        Label lblTitle = new Label {
                            Text = "⚖️ CONFIGURAR TAMAÑO MÍNIMO DE ARCHIVO",
                            Location = new Point(20, 20),
                            Size = new Size(450, 30),
                            Font = new Font("Segoe UI", 12, FontStyle.Bold),
                            ForeColor = Color.FromArgb(100, 200, 255),
                            TextAlign = ContentAlignment.MiddleCenter
                        };
            this.extensions = exts;
            this.minFileSizes = new Dictionary<string, long>(currentMinFileSizes);
            this.sizeControls = new Dictionary<string, NumericUpDown>();

            this.Text = Localization.Get("CONFIG_MIN_SIZE_TITLE");
            this.Size = new Size(500, 400 + (exts.Count * 45));
            if (this.Height > 700) this.Height = 700;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            // Icono incrustado
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            Label lblInfo = new Label {
                Text = "Configura el tamaño mínimo (en KB) para cada extensión.\nArchivos más pequeños serán ignorados durante el análisis.",
                Location = new Point(20, 60),
                Size = new Size(450, 40),
                ForeColor = Color.FromArgb(200, 200, 200),
                Font = new Font("Segoe UI", 9)
            };

            Panel panelScroll = new Panel {
                Location = new Point(20, 110),
                Size = new Size(450, this.Height - 220),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true,
                BackColor = Color.FromArgb(40, 40, 40)
            };

            int yPos = 10;
            foreach (var ext in extensions) {
                Label lblExt = new Label {
                    Text = ext,
                    Location = new Point(10, yPos + 5),
                    Size = new Size(100, 25),
                    Font = new Font("Consolas", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(100, 255, 100)
                };

                NumericUpDown numSize = new NumericUpDown {
                    Location = new Point(120, yPos),
                    Width = 120,
                    Minimum = 0,
                    Maximum = 1024 * 1024,
                    Value = minFileSizes.ContainsKey(ext) ? minFileSizes[ext] / 1024 : 0,
                    BackColor = Color.FromArgb(50, 50, 50),
                    ForeColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Segoe UI", 10)
                };

                Label lblKB = new Label {
                    Text = Localization.Get("SIZE_IN_KB").TrimEnd(':'),
                    Location = new Point(250, yPos + 5),
                    Size = new Size(30, 25),
                    ForeColor = Color.FromArgb(180, 180, 180)
                };

                Button btnPreset = new Button {
                    Text = "Presets ▼",
                    Location = new Point(290, yPos),
                    Width = 140,
                    Height = 25,
                    BackColor = Color.FromArgb(60, 60, 100),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 8)
                };

                ContextMenuStrip presetMenu = new ContextMenuStrip {
                    BackColor = Color.FromArgb(45, 45, 45),
                    ForeColor = Color.White
                };
                presetMenu.Items.Add("Sin límite (0 KB)", null, (s, e) => numSize.Value = 0);
                presetMenu.Items.Add("5 KB", null, (s, e) => numSize.Value = 5);
                presetMenu.Items.Add("10 KB", null, (s, e) => numSize.Value = 10);
                presetMenu.Items.Add("15 KB", null, (s, e) => numSize.Value = 15);
                presetMenu.Items.Add("50 KB", null, (s, e) => numSize.Value = 50);
                presetMenu.Items.Add("100 KB", null, (s, e) => numSize.Value = 100);
                presetMenu.Items.Add("500 KB", null, (s, e) => numSize.Value = 500);
                presetMenu.Items.Add("1 MB (1024 KB)", null, (s, e) => numSize.Value = 1024);
                presetMenu.Items.Add("5 MB (5120 KB)", null, (s, e) => numSize.Value = 5120);

                btnPreset.Click += (s, e) => presetMenu.Show(btnPreset, new Point(0, btnPreset.Height));

                sizeControls[ext] = numSize;

                panelScroll.Controls.Add(lblExt);
                panelScroll.Controls.Add(numSize);
                panelScroll.Controls.Add(lblKB);
                panelScroll.Controls.Add(btnPreset);

                yPos += 40;
            }

            Button btnApplyAll = new Button {
                Text = "Aplicar a Todas",
                Location = new Point(20, this.Height - 95),
                Width = 140,
                Height = 30,
                BackColor = Color.FromArgb(100, 100, 150),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnApplyAll.Click += (s, e) => ApplyToAll();

            Button btnSave = new Button {
                Text = Localization.Get("BTN_ACCEPT"),
                Location = new Point(280, this.Height - 95),
                Width = 110,
                Height = 30,
                BackColor = Color.FromArgb(0, 120, 200),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnSave.Click += (s, e) => SaveAndClose();

            Button btnCancel = new Button {
                Text = Localization.Get("BTN_CANCEL"),
                Location = new Point(400, this.Height - 95),
                Width = 70,
                Height = 30,
                BackColor = Color.FromArgb(100, 40, 40),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] { lblTitle, lblInfo, panelScroll, btnApplyAll, btnSave, btnCancel });
        }

        private void ApplyToAll() {
            using (var inputForm = new Form {
                Text = "Aplicar a Todas",
                Size = new Size(350, 180),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            }) {
                try
                {
                    string iconPath = Path.Combine(Directory.GetCurrentDirectory(), "KiloFilter.ico");
                    if (File.Exists(iconPath))
                    {
                        inputForm.Icon = new Icon(iconPath);
                    }
                }
                catch { }
                Label lbl = new Label {
                    Text = "Ingresa el tamaño mínimo (KB) para aplicar a todas:",
                    Location = new Point(20, 20),
                    Size = new Size(300, 40),
                    Font = new Font("Segoe UI", 9)
                };

                NumericUpDown num = new NumericUpDown {
                    Location = new Point(20, 70),
                    Width = 200,
                    Minimum = 0,
                    Maximum = 1024 * 1024,
                    Value = 15,
                    BackColor = Color.FromArgb(50, 50, 50),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10)
                };

                Button btnOk = new Button {
                    Text = "Aplicar",
                    Location = new Point(230, 68),
                    Width = 80,
                    Height = 30,
                    BackColor = Color.FromArgb(0, 120, 60),
                    FlatStyle = FlatStyle.Flat,
                    DialogResult = DialogResult.OK
                };

                inputForm.Controls.AddRange(new Control[] { lbl, num, btnOk });

                if (inputForm.ShowDialog() == DialogResult.OK) {
                    decimal value = num.Value;
                    foreach (var ctrl in sizeControls.Values) {
                        ctrl.Value = value;
                    }
                    MessageBox.Show($"✅ Aplicado {value} KB a todas las extensiones.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void SaveAndClose() {
            foreach (var kvp in sizeControls) {
                string ext = kvp.Key;
                long bytes = (long)(kvp.Value.Value * 1024);
                
                if (bytes > 0) {
                    minFileSizes[ext] = bytes;
                } else {
                    minFileSizes.Remove(ext);
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        public Dictionary<string, long> GetUpdatedMinFileSizes() {
            return minFileSizes;
        }
    }
}
