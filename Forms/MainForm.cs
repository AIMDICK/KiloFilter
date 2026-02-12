using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Blake3;
using KiloFilter.Core;
using KiloFilter.Models;

namespace KiloFilter.Forms
{
    public partial class MainForm : Form
    {
        // ORDEN PERSONALIZADO PARA LAS CATEGORÍAS
        public static readonly List<string> CategoryOrder = new List<string> {
            "Imagenes",
            "Videos",
            "Documentos",
            "Audio",
            "Comprimidos",
            "JuegosYMundos",
            "AplicacionesAPK",
            "BasesDeDatos",
            "CodigoFuente",
            "Modelos3D",
            "Ebooks",
            "Subtitulos",
            "LoDemas"
        };

        static Dictionary<string, List<string>> Groups = new Dictionary<string, List<string>> {
            { "Imagenes", new List<string> { ".jpg", ".jpeg", ".gif", ".bmp", ".png" } }, 
            { "Videos", new List<string> { ".mp4", ".mkv", ".mov", ".avi", ".flv", ".wmv", ".m4v" } },
            { "Documentos", new List<string> { ".pdf", ".docx", ".xlsx", ".pptx", ".csv", ".rtf", ".txt" } },
            { "Audio", new List<string> { ".mp3", ".wav", ".flac", ".ogg", ".m4a" } },
            { "Comprimidos", new List<string> { ".zip", ".rar", ".7z", ".tar", ".iso" } },
            { "JuegosYMundos", new List<string> { ".mca", ".sav", ".save", ".plr", ".wld", ".pup", ".cube" } },
            { "AplicacionesAPK", new List<string> { ".apk" } },
            { "BasesDeDatos", new List<string> { ".db", ".sqlite", ".mdb", ".accdb" } },
            { "CodigoFuente", new List<string> { ".py", ".js", ".cpp", ".h", ".cs", ".java", ".php", ".c" } },
            { "Modelos3D", new List<string> { ".obj", ".fbx", ".blend", ".stl", ".3ds" } },
            { "Ebooks", new List<string> { ".epub", ".mobi", ".azw3" } },
            { "Subtitulos", new List<string> { ".srt", ".ass", ".sub" } }
        };

        static HashSet<string> Blacklist = new HashSet<string> { 
            // Ejecutables y binarios
            ".exe", ".msi", ".com", ".scr",
            // Archivos de sistema
            ".sys", ".dll", ".drv", ".ocx", ".mui",
            // Logs y eventos
            ".log", ".etl", ".evtx",
            // Registros
            ".blf", ".regtrans-ms",
            // Temporales
            ".tmp", ".temp", ".cache", ".crdownload", ".part",
            // Backups
            ".bak", ".old", ".backup",
            // Thumbnails
            ".thumbdata",
            // Compilación
            ".pdb", ".obj", ".o", ".a", ".lib", ".idb", ".ipdb", ".pyc", ".pyo", ".class", ".dex", ".so", ".tlog",
            // IDEs
            ".suo", ".user", ".sln", ".docstates", ".swp", ".swo",
            // macOS
            ".DS_Store", ".localized",
            // Metadata y configuración
            ".ini", ".lnk", ".metadata", ".manifest", ".iobj", ".ipdb",
            // Videojuegos y cachés
            ".vkpipelinecachewindows", ".vkwarmupcachewindows", ".vkpipelinecacheheaderwindows", ".mdmp",
            // Otros
            ".db-journal", ".dat", ".bin", ".inf", ".cat", ".node", ".vdf", ".svg", ".tif", ".webp", 
            ".meta", ".qml", ".res", ".str", ".vue", ".json", ".jar", ".info", ".descarga", ".cd4nodes", 
            ".xml", ".m3u", ".m3u8", ".vbsfile", ".xaml", ".prf", ".msu", ".sha1", ".ucas" 
        };

        static readonly Dictionary<string, List<string>> DefaultGroups = new Dictionary<string, List<string>> {
            { "Imagenes", new List<string> { ".jpg", ".jpeg", ".gif", ".bmp", ".png" } }, 
            { "Videos", new List<string> { ".mp4", ".mkv", ".mov", ".avi", ".flv", ".wmv", ".m4v" } },
            { "Documentos", new List<string> { ".pdf", ".docx", ".xlsx", ".pptx", ".csv", ".rtf", ".txt" } },
            { "Audio", new List<string> { ".mp3", ".wav", ".flac", ".ogg", ".m4a" } },
            { "Comprimidos", new List<string> { ".zip", ".rar", ".7z", ".tar", ".iso" } },
            { "JuegosYMundos", new List<string> { ".mca", ".sav", ".save", ".plr", ".wld", ".pup", ".cube" } },
            { "AplicacionesAPK", new List<string> { ".apk" } },
            { "BasesDeDatos", new List<string> { ".db", ".sqlite", ".mdb", ".accdb" } },
            { "CodigoFuente", new List<string> { ".py", ".js", ".cpp", ".h", ".cs", ".java", ".php", ".c" } },
            { "Modelos3D", new List<string> { ".obj", ".fbx", ".blend", ".stl", ".3ds" } },
            { "Ebooks", new List<string> { ".epub", ".mobi", ".azw3" } },
            { "Subtitulos", new List<string> { ".srt", ".ass", ".sub" } }
        };

        static readonly HashSet<string> DefaultBlacklist = new HashSet<string> { 
            // Ejecutables y binarios
            ".exe", ".msi", ".com", ".scr",
            // Archivos de sistema
            ".sys", ".dll", ".drv", ".ocx", ".mui",
            // Logs y eventos
            ".log", ".etl", ".evtx",
            // Registros
            ".blf", ".regtrans-ms",
            // Temporales
            ".tmp", ".temp", ".cache", ".crdownload", ".part",
            // Backups
            ".bak", ".old", ".backup",
            // Thumbnails
            ".thumbdata",
            // Compilación
            ".pdb", ".obj", ".o", ".a", ".lib", ".idb", ".ipdb", ".pyc", ".pyo", ".class", ".dex", ".so", ".tlog",
            // IDEs
            ".suo", ".user", ".sln", ".docstates", ".swp", ".swo",
            // macOS
            ".DS_Store", ".localized",
            // Metadata y configuración
            ".ini", ".lnk", ".metadata", ".manifest", ".iobj", ".ipdb",
            // Videojuegos y cachés
            ".vkpipelinecachewindows", ".vkwarmupcachewindows", ".vkpipelinecacheheaderwindows", ".mdmp",
            // Otros
            ".db-journal", ".dat", ".bin", ".inf", ".cat", ".node", ".vdf", ".svg", ".tif", ".webp", 
            ".meta", ".qml", ".res", ".str", ".vue", ".json", ".jar", ".info", ".descarga", ".cd4nodes", 
            ".xml", ".m3u", ".m3u8", ".vbsfile", ".xaml", ".prf", ".msu", ".sha1", ".ucas" 
        };

        static readonly Dictionary<string, long> DefaultMinFileSizes = new Dictionary<string, long> {
            { ".jpg", 10240 },   // 10 KB
            { ".jpeg", 15360 },  // 15 KB
            { ".png", 15360 },   // 15 KB
            { ".gif", 15360 },   // 15 KB
            { ".txt", 5120 }     // 5 KB
        };

        static Dictionary<string, long> MinFileSizes = new Dictionary<string, long> {
            { ".jpg", 10240 },   // 10 KB
            { ".jpeg", 15360 },  // 15 KB
            { ".png", 15360 },   // 15 KB
            { ".gif", 15360 },   // 15 KB
            { ".txt", 5120 }     // 5 KB
        };

        // ✅ Fuentes especiales para emojis
        public static Font emojiFont = new Font("Segoe UI Emoji", 9F, FontStyle.Regular);
        public static Font emojiFontBold = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
        public static Font regularFont = new Font("Segoe UI", 9F, FontStyle.Regular);

        // ✅ Use Dictionary to prevent duplicate files by normalized path
        private Dictionary<string, FileInfo> FilesFound = new Dictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase);
        private List<DuplicateGroup> DuplicatesFound = new List<DuplicateGroup>();
        private List<FileInfo> deduplicatedFiles = new List<FileInfo>();
        private Label lblStatus = null!;
        private TextBox txtSrc = null!;
        private TextBox txtDst = null!;
        private ProgressBar pBar = null!;
        private ListView lv = null!;
        private List<DuplicatesReportForm.DuplicateGroupInfo> lastDuplicateGroups = new List<DuplicatesReportForm.DuplicateGroupInfo>();
        private Button btnReopenReport = null!;
        private Button btnCancel = null!;
        private CancellationTokenSource cancellationTokenSource = null!;
        
        // Referencias a botones principales para deshabilitar durante análisis
        private Button btnAnalyze = null!;
        private Button btnAnalyzeDuplicates = null!;
        private Button btnConfigExt = null!;
        private Button btnNewCategory = null!;
        private Button btnSrc = null!;
        private Button btnClear = null!;
        private Button btnDst = null!;
        private Button btnRescue = null!;

        public MainForm() {
            this.Text = Localization.Get("APP_TITLE");  // ✅ Usando traducciones
            this.Size = new Size(760, 750);
            this.BackColor = Color.FromArgb(24, 24, 24);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            
            // Icono incrustado
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            
            // ✅ Limpiar caché antiguo automáticamente
            Task.Run(() => CacheManager.CleanOldCache(30));
            
            InitializeComponents();
        }

        private void InitializeComponents() {
            // Limpiar controles existentes
            this.Controls.Clear();
            
            Label l1 = new Label { Text = Localization.Get("SOURCE_FOLDER"), Location = new Point(20, 20), AutoSize = true };
            txtSrc = new TextBox { Location = new Point(20, 45), Width = 420, BackColor = Color.FromArgb(40, 40, 40), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, AllowDrop = true };
            txtSrc.DragEnter += (s, e) => { if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true) e.Effect = DragDropEffects.Copy; };
            txtSrc.DragDrop += (s, e) => { var data = e.Data?.GetData(DataFormats.FileDrop) as string[]; if (data?.Length > 0 && Directory.Exists(data[0])) txtSrc.Text = data[0]; };
            btnSrc = new Button { Text = Localization.Get("BTN_BROWSE"), Location = new Point(450, 45), Width = 135, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(60, 60, 60) };
            btnClear = new Button { Text = Localization.Get("BTN_CLEAR"), Location = new Point(595, 45), Width = 130, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(100, 30, 30) };
            
            btnSrc.Click += (s, e) => { using(var f = new FolderBrowserDialog()) if(f.ShowDialog() == DialogResult.OK) txtSrc.Text = f.SelectedPath; };
            btnClear.Click += (s, e) => ResetAll();

            btnAnalyze = new Button { Text = Localization.Get("BTN_ANALYZE"), Location = new Point(20, 85), Width = 135, Height = 35, BackColor = Color.FromArgb(80, 0, 120), FlatStyle = FlatStyle.Flat, Font = new Font(this.Font, FontStyle.Bold) };
            btnAnalyze.Click += async (s, e) => await DoAnalyze();

            btnAnalyzeDuplicates = new Button { Text = Localization.Get("BTN_ANALYZE_DUPLICATES"), Location = new Point(165, 85), Width = 135, Height = 35, BackColor = Color.FromArgb(120, 80, 0), FlatStyle = FlatStyle.Flat, Font = new Font(this.Font, FontStyle.Bold) };
            btnAnalyzeDuplicates.Click += async (s, e) => await DoAnalyzeDuplicates();

            btnReopenReport = new Button { Text = Localization.Get("BTN_REOPEN_REPORT"), Location = new Point(165, 125), Width = 135, Height = 35, BackColor = Color.FromArgb(100, 80, 120), FlatStyle = FlatStyle.Flat, Font = new Font(this.Font, FontStyle.Bold), Visible = false };
            btnReopenReport.Click += (s, e) => {
                if (lastDuplicateGroups.Count > 0) {
                    var reportForm = new DuplicatesReportForm(lastDuplicateGroups);
                    reportForm.ShowDialog(this);
                }
            };

            Button btnHistory = new Button { 
                Text = Localization.Get("BTN_HISTORY"),
                Location = new Point(310, 85), 
                Width = 130, 
                Height = 35, 
                BackColor = Color.FromArgb(70, 130, 180), 
                FlatStyle = FlatStyle.Flat, 
                Font = MainForm.emojiFontBold
            };
            btnHistory.Click += (s, e) => ShowAnalysisHistory();

            btnNewCategory = new Button {
                Text = Localization.Get("BTN_NEW_CATEGORY"),
                Location = new Point(310, 125),
                Width = 130,
                Height = 35,
                BackColor = Color.FromArgb(0, 150, 100),
                FlatStyle = FlatStyle.Flat,
                Font = MainForm.emojiFontBold
            };
            btnNewCategory.Click += (s, e) => CreateNewCategory();

            btnConfigExt = new Button { 
                Text = Localization.Get("BTN_CONFIGURE"), 
                Location = new Point(450, 85), 
                Width = 275, 
                Height = 35, 
                BackColor = Color.FromArgb(0, 100, 150), 
                FlatStyle = FlatStyle.Flat, 
                Font = MainForm.emojiFontBold
            };
            btnConfigExt.Click += (s, e) => OpenExtensionConfig();

            Label l2 = new Label { Text = Localization.Get("DESTINATION_FOLDER"), Location = new Point(20, 140), AutoSize = true };
            txtDst = new TextBox { Location = new Point(20, 165), Width = 565, BackColor = Color.FromArgb(40, 40, 40), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, AllowDrop = true };
            txtDst.DragEnter += (s, e) => { if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true) e.Effect = DragDropEffects.Copy; };
            txtDst.DragDrop += (s, e) => { var data = e.Data?.GetData(DataFormats.FileDrop) as string[]; if (data?.Length > 0 && Directory.Exists(data[0])) txtDst.Text = data[0]; };
            btnDst = new Button { Text = Localization.Get("BTN_BROWSE"), Location = new Point(595, 165), Width = 130, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(60, 60, 60) };
            btnDst.Click += (s, e) => { using(var f = new FolderBrowserDialog()) if(f.ShowDialog() == DialogResult.OK) txtDst.Text = f.SelectedPath; };

            btnRescue = new Button { Text = Localization.Get("BTN_RESCUE"), Location = new Point(20, 205), Width = 705, Height = 40, BackColor = Color.FromArgb(30, 80, 40), FlatStyle = FlatStyle.Flat, Font = new Font(this.Font, FontStyle.Bold) };
            btnRescue.Click += async (s, e) => await DoRescue();

            lv = new ListView { 
                View = View.Details, Location = new Point(20, 260), Size = new Size(705, 360), 
                CheckBoxes = true, FullRowSelect = true, 
                BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White, BorderStyle = BorderStyle.None 
            };
            lv.Columns.Add(Localization.Get("COL_INCLUDE"), 60); 
            lv.Columns.Add(Localization.Get("COL_CATEGORY"), 240); 
            lv.Columns.Add(Localization.Get("COL_FILES"), 100); 
            lv.Columns.Add(Localization.Get("COL_SIZE"), 130);
            lv.Columns.Add("", 175);
            
            lv.Click += ListView_Click;

            pBar = new ProgressBar { Location = new Point(0, 640), Size = new Size(750, 15), Style = ProgressBarStyle.Continuous };
            lblStatus = new Label { Text = Localization.Get("STATUS_READY"), Location = new Point(5, 660), Size = new Size(740, 25), TextAlign = ContentAlignment.MiddleLeft, BackColor = Color.FromArgb(15, 15, 15) };

            // Botón de administrador (arriba a la derecha, a la izquierda del help)
            Button btnAdmin = new Button
            {
                Text = Localization.Get("BTN_ADMIN"),
                Location = new Point(290, 10),
                Width = 150,
                Height = 25,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(139, 69, 19),
                Font = MainForm.emojiFontBold
            };
            btnAdmin.Click += (s, e) => RunAsAdmin();

            // Botón de ayuda (arriba a la derecha, a la izquierda del idioma)
            Button btnHelp = new Button
            {
                Text = Localization.Get("BTN_HELP"),
                Location = new Point(450, 10),
                Width = 135,
                Height = 25,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(70, 130, 180),
                Font = MainForm.emojiFontBold
            };
            btnHelp.Click += (s, e) => ShowHelp();

            // Botón de idioma (arriba a la derecha)
            Button btnLanguage = new Button
            {
                Text = Localization.Get("BTN_LANGUAGE"),
                Location = new Point(595, 10),
                Width = 130,
                Height = 25,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(70, 70, 200),
                Font = MainForm.emojiFontBold
            };
            btnLanguage.Click += (s, e) => ShowLanguageMenu(btnLanguage);

            // Botón de cancelar (se muestra solo durante análisis/rescate)
            btnCancel = new Button
            {
                Text = "⏹ " + Localization.Get("BTN_CANCEL"),
                Location = new Point(20, 125),
                Width = 135,
                Height = 35,
                BackColor = Color.FromArgb(200, 50, 50),
                FlatStyle = FlatStyle.Flat,
                Font = MainForm.emojiFontBold,
                Visible = false
            };
            btnCancel.Click += (s, e) => CancelOperation();

            this.Controls.AddRange(new Control[] { l1, txtSrc, btnSrc, btnClear, btnAnalyze, btnAnalyzeDuplicates, btnReopenReport, btnHistory, btnCancel, btnConfigExt, btnNewCategory, l2, txtDst, btnDst, btnRescue, lv, pBar, lblStatus, btnAdmin, btnHelp, btnLanguage });
            
            // 🎯 Load default categories on startup
            InitializeDefaultCategories();
        }

        private void CreateNewCategory() {
            var newCategoryForm = new NewCategoryForm(Groups);
            if (newCategoryForm.ShowDialog() == DialogResult.OK) {
                string categoryName = newCategoryForm.GetCategoryName();
                List<string> extensions = newCategoryForm.GetExtensions();
                bool shouldAnalyze = newCategoryForm.ShouldAnalyze();
                
                if (!Groups.ContainsKey(categoryName)) {
                    Groups[categoryName] = extensions;
                    CategoryOrder.Insert(CategoryOrder.Count - 1, categoryName);
                    
                    if (shouldAnalyze) {
                        lblStatus.Text = $"✅ Categoría '{categoryName}' creada con {extensions.Count} extensión(es). Analizando...";
                        Task.Run(async () => {
                            await Task.Delay(500);
                            this.Invoke(new Action(async () => await DoAnalyze()));
                        });
                    } else {
                        lblStatus.Text = $"✅ Categoría '{categoryName}' creada con {extensions.Count} extensión(es). Lista para usar.";
                        // Refresh categories view to show the new category
                        InitializeDefaultCategories();
                    }
                } else {
                    MessageBox.Show("Ya existe una categoría con ese nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ListView_Click(object? sender, EventArgs e) {
            var mousePos = lv.PointToClient(Cursor.Position);
            var hitTest = lv.HitTest(mousePos);
            
            if (hitTest.Item != null && hitTest.SubItem != null) {
                int columnIndex = hitTest.Item.SubItems.IndexOf(hitTest.SubItem);
                if (columnIndex == 4) {
                    // ✅ Retrieve internal key from Tag
                    string categoryKey = hitTest.Item.Tag?.ToString() ?? "";
                    ShowCategoryDetails(categoryKey);
                }
            }
        }

        private void ShowCategoryDetails(string categoryKey) {
            // ✅ Use internal key to find files
            var categoryFiles = FilesFound.Values.Where(f => GetFolder(f.Extension.ToLower()) == categoryKey).ToList();
            
            if (categoryFiles.Count == 0) {
                // ✅ Show translated name in message
                string translatedName = Localization.GetFolderName(categoryKey);
                MessageBox.Show($"No hay archivos en la categoría '{translatedName}'.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ✅ Pass internal key to form along with duplicate groups
            var detailsForm = new CategoryDetailsForm(categoryFiles, Groups, Blacklist, categoryKey, lastDuplicateGroups);
            if (detailsForm.ShowDialog() == DialogResult.OK) {
                Groups = detailsForm.GetUpdatedGroups();
                Blacklist = detailsForm.GetUpdatedBlacklist();
                lblStatus.Text = "Categorías actualizadas desde el análisis detallado.";
            }
        }

        private void OpenExtensionConfig() {
            var configForm = new ExtensionConfigForm(Groups, Blacklist, MinFileSizes, DefaultGroups, DefaultBlacklist, DefaultMinFileSizes);
            if (configForm.ShowDialog() == DialogResult.OK) {
                Groups = configForm.GetUpdatedGroups();
                Blacklist = configForm.GetUpdatedBlacklist();
                MinFileSizes = configForm.GetUpdatedMinFileSizes();
                lblStatus.Text = "Configuración actualizada correctamente.";
                // Refresh categories view if no analysis data exists yet
                if (FilesFound.Count == 0) {
                    InitializeDefaultCategories();
                }
            }
        }

        private void ShowHelp()
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog(this);
        }

        private void ShowAnalysisHistory()
        {
            var historyForm = new HistoryForm();
            if (historyForm.ShowDialog(this) == DialogResult.OK)
            {
                var selectedCache = historyForm.GetSelectedCache();
                if (selectedCache != null)
                {
                    // Cargar análisis desde el caché
                    txtSrc.Text = selectedCache.FolderPath;
                    LoadAnalysisFromCache(selectedCache);
                    lblStatus.Text = $"✅ Análisis cargado desde caché ({selectedCache.AnalysisDate:dd/MM/yyyy HH:mm})";
                }
            }
        }

        private void ShowLanguageMenu(Button btnLanguage)
        {
            ContextMenuStrip languageMenu = new ContextMenuStrip
            {
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White
            };

            var languages = new[]
            {
                ("🇬🇧 English", Localization.Language.English),
                ("🇪🇸 Español", Localization.Language.Spanish),
                ("🇫🇷 Français", Localization.Language.French),
                ("🇩🇪 Deutsch", Localization.Language.German),
                ("🇮🇹 Italiano", Localization.Language.Italian),
                ("🇯🇵 日本語", Localization.Language.Japanese)
            };

            foreach (var (text, lang) in languages)
            {
                var item = new ToolStripMenuItem(text)
                {
                    Font = MainForm.emojiFont,
                    Checked = (Localization.CurrentLanguage == lang)
                };
                item.Click += (s, e) =>
                {
                    Localization.CurrentLanguage = lang;
                    RefreshLanguage();
                };
                languageMenu.Items.Add(item);
            }

            languageMenu.Show(btnLanguage, new Point(0, btnLanguage.Height));
        }

        private void RefreshLanguage()
        {
            // Recargar todos los textos del formulario
            InitializeComponents();
            // Recargar categorías con los nombres traducidos
            if (lv.Items.Count > 0) {
                InitializeDefaultCategories();
            }
        }

        private void CancelOperation()
        {
            cancellationTokenSource?.Cancel();
            lblStatus.Text = Localization.Get("STATUS_CANCELLED");
        }

        private void DisableAllControls()
        {
            btnAnalyze.Enabled = false;
            btnAnalyzeDuplicates.Enabled = false;
            btnConfigExt.Enabled = false;
            btnNewCategory.Enabled = false;
            btnSrc.Enabled = false;
            btnClear.Enabled = false;
            btnDst.Enabled = false;
            btnRescue.Enabled = false;
            btnCancel.Enabled = true;
            btnCancel.Visible = true;
        }

        private void EnableAllControls()
        {
            btnAnalyze.Enabled = true;
            btnAnalyzeDuplicates.Enabled = true;
            btnConfigExt.Enabled = true;
            btnNewCategory.Enabled = true;
            btnSrc.Enabled = true;
            btnClear.Enabled = true;
            btnDst.Enabled = true;
            btnRescue.Enabled = true;
            btnCancel.Visible = false;
        }

        private void ResetAll() {
            txtSrc.Clear(); txtDst.Clear(); lv.Items.Clear(); FilesFound.Clear(); deduplicatedFiles.Clear();
            pBar.Value = 0; pBar.Style = ProgressBarStyle.Continuous;
            lblStatus.Text = Localization.Get("STATUS_RESET");
            // Restore default categories after clearing
            InitializeDefaultCategories();
        }

        // ✅ Normalize file path to prevent duplicates from different path representations
        private string NormalizePath(string path) {
            return Path.GetFullPath(path).ToLowerInvariant();
        }

        private async Task DoAnalyze() {
            if (!Directory.Exists(txtSrc.Text)) return;
            
            // ✅ Verificar caché antes de analizar
            await CheckAndOfferCache(txtSrc.Text, isDuplicateAnalysis: false);
        }

        private async Task DoAnalyzeDuplicates() {
            if (!Directory.Exists(txtSrc.Text)) return;
            await CheckAndOfferCache(txtSrc.Text, isDuplicateAnalysis: true);
        }

        private async Task DoAnalyzeDuplicatesInternal() {
            FilesFound.Clear();
            deduplicatedFiles.Clear();
            lv.Items.Clear();
            btnReopenReport.Visible = false;
            
            cancellationTokenSource = new CancellationTokenSource();
            DisableAllControls();
            
            try {
                // Scan files without interfering with controls
                lblStatus.Text = Localization.Get("STATUS_ANALYZING");
                pBar.Style = ProgressBarStyle.Continuous;
                pBar.Value = 0;
                
                var countState = new ScanState { Count = 0 };
                await Task.Run(() => CountFiles(new DirectoryInfo(txtSrc.Text), countState));
                if (cancellationTokenSource.Token.IsCancellationRequested) return;
                int totalFiles = countState.Count;

                var scanState = new ScanState { Count = 0 };
                await Task.Run(() => DeepScanWithProgress(new DirectoryInfo(txtSrc.Text), totalFiles, scanState));
                if (cancellationTokenSource.Token.IsCancellationRequested) return;

                lv.Items.Clear();
                lblStatus.Text = Localization.Get("STATUS_ANALYZING_DUPLICATES");
                pBar.Style = ProgressBarStyle.Continuous;
                pBar.Value = 0;

                int filesProcessed = 0;
                totalFiles = FilesFound.Values.Count;

                var filesToShow = new List<FileInfo>();
            var duplicateGroups = new List<DuplicatesReportForm.DuplicateGroupInfo>();

            await Task.Run(() => {
                // FILTER 1: Group by file size
                var sizeGroups = FilesFound.Values.GroupBy(f => f.Length)
                                           .Where(g => g.Count() > 1);  // Solo grupos con potenciales duplicados

                foreach (var sizeGroup in sizeGroups) {
                    if (cancellationTokenSource.Token.IsCancellationRequested) return;
                    
                    // FILTER 2: Partial hash (first 64KB)
                    var partialHashDict = new Dictionary<string, List<FileInfo>>();
                    
                    foreach (var f in sizeGroup) {
                        if (cancellationTokenSource.Token.IsCancellationRequested) return;
                        try {
                            string partialHash = CalculatePartialHash(f.FullName);
                            if (!partialHashDict.ContainsKey(partialHash)) 
                                partialHashDict[partialHash] = new List<FileInfo>();
                            partialHashDict[partialHash].Add(f);
                        } catch { }
                        filesProcessed++;
                        UpdateProgress(filesProcessed, totalFiles);
                    }

                    // FILTER 3: Full hash only for files with matching partial hash
                    foreach (var partialGroup in partialHashDict) {
                        if (cancellationTokenSource.Token.IsCancellationRequested) return;
                        if (partialGroup.Value.Count == 1) {
                            // Archivo único - mantener
                            filesToShow.Add(partialGroup.Value[0]);
                        } else {
                            // Múltiples archivos con mismo tamaño y hash parcial - verificar con hash completo
                            var fullHashDict = new Dictionary<string, List<FileInfo>>();
                            
                            foreach (var f in partialGroup.Value) {
                                if (cancellationTokenSource.Token.IsCancellationRequested) return;
                                try {
                                    string fullHash = CalculateFullHash(f.FullName);
                                    if (!fullHashDict.ContainsKey(fullHash)) 
                                        fullHashDict[fullHash] = new List<FileInfo>();
                                    fullHashDict[fullHash].Add(f);
                                } catch { }
                            }

                            // Registrar duplicados encontrados y mantener uno de cada grupo
                            foreach (var fullGroup in fullHashDict) {
                                if (cancellationTokenSource.Token.IsCancellationRequested) return;
                                if (fullGroup.Value.Count > 1) {
                                    // Este grupo tiene duplicados
                                    duplicateGroups.Add(new DuplicatesReportForm.DuplicateGroupInfo {
                                        Hash = fullGroup.Key,
                                        FileSize = fullGroup.Value[0].Length,
                                        Files = new List<FileInfo>(fullGroup.Value)
                                    });
                                }
                                filesToShow.Add(fullGroup.Value[0]);
                            }
                        }
                    }
                }

                // Agregar archivos que no tienen posibles duplicados (tamaño único)
                if (cancellationTokenSource.Token.IsCancellationRequested) return;
                var uniqueSizeFiles = FilesFound.Values.GroupBy(f => f.Length)
                                                .Where(g => g.Count() == 1)
                                                .SelectMany(g => g);
                filesToShow.AddRange(uniqueSizeFiles);
            });

            if (cancellationTokenSource.Token.IsCancellationRequested) return;

            // Group by category like normal analysis
            var groups = filesToShow.GroupBy(f => GetFolder(f.Extension.ToLower()))
                                    .OrderBy(g => {
                                        int index = CategoryOrder.IndexOf(g.Key);
                                        return index == -1 ? 999 : index;
                                    });

            // ✅ Guardar la lista deduplicada para usarla en DoRescue()
            deduplicatedFiles = filesToShow;

            foreach (var g in groups) {
                if (cancellationTokenSource.Token.IsCancellationRequested) return;
                string categoryKey = g.Key;
                string translatedName = Localization.GetFolderName(categoryKey);

                var it = new ListViewItem("");
                it.Checked = true;
                it.Tag = categoryKey;
                it.SubItems.Add(translatedName);
                it.SubItems.Add(g.Count().ToString());
                it.SubItems.Add(ToSize(g.Sum(f => f.Length)));
                it.SubItems.Add(Localization.Get("BTN_VIEW_DETAILS"));
                
                it.UseItemStyleForSubItems = false;
                it.SubItems[4].ForeColor = Color.Cyan;
                it.SubItems[4].Font = new Font(lv.Font, FontStyle.Bold);
                
                lv.Items.Add(it);
            }

            pBar.Value = 100;
            lblStatus.Text = string.Format(Localization.Get("STATUS_ANALYSIS_COMPLETE"), filesToShow.Count);

            // Guardar grupos de duplicados y mostrar reporte si se encontraron
            lastDuplicateGroups = duplicateGroups;
            if (duplicateGroups.Count > 0) {
                this.Invoke(new Action(() => {
                    btnReopenReport.Visible = true;
                    var reportForm = new DuplicatesReportForm(duplicateGroups);
                    reportForm.ShowDialog(this);
                }));
            } else {
                this.Invoke(new Action(() => btnReopenReport.Visible = false));
            }
            
            // ✅ Guardar análisis de duplicados en caché
            await Task.Run(() => SaveAnalysisToCache(txtSrc.Text, filesToShow));
            } finally {
                EnableAllControls();
            }
        }

        // Filter 2: Partial hash with BLAKE3 (first min(FileSize, 64KB)) - Fast comparison
        private string CalculatePartialHash(string filePath) {
            const int BUFFER_SIZE = 65536; // 64 KB
            
            // Step 1: Determine chunk size S = min(FileSize, 64 KB)
            var fileInfo = new FileInfo(filePath);
            int chunkSize = (int)Math.Min(fileInfo.Length, BUFFER_SIZE);
            
            // Step 2: Read the first S bytes
            var hasher = Blake3.Hasher.New();
            using (var stream = File.OpenRead(filePath)) {
                byte[] buffer = new byte[chunkSize];
                int bytesRead = stream.Read(buffer, 0, chunkSize);
                
                // Step 3: Hash with BLAKE3 (faster initialization than SHA-256 for small chunks)
                hasher.Update(new ReadOnlySpan<byte>(buffer, 0, bytesRead));
            }
            
            // Get the hash result
            Blake3.Hash hash = hasher.Finalize();
            return hash.ToString().ToLower();
        }

        // Filter 3: Full hash (complete file) - Definitive comparison using BLAKE3
        private string CalculateFullHash(string filePath) {
            var hasher = Blake3.Hasher.New();
            using (var stream = File.OpenRead(filePath)) {
                byte[] buffer = new byte[1024 * 1024]; // 1MB buffer for streaming
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0) {
                    hasher.Update(new ReadOnlySpan<byte>(buffer, 0, bytesRead));
                }
            }
            Blake3.Hash hash = hasher.Finalize();
            return hash.ToString().ToLower();
        }

        private object lockObj = new object();

        // Clase auxiliar para mantener estado en paralelo
        private class ScanState {
            public int Count { get; set; }
        }

        private void CountFiles(DirectoryInfo dir, ScanState state) {
            try {
                foreach (var f in dir.GetFiles()) {
                    if (cancellationTokenSource?.Token.IsCancellationRequested == true) return;
                    lock (lockObj) state.Count++;
                }
                var subDirs = dir.GetDirectories().Where(d => (d.Attributes & FileAttributes.Hidden) == 0).ToArray();
                Parallel.ForEach(subDirs, sub => {
                    if (cancellationTokenSource?.Token.IsCancellationRequested == true) return;
                    CountFiles(sub, state);
                });
            } catch { }
        }

        private void DeepScanWithProgress(DirectoryInfo dir, int totalFiles, ScanState state) {
            try {
                foreach (var f in dir.GetFiles()) {
                    if (cancellationTokenSource?.Token.IsCancellationRequested == true) return;
                    lock (lockObj) state.Count++;
                    string ext = f.Extension.ToLower();
                    
                    // Verificar blacklist
                    if (Blacklist.Contains(ext)) {
                        UpdateProgress(state.Count, totalFiles);
                        continue;
                    }
                    
                    // Aplicar filtro de tamaño mínimo personalizado
                    if (MinFileSizes.ContainsKey(ext)) {
                        if (f.Length < MinFileSizes[ext]) {
                            UpdateProgress(state.Count, totalFiles);
                            continue;
                        }
                    }
                    
                    string normalizedPath = NormalizePath(f.FullName);
                    lock (lockObj) FilesFound[normalizedPath] = f;
                    UpdateProgress(state.Count, totalFiles);
                }
                var subDirs = dir.GetDirectories().Where(d => (d.Attributes & FileAttributes.Hidden) == 0).ToArray();
                Parallel.ForEach(subDirs, sub => {
                    if (cancellationTokenSource?.Token.IsCancellationRequested == true) return;
                    DeepScanWithProgress(sub, totalFiles, state);
                });
            } catch { }
        }

        private void UpdateProgress(int current, int total) {
            if (total <= 0) return;
            int percentage = (int)((double)current / total * 100);
            this.Invoke(new Action(() => {
                pBar.Value = Math.Min(percentage, 99);
                lblStatus.Text = $"Analizando: {percentage}%";
            }));
        }

        private void DeepScan(DirectoryInfo dir) {
            try {
                foreach (var f in dir.GetFiles()) {
                    if (cancellationTokenSource?.Token.IsCancellationRequested == true) return;
                    string ext = f.Extension.ToLower();
                    
                    // Verificar blacklist
                    if (Blacklist.Contains(ext)) continue;
                    
                    // ⭐ NUEVO: Aplicar filtro de tamaño mínimo personalizado
                    if (MinFileSizes.ContainsKey(ext)) {
                        if (f.Length < MinFileSizes[ext]) continue;
                    }
                    
                    string normalizedPath = NormalizePath(f.FullName);
                    lock (lockObj) FilesFound[normalizedPath] = f;
                }
                var subDirs = dir.GetDirectories().Where(d => (d.Attributes & FileAttributes.Hidden) == 0).ToArray();
                Parallel.ForEach(subDirs, sub => {
                    if (cancellationTokenSource?.Token.IsCancellationRequested == true) return;
                    DeepScan(sub);
                });
            } catch { }
            RefreshListViewDetails();
        }

        private void InitializeDefaultCategories() {
            // 🎯 Load categories with 0 files initially (before analysis)
            lv.Items.Clear();
            
            foreach (var categoryKey in CategoryOrder) {
                // Only show categories that are in Groups (active categories)
                if (Groups.ContainsKey(categoryKey)) {
                    string translatedName = Localization.GetFolderName(categoryKey);
                    
                    var it = new ListViewItem("");
                    it.Checked = true;
                    it.Tag = categoryKey;  // Store internal key in Tag
                    it.SubItems.Add(translatedName);
                    it.SubItems.Add("0");  // 0 files initially
                    it.SubItems.Add(ToSize(0));  // 0 bytes
                    it.SubItems.Add(Localization.Get("BTN_VIEW_DETAILS"));
                    
                    it.UseItemStyleForSubItems = false;
                    it.SubItems[4].ForeColor = Color.Cyan;
                    it.SubItems[4].Font = new Font(lv.Font, FontStyle.Bold);
                    
                    lv.Items.Add(it);
                }
            }
        }

        private void RefreshListViewDetails() {
            // Actualiza el texto del botón de detalles en cada fila
            foreach (ListViewItem item in lv.Items) {
                if (item.SubItems.Count > 4) {
                    item.SubItems[4].Text = Localization.Get("BTN_VIEW_DETAILS");
                }
            }
        }
        private async Task DoRescue() {
            if (deduplicatedFiles.Count == 0 || !Directory.Exists(txtDst.Text)) return;
            
            // Crear token de cancelación
            cancellationTokenSource = new CancellationTokenSource();
            DisableAllControls();

            try {
                // ✅ Get internal category keys from Tag
                var allowedCategoryKeys = lv.CheckedItems.Cast<ListViewItem>()
                    .Select(item => item.Tag?.ToString() ?? "")
                    .Where(key => !string.IsNullOrEmpty(key))
                    .ToList();
                // ✅ Use deduplicatedFiles instead of FilesFound to avoid copying duplicates
                var filesToCopy = deduplicatedFiles
                    .Where(f => allowedCategoryKeys.Contains(GetFolder(f.Extension.ToLower())))
                    .ToList();

                if (filesToCopy.Count == 0) { MessageBox.Show(Localization.Get("NO_CATEGORIES_SELECTED")); return; }

                string target = Path.Combine(txtDst.Text, "RESCATE_" + DateTime.Now.ToString("yyyyMMdd_HHmm"));
                string logPath = Path.Combine(target, "RESCUE_LOG.txt");
                long total = filesToCopy.Sum(f => f.Length), current = 0;
                int successCount = 0, failCount = 0;
                int expectedCount = filesToCopy.Count;
                List<string> logLines = new List<string>();
                
                logLines.Add($"KiloFilter Rescue Log - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                logLines.Add($"Expected files: {expectedCount}");
                logLines.Add("========================================\n");
                
                pBar.Style = ProgressBarStyle.Continuous;
                await Task.Run(() => {
                    foreach (var f in filesToCopy) {
                        if (cancellationTokenSource.Token.IsCancellationRequested) break;
                        try {
                            string categoryKey = GetFolder(f.Extension.ToLower());
                            // ✅ Use translated folder name
                            string translatedFolder = Localization.GetFolderName(categoryKey);
                            string folder = Path.Combine(target, translatedFolder);
                            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                            f.CopyTo(Path.Combine(folder, f.Name), true);
                            current += f.Length;
                            successCount++;
                            this.Invoke(new Action(() => pBar.Value = (int)((double)current / total * 100)));
                        } catch (Exception ex) {
                            failCount++;
                            logLines.Add($"FAILED: {f.FullName}");
                            logLines.Add($"  Reason: {ex.GetType().Name} - {ex.Message}");
                            logLines.Add("");
                        }
                    }
                });
                
                logLines.Add("========================================");
                logLines.Add($"Successfully copied: {successCount}/{expectedCount}");
                logLines.Add($"Failed: {failCount}/{expectedCount}");
                
                // Write log file
                if (!Directory.Exists(target)) Directory.CreateDirectory(target);
                File.WriteAllLines(logPath, logLines);
                
                // Report results
                lblStatus.Text = string.Format("Rescue Complete - {0}/{1} files copied", successCount, expectedCount);
                MessageBox.Show(
                    string.Format("Files copied: {0}\nFiles failed: {1}\n\nDetailed log saved to:\n{2}\n\nCommon causes:\n- Files in use by another application\n- Permission restrictions\n- Files deleted during rescue\n- Insufficient disk space\n- Path length exceeds limit", successCount, failCount, logPath),
                    "Rescue Summary",
                    MessageBoxButtons.OK,
                    failCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information
                );
                
                if (!cancellationTokenSource.Token.IsCancellationRequested)
                    Process.Start("explorer.exe", target);
            } finally {
                EnableAllControls();
            }
        }

        private string GetFolder(string e) {
            foreach (var g in Groups) if (g.Value.Contains(e)) return g.Key;
            return "LoDemas";
        }

        private string ToSize(long b) {
            string[] u = { "B", "KB", "MB", "GB", "TB" };
            double s = b; int i = 0;
            while (s >= 1024 && i < 4) { s /= 1024; i++; }
            return $"{s:F2} {u[i]}";
        }

        private void RunAsAdmin() {
            try {
                // Check if already running as admin
                System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator)) {
                    MessageBox.Show(Localization.Get("ALREADY_ADMIN"), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Restart as admin
                ProcessStartInfo psi = new ProcessStartInfo {
                    FileName = Application.ExecutablePath,
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(psi);
                this.Close();
            } catch {
                MessageBox.Show(Localization.Get("ADMIN_DENIED"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CACHE MANAGEMENT METHODS
        private async Task CheckAndOfferCache(string folderPath, bool isDuplicateAnalysis = false)
        {
            var cachedAnalysis = CacheManager.GetCachedAnalysis(folderPath);
            
            if (cachedAnalysis != null)
            {
                // Verificar si el contenido ha cambiado
                string currentHash = CacheManager.GetFolderContentHash(folderPath, Blacklist);
                bool hasChanged = currentHash != cachedAnalysis.ContentHash;

                string message = hasChanged 
                    ? string.Format(Localization.Get("CACHE_CHANGED_MESSAGE"), cachedAnalysis.AnalysisDate.ToString("dd/MM/yyyy HH:mm:ss"))
                    : string.Format(Localization.Get("CACHE_UNCHANGED_MESSAGE"), cachedAnalysis.AnalysisDate.ToString("dd/MM/yyyy HH:mm:ss"));

                using (var decisionForm = new CacheDecisionForm(
                    message,
                    Localization.Get("CACHE_CHECK_DIALOG_TITLE"),
                    hasChanged))
                {
                    decisionForm.ShowDialog(this);
                    
                    if (decisionForm.CacheDecision == DialogResult.Yes)
                    {
                        // Rehacer análisis - continuar con nuevo análisis
                        // No hacer nada aquí, continuará abajo
                    }
                    else if (decisionForm.CacheDecision == DialogResult.No)
                    {
                        // Usar análisis anterior
                        LoadAnalysisFromCache(cachedAnalysis);
                        return;
                    }
                    else if (decisionForm.CacheDecision == DialogResult.Cancel)
                    {
                        // Cancelar operación
                        return;
                    }
                }
            }

            // Hacer nuevo análisis
            if (isDuplicateAnalysis)
            {
                await DoAnalyzeDuplicatesInternal();
            }
            else
            {
                await DoAnalyzeInternal(folderPath);
            }
        }

        private void LoadAnalysisFromCache(AnalysisCache cache)
        {
            FilesFound.Clear();
            deduplicatedFiles.Clear();
            lv.Items.Clear();
            btnReopenReport.Visible = false;

            // Reconstruir FilesFound desde caché
            foreach (var fileData in cache.AllFiles)
            {
                try
                {
                    if (File.Exists(fileData.FullPath))
                    {
                        var fileInfo = new FileInfo(fileData.FullPath);
                        string normalizedPath = NormalizePath(fileData.FullPath);
                        FilesFound[normalizedPath] = fileInfo;
                    }
                }
                catch { }
            }

            // Reconstruir ListView desde caché
            foreach (var category in CategoryOrder)
            {
                if (cache.Categories.ContainsKey(category))
                {
                    var catData = cache.Categories[category];
                    string translatedName = Localization.GetFolderName(category);

                    var it = new ListViewItem("");
                    it.Checked = true;
                    it.Tag = category;
                    it.SubItems.Add(translatedName);
                    it.SubItems.Add(catData.FileCount.ToString());
                    it.SubItems.Add(ToSize(catData.TotalSize));
                    it.SubItems.Add(Localization.Get("BTN_VIEW_DETAILS"));

                    it.UseItemStyleForSubItems = false;
                    it.SubItems[4].ForeColor = Color.Cyan;
                    it.SubItems[4].Font = new Font(lv.Font, FontStyle.Bold);

                    lv.Items.Add(it);
                }
            }

            deduplicatedFiles = new List<FileInfo>(FilesFound.Values);
            pBar.Value = 100;
            
            // Restaurar información de duplicados si aplica
            if (cache.IsDuplicateAnalysis && cache.DuplicateGroups.Count > 0)
            {
                lastDuplicateGroups = new List<DuplicatesReportForm.DuplicateGroupInfo>();
                foreach (var dupInfo in cache.DuplicateGroups)
                {
                    var fileList = new List<FileInfo>();
                    foreach (var filePath in dupInfo.FilePaths)
                    {
                        try
                        {
                            if (File.Exists(filePath))
                            {
                                fileList.Add(new FileInfo(filePath));
                            }
                        }
                        catch { }
                    }
                    
                    if (fileList.Count > 0)
                    {
                        lastDuplicateGroups.Add(new DuplicatesReportForm.DuplicateGroupInfo
                        {
                            Hash = dupInfo.Hash,
                            FileSize = dupInfo.FileSize,
                            Files = fileList
                        });
                    }
                }
                
                if (lastDuplicateGroups.Count > 0)
                {
                    btnReopenReport.Visible = true;
                }
            }
            
            lblStatus.Text = string.Format(Localization.Get("CACHE_LOADED_STATUS"), cache.AnalysisDate) + $" - {FilesFound.Count} " + Localization.Get("COL_FILES").ToLower();
        }

        private async Task DoAnalyzeInternal(string folderPath)
        {
            if (!Directory.Exists(folderPath)) return;
            
            FilesFound.Clear();
            deduplicatedFiles.Clear();
            lv.Items.Clear();
            btnReopenReport.Visible = false;
            lblStatus.Text = Localization.Get("STATUS_ANALYZING");
            pBar.Style = ProgressBarStyle.Continuous;
            pBar.Value = 0;

            cancellationTokenSource = new CancellationTokenSource();
            DisableAllControls();

            try
            {
                var countState = new ScanState { Count = 0 };
                await Task.Run(() => CountFiles(new DirectoryInfo(folderPath), countState));
                if (cancellationTokenSource.Token.IsCancellationRequested) return;
                int totalFiles = countState.Count;

                var scanState = new ScanState { Count = 0 };
                await Task.Run(() => DeepScanWithProgress(new DirectoryInfo(folderPath), totalFiles, scanState));
                if (cancellationTokenSource.Token.IsCancellationRequested) return;

                var groups = FilesFound.Values.GroupBy(f => GetFolder(f.Extension.ToLower()))
                                       .OrderBy(g => {
                                           int index = CategoryOrder.IndexOf(g.Key);
                                           return index == -1 ? 999 : index;
                                       });

                foreach (var g in groups)
                {
                    string categoryKey = g.Key;
                    string translatedName = Localization.GetFolderName(categoryKey);

                    var it = new ListViewItem("");
                    it.Checked = true;
                    it.Tag = categoryKey;
                    it.SubItems.Add(translatedName);
                    it.SubItems.Add(g.Count().ToString());
                    it.SubItems.Add(ToSize(g.Sum(f => f.Length)));
                    it.SubItems.Add(Localization.Get("BTN_VIEW_DETAILS"));

                    it.UseItemStyleForSubItems = false;
                    it.SubItems[4].ForeColor = Color.Cyan;
                    it.SubItems[4].Font = new Font(lv.Font, FontStyle.Bold);

                    lv.Items.Add(it);
                }

                pBar.Value = 100;
                deduplicatedFiles = new List<FileInfo>(FilesFound.Values);
                lblStatus.Text = string.Format(Localization.Get("STATUS_ANALYSIS_COMPLETE"), FilesFound.Count);

                // ✅ Guardar en caché
                await Task.Run(() => SaveAnalysisToCache(folderPath, FilesFound.Values.ToList()));
            }
            finally
            {
                EnableAllControls();
            }
        }

        private void SaveAnalysisToCache(string folderPath, List<FileInfo> files)
        {
            try
            {
                var cache = new AnalysisCache
                {
                    FolderPath = folderPath,
                    AnalysisDate = DateTime.Now,
                    ContentHash = CacheManager.GetFolderContentHash(folderPath, Blacklist),
                    TotalFiles = files.Count,
                    TotalSize = files.Sum(f => f.Length),
                    IsDuplicateAnalysis = lastDuplicateGroups.Count > 0  // Marcar si hay duplicados
                };

                // Agrupar por categoría
                foreach (var group in files.GroupBy(f => GetFolder(f.Extension.ToLower())))
                {
                    cache.Categories[group.Key] = new CategoryData
                    {
                        CategoryKey = group.Key,
                        FileCount = group.Count(),
                        TotalSize = group.Sum(f => f.Length)
                    };
                }

                // Guardar lista de archivos
                foreach (var file in files)
                {
                    cache.AllFiles.Add(new FileData
                    {
                        Name = file.Name,
                        FullPath = file.FullName,
                        Size = file.Length,
                        Extension = file.Extension,
                        LastWriteTime = file.LastWriteTime,
                        CreationTime = file.CreationTime
                    });
                }

                // Guardar información de duplicados si aplica
                if (lastDuplicateGroups.Count > 0)
                {
                    foreach (var dupGroup in lastDuplicateGroups)
                    {
                        cache.DuplicateGroups.Add(new Models.DuplicateGroupInfo
                        {
                            Hash = dupGroup.Hash,
                            FileSize = dupGroup.FileSize,
                            FilePaths = dupGroup.Files.Select(f => f.FullName).ToList()
                        });
                    }
                }

                CacheManager.SaveAnalysisToCache(cache);
            }
            catch { }
        }
    }
}

