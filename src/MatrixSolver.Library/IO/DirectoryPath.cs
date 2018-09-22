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
        public string DirectoryName => System.IO.Path.GetDirectoryName(FSPath);
        
        protected override void CreateOnDisk()
        {
            Directory.CreateDirectory(FSPath);
        }
        
        public override bool IsExists()
        {
            return Directory.Exists(FSPath);
        }

        public IEnumerable<string> EnumerateFiles()
        {
            return Directory.EnumerateFiles(FSPath);
        }

        public FilePath OpenFile(string name)
        {
            var filePath = new FilePath(Path.Combine(FSPath, name));

            if (filePath.IsExists())
            {
                return filePath;
            }
            
            throw new FileNotFoundException();
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