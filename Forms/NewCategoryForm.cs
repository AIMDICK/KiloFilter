using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using KiloFilter.Core;

namespace KiloFilter.Forms
{
    public class NewCategoryForm : Form
    {
        private TextBox txtCategoryName = null!;
        private TextBox txtExtensions = null!;
        private ListBox lstExtensions = null!;
        private List<string> extensions = new List<string>();
        private Dictionary<string, List<string>> existingGroups;
        private bool shouldAnalyze = false;

        public NewCategoryForm(Dictionary<string, List<string>> groups) {
            this.existingGroups = groups;
            this.Text = "Crear Nueva Categoría";
            this.Size = new Size(550, 520);
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            // Icono incrustado
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            InitializeNewCategoryComponents();
        }

        private void InitializeNewCategoryComponents() {
            Label lblTitle = new Label {
                Text = Localization.Get("NEW_CATEGORY_TITLE"),
                Location = new Point(20, 20),
                Size = new Size(500, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 200, 255),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblName = new Label {
                Text = Localization.Get("CATEGORY_NAME"),
                Location = new Point(20, 65),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            txtCategoryName = new TextBox {
                Location = new Point(20, 90),
                Width = 490,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10)
            };

            Label lblInfo = new Label {
                Text = Localization.Get("EXAMPLE"),
                Location = new Point(20, 117),
                Size = new Size(490, 20),
                ForeColor = Color.FromArgb(180, 180, 180),
                Font = new Font("Segoe UI", 8, FontStyle.Italic)
            };

            Label lblExtensions = new Label {
                Text = Localization.Get("INCLUDED_EXTENSIONS"),
                Location = new Point(20, 155),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            lstExtensions = new ListBox {
                Location = new Point(20, 180),
                Size = new Size(340, 180),
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Consolas", 9)
            };

            Button btnRemove = new Button {
                Text = Localization.Get("BTN_REMOVE"),
                Location = new Point(370, 180),
                Width = 140,
                Height = 35,
                BackColor = Color.FromArgb(120, 40, 40),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnRemove.Click += (s, e) => RemoveExtension();

            Button btnClear = new Button {
                Text = Localization.Get("BTN_CLEAR_ALL"),
                Location = new Point(370, 225),
                Width = 140,
                Height = 35,
                BackColor = Color.FromArgb(80, 40, 40),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnClear.Click += (s, e) => {
                lstExtensions.Items.Clear();
                extensions.Clear();
            };

            Label lblCount = new Label {
                Text = string.Format(Localization.Get("TOTAL_EXTENSIONS"), 0),
                Location = new Point(370, 270),
                Size = new Size(140, 25),
                ForeColor = Color.FromArgb(100, 255, 100),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lstExtensions.SelectedIndexChanged += (s, e) => {
                lblCount.Text = string.Format(Localization.Get("TOTAL_EXTENSIONS"), extensions.Count);
            };

            Label lblAdd = new Label {
                Text = Localization.Get("ADD_EXTENSION"),
                Location = new Point(20, 375),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            txtExtensions = new TextBox {
                Location = new Point(20, 400),
                Width = 340,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10)
            };

            Button btnAdd = new Button {
                Text = Localization.Get("BTN_ADD"),
                Location = new Point(370, 398),
                Width = 140,
                Height = 27,
                BackColor = Color.FromArgb(40, 100, 40),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnAdd.Click += (s, e) => AddExtension(lblCount);

            txtExtensions.KeyPress += (s, e) => {
                if (e.KeyChar == (char)Keys.Enter) {
                    AddExtension(lblCount);
                    e.Handled = true;
                }
            };

            Button btnSaveAndAnalyze = new Button {
                Text = Localization.Get("BTN_SAVE_AND_ANALYZE"),
                Location = new Point(20, 445),
                Width = 160,
                Height = 35,
                BackColor = Color.FromArgb(0, 120, 200),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnSaveAndAnalyze.Click += (s, e) => SaveAndAnalyze();

            Button btnSaveOnly = new Button {
                Text = Localization.Get("BTN_SAVE_ONLY"),
                Location = new Point(195, 445),
                Width = 150,
                Height = 35,
                BackColor = Color.FromArgb(0, 120, 60),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnSaveOnly.Click += (s, e) => SaveOnly();

            Button btnCancel = new Button {
                Text = Localization.Get("BTN_CANCEL"),
                Location = new Point(360, 445),
                Width = 150,
                Height = 35,
                BackColor = Color.FromArgb(100, 40, 40),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] { 
                lblTitle, lblName, txtCategoryName, lblInfo, lblExtensions, lstExtensions, 
                btnRemove, btnClear, lblCount, lblAdd, txtExtensions, btnAdd, 
                btnSaveAndAnalyze, btnSaveOnly, btnCancel 
            });
        }

        private void AddExtension(Label lblCount) {
            string ext = txtExtensions.Text.Trim().ToLower();
            
            if (string.IsNullOrEmpty(ext)) {
                MessageBox.Show(Localization.Get("ERROR_ENTER_EXTENSION"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ext.StartsWith(".")) {
                ext = "." + ext;
            }

            if (ext.Contains(" ") || ext.Length < 2) {
                MessageBox.Show(Localization.Get("ERROR_INVALID_EXTENSION"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (extensions.Contains(ext)) {
                MessageBox.Show("Esta extensión ya fue agregada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            extensions.Add(ext);
            lstExtensions.Items.Add(ext);
            lblCount.Text = $"Total: {extensions.Count} extensión{(extensions.Count != 1 ? "es" : "")}";
            txtExtensions.Clear();
            txtExtensions.Focus();
        }

        private void RemoveExtension() {
            if (lstExtensions.SelectedItem != null) {
                string ext = lstExtensions.SelectedItem.ToString()!;
                extensions.Remove(ext);
                lstExtensions.Items.Remove(ext);
            } else {
                MessageBox.Show("Por favor selecciona una extensión de la lista para eliminar.", 
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool ValidateCategory() {
            string categoryName = txtCategoryName.Text.Trim();
            
            if (string.IsNullOrEmpty(categoryName)) {
                MessageBox.Show("Por favor ingresa un nombre para la categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return false;
            }

            if (categoryName.Contains(" ")) {
                MessageBox.Show("El nombre no puede contener espacios. Usa guión bajo (_) en su lugar.\n\nEjemplo: Mi_Categoria", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return false;
            }

            if (existingGroups.ContainsKey(categoryName)) {
                MessageBox.Show("Ya existe una categoría con ese nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return false;
            }

            if (extensions.Count == 0) {
                MessageBox.Show("Debes agregar al menos una extensión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtExtensions.Focus();
                return false;
            }

            return true;
        }

        private void SaveAndAnalyze() {
            if (!ValidateCategory()) return;
            
            shouldAnalyze = true;
            this.DialogResult = DialogResult.OK;
        }

        private void SaveOnly() {
            if (!ValidateCategory()) return;
            
            shouldAnalyze = false;
            this.DialogResult = DialogResult.OK;
        }

        public string GetCategoryName() {
            return txtCategoryName.Text.Trim();
        }

        public List<string> GetExtensions() {
            return new List<string>(extensions);
        }

        public bool ShouldAnalyze() {
            return shouldAnalyze;
        }
    }
}
