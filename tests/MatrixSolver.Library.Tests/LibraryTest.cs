using System;
using System.IO;
using MatrixSolver.Library.IO;
using MatrixSolver.Library.Utils;
using MatrixSolver.TestData;
using Xunit;

namespace MatrixSolver.Library.Tests
{
    public class LibraryTest
    {
        public static readonly string MatrixesTestFolderPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Matrixes");

        [Fact]
        public void LibraryWorkTest()
        {
            // Put test files in directory
            var mtxDirectory = FileSystemHelper.WorkWithFolder(MatrixesTestFolderPath);
            mtxDirectory.Clear();

            // Write each test file in folder
            foreach (var testFile in MatrixesTestData.TestFiles)
            {
                mtxDirectory.ForFile(testFile.Key).WriteLines(testFile.Value);
            }

            // Then test process each file
            foreach (var matrixFile in mtxDirectory.EnumerateFiles())
            {
                matrixFile.ProcessMatrixFile();
            }
        }
    }
}
