using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using MatrixSolver.Library.IO;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;

namespace MatrixSolver.Library.Tests
{
    public class IOTests
    {
        public static Dictionary<string, string[]> TestFiles = new Dictionary<string, string[]>()
        {
            { "1.txt", TestData.TestData.ReadAllLines("1.txt")},
            { "2.txt", TestData.TestData.ReadAllLines("2.txt")},
            { "3.txt", TestData.TestData.ReadAllLines("3.txt")},
            { "4.txt", TestData.TestData.ReadAllLines("4.txt")},
        };

        private static readonly string BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Matrixes");
        
        [Fact]
        public void WriteOnDiskTest()
        {
            // Create and clear folder for matrixes
            var mtxFolder = FileSystemHelper.WorkWithFolder(BasePath);
            mtxFolder.Clear();

            // Write each test file in folder
            foreach (var testFile in TestFiles)
            {
                mtxFolder.ForFile(testFile.Key).WriteLines(testFile.Value);
            }

            // Check that all files has been written
            var writtenFiles = mtxFolder.EnumerateFiles();
            Assert.True(writtenFiles.All(file_name => TestFiles.Any(pair => pair.Key == Path.GetFileName(file_name))));

            bool CheckFilesContent()
            {
                foreach (var file in writtenFiles)
                {
                    var fileData = File.ReadAllText(file).Trim();
                    var templateData = string.Join("\r\n", TestFiles[Path.GetFileName(file)]);
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
