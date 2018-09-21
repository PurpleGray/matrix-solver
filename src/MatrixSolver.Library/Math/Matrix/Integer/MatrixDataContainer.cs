using System.Threading;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix.Integer
{
    public class MatrixDataContainer : MatrixDataContainer<int>
    {
        public readonly int[,] Data;

        public MatrixDataContainer(int rowCount, int columnCount)
            : base(rowCount, columnCount)
        {
            Data = new int[rowCount, columnCount];
        }

        public MatrixDataContainer(int rowCount, int columnCount, int[,] data)
            : base(rowCount, columnCount)
        {
            Data = data;
        }

        public override int Zero { get; } = 0;

        public override int GetAt(int row, int column)
        {
            ValidateRange(row, column);
            return Data[row, column];
        }

        public override void SetAt(int row, int column, int value)
        {
            ValidateRange(row, column);
            Data[row, column] = value;
        }
    }
}
