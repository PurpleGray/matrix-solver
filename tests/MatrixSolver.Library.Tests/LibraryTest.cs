using MatrixSolver.Library.IO;
using MatrixSolver.Library.Utils;
using MatrixSolver.TestData;
using Xunit;

namespace MatrixSolver.Library.Tests
{
    public class LibraryTest
    {
        [Fact]
        public void LibraryWorkTest()
        {
            // Put test files in directory
            var mtxDirectory = FileSystemHelper.WorkWithFolder(IOTests.MatrixesTestFolderPath);
            mtxDirectory.Clear();
            
            // Write each test file in folder
            foreach (var testFile in MatrixesTestData.TestFiles)
            {
                mtxDirectory.ForFile(testFile.Key).WriteLines(testFile.Value);
            }
            
            // Then test process each file
            foreach (var matrixFile in mtxDirectory.EnumerateFiles())
            {
                MatrixHelper.ProcessMatrixFile(matrixFile);
            }
        }
    }
}
