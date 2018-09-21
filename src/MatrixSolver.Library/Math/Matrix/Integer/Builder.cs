using System;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix.Integer
{
    public class MatrixBuilder : MatrixBuilder<int>
    {
        public override int Zero { get; } = 0;

        public override Matrix<int> FromArray(int[,] arr)
        {
            return new Matrix(MatrixDataContainer.OfArray(arr));
        }

        public override Matrix<int> FromFormattedString(string rawString)
        {
            // Delimit string by rows
            var strRows = rawString.Split(new[] {'\r', '\n'});
            // Get matrix sizes
            var rowCount = strRows.Length;
            var colCount = strRows[0].Trim().Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries).Length;

            var matrixContainer = MatrixDataContainer.OfSize(rowCount, colCount);
            
            // Fill the matrix storage
            for (var i = 0; i < strRows.Length; i++)
            {
                var columnValues =
                    strRows[i].Trim().Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

                for (var j = 0; j < columnValues.Length; j++)
                {
                    matrixContainer.SetAt(i, j, int.Parse(columnValues[j]));
                }
            }
            
            return new Matrix(matrixContainer);
        }

        public override Matrix<int> FromDimensions(int rowCount, int columnCount)
        {
            return new Integer.Matrix(MatrixDataContainer.OfSize(rowCount, columnCount));
        }

        public override Matrix<int> SameAs(Matrix<int> matrix)
        {
            return new Integer.Matrix(new MatrixDataContainer(matrix.RowCount, matrix.ColumnCount));
        }
    }
}
