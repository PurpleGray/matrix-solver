using System.IO;

namespace MatrixSolver.Library.IO
{
    public abstract class PathBase
    {
        protected PathBase(string path)
        {
            FSPath = path;
        }

        /// <summary>
        /// Full path to file-system item
        /// </summary>
        public string FSPath { get; }

        /// <summary>
        ///     The filename or folder name for the , including the extension.
        /// </summary>
        public string PathItemName => Path.GetFileName(FSPath);

        /// <summary>
        /// Is exists on disk
        /// </summary>
        public abstract bool IsExists();

        /// <summary>
        /// Create it on disk
        /// </summary>
        protected abstract void CreateOnDisk();

        /// <summary>
        /// Clear file or folder
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Delete it from disk
        /// </summary>
        public abstract void Delete();
    }
}
