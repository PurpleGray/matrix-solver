using System;
using MatrixSolver.Library.Math.LinearAlgebra.Matrix.Integer;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix
{
    /// <summary>
    /// Matrix builder class which helps to create matrices
    /// </summary>
    public abstract class MatrixBuilder<T> where T : struct, IEquatable<T>, IFormattable
    {
        public abstract T Zero { get; }

        public abstract Matrix<T> FromArray(T[,] arr);

        public abstract Matrix<T> FromFormattedString(string rawString);

        public abstract Matrix<T> FromDimensions(int rowCount, int columnCount);

        public abstract Matrix<T> SameAs(Matrix<T> matrix);

        public abstract Matrix<T> CopyOf(Matrix<T> matrix);

        public abstract Matrix<T> Random(int rowCount, int columnCount, T minVal, T maxVal);
    }

    /// <summary>
    /// BuilderInstance which will return correct MatrixBuilder for provided type
    /// </summary>
    internal static class BuilderInstance<T> where T : struct, IEquatable<T>, IFormattable
    {
        private static Lazy<MatrixBuilder<T>> _singleton =
            new Lazy<MatrixBuilder<T>>(Create);

        public static MatrixBuilder<T> Matrix => _singleton.Value;

        private static MatrixBuilder<T> Create()
        {
            if (typeof(T) == typeof(int))
            {
                return (MatrixBuilder<T>)(object)new MatrixBuilder();
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
    }
}
