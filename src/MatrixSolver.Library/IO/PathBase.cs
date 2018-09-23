using System;
using System.IO;
using System.Linq;
using System.Net;

namespace MatrixSolver.Library.IO
{
    public abstract class PathBase
    {
        public string FSPath { get; private set; }

        /// <summary>
        /// The filename or folder name for the , including the extension.
        /// </summary>
        public string PathItemName => System.IO.Path.GetFileName(FSPath);

        public abstract bool IsExists();

        protected abstract void CreateOnDisk();

        public abstract void Clear();

        public abstract void Delete();
        
        protected PathBase(string path)
        {
            FSPath = path;
        }
    }
}
