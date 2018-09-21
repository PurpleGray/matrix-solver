using System;
using System.Numerics;

namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix
{
    public abstract class MatrixBuilder<T> where T : struct, IEquatable<T>, IFormattable
    {
        public abstract T Zero { get; }

        public abstract Matrix<T> FromArray(T[,] arr, int rowNumer, int colNumber);

        public abstract Matrix<T> FromFormattedString(string rawString);
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
            
            // Someday need to add other types
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