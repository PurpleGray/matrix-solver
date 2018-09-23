using System.Collections.Generic;

namespace MatrixSolver.TestData
{
    public static class MatrixesTestData
    {
        public static Dictionary<string, string[]> TestFiles = new Dictionary<string, string[]>()
        {
            { "1.txt", TestDataReader.ReadAllLines("1.txt")},
            { "2.txt", TestDataReader.ReadAllLines("2.txt")},
            { "3.txt", TestDataReader.ReadAllLines("3.txt")},
            { "4.txt", TestDataReader.ReadAllLines("4.txt")},
        };
    }
}
