using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KiloFilter.Core
{
    public static class SmartDuplicateDeleter
    {
        public enum DeletionStrategy
        {
            KeepNewest,
            KeepOldest,
            KeepSmallest
        }

        public class DeletionResult
        {
            public int DeletedCount { get; set; }
            public long SpaceFreed { get; set; }
            public List<string> DeletedFiles { get; set; } = new List<string>();
            public List<string> ErrorFiles { get; set; } = new List<string>();
        }

        /// <summary>
        /// Calcula qué archivos deben ser eliminados según la estrategia seleccionada
        /// </summary>
        public static List<FileInfo> GetFilesToDelete(List<FileInfo> duplicateFiles, DeletionStrategy strategy)
        {
            if (duplicateFiles.Count <= 1) return new List<FileInfo>();

            FileInfo keepFile = strategy switch
            {
                DeletionStrategy.KeepNewest => duplicateFiles.OrderByDescending(f => f.LastWriteTime).First(),
                DeletionStrategy.KeepOldest => duplicateFiles.OrderBy(f => f.LastWriteTime).First(),
                DeletionStrategy.KeepSmallest => duplicateFiles.OrderBy(f => f.Length).First(),
                _ => duplicateFiles.First()
            };

            return duplicateFiles.Where(f => f.FullName != keepFile.FullName).ToList();
        }

        /// <summary>
        /// Elimina archivos duplicados según la estrategia
        /// </summary>
        public static DeletionResult DeleteDuplicates(List<FileInfo> filesToDelete)
        {
            var result = new DeletionResult();

            foreach (var file in filesToDelete)
            {
                try
                {
                    if (File.Exists(file.FullName))
                    {
                        result.SpaceFreed += file.Length;
                        File.Delete(file.FullName);
                        result.DeletedFiles.Add(file.FullName);
                        result.DeletedCount++;
                    }
                }
                catch (Exception ex)
                {
                    result.ErrorFiles.Add($"{file.FullName} - {ex.Message}");
                }
            }

            return result;
        }

        /// <summary>
        /// Filtra duplicados según criterios
        /// </summary>
        public static List<FileInfo> FilterDuplicates(List<FileInfo> duplicates, 
            string nameFilter = "", long minSize = 0, long maxSize = long.MaxValue)
        {
            var filtered = duplicates.AsEnumerable();

            // Filtro por nombre
            if (!string.IsNullOrEmpty(nameFilter))
            {
                filtered = filtered.Where(f => f.Name.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // Filtro por tamaño
            filtered = filtered.Where(f => f.Length >= minSize && f.Length <= maxSize);

            return filtered.ToList();
        }

        /// <summary>
        /// Formatea el tamaño en bytes a formato legible
        /// </summary>
        public static string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
