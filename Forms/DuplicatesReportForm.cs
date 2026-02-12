using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using KiloFilter.Core;

namespace KiloFilter.Forms
{
    public class DuplicatesReportForm : Form
    {
        private List<DuplicateGroupInfo> duplicateGroups;
        private TabControl tabControl = null!;
        private ListView lvDuplicates = null!;
        private ListView lvDetails = null!;
        private Label lblSummary = null!;
        private TextBox txtSearchFileName = null!;
        private TextBox txtMinSize = null!;
        private TextBox txtMaxSize = null!;
        private ComboBox cmbDeletionStrategy = null!;
        private ListView lvDeletePreview = null!;

        public class DuplicateGroupInfo
        {
            public string Hash { get; set; } = "";
            public long FileSize { get; set; }
            public List<FileInfo> Files { get; set; } = new List<FileInfo>();
        }

        public DuplicatesReportForm(List<DuplicateGroupInfo> duplicates) {
            this.duplicateGroups = duplicates;
            this.Text = Localization.Get("DUPLICATES_REPORT_TITLE");
            this.Size = new Size(1400, 800);
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(1000, 600);
            
            // Icono incrustado
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            InitializeComponents();
        }

        private void InitializeComponents() {
            // Summary label
            lblSummary = new Label {
                Text = string.Format(Localization.Get("DUPLICATES_FOUND"), duplicateGroups.Count),
                Location = new Point(15, 15),
                Size = new Size(1170, 30),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 150, 0),
                BackColor = Color.FromArgb(30, 30, 30)
            };

            // TabControl
            tabControl = new DarkTabControl {
                Location = new Point(15, 55),
                Size = new Size(1370, 620),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // TAB 1: Resumen de grupos
            TabPage tabSummary = CreateSummaryTab();
            tabControl.TabPages.Add(tabSummary);

            // TAB 2: Detalles completos
            TabPage tabDetails = CreateDetailsTab();
            tabControl.TabPages.Add(tabDetails);

            // TAB 3: Búsqueda y Filtrado + Eliminación Inteligente
            TabPage tabSmartDelete = CreateSmartDeleteTab();
            tabControl.TabPages.Add(tabSmartDelete);

            // Botones
            Button btnClose = new Button {
                Text = Localization.Get("BTN_CLOSE") ?? "Cerrar",
                Location = new Point(1270, 690),
                Width = 110,
                Height = 30,
                BackColor = Color.FromArgb(80, 80, 120),
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            this.Controls.AddRange(new Control[] { lblSummary, tabControl, btnClose });
        }

        private TabPage CreateSummaryTab() {
            TabPage tab = new TabPage {
                Text = Localization.Get("TAB_SUMMARY_GROUPS"),
                BackColor = Color.FromArgb(45, 45, 45)
            };

            lvDuplicates = new ListView {
                Location = new Point(10, 10),
                Size = new Size(1150, 480),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            lvDuplicates.Columns.Add(Localization.Get("COL_HASH_GROUP"), 140);
            lvDuplicates.Columns.Add(Localization.Get("COL_SIZE"), 100);
            lvDuplicates.Columns.Add(Localization.Get("COL_DUPLICATE_FILES"), 120);
            lvDuplicates.Columns.Add(Localization.Get("COL_WASTED_SPACE"), 140);
            lvDuplicates.Columns.Add(Localization.Get("COL_FILE_NAMES"), 650);

            var totalWaste = 0L;

            foreach (var group in duplicateGroups.OrderByDescending(g => g.Files.Count * g.FileSize)) {
                var item = new ListViewItem(group.Hash.Substring(0, Math.Min(16, group.Hash.Length)) + "...");
                item.SubItems.Add(FormatSize(group.FileSize));
                item.SubItems.Add(group.Files.Count.ToString());
                
                long waste = (group.Files.Count - 1) * group.FileSize;
                totalWaste += waste;
                item.SubItems.Add(FormatSize(waste));
                
                string fileNames = string.Join(", ", group.Files.Select(f => System.IO.Path.GetFileName(f.FullName)).Take(3));
                if (group.Files.Count > 3) fileNames += $"... +{group.Files.Count - 3}";
                item.SubItems.Add(fileNames);
                
                item.Tag = group;
                item.UseItemStyleForSubItems = false;
                if (waste > 0) item.SubItems[3].ForeColor = Color.FromArgb(255, 100, 100);
                
                lvDuplicates.Items.Add(item);
            }

            // Footer con total
            var footer = new ListViewItem(Localization.Get("LABEL_TOTAL"), 0);
            footer.SubItems.Add("");
            footer.SubItems.Add(duplicateGroups.Sum(g => g.Files.Count).ToString());
            footer.SubItems.Add(FormatSize(totalWaste));
            footer.SubItems.Add("");
            footer.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            footer.ForeColor = Color.FromArgb(255, 200, 100);
            lvDuplicates.Items.Add(footer);

            lvDuplicates.Click += (s, e) => ShowDetailsForGroup();
            lvDuplicates.DoubleClick += (s, e) => OpenFileDirectory(lvDuplicates);

            tab.Controls.Add(lvDuplicates);
            return tab;
        }

        private TabPage CreateDetailsTab() {
            TabPage tab = new TabPage {
                Text = Localization.Get("TAB_DETAILS_FILES"),
                BackColor = Color.FromArgb(45, 45, 45)
            };

            lvDetails = new ListView {
                Location = new Point(10, 10),
                Size = new Size(1150, 480),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            lvDetails.Columns.Add(Localization.Get("COL_HASH_GROUP"), 120);
            lvDetails.Columns.Add(Localization.Get("COL_FILENAME"), 200);
            lvDetails.Columns.Add(Localization.Get("COL_SIZE"), 80);
            lvDetails.Columns.Add(Localization.Get("COL_FULL_PATH"), 550);
            lvDetails.Columns.Add(Localization.Get("COL_MODIFIED_DATE"), 180);

            foreach (var group in duplicateGroups) {
                // Mostrar todos los archivos duplicados del grupo
                foreach (var file in group.Files) {
                    var item = new ListViewItem(group.Hash.Substring(0, Math.Min(16, group.Hash.Length)) + "...");
                    item.SubItems.Add(System.IO.Path.GetFileName(file.FullName));
                    item.SubItems.Add(FormatSize(file.Length));
                    item.SubItems.Add(file.DirectoryName ?? "");
                    item.SubItems.Add(file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    item.Tag = file;  // Store FileInfo for double-click handler
                    
                    lvDetails.Items.Add(item);
                }
            }

            lvDetails.DoubleClick += (s, e) => OpenFileDirectory(lvDetails);

            tab.Controls.Add(lvDetails);
            return tab;
        }

        private void ShowDetailsForGroup() {
            if (lvDuplicates.SelectedItems.Count == 0) return;
            
            var selectedItem = lvDuplicates.SelectedItems[0];
            if (selectedItem.Tag is DuplicateGroupInfo group) {
                // Filtrar lvDetails para mostrar todos los archivos del grupo
                lvDetails.Items.Clear();
                
                foreach (var file in group.Files) {
                    var item = new ListViewItem(group.Hash.Substring(0, Math.Min(16, group.Hash.Length)) + "...");
                    item.SubItems.Add(System.IO.Path.GetFileName(file.FullName));
                    item.SubItems.Add(FormatSize(file.Length));
                    item.SubItems.Add(file.DirectoryName ?? "");
                    item.SubItems.Add(file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    
                    lvDetails.Items.Add(item);
                }
    
                tabControl.SelectedIndex = 1;
            }
        }

        private void ExportToClipboard() {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("=== REPORTE DE DUPLICADOS ===\n");
            sb.AppendLine($"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Total de grupos: {duplicateGroups.Count}");
            sb.AppendLine($"Total de archivos duplicados: {duplicateGroups.Sum(g => g.Files.Count)}\n");

            long totalWaste = 0;
            foreach (var group in duplicateGroups) {
                long waste = (group.Files.Count - 1) * group.FileSize;
                totalWaste += waste;
            }
            sb.AppendLine($"Espacio desperdiciado: {FormatSize(totalWaste)}\n");

            sb.AppendLine("DETALLES POR GRUPO:");
            sb.AppendLine("==================\n");

            foreach (var group in duplicateGroups.OrderByDescending(g => g.Files.Count * g.FileSize)) {
                sb.AppendLine($"Hash: {group.Hash}");
                sb.AppendLine($"Tamaño: {FormatSize(group.FileSize)}");
                sb.AppendLine($"Cantidad: {group.Files.Count} archivo(s)");
                sb.AppendLine("Archivos:");
                
                foreach (var file in group.Files) {
                    sb.AppendLine($"  - {file.FullName} ({FormatSize(file.Length)})");
                }
                
                sb.AppendLine();
            }

            Clipboard.SetText(sb.ToString());
            MessageBox.Show("Report copied to clipboard", Localization.Get("BTN_COPY_CLIPBOARD"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenFileDirectory(ListView listView) {
            if (listView.SelectedItems.Count == 0) return;

            try {
                string filePath = "";
                
                if (listView == lvDetails) {
                    // En la pestaña de detalles, el Tag contiene el FileInfo
                    var selectedItem = listView.SelectedItems[0];
                    if (selectedItem.Tag is FileInfo fileInfo) {
                        filePath = fileInfo.FullName;
                    } else {
                        // Fallback: obtener del SubItem del Full Path (columna 3 en detalles)
                        filePath = selectedItem.SubItems[3].Text;
                    }
                } else if (listView == lvDuplicates) {
                    // En la pestaña de resumen, obtener del SubItem del File Names (columna 4)
                    var selectedItem = listView.SelectedItems[0];
                    if (selectedItem.Tag is DuplicateGroupInfo group && group.Files.Count > 0) {
                        filePath = group.Files[0].FullName;
                    }
                }

                if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath)) {
                    // Abrir el directorio y seleccionar el archivo
                    Process.Start(new ProcessStartInfo {
                        FileName = "explorer.exe",
                        Arguments = $"/select,\"{filePath}\"",
                        UseShellExecute = true
                    });
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error opening directory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TabPage CreateSmartDeleteTab() {
            TabPage tab = new TabPage {
                Text = Localization.Get("SEARCH_FILTER"),
                BackColor = Color.FromArgb(45, 45, 45)
            };

            // Panel de búsqueda y filtrado
            Label lblSearch = new Label {
                Text = Localization.Get("FILTER_BY_NAME"),
                Location = new Point(15, 15),
                AutoSize = true,
                ForeColor = Color.FromArgb(200, 200, 200)
            };

            txtSearchFileName = new TextBox {
                Location = new Point(150, 13),
                Width = 200,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtSearchFileName.TextChanged += (s, e) => RefreshDeletePreview();

            Label lblMinSize = new Label {
                Text = Localization.Get("FILTER_BY_SIZE"),
                Location = new Point(370, 15),
                AutoSize = true,
                ForeColor = Color.FromArgb(200, 200, 200)
            };

            txtMinSize = new TextBox {
                Location = new Point(460, 13),
                Width = 80,
                Text = "0",
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtMinSize.TextChanged += (s, e) => RefreshDeletePreview();

            Label lblMaxSize = new Label {
                Text = Localization.Get("FILTER_MAX_SIZE"),
                Location = new Point(550, 15),
                AutoSize = true,
                ForeColor = Color.FromArgb(200, 200, 200)
            };

            txtMaxSize = new TextBox {
                Location = new Point(650, 13),
                Width = 80,
                Text = long.MaxValue.ToString(),
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtMaxSize.TextChanged += (s, e) => RefreshDeletePreview();

            // Panel de eliminación inteligente
            Label lblStrategy = new Label {
                Text = Localization.Get("SMART_DELETE"),
                Location = new Point(15, 50),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 200, 100)
            };

            cmbDeletionStrategy = new ComboBox {
                Location = new Point(15, 75),
                Width = 250,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            cmbDeletionStrategy.Items.Add(Localization.Get("KEEP_NEWEST"));
            cmbDeletionStrategy.Items.Add(Localization.Get("KEEP_OLDEST"));
            cmbDeletionStrategy.Items.Add(Localization.Get("KEEP_SMALLEST"));
            cmbDeletionStrategy.SelectedIndex = 0;
            cmbDeletionStrategy.SelectedIndexChanged += (s, e) => RefreshDeletePreview();

            Button btnExecuteDelete = new Button {
                Text = Localization.Get("SMART_DELETE"),
                Location = new Point(15, 110),
                Width = 250,
                Height = 30,
                BackColor = Color.FromArgb(150, 60, 60),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnExecuteDelete.Click += (s, e) => ExecuteSmartDelete();

            // Lista de vista previa
            Label lblPreviewTitle = new Label {
                Text = "Preview de archivos a eliminar:",
                Location = new Point(15, 155),
                AutoSize = true,
                ForeColor = Color.FromArgb(200, 200, 200)
            };

            lvDeletePreview = new ListView {
                Location = new Point(15, 180),
                Size = new Size(1340, 330),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 8),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            lvDeletePreview.Columns.Add("Archivo", 300);
            lvDeletePreview.Columns.Add("Tamaño", 100);
            lvDeletePreview.Columns.Add("Ruta", 500);
            lvDeletePreview.Columns.Add("Acción", 100);

            tab.Controls.AddRange(new Control[] {
                lblSearch, txtSearchFileName, lblMinSize, txtMinSize, lblMaxSize, txtMaxSize,
                lblStrategy, cmbDeletionStrategy, btnExecuteDelete, lblPreviewTitle, lvDeletePreview
            });

            // Cargar vista previa inicial con todos los duplicados
            InitializeDeletePreview();

            return tab;
        }

        private void InitializeDeletePreview() {
            lvDeletePreview.Items.Clear();

            var strategy = SmartDuplicateDeleter.DeletionStrategy.KeepNewest;
            long totalSpaceToFree = 0;
            int totalFilesToDelete = 0;

            // Mostrar vista previa inicial con todos los duplicados
            foreach (var group in duplicateGroups) {
                var filesToDelete = SmartDuplicateDeleter.GetFilesToDelete(group.Files, strategy);
                foreach (var file in filesToDelete) {
                    var item = new ListViewItem(System.IO.Path.GetFileName(file.FullName));
                    item.SubItems.Add(SmartDuplicateDeleter.FormatSize(file.Length));
                    item.SubItems.Add(file.DirectoryName ?? "");
                    item.SubItems.Add("DELETE");
                    item.Tag = file;
                    item.ForeColor = Color.FromArgb(255, 100, 100);
                    lvDeletePreview.Items.Add(item);
                    totalSpaceToFree += file.Length;
                    totalFilesToDelete++;
                }
            }

            // Mostrar resumen
            if (totalFilesToDelete > 0) {
                var summary = new ListViewItem($"Total: {totalFilesToDelete} archivos");
                summary.SubItems.Add(SmartDuplicateDeleter.FormatSize(totalSpaceToFree));
                summary.SubItems.Add("");
                summary.SubItems.Add("");
                summary.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                summary.ForeColor = Color.FromArgb(255, 150, 0);
                lvDeletePreview.Items.Add(summary);
            }
        }

        private void RefreshDeletePreview() {
            lvDeletePreview.Items.Clear();

            // Obtener filtros
            string nameFilter = txtSearchFileName.Text.ToLower();
            long.TryParse(txtMinSize.Text, out long minSize);
            long.TryParse(txtMaxSize.Text, out long maxSize);

            if (maxSize == 0) maxSize = long.MaxValue;

            // Recopilar todos los archivos duplicados con filtros
            var allDuplicateFiles = new List<FileInfo>();
            foreach (var group in duplicateGroups) {
                allDuplicateFiles.AddRange(group.Files);
            }

            var filtered = SmartDuplicateDeleter.FilterDuplicates(allDuplicateFiles, nameFilter, minSize, maxSize);

            if (filtered.Count == 0) {
                return;
            }

            // Recopilar grupos de duplicados filtrados
            var filteredGroups = new Dictionary<string, List<FileInfo>>();
            foreach (var group in this.duplicateGroups) {
                var groupFiltered = group.Files.Where(f => filtered.Contains(f)).ToList();
                if (groupFiltered.Count > 0) {
                    filteredGroups[group.Hash] = groupFiltered;
                }
            }

            // Determinar estrategia de eliminación
            var strategy = cmbDeletionStrategy.SelectedIndex switch {
                1 => SmartDuplicateDeleter.DeletionStrategy.KeepOldest,
                2 => SmartDuplicateDeleter.DeletionStrategy.KeepSmallest,
                _ => SmartDuplicateDeleter.DeletionStrategy.KeepNewest
            };

            long totalSpaceToFree = 0;
            int totalFilesToDelete = 0;

            // Mostrar vista previa
            foreach (var kvp in filteredGroups) {
                var filesToDelete = SmartDuplicateDeleter.GetFilesToDelete(kvp.Value, strategy);
                foreach (var file in filesToDelete) {
                    var item = new ListViewItem(System.IO.Path.GetFileName(file.FullName));
                    item.SubItems.Add(SmartDuplicateDeleter.FormatSize(file.Length));
                    item.SubItems.Add(file.DirectoryName ?? "");
                    item.SubItems.Add("DELETE");
                    item.Tag = file;
                    item.ForeColor = Color.FromArgb(255, 100, 100);
                    lvDeletePreview.Items.Add(item);
                    totalSpaceToFree += file.Length;
                    totalFilesToDelete++;
                }
            }

            // Mostrar resumen
            if (totalFilesToDelete > 0) {
                var summary = new ListViewItem($"Total: {totalFilesToDelete} archivos");
                summary.SubItems.Add(SmartDuplicateDeleter.FormatSize(totalSpaceToFree));
                summary.SubItems.Add("");
                summary.SubItems.Add("");
                summary.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                summary.ForeColor = Color.FromArgb(255, 150, 0);
                lvDeletePreview.Items.Add(summary);
            }
        }

        private void ExecuteSmartDelete() {
            if (lvDeletePreview.Items.Count == 0) {
                MessageBox.Show(Localization.Get("NO_DUPLICATES_FOUND"));
                return;
            }

            // Contar archivos a eliminar (excluyendo la fila de resumen)
            int filesToDeleteCount = lvDeletePreview.Items.Count - 1;

            // Confirmación
            var confirmMsg = string.Format(Localization.Get("DELETE_CONFIRM"), filesToDeleteCount);
            if (MessageBox.Show(confirmMsg, "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) {
                return;
            }

            // Recopilar archivos a eliminar
            var filesToDelete = new List<FileInfo>();
            foreach (ListViewItem item in lvDeletePreview.Items) {
                if (item.Tag is FileInfo file) {
                    filesToDelete.Add(file);
                }
            }

            // Ejecutar eliminación
            var result = SmartDuplicateDeleter.DeleteDuplicates(filesToDelete);

            // Mostrar resultado
            var successMsg = string.Format(Localization.Get("DELETE_SUCCESS"), result.DeletedCount, SmartDuplicateDeleter.FormatSize(result.SpaceFreed));
            MessageBox.Show(successMsg, "✅ Eliminación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (result.ErrorFiles.Count > 0) {
                MessageBox.Show($"Errores durante eliminación:\n{string.Join("\n", result.ErrorFiles)}", "⚠️ Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Limpiar preview
            lvDeletePreview.Items.Clear();
        }

        private string FormatSize(long bytes) {
            string[] units = { "B", "KB", "MB", "GB", "TB" };
            double size = bytes;
            int unitIndex = 0;
            
            while (size >= 1024 && unitIndex < units.Length - 1) {
                size /= 1024;
                unitIndex++;
            }
            
            return $"{size:F2} {units[unitIndex]}";
        }
    }
}
