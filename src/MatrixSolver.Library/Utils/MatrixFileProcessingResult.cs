using System.Collections.Generic;
using MatrixSolver.Library.IO;
using MatrixSolver.Library.Math.LinearAlgebra.Matrix;

namespace MatrixSolver.Library.Utils
{
    public class MatrixFileProcessingResult
    {
        public FilePath ResultFile { get; }
        
        public IList<Matrix<int>> ResultMatrixes { get; }
        
        public bool Success { get; }
        
        public string ErrorMessage { get; }

        public MatrixFileProcessingResult(FilePath resultFile, IList<Matrix<int>> resMatrixes,
            bool success, string errorMessage)
        {
            ResultFile = resultFile;
            ResultMatrixes = resMatrixes;
            Success = success;
            ErrorMessage = errorMessage;
        }
    }
}
