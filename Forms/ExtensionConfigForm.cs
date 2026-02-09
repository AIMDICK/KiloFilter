using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using KiloFilter.Core;

namespace KiloFilter.Forms
{
    public class ExtensionConfigForm : Form
    {
        private TabControl tabControl = null!;
        private Button btnOk = null!;
        private Button btnApply = null!;
        private Button btnCancel = null!;
        private Button btnReset = null!;
        private Dictionary<string, List<string>> groups;
        private HashSet<string> blacklist;
        private Dictionary<string, long> minFileSizes;
        private Dictionary<string, List<string>> defaultGroups;
        private HashSet<string> defaultBlacklist;
        private Dictionary<string, long> defaultMinFileSizes;
        private Dictionary<string, List<string>> originalGroups;
        private HashSet<string> originalBlacklist;
        private Dictionary<string, CheckedListBox> categoryCheckListBoxes;
        private Dictionary<string, TextBox> addTextBoxes;
        private CheckedListBox blacklistCheckListBox = null!;
        private TextBox blacklistTextBox = null!;
        private bool hasUnsavedChanges = false;

        public ExtensionConfigForm(Dictionary<string, List<string>> currentGroups, 
                                   HashSet<string> currentBlacklist,
                                   Dictionary<string, long> currentMinFileSizes,
                                   Dictionary<string, List<string>> defGroups, 
                                   HashSet<string> defBlacklist,
                                   Dictionary<string, long> defMinFileSizes) {
            this.groups = new Dictionary<string, List<string>>();
            foreach (var kvp in currentGroups) {
                this.groups[kvp.Key] = new List<string>(kvp.Value);
            }
            
            this.blacklist = new HashSet<string>(currentBlacklist);
            this.minFileSizes = new Dictionary<string, long>(currentMinFileSizes);
            this.defaultGroups = defGroups;
            this.defaultBlacklist = defBlacklist;
            this.defaultMinFileSizes = defMinFileSizes;
            
            this.originalGroups = new Dictionary<string, List<string>>();
            foreach (var kvp in currentGroups) {
                this.originalGroups[kvp.Key] = new List<string>(kvp.Value);
            }
            this.originalBlacklist = new HashSet<string>(currentBlacklist);
            
            this.categoryCheckListBoxes = new Dictionary<string, CheckedListBox>();
            this.addTextBoxes = new Dictionary<string, TextBox>();

            this.Text = Localization.Get("CONFIG_TITLE");
            this.Size = new Size(700, 550);
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            
            // Icono incrustado
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.MinimizeBox = false;

            this.FormClosing += ExtensionConfigForm_FormClosing;

            InitializeConfigComponents();
        }

        private void ExtensionConfigForm_FormClosing(object? sender, FormClosingEventArgs e) {
            if (hasUnsavedChanges && this.DialogResult != DialogResult.OK) {
                var result = MessageBox.Show(
                    "⚠️ Tienes cambios sin guardar.\n\n¿Deseas guardar los cambios antes de salir?",
                    "Cambios sin guardar",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes) {
                    this.DialogResult = DialogResult.OK;
                } else if (result == DialogResult.Cancel) {
                    e.Cancel = true;
                }
            }
        }

        private void InitializeConfigComponents() {
            Label lblInfo = new Label { 
                Text = Localization.Get("CONFIG_TITLE"),
                Location = new Point(15, 15), 
                Size = new Size(650, 25),
                AutoSize = false
            };

            tabControl = new DarkTabControl()
            {
                Location = new Point(15, 45),
                Size = new Size(655, 400),
                ItemSize = new Size(100, 35),
                Padding = new Point(15, 0)
            };

            TabPage blacklistTab = CreateBlacklistTab();
            blacklistTab.Text = Localization.Get("TAB_BLACKLIST");
            tabControl.TabPages.Add(blacklistTab);

            var orderedCategories = MainForm.CategoryOrder
                .Where(cat => groups.ContainsKey(cat))
                .Select(cat => new KeyValuePair<string, List<string>>(cat, groups[cat]));

            foreach (var category in orderedCategories) {
                TabPage tab = CreateCategoryTab(category.Key, category.Value);
                tabControl.TabPages.Add(tab);
            }

            foreach (var category in groups.Where(g => !MainForm.CategoryOrder.Contains(g.Key))) {
                TabPage tab = CreateCategoryTab(category.Key, category.Value, true);
                tabControl.TabPages.Add(tab);
            }

            foreach (TabPage page in tabControl.TabPages)
            {
                page.BackColor = Color.FromArgb(35, 35, 35);
            }

            btnReset = new Button { 
                Text = Localization.Get("BTN_RESET"), 
                Location = new Point(15, 460), 
                Width = 130, 
                Height = 35,
                BackColor = Color.FromArgb(255, 152, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = MainForm.emojiFontBold
            };
            btnReset.Click += (s, e) => RestoreDefaults();

            btnApply = new Button { 
                Text = Localization.Get("BTN_APPLY"), 
                Location = new Point(290, 460), 
                Width = 130, 
                Height = 35,
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = MainForm.emojiFontBold
            };
            btnApply.Click += (s, e) => ApplyChanges();

            btnOk = new Button { 
                Text = Localization.Get("BTN_OK"), 
                Location = new Point(430, 460), 
                Width = 130, 
                Height = 35,
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = MainForm.emojiFontBold,
                DialogResult = DialogResult.OK
            };

            btnCancel = new Button { 
                Text = Localization.Get("BTN_CANCEL"), 
                Location = new Point(570, 460), 
                Width = 110, 
                Height = 35,
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(this.Font, FontStyle.Bold),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lblInfo, tabControl, btnReset, btnApply, btnOk, btnCancel });
        }

        private void ApplyChanges() {
            hasUnsavedChanges = false;
            
            originalGroups.Clear();
            foreach (var kvp in groups) {
                originalGroups[kvp.Key] = new List<string>(kvp.Value);
            }
            originalBlacklist = new HashSet<string>(blacklist);

            MessageBox.Show("✅ Cambios aplicados correctamente.\n\nLa ventana permanecerá abierta.", 
                "Aplicación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RestoreDefaults() {
            var result = MessageBox.Show(
                "¿Estás seguro de que deseas restablecer TODAS las configuraciones a sus valores por defecto?\n\n" +
                "Esto incluye:\n" +
                "• Todas las extensiones de las categorías\n" +
                "• Todas las extensiones de la blacklist\n" +
                "• Configuraciones de tamaño mínimo por extensión\n" +
                "• Se eliminarán las categorías personalizadas\n\n" +
                "Se perderán todos los cambios personalizados.",
                "Confirmar Restablecimiento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes) {
                groups.Clear();
                foreach (var kvp in defaultGroups) {
                    groups[kvp.Key] = new List<string>(kvp.Value);
                }

                blacklist.Clear();
                foreach (var ext in defaultBlacklist) {
                    blacklist.Add(ext);
                }

                minFileSizes.Clear();
                foreach (var kvp in defaultMinFileSizes) {
                    minFileSizes[kvp.Key] = kvp.Value;
                }

                hasUnsavedChanges = true;

                tabControl.TabPages.Clear();

                TabPage blacklistTab = CreateBlacklistTab();
                tabControl.TabPages.Add(blacklistTab);

                var orderedCategories = MainForm.CategoryOrder
                    .Where(cat => groups.ContainsKey(cat))
                    .Select(cat => new KeyValuePair<string, List<string>>(cat, groups[cat]));

                foreach (var category in orderedCategories) {
                    TabPage tab = CreateCategoryTab(category.Key, category.Value);
                    tabControl.TabPages.Add(tab);
                }

                MessageBox.Show(
                    "✅ Configuración restablecida a valores por defecto:\n\n" +
                    "• Extensiones de categorías restauradas\n" +
                    "• Blacklist restaurada\n" +
                    "• Tamaños mínimos restaurados:\n" +
                    "  - .jpg: 10 KB\n" +
                    "  - .jpeg: 15 KB\n" +
                    "  - .png: 15 KB\n" +
                    "  - .gif: 15 KB\n" +
                    "  - .txt: 5 KB\n\n" +
                    "Recuerda hacer clic en 'Aplicar' o 'Guardar y Cerrar'.", 
                    "Restablecimiento Completo", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information
                );
            }
        }

        private TabPage CreateCategoryTab(string categoryName, List<string> extensions, bool isCustom = false) {
            string translatedName = Localization.GetFolderName(categoryName);
            string tabTitle = translatedName;
            
            if (!isCustom)
            {
                string icon = categoryName == "Imagenes" ? "📷" :
                              categoryName == "Videos" ? "🎬" :
                              categoryName == "Documentos" ? "📄" :
                              categoryName == "Audio" ? "🎵" :
                              categoryName == "Comprimidos" ? "📦" :
                              categoryName == "JuegosYMundos" ? "🎮" :
                              categoryName == "AplicacionesAPK" ? "📱" :
                              categoryName == "BasesDeDatos" ? "🗄️" :
                              categoryName == "CodigoFuente" ? "💻" :
                              categoryName == "Modelos3D" ? "🧊" :
                              categoryName == "Ebooks" ? "📚" :
                              categoryName == "Subtitulos" ? "💬" : "📁";
                
                tabTitle = icon + " " + tabTitle;
            }
            else
            {
                tabTitle = $"📁 {tabTitle}";
            }
            
            TabPage tab = new TabPage(tabTitle) { 
                BackColor = Color.FromArgb(40, 40, 40),
                Padding = new Padding(10)
            };

            Panel panelList = new Panel {
                Location = new Point(10, 10),
                Size = new Size(615, 230),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblCurrent = new Label {
                Text = Localization.Get("CATEGORY_EXTENSIONS_LABEL"),
                Location = new Point(5, 5),
                Size = new Size(400, 20),
                AutoSize = false,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };

            CheckedListBox clb = new CheckedListBox { 
                Location = new Point(5, 30),
                Size = new Size(400, 190),
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                CheckOnClick = true,
                Font = new Font("Consolas", 9)
            };

            foreach (var ext in extensions) {
                string displayText = ext;
                if (minFileSizes.ContainsKey(ext)) {
                    long bytes = minFileSizes[ext];
                    string sizeText = FormatBytes(bytes);
                    displayText = ext + string.Format(Localization.Get("MIN_SIZE_SUFFIX"), sizeText);
                }
                clb.Items.Add(displayText);
            }

            Button btnRemoveSelected = new Button {
                Text = Localization.Get("BTN_REMOVE_SELECTED"),
                Location = new Point(415, 30),
                Size = new Size(190, 50),
                BackColor = Color.FromArgb(150, 50, 50),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnRemoveSelected.Click += (s, e) => RemoveSelectedExtensions(categoryName, clb);

            Button btnSelectAll = new Button {
                Text = Localization.Get("BTN_CHECK_ALL"),
                Location = new Point(415, 90),
                Size = new Size(90, 30),
                BackColor = Color.FromArgb(60, 120, 60),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnSelectAll.Click += (s, e) => {
                for (int i = 0; i < clb.Items.Count; i++) {
                    clb.SetItemChecked(i, true);
                }
            };

            Button btnDeselectAll = new Button {
                Text = Localization.Get("BTN_UNCHECK_ALL"),
                Location = new Point(515, 90),
                Size = new Size(90, 30),
                BackColor = Color.FromArgb(120, 60, 60),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnDeselectAll.Click += (s, e) => {
                for (int i = 0; i < clb.Items.Count; i++) {
                    clb.SetItemChecked(i, false);
                }
            };

            Button btnConfigSize = new Button {
                Text = Localization.Get("BTN_CONFIG_MIN_SIZE"),
                Location = new Point(415, 130),
                Size = new Size(190, 50),
                BackColor = Color.FromArgb(100, 60, 150),
                FlatStyle = FlatStyle.Flat,
                Font = MainForm.emojiFontBold,
                ForeColor = Color.White
            };
            btnConfigSize.Click += (s, e) => ConfigureMinFileSize(categoryName, clb, extensions);

            if (isCustom) {
                Button btnDeleteCategory = new Button {
                    Text = "🗑️ Eliminar\nCategoría",
                    Location = new Point(415, 190),
                    Size = new Size(190, 30),
                    BackColor = Color.FromArgb(150, 30, 30),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font(this.Font, FontStyle.Bold)
                };
                btnDeleteCategory.Click += (s, e) => DeleteCustomCategory(categoryName, tab);
                panelList.Controls.Add(btnDeleteCategory);
            }

            categoryCheckListBoxes[categoryName] = clb;
            panelList.Controls.AddRange(new Control[] { lblCurrent, clb, btnRemoveSelected, btnSelectAll, btnDeselectAll, btnConfigSize });

            Panel panelAdd = new Panel {
                Location = new Point(10, 250),
                Size = new Size(615, 55),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblAdd = new Label {
                Text = Localization.Get("ADD_EXTENSION_LABEL"),
                Location = new Point(5, 8),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };

            TextBox txtAdd = new TextBox {
                Location = new Point(5, 28),
                Size = new Size(400, 25),
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            addTextBoxes[categoryName] = txtAdd;

            Button btnAdd = new Button {
                Text = Localization.Get("BTN_ADD"),
                Location = new Point(415, 23),
                Size = new Size(190, 25),
                BackColor = Color.FromArgb(40, 100, 40),
                FlatStyle = FlatStyle.Flat
            };
            btnAdd.Click += (s, e) => AddExtension(categoryName, txtAdd, clb, extensions);

            txtAdd.KeyPress += (s, e) => {
                if (e.KeyChar == (char)Keys.Enter) {
                    AddExtension(categoryName, txtAdd, clb, extensions);
                    e.Handled = true;
                }
            };

            panelAdd.Controls.AddRange(new Control[] { lblAdd, txtAdd, btnAdd });

            tab.Controls.AddRange(new Control[] { panelList, panelAdd });
            return tab;
        }

        private void ConfigureMinFileSize(string categoryName, CheckedListBox clb, List<string> extensions) {
            var selectedItems = clb.CheckedItems.Cast<string>()
                .Select(item => item.Split(new[] { " - " }, StringSplitOptions.None)[0].Trim())
                .ToList();

            if (selectedItems.Count == 0)
            {
                MessageBox.Show(
                    Localization.Get("ERROR_NO_EXTENSION_SELECTED"),
                    Localization.Get("INFO"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            if (selectedItems.Count > 1)
            {
                MessageBox.Show(
                    Localization.Get("ERROR_SELECT_ONE_EXTENSION"),
                    Localization.Get("INFO"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            string ext = selectedItems[0];
            long currentSizeKB = minFileSizes.ContainsKey(ext) ? minFileSizes[ext] / 1024 : 10;

            Form configForm = new Form
            {
                Text = "🔧📏 " + Localization.Get("CONFIG_MIN_SIZE_TITLE"),
                Size = new Size(500, 300),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                Font = MainForm.emojiFont
            };
            
            try
            {
                string iconPath = Path.Combine(Directory.GetCurrentDirectory(), "KiloFilter.ico");
                if (File.Exists(iconPath))
                {
                    configForm.Icon = new Icon(iconPath);
                }
            }
            catch { }

            Label lblInfo1 = new Label
            {
                Text = Localization.Get("MIN_SIZE_CONFIG_DESCRIPTION_1"),
                Location = new Point(20, 20),
                Size = new Size(450, 25),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            Label lblInfo2 = new Label
            {
                Text = Localization.Get("MIN_SIZE_CONFIG_DESCRIPTION_2"),
                Location = new Point(20, 45),
                Size = new Size(450, 25),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(180, 180, 180)
            };

            Label lblExt = new Label
            {
                Text = ext,
                Location = new Point(20, 85),
                Size = new Size(100, 30),
                Font = new Font("Consolas", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 200, 255),
                TextAlign = ContentAlignment.MiddleLeft
            };

            NumericUpDown numSize = new NumericUpDown
            {
                Location = new Point(130, 85),
                Width = 120,
                Height = 30,
                Minimum = 0,
                Maximum = 1000000,
                Value = currentSizeKB,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblKB = new Label
            {
                Text = "KB",
                Location = new Point(260, 85),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            Label lblPresets = new Label
            {
                Text = Localization.Get("PRESETS") + ":",
                Location = new Point(310, 85),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            ComboBox cboPresets = new ComboBox
            {
                Location = new Point(310, 105),
                Width = 160,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9)
            };

            cboPresets.Items.Add(Localization.Get("NO_LIMIT"));
            cboPresets.Items.Add("5 KB");
            cboPresets.Items.Add("10 KB");
            cboPresets.Items.Add("15 KB");
            cboPresets.Items.Add("50 KB");
            cboPresets.Items.Add("100 KB");
            cboPresets.Items.Add("500 KB");
            cboPresets.Items.Add(string.Format(Localization.Get("MB_UNIT"), "1", "1024"));
            cboPresets.Items.Add(string.Format(Localization.Get("MB_UNIT"), "5", "5120"));

            cboPresets.SelectedIndexChanged += (s, e) =>
            {
                string selected = cboPresets.SelectedItem?.ToString() ?? "";
                
                if (selected == Localization.Get("NO_LIMIT"))
                {
                    numSize.Value = 0;
                }
                else if (selected.Contains("KB") && !selected.Contains("MB"))
                {
                    string numStr = selected.Replace("KB", "").Trim();
                    if (long.TryParse(numStr, out long kb))
                    {
                        numSize.Value = kb;
                    }
                }
                else if (selected.Contains("MB"))
                {
                    int start = selected.IndexOf('(') + 1;
                    int end = selected.IndexOf(" KB", start);
                    if (start > 0 && end > start)
                    {
                        string numStr = selected.Substring(start, end - start).Trim();
                        if (long.TryParse(numStr, out long kb))
                        {
                            numSize.Value = kb;
                        }
                    }
                }
            };

            Button btnAccept = new Button
            {
                Text = Localization.Get("BTN_ACCEPT"),
                Location = new Point(240, 210),
                Width = 110,
                Height = 35,
                BackColor = Color.FromArgb(0, 120, 200),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                DialogResult = DialogResult.OK
            };

            Button btnCancel = new Button
            {
                Text = Localization.Get("BTN_CANCEL"),
                Location = new Point(360, 210),
                Width = 110,
                Height = 35,
                BackColor = Color.FromArgb(100, 40, 40),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                DialogResult = DialogResult.Cancel
            };

            configForm.Controls.Add(lblInfo1);
            configForm.Controls.Add(lblInfo2);
            configForm.Controls.Add(lblExt);
            configForm.Controls.Add(numSize);
            configForm.Controls.Add(lblKB);
            configForm.Controls.Add(lblPresets);
            configForm.Controls.Add(cboPresets);
            configForm.Controls.Add(btnAccept);
            configForm.Controls.Add(btnCancel);

            if (configForm.ShowDialog() == DialogResult.OK)
            {
                long sizeInBytes = (long)numSize.Value * 1024;
                minFileSizes[ext] = sizeInBytes;
                hasUnsavedChanges = true;

                Label lblCount = new Label();
                RefreshCategoryList(categoryName, clb, lblCount);
            }
        }

        private string FormatBytes(long bytes) {
            if (bytes < 1024) return $"{bytes} B";
            else if (bytes < 1024 * 1024) return $"{bytes / 1024} KB";
            else if (bytes < 1024 * 1024 * 1024) return $"{bytes / (1024 * 1024)} MB";
            else return $"{bytes / (1024 * 1024 * 1024)} GB";
        }

        private void DeleteCustomCategory(string categoryName, TabPage tab) {
            var result = MessageBox.Show(
                $"¿Estás seguro de eliminar la categoría '{categoryName}'?\n\n" +
                "Esta acción no se puede deshacer.",
                "Confirmar eliminación de categoría",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes) {
                groups.Remove(categoryName);
                MainForm.CategoryOrder.Remove(categoryName);
                tabControl.TabPages.Remove(tab);
                hasUnsavedChanges = true;
                MessageBox.Show($"Categoría '{categoryName}' eliminada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private TabPage CreateBlacklistTab() {
            TabPage tab = new TabPage(Localization.Get("TAB_BLACKLIST")) { 
                BackColor = Color.FromArgb(40, 40, 40),
                Padding = new Padding(10)
            };

            Panel panelList = new Panel {
                Location = new Point(10, 10),
                Size = new Size(615, 280),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblInfo = new Label {
                Text = Localization.Get("BLOCKED_EXTENSIONS_LABEL"),
                Location = new Point(5, 5),
                Size = new Size(600, 20),
                AutoSize = false,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };

            blacklistCheckListBox = new CheckedListBox { 
                Location = new Point(5, 30),
                Size = new Size(400, 240),
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                CheckOnClick = true
            };

            foreach (var ext in blacklist.OrderBy(e => e)) {
                blacklistCheckListBox.Items.Add(ext);
            }

            Button btnRemoveSelected = new Button {
                Text = Localization.Get("BTN_REMOVE_FROM_BLACKLIST"),
                Location = new Point(415, 30),
                Size = new Size(190, 50),
                BackColor = Color.FromArgb(0, 150, 100),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnRemoveSelected.Click += (s, e) => RemoveFromBlacklist();

            Button btnSelectAll = new Button {
                Text = Localization.Get("BTN_CHECK_ALL"),
                Location = new Point(415, 90),
                Size = new Size(90, 30),
                BackColor = Color.FromArgb(60, 120, 60),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnSelectAll.Click += (s, e) => {
                for (int i = 0; i < blacklistCheckListBox.Items.Count; i++) {
                    blacklistCheckListBox.SetItemChecked(i, true);
                }
            };

            Button btnDeselectAll = new Button {
                Text = Localization.Get("BTN_UNCHECK_ALL"),
                Location = new Point(515, 90),
                Size = new Size(90, 30),
                BackColor = Color.FromArgb(120, 60, 60),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnDeselectAll.Click += (s, e) => {
                for (int i = 0; i < blacklistCheckListBox.Items.Count; i++) {
                    blacklistCheckListBox.SetItemChecked(i, false);
                }
            };

            Label lblCount = new Label {
                Text = string.Format(Localization.Get("TOTAL_BLOCKED"), blacklist.Count),
                Location = new Point(415, 130),
                Size = new Size(190, 20),
                ForeColor = Color.FromArgb(100, 255, 100),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            panelList.Controls.AddRange(new Control[] { lblInfo, blacklistCheckListBox, btnRemoveSelected, btnSelectAll, btnDeselectAll, lblCount });

            Panel panelAdd = new Panel {
                Location = new Point(10, 300),
                Size = new Size(615, 55),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblAdd = new Label {
                Text = Localization.Get("ADD_TO_BLACKLIST_LABEL"),
                Location = new Point(5, 8),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };

            blacklistTextBox = new TextBox {
                Location = new Point(5, 28),
                Size = new Size(400, 25),
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Button btnAdd = new Button {
                Text = Localization.Get("BTN_BLOCK"),
                Location = new Point(415, 23),
                Size = new Size(190, 25),
                BackColor = Color.FromArgb(150, 50, 50),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnAdd.Click += (s, e) => AddToBlacklist(lblCount);

            blacklistTextBox.KeyPress += (s, e) => {
                if (e.KeyChar == (char)Keys.Enter) {
                    AddToBlacklist(lblCount);
                    e.Handled = true;
                }
            };

            panelAdd.Controls.AddRange(new Control[] { lblAdd, blacklistTextBox, btnAdd });

            tab.Controls.AddRange(new Control[] { panelList, panelAdd });
            return tab;
        }

        private void AddExtension(string category, TextBox txtBox, CheckedListBox checkListBox, List<string> extensions) {
            string ext = txtBox.Text.Trim().ToLower();
            
            if (string.IsNullOrEmpty(ext)) {
                MessageBox.Show("Por favor ingresa una extensión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ext.StartsWith(".")) {
                ext = "." + ext;
            }

            if (ext.Contains(" ") || ext.Length < 2) {
                MessageBox.Show("La extensión no es válida. Use formato: .ext o ext", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (groups[category].Contains(ext)) {
                MessageBox.Show("Esta extensión ya existe en la categoría.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            groups[category].Add(ext);
            extensions.Add(ext);
            
            string displayText = ext;
            if (minFileSizes.ContainsKey(ext)) {
                string sizeText = FormatBytes(minFileSizes[ext]);
                displayText = $"{ext} - {sizeText} tamaño mínimo permitido en análisis";
            }
            checkListBox.Items.Add(displayText);
            
            txtBox.Clear();
            txtBox.Focus();
            hasUnsavedChanges = true;
        }

        private void RemoveSelectedExtensions(string category, CheckedListBox checkListBox) {
            if (checkListBox.CheckedItems.Count == 0) {
                MessageBox.Show("Por favor marca las extensiones que deseas eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"¿Estás seguro de eliminar {checkListBox.CheckedItems.Count} extensión(es) de esta categoría?", 
                "Confirmar eliminación múltiple", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes) {
                List<string> toRemove = new List<string>();
                foreach (var item in checkListBox.CheckedItems) {
                    string itemText = item.ToString()!;
                    string ext = itemText.Split(new[] { " - " }, StringSplitOptions.None)[0].Trim();
                    toRemove.Add(ext);
                }

                foreach (var ext in toRemove) {
                    groups[category].Remove(ext);
                    var itemToRemove = checkListBox.Items.Cast<string>().FirstOrDefault(i => i.StartsWith(ext + " ") || i == ext);
                    if (itemToRemove != null) {
                        checkListBox.Items.Remove(itemToRemove);
                    }
                }
                
                hasUnsavedChanges = true;
            }
        }

        private void AddToBlacklist(Label lblCount) {
            string ext = blacklistTextBox.Text.Trim().ToLower();
            
            if (string.IsNullOrEmpty(ext)) {
                MessageBox.Show(
                    Localization.Get("ERROR_ENTER_EXTENSION"),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!ext.StartsWith(".")) {
                ext = "." + ext;
            }

            if (ext.Contains(" ") || ext.Length < 2) {
                MessageBox.Show(
                    Localization.Get("ERROR_INVALID_EXTENSION"),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (blacklist.Contains(ext)) {
                MessageBox.Show(
                    Localization.Get("ERROR_EXTENSION_ALREADY_BLOCKED"),
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            blacklist.Add(ext);
            
            blacklistCheckListBox.Items.Clear();
            foreach (var item in blacklist.OrderBy(e => e)) {
                blacklistCheckListBox.Items.Add(item);
            }
            
            lblCount.Text = string.Format(Localization.Get("TOTAL_BLOCKED"), blacklist.Count);
            blacklistTextBox.Clear();
            blacklistTextBox.Focus();
            hasUnsavedChanges = true;
        }

        private void RemoveFromBlacklist() {
            if (blacklistCheckListBox.CheckedItems.Count == 0) {
                MessageBox.Show(
                    Localization.Get("ERROR_SELECT_EXTENSIONS"),
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            var result = MessageBox.Show(
                string.Format(Localization.Get("CONFIRM_REMOVE_BLACKLIST"), blacklistCheckListBox.CheckedItems.Count),
                "Confirmar eliminación de blacklist",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            
            if (result == DialogResult.Yes) {
                List<string> toRemove = new List<string>();
                foreach (var item in blacklistCheckListBox.CheckedItems) {
                    if (item != null) toRemove.Add(item.ToString()!);
                }

                foreach (var ext in toRemove) {
                    blacklist.Remove(ext);
                    blacklistCheckListBox.Items.Remove(ext);
                }

                var lblCount = blacklistCheckListBox.Parent?.Controls.OfType<Label>().FirstOrDefault(l => l.Text.Contains(Localization.Get("TOTAL_BLOCKED").Split(':')[0]));
                if (lblCount != null) {
                    lblCount.Text = string.Format(Localization.Get("TOTAL_BLOCKED"), blacklist.Count);
                }
                
                hasUnsavedChanges = true;
            }
        }

        private void RefreshCategoryList(string categoryKey, CheckedListBox clb, Label? lblCount)
        {
            clb.Items.Clear();

            if (groups.ContainsKey(categoryKey))
            {
                foreach (string ext in groups[categoryKey].OrderBy(x => x))
                {
                    string displayText = ext;
                    if (minFileSizes.ContainsKey(ext))
                    {
                        long bytes = minFileSizes[ext];
                        long kb = bytes / 1024;
                        string sizeText = kb >= 1024
                            ? string.Format(Localization.Get("MB_UNIT"), kb / 1024, kb)
                            : string.Format(Localization.Get("KB_UNIT"), kb);
                        displayText = ext + string.Format(Localization.Get("MIN_SIZE_SUFFIX"), sizeText);
                    }
                    clb.Items.Add(displayText);
                }
            }

            if (lblCount != null && groups.ContainsKey(categoryKey))
            {
                lblCount.Text = string.Format(
                    Localization.Get("TOTAL_EXTENSIONS_COUNT"),
                    groups[categoryKey].Count
                );
            }
        }

        public Dictionary<string, List<string>> GetUpdatedGroups() {
            return groups;
        }

        public HashSet<string> GetUpdatedBlacklist() {
            return blacklist;
        }

        public Dictionary<string, long> GetUpdatedMinFileSizes() {
            return minFileSizes;
        }
    }
}
