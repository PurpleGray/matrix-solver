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
            throw new NotImplementedException();
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
