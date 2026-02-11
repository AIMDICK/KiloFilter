using System;
using System.Collections.Generic;

namespace KiloFilter.Models
{
    public class DuplicateGroup
    {
        public string FileHash { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public List<DuplicateFile> Files { get; set; } = new List<DuplicateFile>();
    }

    public class DuplicateFile
    {
        public string FileName { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
    }
}
