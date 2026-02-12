using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using KiloFilter.Models;
using Blake3;

namespace KiloFilter.Core
{
    /// <summary>
    /// Gestiona el cacheado de análisis para evitar análisis repetidos
    /// </summary>
    public class CacheManager
    {
        private static readonly string CachePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "KiloFilter",
            "Cache"
        );

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        static CacheManager()
        {
            // Crear carpeta de caché si no existe
            if (!Directory.Exists(CachePath))
            {
                Directory.CreateDirectory(CachePath);
            }
        }

        /// <summary>
        /// Obtener el hash SHA256 de todos los archivos en una carpeta
        /// </summary>
        public static string GetFolderContentHash(string folderPath, HashSet<string> blacklist)
        {
            try
            {
                var hasher = Blake3.Hasher.New();
                var files = Directory.EnumerateFiles(folderPath, "*", SearchOption.AllDirectories).ToList();
                
                foreach (var file in files.OrderBy(f => f))
                {
                    try
                    {
                        string ext = Path.GetExtension(file).ToLower();
                        if (blacklist.Contains(ext))
                            continue;
                            
                        var fileInfo = new FileInfo(file);
                        string fileMetadata = file + "|" + fileInfo.Length + "|" + fileInfo.LastWriteTimeUtc.Ticks;
                        byte[] metadataBytes = System.Text.Encoding.UTF8.GetBytes(fileMetadata);
                        hasher.Update(new ReadOnlySpan<byte>(metadataBytes));
                    }
                    catch { }
                }

                Blake3.Hash hash = hasher.Finalize();
                return hash.ToString().ToLower();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Obtener caché existente para una carpeta
        /// </summary>
        public static AnalysisCache? GetCachedAnalysis(string folderPath)
        {
            try
            {
                string cacheFile = GetCacheFilePath(folderPath);
                if (!File.Exists(cacheFile))
                    return null;

                string json = File.ReadAllText(cacheFile);
                return JsonSerializer.Deserialize<AnalysisCache>(json, JsonOptions);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Guardar análisis en caché
        /// </summary>
        public static void SaveAnalysisToCache(AnalysisCache cache)
        {
            try
            {
                string cacheFile = GetCacheFilePath(cache.FolderPath);
                string json = JsonSerializer.Serialize(cache, JsonOptions);
                File.WriteAllText(cacheFile, json);
            }
            catch { }
        }

        /// <summary>
        /// Eliminar caché de una carpeta
        /// </summary>
        public static void ClearCache(string folderPath)
        {
            try
            {
                string cacheFile = GetCacheFilePath(folderPath);
                if (File.Exists(cacheFile))
                    File.Delete(cacheFile);
            }
            catch { }
        }

        /// <summary>
        /// Obtener la ruta del archivo de caché para una carpeta
        /// </summary>
        private static string GetCacheFilePath(string folderPath)
        {
            // Crear nombre único basado en la ruta completa
            byte[] pathBytes = System.Text.Encoding.UTF8.GetBytes(folderPath.ToLower());
            var hash = Blake3.Hasher.Hash(new ReadOnlySpan<byte>(pathBytes));
            return Path.Combine(CachePath, $"{hash.ToString().Substring(0, 16)}.cache.json");
        }

        /// <summary>
        /// Obtener lista de análisis en caché
        /// </summary>
        public static List<(string path, DateTime date)> GetCachedAnalysisList()
        {
            var result = new List<(string, DateTime)>();
            try
            {
                if (!Directory.Exists(CachePath))
                    return result;

                foreach (var file in Directory.GetFiles(CachePath, "*.cache.json"))
                {
                    try
                    {
                        var cache = JsonSerializer.Deserialize<AnalysisCache>(File.ReadAllText(file), JsonOptions);
                        if (cache != null)
                        {
                            result.Add((cache.FolderPath, cache.AnalysisDate));
                        }
                    }
                    catch { }
                }
            }
            catch { }

            return result.OrderByDescending(x => x.Item2).ToList();
        }

        /// <summary>
        /// Limpiar caché antiguo (más de 30 días)
        /// </summary>
        public static void CleanOldCache(int daysToKeep = 30)
        {
            try
            {
                if (!Directory.Exists(CachePath))
                    return;

                var cutoffDate = DateTime.Now.AddDays(-daysToKeep);
                foreach (var file in Directory.GetFiles(CachePath, "*.cache.json"))
                {
                    if (File.GetLastWriteTime(file) < cutoffDate)
                    {
                        File.Delete(file);
                    }
                }
            }
            catch { }
        }
    }
}
