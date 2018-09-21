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

        /// <summary>
        /// The fully qualified path string for the first path in the collection.
        /// </summary>
        public string FullPath => System.IO.Path.GetFullPath(FSPath);

        public abstract bool IsExists();

        protected abstract void CreateOnDisk();
        
        protected PathBase(string path)
        {
            FSPath = path;
        }
    }
}
