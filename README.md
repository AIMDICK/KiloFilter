

# KiloFilter

A modern utility to organize and rescue files by type, with custom categories and multilingual support.

## 📸 Screenshot

![KiloFilter Screenshot](Assets/screenshot.png)

KiloFilter features a clean interface for scanning, categorizing, and rescuing files from messy folders. Easily customize categories, set minimum file sizes, and exclude unwanted file types.

## ✨ Key Features

| Feature | Description |
|---------|-------------|
| 📂 Auto classification | Scans folders and sorts files by type (images, videos, docs, etc.) |
| 🏷️ Custom categories | Create your own file groups and add extensions |
| 🚫 Blacklist | Exclude file types from analysis and rescue |
| 📏 Min file size | Ignore small/temporary files by setting size limits per extension |
| 🌐 Multilingual UI | Switch between languages instantly |
| 🗃️ Safe copy | Files are copied to organized folders, originals remain untouched |

## 🧠 The Logic

KiloFilter scans the selected source folder, analyzes all files, and groups them by extension and category. You can customize categories, blacklist extensions, and set minimum file sizes. When rescuing, files are copied to a new destination folder, organized by category.

## 📥 Installation

• Portable app: No installation required. Download or build and run `KiloFilter.exe`.
• Requirement: [.NET 8 Runtime](https://dotnet.microsoft.com/download/dotnet/8.0) (Windows).

Build from source:

```sh
git clone https://github.com/AIMDICK/KiloFilter.git
cd KiloFilter
dotnet publish -c Release -r win-x64 --self-contained false -o publish
```
The executable will be at `publish\KiloFilter.exe`.

## 🚀 How to Use

1. Source Folder — Select the folder with your files.
2. Analyze — Scan and auto-classify files.
3. Configure — Adjust categories, blacklist, and min file sizes.
4. Destination Folder — Choose where rescued files will be copied.
5. Rescue — Copy selected files to organized folders.

See the in-app help for full instructions in multiple languages.

## ⚠️ Disclaimer

Use at your own risk. The author is not responsible for any data loss. Always verify destination paths and keep backups of important files.

## 🛠 Tech Stack

• C# · .NET 8 · Windows Forms

## 📄 License

This project is licensed under the MIT License. See the LICENSE file for details.

## Author

- AIMDICK ([GitHub](https://github.com/AIMDICK))
