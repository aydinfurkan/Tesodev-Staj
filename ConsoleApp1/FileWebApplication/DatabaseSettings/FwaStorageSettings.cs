using System.Collections.Generic;

namespace FileWebApplication.DatabaseSettings
{
    public class FwaStorageSettings
    {
        public string BaseFolderName { get; set; }
        
        public Dictionary<string, List<string>> FolderNames { get; set; }
        
        public string DefaultFolderName { get; set; }
    }
}