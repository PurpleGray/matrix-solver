using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;

namespace MatrixSolver.Library.IO
{
    public static class FileSystemHelper
    {
        public static DirectoryPath WorkWithFolder(string path)
        {
            if (IsDirectoryPath(path) == FSItemType.Directory)
            {
                 return (DirectoryPath)DirectoryPath.OpenOrCreate(path);
            }
                
            throw new ArgumentException($"Provided path is not a folder");
        }
        
        private static FSItemType IsDirectoryPath(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (Directory.Exists(path))
            {
                return FSItemType.Directory;
            }

            if (File.Exists(path))
            {
                return FSItemType.File;
            }
            
            // if has trailing slash then it's a directory
            if (new[] {Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar}.Any(x => path.EndsWith(x)))
                return FSItemType.Directory; // ends with slash

            // if has extension then its a file; directory otherwise
            return string.IsNullOrWhiteSpace(Path.GetExtension(path)) ? FSItemType.Directory : FSItemType.File;
        }
    }
}
