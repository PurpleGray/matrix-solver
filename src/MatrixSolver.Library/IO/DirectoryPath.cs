using System;
using System.Collections.Generic;
using System.IO;
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
        
        protected override bool CreateOnDisk()
        {
            if (!IsExists())
            {
                Directory.CreateDirectory(FSPath);
                return true;
            }

            return false;
        }
        
        public override bool IsExists()
        {
            return Directory.Exists(FSPath);
        }

        public static PathBase Create(string path)
        {
            var dir = new DirectoryPath(path);
            dir.CreateOnDisk();

            return dir;
        }
    }
}
