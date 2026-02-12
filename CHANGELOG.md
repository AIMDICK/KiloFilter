# Changelog - KiloFilter

## [v2.1.0] - 2026-02-11

### ✨ Smart Duplicate Deletion
- **Advanced deletion strategies** for intelligent duplicate management:
  - Keep Newest: Deletes older duplicates, keeps latest modified
  - Keep Oldest: Preserves original files, deletes newer copies
  - Keep Smallest: Optimizes storage by keeping smallest version
- **Real-time preview**: Preview shows which files will be deleted based on selected strategy
- **Automatic filtering**: Search by filename, filter by size (min/max)
- **Interactive control**: Apply filters and see results instantly

### 🎨 UI Polish
- **Removed redundant Preview button**: Eliminated unnecessary "Preview Delete" action
- **Button emoji consistency**: Added emojis to all action buttons for better visual hierarchy
  - 📂 Browse, Move
  - 🔍 Analyze, Filter
  - 💾 Rescue, Save
  - ✓ Apply, Check, Accept
  - ❌ Remove, Cancel, Block
  - ➕ Add
- **Improved button layout**: Better spacing and alignment in duplicate report form

### 🔧 Technical Improvements
- **SmartDuplicateDeleter class**: New core logic for intelligent deletion strategies
- **Real-time filtering**: Duplicate preview updates instantly as filters change
- **Size-based selection**: Support for min/max file size filtering in duplicate removal
- **Strategy selection dropdown**: Easy choice between deletion strategies

### 📝 Localization
- All new features fully translated (6 languages):
  - Delete strategy options
  - Filter labels and tooltips
  - Smart delete button with emojis

---

## [v2.0.0] - 2024-12-19

### ✨ Major Features
- **Smart Cache System**: Revolutionary performance improvement for repeated folder analysis
  - Automatically saves analysis results for all previously scanned folders
  - Intelligent change detection using cryptographic hashing detects if folder contents have changed
  - Load previous analysis results in <1 second instead of waiting minutes for a full rescan
  - Perfect for users who regularly analyze the same large directories

### 🎨 UI Enhancements
- **📋 History Browser**: New dedicated window to manage cached analyses
  - View all analysis history with folder path, analysis date, and file count
  - Load any previous analysis instantly to restore categorization and statistics
  - Individual delete: Remove specific cache entries to free up space
  - Clear all: Remove all cached analyses at once
  - Supports all 6 languages (English, Spanish, French, German, Italian, Japanese)

### 🔧 Technical Features
- **Content-based cache key**: Uses BLAKE3 hash of folder contents to detect changes
- **Persistent storage**: Cache stored in `%AppData%\Roaming\KiloFilter\Cache` for user accessibility
- **Auto-cleanup**: Automatically removes cache entries older than 30 days on application startup
- **JSON serialization**: Human-readable cache format for transparency and debugging
- **Non-blocking operations**: Cache cleanup runs as background task during startup

### 📝 Documentation
- **Help system**: Updated with complete cache system explanation in all 6 languages
- **README**: Added new "Performance: Smart Cache System" section with usage guide
- **User experience**: Intelligent prompts showing whether folder content changed since last analysis

### 🐛 Improvements
- Categories now display on application startup (visible before any analysis)
- Improved scroll bar visibility in detailed analysis and duplicate report windows
- Added missing application icon to DuplicatesReportForm

### ✅ Localization
All new features fully translated and tested in:
- 🇬🇧 English
- 🇪🇸 Spanish (Español)
- 🇫🇷 French (Français)
- 🇩🇪 German (Deutsch)
- 🇮🇹 Italian (Italiano)
- 🇯🇵 Japanese (日本語)

---

## [v1.1.0] - 2026-02-10


### 🐛 Fixes
- **Fixed duplicate file handling in Rescue operation**: The program was copying all duplicate files instead of filtering them out. Now when using "Analyze Duplicates" followed by "Rescue", only one copy of each duplicated file is copied to the destination folder.
- **Fixed file count discrepancy**: The number of files shown in the analysis results now matches the exact number of files that will be copied during the Rescue operation.
- **Improved Analyze Duplicates workflow**: The "Analyze Duplicates" feature now properly identifies duplicate files by:
  - Grouping files by size
  - Calculating partial hashes (first 64KB) for fast comparison
  - Computing full hashes only for files with matching partial hashes
  - Keeping only one file from each duplicate group

### ✨ Features
- Added deduplication logic to support both analysis modes:
  - **Analyze (Normal)**: Shows all files found, including duplicates
  - **Analyze Duplicates**: Shows only unique files (duplicate copies are hidden)
- Rescue operation now respects the selected analysis mode
- When using "Analyze Duplicates" + "Rescue", storage space is optimized by eliminating redundant copies

### 🔧 Technical Changes
- Added `deduplicatedFiles` class variable to track unique files separately from `FilesFound`
- Modified `DoAnalyzeDuplicates()` to populate the deduplication list with filtered results
- Updated `DoRescue()` to use `deduplicatedFiles` instead of `FilesFound` for copying operations
- Enhanced duplicate detection using BLAKE3 hashing algorithm for reliable file comparison

### 📝 Notes
- **Backwards Compatible**: Existing functionality is preserved. Users can still use the normal "Analyze" mode to copy all files including duplicates if desired.
- **Performance**: Full hash calculation only occurs for files with matching partial hashes, optimizing performance on large datasets.
- **Supported Hash Algorithm**: BLAKE3 for fast and secure file comparison

### 🎯 Recommended Usage
1. Use **"Analyze Duplicates"** to identify and remove duplicate files while preserving one copy
2. Use **"Analyze"** (normal analysis) if you want to copy all files including duplicates
3. Always review the file count before clicking "Rescue" to ensure the correct number matches your expectations

---

## Reporting Issues
If you encounter any issues with duplicate file detection or the Rescue operation, please provide:
- Number of files found vs. number of files copied
- File types involved
- Total size of the source folder
