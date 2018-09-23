using System;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix.Integer
{
    public class Matrix : Matrix<int>
    {
        public Matrix(MatrixDataContainer<int> storage) : base(storage)
        {
        }

        protected override Matrix<int> Add(Matrix<int> other)
        {
            if (other.RowCount != RowCount || other.ColumnCount != ColumnCount)
            {
                throw new ArgumentOutOfRangeException(
                    "Error while trying to Add two matrixes, dimensions are not the same " +
                    $"({RowCount} x {ColumnCount}, " +
                    $"{other.RowCount} x {other.ColumnCount})");
            }

            var result = Build.FromDimensions(RowCount, ColumnCount);

            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    result[i, j] = this[i, j] + other[i, j];
                }
            }

            return result;
        }

        protected override Matrix<int> Subtract(Matrix<int> other)
        {
            if (other.RowCount != RowCount || other.ColumnCount != ColumnCount)
            {
                throw new ArgumentOutOfRangeException(
                    "Error while trying to Add two matrixes, dimensions are not the same " +
                    $"({RowCount} x {ColumnCount}, " +
                    $"{other.RowCount} x {other.ColumnCount})");
            }

            var result = Build.FromDimensions(RowCount, ColumnCount);

            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    result[i, j] = this[i, j] - other[i, j];
                }
            }

            return result;
        }

        protected override Matrix<int> Multiply(Matrix<int> other)
        {
            if (ColumnCount != other.RowCount)
            {
                throw new ArgumentOutOfRangeException(
                    "Error while trying to Add two matrixes, dimensions are not compatible " +
                    $"({RowCount} x {ColumnCount}, " +
                    $"{other.RowCount} x {other.ColumnCount})");
            }

            var resultMatrix = Build.FromDimensions(RowCount, other.ColumnCount);
            for (var i = 0; i < resultMatrix.RowCount; i++)
            {
                for (var j = 0; j < resultMatrix.ColumnCount; j++)
                {
                    resultMatrix[i, j] = 0;
                    for (var k = 0; k < ColumnCount; k++)
                    {
                        resultMatrix[i, j] += this[i, k] * other[k, j];
                    }
                }
            }

            return resultMatrix;
        }
    }
}
