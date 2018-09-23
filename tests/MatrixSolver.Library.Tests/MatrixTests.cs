using MatrixSolver.Library.Math.LinearAlgebra.Matrix;
using Xunit;

namespace MatrixSolver.Library.Tests
{
    public class MatrixTests
    {
        private readonly string matrixStrSample1 = "2 5 6 99\r\n8 55 6 9\r\n7 8 5 56";
        private readonly string matrixStrSample2 = "59 48 65\r\n59 141 56\r\n5 5 6";
        private readonly string matrixStrSample3 = "10 2 42\r\n72 13 53\r\n16 39 12";
        private readonly string matrixStrSample4 = "59 48 65\r\n59 141 56\r\n5 5 6\r\n14 28 1";

        /// <summary>
        ///     Result of matrixStrSample2 + matrixStrSample3
        /// </summary>
        private readonly Matrix<int> AdditionResultSample = Matrix<int>.Build.FromArray(new[,]
        {
            {69, 50, 107},
            {131, 154, 109},
            {21, 44, 18}
        });

        /// <summary>
        ///     Result of matrixStrSample2 - matrixStrSample3
        /// </summary>
        private readonly Matrix<int> SubtractionResultSample = Matrix<int>.Build.FromArray(new[,]
        {
            {49, 46, 23},
            {-13, 128, 3},
            {-11, -34, -6}
        });

        /// <summary>
        ///     Result of matrixStrSample1 * matrixStrSample4
        /// </summary>
        private readonly Matrix<int> MultiplyResultSample = Matrix<int>.Build.FromArray(new[,]
        {
            {1829, 3603, 545},
            {3873, 8421, 3645},
            {1694, 3057, 989}
        });

        /// <summary>
        ///     Result of matrixStrSample2 * matrixStrSample3
        /// </summary>
        private readonly Matrix<int> MultiplyResultSample2 = Matrix<int>.Build.FromArray(new[,]
        {
            {5086, 3277, 5802},
            {11638, 4135, 10623},
            {506, 309, 547}
        });

        /// <summary>
        ///     Result of transpose matrixStrSample2
        /// </summary>
        private readonly Matrix<int> TransposedMatrixSample = Matrix<int>.Build.FromArray(new[,]
        {
            {59, 59, 5},
            {48, 141, 5},
            {65, 56, 6}
        });

        [Fact]
        public void TestMatrixInit()
        {
            var randMtx = Matrix<int>.Build.Random(5, 5, 0, 150);
            var arrMtx = Matrix<int>.Build.FromArray(new[,]
            {
                {0, 1, 2},
                {3, 4, 5},
                {6, 7, 8}
            });
            var simpleMtx = Matrix<int>.Build.FromDimensions(7, 7);
            var formattedStrMtx = Matrix<int>.Build.FromFormattedString(matrixStrSample1);


            // Test random mtx
            Assert.Equal(5, randMtx.ColumnCount);
            Assert.InRange(randMtx.GetAt(1, 3), 0, 150);

            // Test arr mtx
            Assert.Equal(3, arrMtx.ColumnCount);
            Assert.Equal(3, arrMtx.RowCount);
            Assert.Equal(4, arrMtx.GetAt(1, 1));

            // Test simple mtx
            Assert.Equal(7, simpleMtx.ColumnCount);
            Assert.Equal(7, simpleMtx.RowCount);

            // Test formatted string matrix
            Assert.Equal(4, formattedStrMtx.ColumnCount);
            Assert.Equal(3, formattedStrMtx.RowCount);
            Assert.Equal(56, formattedStrMtx.GetAt(2, 3));
        }

        [Fact]
        public void TestMatrixOperations()
        {
            var mtx1 = Matrix<int>.Build.FromFormattedString(matrixStrSample1);
            var mtx2 = Matrix<int>.Build.FromFormattedString(matrixStrSample2);
            var mtx3 = Matrix<int>.Build.FromFormattedString(matrixStrSample3);
            var mtx4 = Matrix<int>.Build.FromFormattedString(matrixStrSample4);

            // Test addition
            var addRes = mtx2 + mtx3;
            Assert.True(addRes.Equals(AdditionResultSample));

            // Test subtract
            var subRes = mtx2 - mtx3;
            Assert.True(subRes.Equals(SubtractionResultSample));

            // Test mul with different dimensions matrixes
            var mulRes = mtx1 * mtx4;
            Assert.True(mulRes.Equals(MultiplyResultSample));

            // Test mul with equal dim matrixes
            var mulRes2 = mtx2 * mtx3;
            Assert.True(mulRes2.Equals(MultiplyResultSample2));

            // Test transpose
            var transRes = mtx2.Transpose();
            Assert.True(transRes.Equals(TransposedMatrixSample));
        }
    }
}
