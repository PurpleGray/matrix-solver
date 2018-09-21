using System;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix
{
    public abstract class MatrixDataContainer<T> : IEquatable<MatrixDataContainer<T>>
        where T : struct, IEquatable<T>, IFormattable
    {
        protected static readonly T Zero = BuilderInstance<T>.Matrix.Zero;
        
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
        
        public abstract void SetAt(int row, int column, T value);
        
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
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    SetAt(i, j, Zero);
                }
            }
        }

        public virtual T[,] ToArray()
        {
            var retArr = new T[RowCount, ColumnCount];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    retArr[i, j] = GetAt(i, j);
                }
            }
            return retArr;
        }
        
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
            for (int j = 0; j < ColumnCount; j++)
            {
                for (int i = 0; i < RowCount; i++)
                {
                    target.SetAt(i, j, GetAt(i, j));
                }
            }
        }
        
        internal virtual void TransposeTo(MatrixDataContainer<T> target)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                for (int i = 0; i < RowCount; i++)
                {
                    target.SetAt(j, i, GetAt(i, j));
                }
            }
        }

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
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
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

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as MatrixDataContainer<T>);
        }

        public override int GetHashCode()
        {
            var hashNum = System.Math.Min(RowCount*ColumnCount, 25);
            int hash = 17;
            unchecked
            {
                for (var i = 0; i < hashNum; i++)
                {
                    int col;
                    int row = System.Math.DivRem(i, ColumnCount, out col);
                    hash = hash*31 + GetAt(row, col).GetHashCode();
                }
            }
            return hash;
        }
    }
}
