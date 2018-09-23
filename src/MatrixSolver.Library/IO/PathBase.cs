using System.IO;

namespace MatrixSolver.Library.IO
{
    public abstract class PathBase
    {
        protected PathBase(string path)
        {
            FSPath = path;
        }

        public string FSPath { get; }

        /// <summary>
        ///     The filename or folder name for the , including the extension.
        /// </summary>
        public string PathItemName => Path.GetFileName(FSPath);

        public abstract bool IsExists();

        protected abstract void CreateOnDisk();

        public abstract void Clear();

        public abstract void Delete();
    }
}
