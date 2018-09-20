using System;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix
{
    public abstract partial class Matrix<T> : IFormattable, IEquatable<Matrix<T>>
                                    where T : struct, IEquatable<T>, IFormattable, ICloneable
    {
        protected Matrix(MatrixDataContainer<T> storage)
        {
            Storage = storage;
            RowCount = storage.RowCount;
            ColumnCount = storage.ColumnCount;
        }
        
        public MatrixDataContainer<T> Storage { get; private set; }
        
        public int ColumnCount { get; private set; }
        
        public int RowCount { get; private set; }
        
        public T this[int row, int column]
        {
            get { return Storage[row, column]; }
            set { Storage[row, column] = value; }
        }
        
        public T GetAt(int row, int column)
        {
            return Storage.GetAt(row, column);
        }
        
        public void SetAt(int row, int column, T value)
        {
            Storage.SetAt(row, column, value);
        }
        
        public void Clear()
        {
            Storage.Clear();
        }
        
        public T[,] ToArray()
        {
            return Storage.ToArray();
        }

        protected abstract Matrix<T> Add(Matrix<T> rightMatrix);

        protected abstract Matrix<T> Subtract(Matrix<T> other);

        protected abstract Matrix<T> Multiply(Matrix<T> other);

        protected abstract Matrix<T> Transpose();
        
        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException(); // TODO
        }

        public bool Equals(Matrix<T> other)
        {
            throw new NotImplementedException(); // TODO
        }
    }
}
