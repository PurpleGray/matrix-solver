﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;

namespace MatrixSolver.TestData
{
    public static class TestDataReader
    {
        static readonly Assembly DataAssembly = typeof(TestDataReader).GetTypeInfo().Assembly;
        
        public static Stream ReadStream(string name)
        {
            return DataAssembly.GetManifestResourceStream("MatrixSolver.TestData.TestMatrixes." + name);
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

        public static string ReadAllText(string name)
        {
            return String.Join("", ReadAllLines(name));
        }
    }
}