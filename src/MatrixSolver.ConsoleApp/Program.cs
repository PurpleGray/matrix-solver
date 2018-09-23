using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using MatrixSolver.Library.IO;
using MatrixSolver.Library.Utils;

namespace MatrixSolver.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No directory to work with is being provided\r\n");
                Console.ReadLine();
                Environment.Exit(0);
            }

            var directoryPath = args[0];
            var mtxDir = FileSystemHelper.WorkWithFolder(directoryPath);

            CheckDirectoryValidity(mtxDir);
            
            var anyErrorsFlag = false;
            var problematicFiles = new List<string>();

            foreach (var matrixFile in mtxDir.EnumerateFiles("*.txt"))
            {
                var fileProcessingResult = MatrixHelper.ProcessMatrixFile(matrixFile);

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

            if (anyErrorsFlag)
            {
                Console.WriteLine("Program completed calculations with errors for some files. List of these files:\r\n");
                if (problematicFiles.Count > 1)
                {
                    StringBuilder fileNamesBuilder = new StringBuilder();

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
                    "where original matrixes was (with _result.txt postfix)");
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
                Console.WriteLine("Provided directory path containts no .txt files");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}
