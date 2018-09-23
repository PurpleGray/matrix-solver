using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MatrixSolver.Library.IO;
using MatrixSolver.Library.Math.LinearAlgebra.Matrix;

namespace MatrixSolver.Library.Utils
{
    public static class MatrixHelper
    {
        private static readonly Dictionary<string, MatrixOperation> MtxOperations =
            new Dictionary<string, MatrixOperation>
            {
                {"add", MatrixOperation.Add}, {"subtract", MatrixOperation.Subtract},
                {"multiply", MatrixOperation.Multiply}, {"transpose", MatrixOperation.Transpose}
            };

        public static MatrixFileProcessingResult ProcessMatrixFile(
            this FilePath matrixFile)
        {
            // Generate result file name
            var resultFileName = $"{Path.GetFileNameWithoutExtension(matrixFile.FSPath)}_result.txt";
            // At first we will store result file in Results folder
            var resultFolder = FileSystemHelper.WorkWithFolder(Path.Combine(matrixFile.DirectoryPath,
                "Results"));
            // Result file
            var resultFile = new FilePath(Path.Combine(resultFolder.FSPath, resultFileName));

            try
            {
                var fileLines = matrixFile.ReadAllLines();
                // Determine operation we should perform on matrixes
                var mtxOperation = MtxOperations[fileLines[0].ToLower().Trim()];
                
                var rawMatrixBufer = new List<string>();
                // Matrixes stored in file
                var matrixes = new List<Matrix<int>>();

                for (var i = 2; i < fileLines.Length; i++)
                {
                    if (string.IsNullOrEmpty(fileLines[i].Trim()))
                    {
                        // Add to the storage another one
                        matrixes.Add(Matrix<int>.Build
                            .FromFormattedString(rawMatrixBufer.Aggregate((s, s1) => $"{s}\r\n{s1}")));
                        rawMatrixBufer.Clear();
                    }

                    rawMatrixBufer.Add(fileLines[i]);
                }

                // If encountered EOF, but last matrix was not added to matrixes collection
                if (rawMatrixBufer.Any())
                {
                    matrixes.Add(Matrix<int>.Build
                        .FromFormattedString(rawMatrixBufer.Aggregate((s, s1) => $"{s}\r\n{s1}")));
                }

                var outMatrixes = new List<Matrix<int>>();

                // Perform calculations
                switch (mtxOperation)
                {
                    case MatrixOperation.Add:
                    {
                        var resultMtx = matrixes.Aggregate((mtx1, mtx2) => mtx1 + mtx2);
                        outMatrixes.Add(resultMtx);
                        resultFile.WriteToFile(resultMtx.ToString());
                        break;
                    }
                    case MatrixOperation.Subtract:
                    {
                        var resultMtx = matrixes.Aggregate((mtx1, mtx2) => mtx1 - mtx2);
                        outMatrixes.Add(resultMtx);
                        resultFile.WriteToFile(resultMtx.ToString());
                        break;
                    }
                    case MatrixOperation.Multiply:
                    {
                        var resultMtx = matrixes.Aggregate((mtx1, mtx2) => mtx1 * mtx2);
                        outMatrixes.Add(resultMtx);
                        resultFile.WriteToFile(resultMtx.ToString());
                        break;
                    }
                    case MatrixOperation.Transpose:
                        var resultMtxs = matrixes.Select(mtx => mtx.Transpose());
                        outMatrixes.AddRange(resultMtxs);
                        resultFile.WriteToFile(resultMtxs.Select(mtx => mtx.ToString())
                            .Aggregate((mtx1, mtx2) => $"{mtx1}\r\n{mtx2}"));
                        break;
                }

                return new MatrixFileProcessingResult(resultFile, outMatrixes, true, string.Empty);
            }
            catch (Exception ex)
            {
                var errMessage = $"Error happened while processing {matrixFile.PathItemName}\r\n" +
                                 $"Error message: {ex.Message}";
                return new MatrixFileProcessingResult(resultFile, null, false, errMessage);
            }
        }
    }
}
