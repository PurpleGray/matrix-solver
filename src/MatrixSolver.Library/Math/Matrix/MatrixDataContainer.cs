using System;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix
{
    public abstract class MatrixDataContainer<T> where T : struct, IEquatable<T>, IFormattable
    {
        public readonly int RowCount;

        public readonly int ColumnCount;

        protected MatrixDataContainer(int rowCount, int columnCount)
        {
            if (rowCount <= 0)
            {
                throw new ArgumentOutOfRangeException("RowCount should be positive number");
            }

            if (columnCount <= 0)
            {
                throw new ArgumentOutOfRangeException("ColumnCount should be positive number");
            }

            RowCount = rowCount;
            ColumnCount = columnCount;
        }
        
        public abstract T GetAt(int row, int column);
        
        public abstract T SetAt(int row, int column, T value);
        
        public T this[int row, int column]
        {
            get
            {
                ValidateRange(row, column);
                return GetAt(row, column);
            }

            set
            {
                ValidateRange(row, column);
                SetAt(row, column, value);
            }
        }

        public virtual void Clear()
        {
            throw new NotImplementedException();
        }

        public virtual T[,] ToArray()
        {
            throw new NotImplementedException();
        }
        
        private void ValidateRange(int row, int column)
        {
            if (row < 0 || row >= RowCount)
            {
                throw new ArgumentOutOfRangeException("row");
            }

            if (column < 0 || column >= ColumnCount)
            {
                throw new ArgumentOutOfRangeException("column");
            }
        }
    }
}
