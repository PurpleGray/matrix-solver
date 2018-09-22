using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MatrixSolver.TestData
{
    public static class TestData
    {
        static readonly Assembly DataAssembly = typeof(TestData).GetTypeInfo().Assembly;
        
        public static Stream ReadStream(string name)
        {
            return DataAssembly.GetManifestResourceStream("MatrixSolver.TestData.TestMatrixes" + name);
        }
        
        public static TextReader ReadText(string name)
        {
            var stream = ReadStream(name);
            return new StreamReader(stream);
        }

        public static string[] ReadAllLines(string name)
        {
            List<string> lines = new List<string>();

            using (TextReader reader = ReadText(name))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }
    }
}
