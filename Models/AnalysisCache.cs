using System;
using System.Collections.Generic;

namespace KiloFilter.Models
{
    /// <summary>
    /// Representa un análisis cacheado de una carpeta
    /// </summary>
    public class AnalysisCache
    {
        public string FolderPath { get; set; } = "";
        public DateTime AnalysisDate { get; set; }
        public string ContentHash { get; set; } = "";  // Hash de todos los archivos para detectar cambios
        public int TotalFiles { get; set; }
        public long TotalSize { get; set; }
        public bool IsDuplicateAnalysis { get; set; } = false;  // Indica si fue análisis de duplicados
        public Dictionary<string, CategoryData> Categories { get; set; } = new Dictionary<string, CategoryData>();
        public List<FileData> AllFiles { get; set; } = new List<FileData>();
        public List<DuplicateGroupInfo> DuplicateGroups { get; set; } = new List<DuplicateGroupInfo>();
    }

    /// <summary>
    /// Información de un grupo de duplicados
    /// </summary>
    public class DuplicateGroupInfo
    {
        public string Hash { get; set; } = "";
        public long FileSize { get; set; }
        public List<string> FilePaths { get; set; } = new List<string>();  // Rutas de archivos duplicados
    }

    /// <summary>
    /// Datos de una categoría en caché
    /// </summary>
    public class CategoryData
    {
        public string CategoryKey { get; set; } = "";
        public int FileCount { get; set; }
        public long TotalSize { get; set; }
    }

    /// <summary>
    /// Datos de un archivo en caché
    /// </summary>
    public class FileData
    {
        public string Name { get; set; } = "";
        public string FullPath { get; set; } = "";
        public long Size { get; set; }
        public string Extension { get; set; } = "";
        public DateTime LastWriteTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
