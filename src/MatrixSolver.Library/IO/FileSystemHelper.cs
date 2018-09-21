using System;
using System.ComponentModel;
using System.IO;
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
            
            FileAttributes attr = File.GetAttributes(path);

            if (attr.HasFlag(FileAttributes.Directory))
            {
                return FSItemType.Directory;
            }
            else
            {
                return FSItemType.File;
            }
        }
    }
}
