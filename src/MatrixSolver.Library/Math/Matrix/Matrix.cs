using System;
using System.Diagnostics;
using System.Text;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix
{
    [DebuggerDisplay("Matrix {RowCount}x{ColumnCount}")]
    public abstract partial class Matrix<T> : IFormattable, IEquatable<Matrix<T>>
        where T : struct, IEquatable<T>, IFormattable
    {
        public static readonly MatrixBuilder<T> Build = BuilderInstance<T>.Matrix;

        protected Matrix(MatrixDataContainer<T> storage)
        {
            Storage = storage;
            RowCount = storage.RowCount;
            ColumnCount = storage.ColumnCount;
        }

        /// <summary>
        /// Storage where all matrix data is hidden :)
        /// </summary>
        public MatrixDataContainer<T> Storage { get; }

        public int ColumnCount { get; }

        public int RowCount { get; }

        /// <summary>
        /// Data accessor by row and column indexes, with range checking
        /// </summary>
        /// <param name="row">Requested row</param>
        /// <param name="column">Requested column</param>
        public T this[int row, int column]
        {
            get => Storage[row, column];
            set => Storage[row, column] = value;
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

        /// <summary>
        /// Make a copy of current matrix
        /// </summary>
        /// <returns>Full copy (data too) of current matrix</returns>
        public Matrix<T> Clone()
        {
            var result = Build.SameAs(this);
            Storage.CopyTo(result.Storage);
            return result;
        }
        
        /// <summary>
        /// Transpose current matrix
        /// </summary>
        /// <returns></returns>
        public Matrix<T> Transpose()
        {
            var result = Build.FromDimensions(ColumnCount, RowCount);
            Storage.TransposeTo(result.Storage);
            return result;
        }

        protected abstract Matrix<T> Add(Matrix<T> other);

        protected abstract Matrix<T> Subtract(Matrix<T> other);

        protected abstract Matrix<T> Multiply(Matrix<T> other);

        public bool Equals(Matrix<T> other)
        {
            return other != null && Storage.Equals(other.Storage);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }
        
        /// <summary>
        /// Formatting matrix to string representation
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            var strBuilder = new StringBuilder();

            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    strBuilder.Append($"{GetAt(i, j)} ");
                }

                strBuilder.Append("\r\n");
            }

            return strBuilder.ToString();
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
