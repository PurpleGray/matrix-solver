namespace MatrixSolver.Library.Math.LinearAlgebra.Matrix.Integer
{
    public class Matrix : Matrix<int>
    {
        public Matrix(MatrixDataContainer<int> storage) : base(storage)
        {
        }

        protected override Matrix<int> Add(Matrix<int> other)
        {
            Matrix<int> result = Matrix<int>.Build.FromDimensions(RowCount, ColumnCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    result [i, j] = this[i,j] + other[i,j];
                }
            }

            return result;
        }

        protected override Matrix<int> Subtract(Matrix<int> other)
        {
            Matrix<int> result = Matrix<int>.Build.FromDimensions(RowCount, ColumnCount);

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    result [i, j] = this[i,j] - other[i,j];
                }
            }

            return result;
        }

        protected override Matrix<int> Multiply(Matrix<int> other)
        {
            Matrix<int> resultMatrix = Matrix<int>.Build.FromDimensions(RowCount, other.ColumnCount);
            for (int i = 0; i < resultMatrix.RowCount; i++)
            {
                for (int j = 0; j < resultMatrix.ColumnCount; j++)
                {
                    resultMatrix[i, j] = 0;
                    for (int k = 0; k < ColumnCount; k++)
                    {
                        resultMatrix[i, j] += this[i, k] * other[k, j];
                    }
                }
            }
            return resultMatrix;
        }
    }
}
