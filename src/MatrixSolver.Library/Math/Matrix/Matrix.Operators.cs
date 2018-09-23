namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix
{
    public abstract partial class Matrix<T>
    {
        public static Matrix<T> operator +(Matrix<T> leftSide, Matrix<T> rightSide)
        {
            return leftSide.Add(rightSide);
        }

        public static Matrix<T> operator -(Matrix<T> leftSide, Matrix<T> rightSide)
        {
            return leftSide.Subtract(rightSide);
        }

        public static Matrix<T> operator *(Matrix<T> leftSide, Matrix<T> rightSide)
        {
            return leftSide.Multiply(rightSide);
        }
    }
}
