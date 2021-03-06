using System.IO;

namespace MatrixSolver.Library.IO
{
    public class FilePath : PathBase
    {
        public FilePath(string path) : base(path)
        {
        }

        /// <summary>
        /// Path to directory where file is being stored
        /// </summary>
        public string DirectoryPath => Path.GetDirectoryName(FSPath);

        public override bool IsExists()
        {
            return File.Exists(FSPath);
        }

        protected override void CreateOnDisk()
        {
            var file = File.Create(FSPath);
            file.Close();
        }

        public override void Clear()
        {
            File.WriteAllText(FSPath, string.Empty);
        }

        public override void Delete()
        {
            File.Delete(FSPath);
        }

        public void WriteToFile(string content)
        {
            File.WriteAllText(FSPath, content);
        }

        public void WriteLines(string[] content)
        {
            File.WriteAllLines(FSPath, content);
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
