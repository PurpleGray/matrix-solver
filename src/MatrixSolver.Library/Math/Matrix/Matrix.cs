using System;
using System.Text;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix
{
    public abstract partial class Matrix<T> : IFormattable, IEquatable<Matrix<T>>
                                    where T : struct, IEquatable<T>, IFormattable
    {
        protected Matrix(MatrixDataContainer<T> storage)
        {
            Storage = storage;
            RowCount = storage.RowCount;
            ColumnCount = storage.ColumnCount;
        }
        
        public MatrixDataContainer<T> Storage { get; private set; }
        
        public static readonly MatrixBuilder<T> Build = BuilderInstance<T>.Matrix;
        
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

        public Matrix<T> Clone()
        {
            var result = Build.SameAs(this);
            Storage.CopyTo(result.Storage);
            return result;
        }

        protected abstract Matrix<T> Add(Matrix<T> other);

        protected abstract Matrix<T> Subtract(Matrix<T> other);

        protected abstract Matrix<T> Multiply(Matrix<T> other);

        public Matrix<T> Transpose()
        {
            var result = Build.SameAs(this);
            Storage.TransposeTo(result.Storage);
            return result;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    strBuilder.Append($"{GetAt(i, j)} ");
                }

                strBuilder.Append('\n');
            }

            return strBuilder.ToString();
        }

        public bool Equals(Matrix<T> other)
        {
            return other != null && Storage.Equals(other.Storage);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Matrix<T>;
            return other != null && Storage.Equals(other.Storage);
        }

        public override int GetHashCode()
        {
            return Storage.GetHashCode();
        }
    }
}
