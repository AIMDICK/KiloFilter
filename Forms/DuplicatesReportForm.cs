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

        public class DuplicateGroupInfo
        {
            public string Hash { get; set; } = "";
            public long FileSize { get; set; }
            public List<FileInfo> Files { get; set; } = new List<FileInfo>();
        }

        public DuplicatesReportForm(List<DuplicateGroupInfo> duplicates) {
            this.duplicateGroups = duplicates;
            this.Text = Localization.Get("DUPLICATES_REPORT_TITLE");
            this.Size = new Size(1200, 700);
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(900, 500);

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
                Size = new Size(1170, 520),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // TAB 1: Resumen de grupos
            TabPage tabSummary = CreateSummaryTab();
            tabControl.TabPages.Add(tabSummary);

            // TAB 2: Detalles completos
            TabPage tabDetails = CreateDetailsTab();
            tabControl.TabPages.Add(tabDetails);

            // Botones
            Button btnClose = new Button {
                Text = Localization.Get("BTN_CLOSE") ?? "Cerrar",
                Location = new Point(1070, 590),
                Width = 110,
                Height = 30,
                BackColor = Color.FromArgb(80, 80, 120),
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            Button btnExport = new Button {
                Text = Localization.Get("BTN_COPY_CLIPBOARD"),
                Location = new Point(915, 590),
                Width = 150,
                Height = 30,
                BackColor = Color.FromArgb(60, 100, 60),
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnExport.Click += (s, e) => ExportToClipboard();

            this.Controls.AddRange(new Control[] { lblSummary, tabControl, btnExport, btnClose });
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

            lvDuplicates.Columns.Add(Localization.Get("COL_HASH_GROUP"), 150);
            lvDuplicates.Columns.Add(Localization.Get("COL_SIZE"), 120);
            lvDuplicates.Columns.Add(Localization.Get("COL_DUPLICATE_FILES"), 150);
            lvDuplicates.Columns.Add(Localization.Get("COL_WASTED_SPACE"), 180);
            lvDuplicates.Columns.Add(Localization.Get("COL_FILE_NAMES"), 550);

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

            lvDetails.Columns.Add(Localization.Get("COL_HASH_GROUP"), 150);
            lvDetails.Columns.Add(Localization.Get("COL_FILENAME"), 250);
            lvDetails.Columns.Add(Localization.Get("COL_SIZE"), 100);
            lvDetails.Columns.Add(Localization.Get("COL_FULL_PATH"), 450);
            lvDetails.Columns.Add(Localization.Get("COL_MODIFIED_DATE"), 150);

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
