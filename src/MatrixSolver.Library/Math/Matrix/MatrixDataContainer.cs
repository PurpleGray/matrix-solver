using System;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix
{
    public abstract class MatrixDataContainer<T> : IEquatable<MatrixDataContainer<T>>
        where T : struct, IEquatable<T>, IFormattable
    {
        protected static readonly T Zero = BuilderInstance<T>.Matrix.Zero;

        public readonly int ColumnCount;

        public readonly int RowCount;

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

        /// <summary>
        /// Gets or sets the value at the given row and column, with range validation
        /// </summary>
        /// <param name="row">
        /// The row of the element
        /// </param>
        /// <param name="column">
        /// The column of the element
        /// </param>
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

        /// <summary>
        /// Retrieves the requested element
        /// </summary>
        /// <param name="row">
        /// The row of the element
        /// </param>
        /// <param name="column">
        /// The column of the element
        /// </param>
        /// <returns>
        /// The requested element
        /// </returns>
        public abstract T GetAt(int row, int column);

        /// <summary>
        /// Sets the element
        /// </summary>
        /// <param name="row">
        /// The row of the element
        /// </param>
        /// <param name="column">
        /// The column of the element
        /// </param>
        public abstract void SetAt(int row, int column, T value);

        /// <summary>
        /// Clear current storage
        /// </summary>
        public virtual void Clear()
        {
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    SetAt(i, j, Zero);
                }
            }
        }

        /// <summary>
        /// Get the two-dimensional array of elements stored in this container
        /// </summary>
        /// <returns>Array of elements</returns>
        public virtual T[,] ToArray()
        {
            var retArr = new T[RowCount, ColumnCount];
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    retArr[i, j] = GetAt(i, j);
                }
            }

            return retArr;
        }

        /// <summary>
        /// Checks that provided indexes is in the correct ranges
        /// </summary>
        /// <param name="row">Requested row</param>
        /// <param name="column">Requested column</param>
        /// <exception cref="ArgumentOutOfRangeException">If not in range</exception>
        protected void ValidateRange(int row, int column)
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

        /// <summary>
        /// Copies all data from this container to another with
        /// range checking
        /// </summary>
        /// <param name="target">Target container</param>
        /// <exception cref="ArgumentNullException">If target is null</exception>
        /// <exception cref="ArgumentException">If dimensions of containers are not the same</exception>
        public void CopyTo(MatrixDataContainer<T> target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            if (ReferenceEquals(this, target))
            {
                return;
            }

            if (RowCount != target.RowCount || ColumnCount != target.ColumnCount)
            {
                var message = $"Matrix dimensions must be the same, first is ({RowCount} x {ColumnCount}), " +
                              $"second is ({target.RowCount} x {target.ColumnCount})";
                throw new ArgumentException(message, "target");
            }

            CopyToUnchecked(target);
        }

        internal virtual void CopyToUnchecked(MatrixDataContainer<T> target)
        {
            for (var j = 0; j < ColumnCount; j++)
            {
                for (var i = 0; i < RowCount; i++)
                {
                    target.SetAt(i, j, GetAt(i, j));
                }
            }
        }

        /// <summary>
        /// Transposes data from current container to another
        /// </summary>
        /// <param name="target">Target container</param>
        internal virtual void TransposeTo(MatrixDataContainer<T> target)
        {
            for (var j = 0; j < ColumnCount; j++)
            {
                for (var i = 0; i < RowCount; i++)
                {
                    target.SetAt(j, i, GetAt(i, j));
                }
            }
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as MatrixDataContainer<T>);
        }
        
        /// <summary>
        /// Checks equality of two containers
        /// </summary>
        /// <param name="other">Other container</param>
        /// <returns>True if data is equals</returns>
        public bool Equals(MatrixDataContainer<T> other)
        {
            if (other == null)
            {
                return false;
            }

            if (ColumnCount != other.ColumnCount || RowCount != other.RowCount)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // Perform element wise comparison.
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    var item = GetAt(i, j);
                    var otherItem = other.GetAt(i, j);
                    if (!item.Equals(otherItem))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashNum = System.Math.Min(RowCount * ColumnCount, 25);
            var hash = 17;
            unchecked
            {
                for (var i = 0; i < hashNum; i++)
                {
                    int col;
                    var row = System.Math.DivRem(i, ColumnCount, out col);
                    hash = hash * 31 + GetAt(row, col).GetHashCode();
                }
            }

            return hash;
        }
    }
}
