using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using KiloFilter.Core;
using KiloFilter.Models;

namespace KiloFilter.Forms
{
    public class HistoryForm : Form
    {
        private ListView lvHistory = null!;
        private Label lblInfo = null!;
        private Button btnLoad = null!;
        private Button btnDelete = null!;
        private Button btnRefresh = null!;
        private Button btnClear = null!;
        private Label lblStatus = null!;

        public HistoryForm()
        {
            this.Text = Localization.Get("HISTORY_TITLE");
            this.Size = new Size(900, 600);
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(700, 400);

            // Icono incrustado
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            InitializeComponents();
            LoadHistory();
        }

        private void InitializeComponents()
        {
            lblInfo = new Label
            {
                Text = "",
                Location = new Point(15, 15),
                Size = new Size(870, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 200, 255),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            lvHistory = new ListView
            {
                Location = new Point(15, 50),
                Size = new Size(870, 400),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 9)
            };

            lvHistory.Columns.Add(Localization.Get("HISTORY_COL_FOLDER"), 590);
            lvHistory.Columns.Add(Localization.Get("HISTORY_COL_DATE"), 150);
            lvHistory.Columns.Add(Localization.Get("HISTORY_COL_FILES"), 130);

            btnLoad = new Button
            {
                Text = Localization.Get("HISTORY_BTN_LOAD"),
                Location = new Point(15, 465),
                Width = 150,
                Height = 35,
                BackColor = Color.FromArgb(0, 120, 180),
                FlatStyle = FlatStyle.Flat,
                Font = MainForm.emojiFontBold,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            btnLoad.Click += (s, e) => LoadSelectedAnalysis();

            btnDelete = new Button
            {
                Text = Localization.Get("HISTORY_BTN_DELETE"),
                Location = new Point(175, 465),
                Width = 120,
                Height = 35,
                BackColor = Color.FromArgb(180, 50, 50),
                FlatStyle = FlatStyle.Flat,
                Font = MainForm.emojiFontBold,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            btnDelete.Click += (s, e) => DeleteSelectedAnalysis();

            btnRefresh = new Button
            {
                Text = Localization.Get("HISTORY_BTN_REFRESH"),
                Location = new Point(305, 465),
                Width = 120,
                Height = 35,
                BackColor = Color.FromArgb(60, 120, 60),
                FlatStyle = FlatStyle.Flat,
                Font = MainForm.emojiFontBold,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            btnRefresh.Click += (s, e) => LoadHistory();

            btnClear = new Button
            {
                Text = Localization.Get("HISTORY_BTN_CLEAR"),
                Location = new Point(435, 465),
                Width = 130,
                Height = 35,
                BackColor = Color.FromArgb(139, 69, 19),
                FlatStyle = FlatStyle.Flat,
                Font = MainForm.emojiFontBold,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            btnClear.Click += (s, e) => ClearAllCache();

            lblStatus = new Label
            {
                Location = new Point(15, 510),
                Size = new Size(870, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(15, 15, 15),
                ForeColor = Color.FromArgb(150, 150, 150),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            Button btnClose = new Button
            {
                Text = Localization.Get("HISTORY_BTN_CLOSE"),
                Location = new Point(755, 465),
                Width = 120,
                Height = 35,
                BackColor = Color.FromArgb(80, 80, 120),
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Font = MainForm.emojiFontBold
            };

            this.Controls.AddRange(new Control[] { lblInfo, lvHistory, btnLoad, btnDelete, btnRefresh, btnClear, lblStatus, btnClose });
        }

        private void LoadHistory()
        {
            lvHistory.Items.Clear();
            var cacheList = CacheManager.GetCachedAnalysisList();

            if (cacheList.Count == 0)
            {
                lblStatus.Text = Localization.Get("HISTORY_NO_CACHE");
                return;
            }

            foreach (var (path, date) in cacheList)
            {
                try
                {
                    var cache = CacheManager.GetCachedAnalysis(path);
                    if (cache != null)
                    {
                        var item = new ListViewItem(path);
                        item.SubItems.Add(date.ToString("dd/MM/yyyy HH:mm:ss"));
                        item.SubItems.Add(cache.TotalFiles.ToString());
                        item.Tag = cache;  // Guardar cache completo para cargar después

                        if (lvHistory.Items.Count % 2 == 0)
                        {
                            item.BackColor = Color.FromArgb(45, 45, 45);
                        }

                        lvHistory.Items.Add(item);
                    }
                }
                catch { }
            }

            lblStatus.Text = string.Format(Localization.Get("HISTORY_FOUND_FORMAT"), lvHistory.Items.Count);
        }

        private void LoadSelectedAnalysis()
        {
            if (lvHistory.SelectedItems.Count == 0)
            {
                MessageBox.Show(Localization.Get("HISTORY_SELECT_TO_LOAD"), Localization.Get("TITLE_INFORMATION"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedItem = lvHistory.SelectedItems[0];
            var cache = selectedItem.Tag as AnalysisCache;

            if (cache == null)
            {
                MessageBox.Show(Localization.Get("HISTORY_ERROR_LOAD"), Localization.Get("TITLE_ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Tag = cache;  // Pasar caché a MainForm
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DeleteSelectedAnalysis()
        {
            if (lvHistory.SelectedItems.Count == 0)
            {
                MessageBox.Show(Localization.Get("HISTORY_SELECT_TO_DELETE"), Localization.Get("TITLE_INFORMATION"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedItem = lvHistory.SelectedItems[0];
            string folderPath = selectedItem.Text;

            var result = MessageBox.Show(
                string.Format(Localization.Get("HISTORY_CONFIRM_DELETE"), folderPath),
                Localization.Get("HISTORY_CONFIRM_DELETE_TITLE"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                CacheManager.ClearCache(folderPath);
                LoadHistory();
                MessageBox.Show(Localization.Get("HISTORY_DELETED_SUCCESS"), Localization.Get("TITLE_SUCCESS"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ClearAllCache()
        {
            var result = MessageBox.Show(
                Localization.Get("HISTORY_CLEAR_WARNING"),
                Localization.Get("HISTORY_CLEAR_TITLE"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                var cacheList = CacheManager.GetCachedAnalysisList();
                int count = 0;

                foreach (var (path, _) in cacheList)
                {
                    try
                    {
                        CacheManager.ClearCache(path);
                        count++;
                    }
                    catch { }
                }

                LoadHistory();
                MessageBox.Show(string.Format(Localization.Get("HISTORY_CLEARED_FORMAT"), count), Localization.Get("TITLE_SUCCESS"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public AnalysisCache? GetSelectedCache()
        {
            return this.Tag as AnalysisCache;
        }

        private string FormatSize(long bytes)
        {
            string[] units = { "B", "KB", "MB", "GB", "TB" };
            double size = bytes;
            int unitIndex = 0;

            while (size >= 1024 && unitIndex < units.Length - 1)
            {
                size /= 1024;
                unitIndex++;
            }

            return $"{size:F2} {units[unitIndex]}";
        }
    }
}
