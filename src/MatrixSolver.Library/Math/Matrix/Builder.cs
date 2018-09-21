using System;
using System.Numerics;
using MatrixSolver.Library.Math.LinearAlgebra.Matrix.Integer;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix
{
    public abstract class MatrixBuilder<T> where T : struct, IEquatable<T>, IFormattable
    {
        public abstract T Zero { get; }

        public abstract Matrix<T> FromArray(T[,] arr);

        public abstract Matrix<T> FromFormattedString(string rawString);

        public abstract Matrix<T> FromDimensions(int rowCount, int columnCount);

        public abstract Matrix<T> SameAs(Matrix<T> matrix);
    }

    public class MatrixBuilder : MatrixBuilder<int>
    {
        public override int Zero { get; } = 0;
        
        public override Matrix<int> FromArray(int[,] arr)
        {
            return new Integer.Matrix(MatrixDataContainer.OfArray(arr));
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
    
    internal static class BuilderInstance<T> where T : struct, IEquatable<T>, IFormattable
    {
        static Lazy<MatrixBuilder<T>> _singleton =
            new Lazy<MatrixBuilder<T>>(Create);

        static MatrixBuilder<T> Create()
        {
            if (typeof(T) == typeof(int))
            {
                throw new NotImplementedException(); // TODO: implement integer builder
            }
            
            // In further releases implement other types
            throw new NotSupportedException(string.Format(
                "Matrices of type '{0}' are not supported. Only Integer is supported at this point.",
                typeof(T).Name));
        }

        public static void Register(MatrixBuilder<T> matrixBuilder)
        {
            _singleton = new Lazy<MatrixBuilder<T>>(() => matrixBuilder);
        }

        public static MatrixBuilder<T> Matrix
        {
            get { return _singleton.Value; }
        }
    }
}
