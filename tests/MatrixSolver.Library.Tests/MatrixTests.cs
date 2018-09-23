using MatrixSolver.Library.Math.LinearAlgebra.Matrix;
using Xunit;

namespace MatrixSolver.Library.Tests
{
    public class MatrixTests
    {
        private string matrixStrSample1 = "2 5 6 99\r\n8 55 6 9\r\n7 8 5 56";
        private string matrixStrSample2 = "59 48 65\r\n59 141 56\r\n5 5 6";
        
        [Fact]
        public void TestMatrixInit()
        {
            var randMtx = Matrix<int>.Build.Random(5, 5, 0, 150);
            var arrMtx = Matrix<int>.Build.FromArray(new int[,]
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
    }
}
