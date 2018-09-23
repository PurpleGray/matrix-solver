using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MatrixSolver.Library.IO
{
    public class DirectoryPath : PathBase
    {
        private DirectoryPath(string path) : base(path) {}

        /// <summary>
        /// The name of the directory for the path
        /// </summary>
        public string DirectoryName => new DirectoryInfo(FSPath).Name;
        
        protected override void CreateOnDisk()
        {
            Directory.CreateDirectory(FSPath);
        }

        public override void Clear()
        {
            foreach (var file in Directory.EnumerateFiles(FSPath))
            {
                File.Delete(Path.Combine(FSPath, file));
            }
        }

        public override bool IsExists()
        {
            return Directory.Exists(FSPath);
        }

        public bool IsFileExists(string name)
        {
            return File.Exists(Path.Combine(FSPath, name));
        }

        public void DeleteFile(string name)
        {
            File.Delete(Path.Combine(FSPath, name));
        }

        public IEnumerable<string> EnumerateFiles()
        {
            return Directory.EnumerateFiles(FSPath);
        }

        public FilePath ForFile(string name)
        {
            return new FilePath(Path.Combine(FSPath, name));
        }
        
        public static PathBase OpenOrCreate(string path)
        {
            var dir = new DirectoryPath(path);

            if (!dir.IsExists())
            {
                dir.CreateOnDisk();
            }

            return dir;
        }
    }
}
