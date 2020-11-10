using System.Collections.Generic;

namespace FileApi.DatabaseSettings
{
    public class FileStorageSettings
    {
        public string BaseFolderName { get; set; }
        
        public Dictionary<string, List<string>> FolderNames { get; set; }
        
        public string DefaultFolderName { get; set; }
    }
}