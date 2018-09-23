using System;
using System.IO;
using System.Linq;
using MatrixSolver.Library.IO;
using MatrixSolver.TestData;
using Xunit;

namespace MatrixSolver.Library.Tests
{
    public class IOTests
    {
        public static readonly string MatrixesTestFolderPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MatrixesIOTest");

        [Fact]
        public void WriteOnDiskTest()
        {
            // Create and clear folder for matrixes
            var mtxFolder = FileSystemHelper.WorkWithFolder(MatrixesTestFolderPath);
            mtxFolder.Clear();

            // Write each test file in folder
            foreach (var testFile in MatrixesTestData.TestFiles)
            {
                mtxFolder.ForFile(testFile.Key).WriteLines(testFile.Value);
            }

            // Check that all files has been written
            var writtenFiles = mtxFolder.EnumerateFiles();
            Assert.True(writtenFiles.All(file =>
                MatrixesTestData.TestFiles.Any(pair => pair.Key == file.PathItemName)));

            bool CheckFilesContent()
            {
                foreach (var file in writtenFiles)
                {
                    var fileData = file.ReadAllText().Trim();
                    var templateData = string.Join("\r\n", MatrixesTestData.TestFiles[file.PathItemName]);
                    if (!fileData.Equals(templateData))
                    {
                        return false;
                    }
                }

                return true;
            }

            // Check that content of these files is correct
            Assert.True(CheckFilesContent());
        }
    }
}
