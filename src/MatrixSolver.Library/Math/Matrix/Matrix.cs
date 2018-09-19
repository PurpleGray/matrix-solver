using System;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix
{
    public abstract class Matrix<T> : IFormattable, IEquatable<Matrix<T>>
                                    where T : struct, IEquatable<T>, IFormattable
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

        public Matrix<T> Append(Matrix<T> rightMatrix)
        {
            throw new NotImplementedException();
        }

        public Matrix<T> Subtract(Matrix<T> other)
        {
            throw new NotImplementedException();
        }

        public Matrix<T> Multiply(Matrix<T> other)
        {
            throw new NotImplementedException();
        }

        public Matrix<T> Transpose()
        {
            throw new NotImplementedException();
        }
        
        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Matrix<T> other)
        {
            throw new NotImplementedException();
        }
    }
}
