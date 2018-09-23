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
            new Dictionary<string, MatrixOperation>()
            {
                {"add", MatrixOperation.Add}, {"subtract", MatrixOperation.Subtract},
                {"multiply", MatrixOperation.Multiply}, {"transpose", MatrixOperation.Transpose}
            };

        public static (FilePath resultFile, List<Matrix<int>> resultMatrixes) ProcessMatrixFile(
            this FilePath matrixFile)
        {
            (FilePath resultFile, List<Matrix<int>> resultMatrixes) outResult = (null, new List<Matrix<int>>());
            
            var resultFileName = $"{Path.GetFileNameWithoutExtension(matrixFile.FSPath)}_result.txt";
            outResult.resultFile = new FilePath(Path.Combine(matrixFile.DirectoryPath, resultFileName));

            var fileLines = matrixFile.ReadAllLines();
            var mtxOperation = MtxOperations[fileLines[0].ToLower().Trim()];
            var rawMatrixBufer = new List<string>();
            var matrixes = new List<Matrix<int>>();
            
            for (var i = 2; i < fileLines.Length; i++)
            {
                if (string.IsNullOrEmpty(fileLines[i].Trim()) || i + 1 == fileLines.Length)
                {
                    matrixes.Add(Matrix<int>.Build
                        .FromFormattedString(rawMatrixBufer.Aggregate((s, s1) => $"{s}\r\n{s1}")));
                    rawMatrixBufer.Clear();
                }
                rawMatrixBufer.Add(fileLines[i]);
            }

            switch (mtxOperation)
            {
                case MatrixOperation.Add:
                {
                    var resultMtx = matrixes.Aggregate((mtx1, mtx2) => mtx1 + mtx2);
                    outResult.resultMatrixes.Add(resultMtx);
                    outResult.resultFile.WriteToFile(resultMtx.ToString());
                    break;
                }
                case MatrixOperation.Subtract:
                {
                    var resultMtx = matrixes.Aggregate((mtx1, mtx2) => mtx1 - mtx2);
                    outResult.resultMatrixes.Add(resultMtx);
                    outResult.resultFile.WriteToFile(resultMtx.ToString());
                    break;
                }
                case MatrixOperation.Multiply:
                {
                    var resultMtx = matrixes.Aggregate((mtx1, mtx2) => mtx1 * mtx2);
                    outResult.resultMatrixes.Add(resultMtx);
                    outResult.resultFile.WriteToFile(resultMtx.ToString());
                    break;
                }
                case MatrixOperation.Transpose:
                    var resultMtxs = matrixes.Select(mtx => mtx.Transpose());
                    outResult.resultMatrixes.AddRange(resultMtxs);
                    outResult.resultFile.WriteToFile(resultMtxs.Select(mtx => mtx.ToString())
                        .Aggregate((mtx1, mtx2) => $"{mtx1}\r\n{mtx2}"));
                    break;
            }

            return outResult;
        }
    }
}
