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

        public string ReadAllText()
        {
            return File.ReadAllText(FSPath);
        }

        public string[] ReadAllLines()
        {
            return File.ReadAllLines(FSPath);
        }
    }
}
