using System.IO;

namespace MatrixSolver.Library.IO
{
    public class FilePath : PathBase
    {
        public FilePath(string path) : base(path)
        {
        }

        public override bool IsExists()
        {
            return File.Exists(FSPath);
        }

        protected override void CreateOnDisk()
        {
            File.Create(FSPath);
        }
    }
}
