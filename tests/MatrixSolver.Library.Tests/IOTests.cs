using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using MatrixSolver.Library.IO;
using MatrixSolver.TestData;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;

namespace MatrixSolver.Library.Tests
{
    public class IOTests
    {
        private static readonly string BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Matrixes");
        
        [Fact]
        public void WriteOnDiskTest()
        {
            // Create and clear folder for matrixes
            var mtxFolder = FileSystemHelper.WorkWithFolder(BasePath);
            mtxFolder.Clear();

            // Write each test file in folder
            foreach (var testFile in MatrixesTestData.TestFiles)
            {
                mtxFolder.ForFile(testFile.Key).WriteLines(testFile.Value);
            }

            // Check that all files has been written
            var writtenFiles = mtxFolder.EnumerateFiles();
            Assert.True(writtenFiles.All(file_name => MatrixesTestData.TestFiles.Any(pair => pair.Key == Path.GetFileName(file_name))));

            bool CheckFilesContent()
            {
                foreach (var file in writtenFiles)
                {
                    var fileData = File.ReadAllText(file).Trim();
                    var templateData = string.Join("\r\n", MatrixesTestData.TestFiles[Path.GetFileName(file)]);
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
