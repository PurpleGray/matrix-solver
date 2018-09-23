using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MatrixSolver.Library.IO;
using MatrixSolver.Library.Utils;

namespace MatrixSolver.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string directoryPath = null;
            
            // If path is not provided in startup args
            // trying to get them from console itself
            if (args.Length == 0)
            {
                Console.WriteLine("No directory path to work with is being provided\r\n" +
                                  "Enter it in console then.");
                directoryPath = Console.ReadLine();
            }
            else
            {
                directoryPath = args[0];
            }

            var mtxDir = FileSystemHelper.WorkWithFolder(directoryPath);
            
            // Check if Directory is exists and contains needed files
            CheckDirectoryValidity(mtxDir);

            var anyErrorsFlag = false;
            var problematicFiles = new List<string>();

            // Process files and gather errors, if occurred
            foreach (var matrixFile in mtxDir.EnumerateFiles("*.txt"))
            {
                var fileProcessingResult = matrixFile.ProcessMatrixFile();

                if (!fileProcessingResult.Success)
                {
                    if (!anyErrorsFlag)
                    {
                        anyErrorsFlag = true;
                    }

                    problematicFiles.Add(matrixFile.PathItemName);

                    Console.WriteLine(fileProcessingResult.ErrorMessage);
                    try
                    {
                        // Try to write same message in *_result.txt file if can
                        fileProcessingResult.ResultFile.WriteToFile(fileProcessingResult.ErrorMessage);
                    }
                    catch
                    {
                    }
                }

                Console.WriteLine($"Calculation process for file {matrixFile.PathItemName} is completed successfully!");
            }

            // Move result files to the same folder
            // NOTE: this done because of performance reasons. At this time we are enumerating files in folder,
            // not just gather them all in memory and process. This can be optimized in future by multithreading
            var resultsDir = FileSystemHelper.WorkWithFolder(Path.Combine(directoryPath, "Results"));
            resultsDir.MoveAllFiles(mtxDir);
            resultsDir.Delete();

            // Show filenames with problems
            if (anyErrorsFlag)
            {
                Console.WriteLine(
                    "Program completed calculations with errors for some files. List of these files:\r\n");
                if (problematicFiles.Count > 1)
                {
                    var fileNamesBuilder = new StringBuilder();

                    for (var i = 0; i < problematicFiles.Count; i++)
                    {
                        fileNamesBuilder.Append($"{problematicFiles[i]}");
                        // 5 file names on each line
                        if (i % 5 == 0)
                        {
                            fileNamesBuilder.Append("\r\n");
                        }
                    }

                    Console.WriteLine(fileNamesBuilder.ToString());
                }
                else
                {
                    Console.WriteLine(problematicFiles.First());
                }
            }
            else
            {
                Console.WriteLine(
                    "Program completed calculations without errors, result can be found in the same folder " +
                    "were original matrices was (with _result.txt postfix)");
            }

            Console.ReadLine();
        }

        private static void CheckDirectoryValidity(DirectoryPath directory)
        {
            if (!directory.IsExists())
            {
                Console.WriteLine("Provided directory path is not found on disk");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (directory.IsEmpty())
            {
                Console.WriteLine("Provided directory path contains no files in it");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (!directory.IsFilesWithExtensionExists(".txt"))
            {
                Console.WriteLine("Provided directory path contains no .txt files");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}
