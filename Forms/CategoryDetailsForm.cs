using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using KiloFilter.Core;
using KiloFilter.Models;

namespace KiloFilter.Forms
{
    public class CategoryDetailsForm : Form
    {
        private TabControl tabControl = null!;
        private ListView lvSummary = null!;
        private ListView lvFileExplorer = null!;
        private Label lblSummary = null!;
        private TextBox txtFilter = null!;
        private TextBox txtFileFilter = null!;
        private ComboBox cboExtensionFilter = null!;
        private ComboBox cboSortBy = null!;
        private List<ExtensionInfo> allExtensions = new List<ExtensionInfo>();
        private List<FileInfo> allFiles = new List<FileInfo>();
        private List<FileInfo> currentFilteredFiles = new List<FileInfo>();
        private Dictionary<string, List<string>> groups;
        private HashSet<string> blacklist;
        private Dictionary<string, List<string>> originalGroups;
        private HashSet<string> originalBlacklist;
        private bool hasUnsavedChanges = false;
        private string categoryName;
        private HashSet<string> redundantFilePaths = new HashSet<string>();

        public CategoryDetailsForm(List<FileInfo> files, Dictionary<string, List<string>> currentGroups, HashSet<string> currentBlacklist, string catName, List<DuplicatesReportForm.DuplicateGroupInfo> duplicateGroups) {
            this.categoryName = catName;
            this.Text = $"Análisis Detallado - {categoryName}";
            this.Size = new Size(1000, 650);
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(800, 500);
            
            // Icono incrustado
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            // Build set of redundant file paths (all except first in each duplicate group)
            foreach (var group in duplicateGroups) {
                foreach (var file in group.Files.Skip(1)) {
                    redundantFilePaths.Add(file.FullName.ToLower());
                }
            }

            this.groups = new Dictionary<string, List<string>>();
            foreach (var kvp in currentGroups) {
                this.groups[kvp.Key] = new List<string>(kvp.Value);
            }
            this.blacklist = new HashSet<string>(currentBlacklist);

            this.originalGroups = new Dictionary<string, List<string>>();
            foreach (var kvp in currentGroups) {
                this.originalGroups[kvp.Key] = new List<string>(kvp.Value);
            }
            this.originalBlacklist = new HashSet<string>(currentBlacklist);

            // Filter files to remove redundant copies
            this.allFiles = files.Where(f => !redundantFilePaths.Contains(f.FullName.ToLower())).ToList();
            this.currentFilteredFiles = this.allFiles;
            
            this.FormClosing += CategoryDetailsForm_FormClosing;
            
            InitializeDetailsComponents(this.allFiles);
        }

        private void CategoryDetailsForm_FormClosing(object? sender, FormClosingEventArgs e) {
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

        private void InitializeDetailsComponents(List<FileInfo> files) {
            var extensionGroups = files.GroupBy(f => f.Extension.ToLower())
                                      .Select(g => new ExtensionInfo {
                                          Extension = g.Key,
                                          Count = g.Count(),
                                          TotalSize = g.Sum(f => f.Length)
                                      })
                                      .OrderByDescending(g => g.TotalSize)
                                      .ToList();

            allExtensions = extensionGroups;

            this.Text = string.Format(Localization.Get("DETAILED_ANALYSIS"), Localization.GetFolderName(categoryName));

            lblSummary = new Label {
                Text = string.Format(Localization.Get("CATEGORY_INFO"), 
                    Localization.GetFolderName(categoryName), 
                    extensionGroups.Count, 
                    files.Count, 
                    ToSize(files.Sum(f => f.Length))),
                Location = new Point(15, 15),
                Size = new Size(960, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 200, 255),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            tabControl = new DarkTabControl()
            {
                Location = new Point(15, 50),
                Size = new Size(960, 390),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ItemSize = new Size(180, 40),
                Padding = new Point(20, 0)
            };

            TabPage tabExplorer = CreateFileExplorerTab(files, extensionGroups);
            tabExplorer.Text = Localization.Get("TAB_FILE_EXPLORER");
            tabControl.TabPages.Add(tabExplorer);

            TabPage tabSummary = CreateSummaryTab(extensionGroups, files);
            tabSummary.Text = Localization.Get("TAB_SUMMARY");
            tabControl.TabPages.Add(tabSummary);

            Button btnApply = new Button {
                Text = Localization.Get("BTN_APPLY_CHANGES"),
                Location = new Point(530, 460),
                Width = 130,
                Height = 30,
                BackColor = Color.FromArgb(0, 100, 200),
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnApply.Click += (s, e) => ApplyChanges();

            Button btnSave = new Button {
                Text = Localization.Get("BTN_SAVE_AND_CLOSE"),
                Location = new Point(670, 460),
                Width = 130,
                Height = 30,
                BackColor = Color.FromArgb(0, 120, 60),
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                DialogResult = DialogResult.OK,
                Font = MainForm.emojiFontBold
            };

            Button btnClose = new Button {
                Text = Localization.Get("BTN_CANCEL"),
                Location = new Point(810, 460),
                Width = 90,
                Height = 30,
                BackColor = Color.FromArgb(80, 80, 120),
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lblSummary, tabControl, btnApply, btnSave, btnClose });
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

        private TabPage CreateSummaryTab(List<ExtensionInfo> extensionGroups, List<FileInfo> files) {
            TabPage tab = new TabPage {
                BackColor = Color.FromArgb(45, 45, 45)
            };

            Label lblFilter = new Label {
                Text = Localization.Get("FILTER"),
                Location = new Point(15, 15),
                AutoSize = true
            };

            txtFilter = new TextBox {
                Location = new Point(70, 13),
                Width = 200,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtFilter.TextChanged += (s, e) => FilterExtensions();

            Button btnClearFilter = new Button {
                Text = Localization.Get("BTN_CLEAR"),
                Location = new Point(280, 11),
                Width = 80,
                BackColor = Color.FromArgb(60, 60, 60),
                FlatStyle = FlatStyle.Flat
            };
            btnClearFilter.Click += (s, e) => txtFilter.Clear();

            Button btnExport = new Button {
                Text = Localization.Get("BTN_COPY_SUMMARY"),
                Location = new Point(820, 10),
                Width = 120,
                Height = 25,
                BackColor = Color.FromArgb(60, 100, 60),
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnExport.Click += (s, e) => ExportSummaryToClipboard(extensionGroups, files.Sum(f => f.Length));

            lvSummary = new ListView {
                Location = new Point(15, 50),
                Size = new Size(925, 170),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                CheckBoxes = true,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lvSummary.Columns.Add(Localization.Get("COL_SELECTION"), 40);
            lvSummary.Columns.Add(Localization.Get("COL_EXTENSION"), 130);
            lvSummary.Columns.Add(Localization.Get("COL_QUANTITY"), 110);
            lvSummary.Columns.Add(Localization.Get("COL_TOTAL_SIZE"), 140);
            lvSummary.Columns.Add(Localization.Get("COL_AVERAGE_SIZE"), 140);
            lvSummary.Columns.Add(Localization.Get("COL_PERCENTAGE"), 125);
            lvSummary.Columns.Add(Localization.Get("COL_LARGEST"), 240);

            Label lblActions = new Label {
                Text = Localization.Get("MOVE_TO"),
                Location = new Point(25, 235),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 200, 100)
            };

            ComboBox cboDestination = new ComboBox {
                Location = new Point(25, 260),
                Width = 280,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10)
            };

            cboDestination.Items.Add("⛔ " + Localization.Get("BLACKLIST_IGNORE"));
            foreach (var category in groups.Keys.OrderBy(k => {
                int index = MainForm.CategoryOrder.IndexOf(k);
                return index == -1 ? 999 : index;
            })) {
                if (category != "LoDemas") {
                    string icon = category == "Imagenes" ? "🖼️" :
                                  category == "Videos" ? "🎬" :
                                  category == "Documentos" ? "📄" :
                                  category == "Audio" ? "🎵" :
                                  category == "Comprimidos" ? "📦" :
                                  category == "JuegosYMundos" ? "🎮" :
                                  category == "AplicacionesAPK" ? "📱" :
                                  category == "BasesDeDatos" ? "🗄️" :
                                  category == "CodigoFuente" ? "💻" :
                                  category == "Modelos3D" ? "🧊" :
                                  category == "Ebooks" ? "📚" :
                                  category == "Subtitulos" ? "💬" : "📁";
                    string translatedName = Localization.GetFolderName(category);
                    cboDestination.Items.Add($"{icon} {translatedName}");
                }
            }
            cboDestination.SelectedIndex = 0;

            Button btnMove = new Button {
                Text = Localization.Get("BTN_MOVE"),
                Location = new Point(315, 260),
                Width = 180,
                Height = 30,
                BackColor = Color.FromArgb(0, 150, 255),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnMove.Click += (s, e) => {
                if (lvSummary.CheckedItems.Count == 0) {
                    MessageBox.Show("Por favor selecciona al menos una extensión marcando las casillas.", 
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string selected = cboDestination.SelectedItem?.ToString() ?? "";
                
                if (selected.StartsWith("⛔")) {
                    MoveSelectedToBlacklist();
                } else {
                    string translatedName = selected.Substring(selected.IndexOf(' ') + 1);
                    string? categoryKey = groups.Keys.FirstOrDefault(k => Localization.GetFolderName(k) == translatedName);
                    if (!string.IsNullOrEmpty(categoryKey)) {
                        MoveSelectedToCategory(categoryKey, categoryKey);
                    }
                }
            };

            Button btnSelectAll = new Button {
                Text = Localization.Get("BTN_SELECT_ALL"),
                Location = new Point(505, 260),
                Width = 130,
                Height = 30,
                BackColor = Color.FromArgb(60, 120, 60),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White
            };
            btnSelectAll.Click += (s, e) => {
                foreach (ListViewItem item in lvSummary.Items) {
                    item.Checked = true;
                }
            };

            Button btnDeselectAll = new Button {
                Text = Localization.Get("BTN_DESELECT_ALL"),
                Location = new Point(645, 260),
                Width = 130,
                Height = 30,
                BackColor = Color.FromArgb(120, 60, 60),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = MainForm.emojiFontBold
            };
            btnDeselectAll.Click += (s, e) => {
                foreach (ListViewItem item in lvSummary.Items) {
                    item.Checked = false;
                }
            };

            Label lblCount = new Label {
                Text = string.Format(Localization.Get("SELECTED_COUNT"), 0),
                Location = new Point(785, 267),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 255, 100)
            };

            Label lblInfo = new Label {
                Text = Localization.Get("TIP_MOVE_EXTENSIONS"),
                Location = new Point(25, 300),
                AutoSize = true,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.FromArgb(180, 180, 180)
            };
            lblInfo.Font = MainForm.emojiFont;

            lvSummary.ItemChecked += (s, e) => {
                int count = lvSummary.CheckedItems.Count;
                lblCount.Text = string.Format(Localization.Get("SELECTED_COUNT"), count);
            };

            long totalSize = files.Sum(f => f.Length);

            foreach (var group in extensionGroups) {
                AddSummaryListViewItem(group, totalSize, files);
            }

            tab.Controls.Add(lblFilter);
            tab.Controls.Add(txtFilter);
            tab.Controls.Add(btnClearFilter);
            tab.Controls.Add(btnExport);
            tab.Controls.Add(lvSummary);
            tab.Controls.Add(lblActions);
            tab.Controls.Add(cboDestination);
            tab.Controls.Add(btnMove);
            tab.Controls.Add(btnSelectAll);
            tab.Controls.Add(btnDeselectAll);
            tab.Controls.Add(lblCount);
            tab.Controls.Add(lblInfo);

            return tab;
        }

        private void MoveSelectedToBlacklist() {
            int count = lvSummary.CheckedItems.Count;
            
            var result = MessageBox.Show(
                $"¿Estás seguro de mover {count} extensión(es) a la BLACKLIST?\n\n" +
                "Estas extensiones serán ignoradas en futuros análisis.",
                "Confirmar movimiento a Blacklist",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes) {
                int moved = 0;
                List<ListViewItem> itemsToUncheck = new List<ListViewItem>();
                
                foreach (ListViewItem item in lvSummary.CheckedItems) {
                    string ext = item.SubItems[1].Text;
                    if (ext != "(sin extensión)" && !blacklist.Contains(ext)) {
                        blacklist.Add(ext);
                        moved++;
                        itemsToUncheck.Add(item);
                        hasUnsavedChanges = true;
                    }
                }
                
                foreach (var item in itemsToUncheck) {
                    item.Checked = false;
                }
                
                MessageBox.Show($"✅ {moved} extensión(es) movida(s) a la Blacklist.\n\n" +
                               "Haz clic en 'Aplicar Cambios' o 'Guardar y Cerrar' para confirmar.",
                               "Movimiento Exitoso", 
                               MessageBoxButtons.OK, 
                               MessageBoxIcon.Information);
            }
        }

        private void MoveSelectedToCategory(string categoryKey, string categoryName) {
            int count = lvSummary.CheckedItems.Count;
            
            var result = MessageBox.Show(
                $"¿Mover {count} extensión(es) a la categoría '{categoryName}'?\n\n" +
                "Estas extensiones aparecerán automáticamente en esta categoría en futuros análisis.",
                "Confirmar movimiento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes) {
                int moved = 0;
                List<ListViewItem> itemsToUncheck = new List<ListViewItem>();
                
                foreach (ListViewItem item in lvSummary.CheckedItems) {
                    string ext = item.SubItems[1].Text;
                    if (ext != "(sin extensión)" && !groups[categoryKey].Contains(ext)) {
                        groups[categoryKey].Add(ext);
                        moved++;
                        itemsToUncheck.Add(item);
                        hasUnsavedChanges = true;
                    }
                }
                
                foreach (var item in itemsToUncheck) {
                    item.Checked = false;
                }
                
                MessageBox.Show($"✅ {moved} extensión(es) movida(s) a '{categoryName}'.\n\n" +
                               "Haz clic en 'Aplicar Cambios' o 'Guardar y Cerrar' para confirmar.",
                               "Movimiento Exitoso", 
                               MessageBoxButtons.OK, 
                               MessageBoxIcon.Information);
            }
        }

        private TabPage CreateFileExplorerTab(List<FileInfo> files, List<ExtensionInfo> extensionGroups)
        {
            TabPage tab = new TabPage(Localization.Get("TAB_FILE_EXPLORER"))
            {
                BackColor = Color.FromArgb(45, 45, 45)
            };

            Label lblExtFilter = new Label
            {
                Text = Localization.Get("EXTENSION_COLUMN") + ":",
                Location = new Point(15, 15),
                AutoSize = true
            };

            cboExtensionFilter = new ComboBox
            {
                Location = new Point(85, 13),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            
            cboExtensionFilter.Items.Add(Localization.Get("ALL_EXTENSIONS"));
            
            foreach (var ext in extensionGroups.Select(e => e.Extension).OrderBy(e => e))
            {
                cboExtensionFilter.Items.Add(string.IsNullOrEmpty(ext) ? "sin extensión" : ext);
            }
            cboExtensionFilter.SelectedIndex = 0;
            cboExtensionFilter.SelectedIndexChanged += (s, e) => FilterAndSortFiles();

            Label lblNameFilter = new Label
            {
                Text = Localization.Get("COL_NAME") + ":",
                Location = new Point(250, 15),
                AutoSize = true
            };

            txtFileFilter = new TextBox
            {
                Location = new Point(310, 13),
                Width = 200,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtFileFilter.TextChanged += (s, e) => FilterAndSortFiles();

            Button btnClearFileFilter = new Button
            {
                Text = Localization.Get("BTN_CLEAR"),
                Location = new Point(520, 11),
                Width = 100,
                BackColor = Color.FromArgb(60, 60, 60),
                FlatStyle = FlatStyle.Flat
            };
            btnClearFileFilter.Click += (s, e) =>
            {
                cboExtensionFilter.SelectedIndex = 0;
                txtFileFilter.Clear();
            };

            Label lblSortBy = new Label
            {
                Text = Localization.Get("SORT_BY") + ":",
                Location = new Point(15, 50),
                AutoSize = true
            };

            cboSortBy = new ComboBox
            {
                Location = new Point(95, 48),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            
            cboSortBy.Items.AddRange(new object[]
            {
                Localization.Get("SORT_NAME_AZ"),
                Localization.Get("SORT_SIZE_DESC"),
                Localization.Get("SORT_SIZE_ASC"),
                Localization.Get("SORT_EXTENSION"),
                Localization.Get("SORT_DATE_MODIFIED_DESC"),
                Localization.Get("SORT_DATE_MODIFIED_ASC"),
                Localization.Get("SORT_DATE_CREATED_DESC"),
                Localization.Get("SORT_DATE_CREATED_ASC"),
                Localization.Get("SORT_FOLDER_AZ")
            });
            
            cboSortBy.SelectedIndexChanged += (s, e) => FilterAndSortFiles();

            Label lblStatus = new Label
            {
                Text = Localization.Get("TIP_DOUBLE_CLICK"),
                Location = new Point(300, 50),
                AutoSize = true,
                ForeColor = Color.FromArgb(150, 150, 150),
                Font = MainForm.emojiFont
            };

            lvFileExplorer = new ListView
            {
                View = View.Details,
                Location = new Point(15, 85),
                Size = new Size(960, 385),
                FullRowSelect = true,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                Font = new Font("Consolas", 9),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            lvFileExplorer.Columns.Add(Localization.Get("COL_NAME"), 230);
            lvFileExplorer.Columns.Add(Localization.Get("COL_EXTENSION"), 80);
            lvFileExplorer.Columns.Add(Localization.Get("COL_SIZE"), 100);
            lvFileExplorer.Columns.Add(Localization.Get("COL_MODIFIED_DATE"), 140);
            lvFileExplorer.Columns.Add(Localization.Get("COL_CREATION_DATE"), 140);
            lvFileExplorer.Columns.Add(Localization.Get("COL_FOLDER"), 235);

            lvFileExplorer.DoubleClick += (s, e) =>
            {
                if (lvFileExplorer.SelectedItems.Count > 0)
                {
                    var item = lvFileExplorer.SelectedItems[0];
                    string folderPath = item.SubItems[5].Text;
                    if (Directory.Exists(folderPath))
                    {
                        Process.Start("explorer.exe", folderPath);
                    }
                }
            };

            tab.Controls.Add(lblExtFilter);
            tab.Controls.Add(cboExtensionFilter);
            tab.Controls.Add(lblNameFilter);
            tab.Controls.Add(txtFileFilter);
            tab.Controls.Add(btnClearFileFilter);
            tab.Controls.Add(lblSortBy);
            tab.Controls.Add(cboSortBy);
            tab.Controls.Add(lblStatus);
            tab.Controls.Add(lvFileExplorer);

            allFiles = files;
            currentFilteredFiles = files;
            
            cboSortBy.SelectedIndex = 1;
            
            currentFilteredFiles = files.OrderByDescending(f => f.Length).ToList();
            LoadFiles(currentFilteredFiles);

            return tab;
        }

        private void LoadFiles(List<FileInfo> files) {
            lvFileExplorer.Items.Clear();
            foreach (var file in files) {
                var item = new ListViewItem(file.Name);
                item.SubItems.Add(file.Extension);
                item.SubItems.Add(ToSize(file.Length));
                item.SubItems.Add(file.LastWriteTime.ToString("dd/MM/yyyy HH:mm"));
                item.SubItems.Add(file.CreationTime.ToString("dd/MM/yyyy HH:mm"));
                item.SubItems.Add(file.DirectoryName ?? "");
                item.SubItems.Add(file.FullName);
                
                if (lvFileExplorer.Items.Count % 2 == 0) {
                    item.BackColor = Color.FromArgb(45, 45, 45);
                }
                
                lvFileExplorer.Items.Add(item);
            }
        }

        private void FilterAndSortFiles() {
            string selectedExt = cboExtensionFilter.SelectedItem?.ToString() ?? "";
            string nameFilter = txtFileFilter.Text.ToLower().Trim();

            var filtered = allFiles.AsEnumerable();

            if (selectedExt != Localization.Get("ALL_EXTENSIONS")) {
                string ext = selectedExt == "(sin extensión)" ? "" : selectedExt;
                filtered = filtered.Where(f => f.Extension.ToLower() == ext.ToLower());
            }

            if (!string.IsNullOrEmpty(nameFilter)) {
                filtered = filtered.Where(f => f.Name.ToLower().Contains(nameFilter));
            }

            string sortOption = cboSortBy.SelectedItem?.ToString() ?? Localization.Get("SORT_NAME_AZ");
            filtered = sortOption switch {
                var s when s == Localization.Get("SORT_NAME_AZ") => filtered.OrderBy(f => f.Name),
                var s when s == Localization.Get("SORT_SIZE_DESC") => filtered.OrderByDescending(f => f.Length),
                var s when s == Localization.Get("SORT_SIZE_ASC") => filtered.OrderBy(f => f.Length),
                var s when s == Localization.Get("SORT_EXTENSION") => filtered.OrderBy(f => f.Extension).ThenBy(f => f.Name),
                var s when s == Localization.Get("SORT_DATE_MODIFIED_DESC") => filtered.OrderByDescending(f => f.LastWriteTime),
                var s when s == Localization.Get("SORT_DATE_MODIFIED_ASC") => filtered.OrderBy(f => f.LastWriteTime),
                var s when s == Localization.Get("SORT_DATE_CREATED_DESC") => filtered.OrderByDescending(f => f.CreationTime),
                var s when s == Localization.Get("SORT_DATE_CREATED_ASC") => filtered.OrderBy(f => f.CreationTime),
                var s when s == Localization.Get("SORT_FOLDER_AZ") => filtered.OrderBy(f => f.DirectoryName).ThenBy(f => f.Name),
                _ => filtered.OrderBy(f => f.Name)
            };

            currentFilteredFiles = filtered.ToList();
            LoadFiles(currentFilteredFiles);
        }

        private void AddSummaryListViewItem(ExtensionInfo group, long totalSize, List<FileInfo> allFiles) {
            var filesWithExt = allFiles.Where(f => f.Extension.ToLower() == group.Extension.ToLower()).ToList();
            var largestFile = filesWithExt.OrderByDescending(f => f.Length).FirstOrDefault();

            var item = new ListViewItem("");
            item.SubItems.Add(string.IsNullOrEmpty(group.Extension) ? "(sin extensión)" : group.Extension);
            item.SubItems.Add(group.Count.ToString());
            item.SubItems.Add(ToSize(group.TotalSize));
            item.SubItems.Add(ToSize(group.TotalSize / group.Count));
            item.SubItems.Add($"{(group.TotalSize * 100.0 / totalSize):F2}%");
            item.SubItems.Add(largestFile != null ? ToSize(largestFile.Length) : "N/A");
            
            if (lvSummary.Items.Count % 2 == 0) {
                item.BackColor = Color.FromArgb(45, 45, 45);
            }
            
            lvSummary.Items.Add(item);
        }

        private void FilterExtensions() {
            string filter = txtFilter.Text.ToLower().Trim();
            lvSummary.Items.Clear();

            var filtered = allExtensions.Where(g => string.IsNullOrEmpty(filter) || g.Extension.Contains(filter)).ToList();
            long totalSize = allExtensions.Sum(g => g.TotalSize);

            foreach (var group in filtered) {
                AddSummaryListViewItem(group, totalSize, allFiles);
            }
        }

        private void ExportSummaryToClipboard(List<ExtensionInfo> extensionGroups, long totalSize) {
            var text = $"RESUMEN DE ARCHIVOS - {categoryName}\n";
            text += "===========================================\n\n";
            text += $"Total de extensiones: {extensionGroups.Count}\n";
            text += $"Tamaño total: {ToSize(totalSize)}\n\n";
            text += "Extensión\tCantidad\tTamaño Total\tTamaño Promedio\t% del Total\n";
            text += "----------------------------------------------------------------\n";

            foreach (var group in extensionGroups) {
                text += $"{(string.IsNullOrEmpty(group.Extension) ? "(sin ext)" : group.Extension)}\t";
                text += $"{group.Count}\t";
                text += $"{ToSize(group.TotalSize)}\t";
                text += $"{ToSize(group.TotalSize / group.Count)}\t";
                text += $"{(group.TotalSize * 100.0 / totalSize):F2}%\n";
            }

            Clipboard.SetText(text);
            MessageBox.Show("Resumen copiado al portapapeles.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public Dictionary<string, List<string>> GetUpdatedGroups() {
            return groups;
        }

        public HashSet<string> GetUpdatedBlacklist() {
            return blacklist;
        }

        private string ToSize(long b) {
            string[] u = { "B", "KB", "MB", "GB", "TB" };
            double s = b; int i = 0;
            while (s >= 1024 && i < 4) { s /= 1024; i++; }
            return $"{s:F2} {u[i]}";
        }
    }
}
