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
            var file = File.Create(FSPath);
            file.Close();
        }

        public void WriteToFile(string content)
        {
            File.WriteAllText(FSPath, content);
        }
    }
}
