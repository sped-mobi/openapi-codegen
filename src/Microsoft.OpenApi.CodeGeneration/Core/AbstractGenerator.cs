﻿using System;

namespace Microsoft.OpenApi.CodeGeneration
{
    public abstract class AbstractGenerator
    {
        private readonly IndentedWriter _writer;

        protected AbstractGenerator(GeneratorDependencies dependencies)
        {
            _writer = new IndentedWriter(dependencies.Provider.Builder);
            Dependencies = dependencies;
        }

        public GeneratorDependencies Dependencies { get; }

        protected void PushIndent()
        {
            _writer.PushIndent();
        }

        protected void PopIndent()
        {
            _writer.PopIndent();
        }

        protected void Write(string value)
        {
            _writer.Write(value);
        }

        protected void WriteLine(string value)
        {
            _writer.WriteLine(value);
        }

        protected void WriteLine()
        {
            _writer.WriteLine();
        }

        protected void Clear()
        {
            Dependencies.Provider.Builder.Clear();
        }

        protected string GetText()
        {
            return Dependencies.Provider.Builder.ToString();
        }

        protected IDisposable OpenBlock()
        {
            return _writer.OpenBlock();
        }

        protected IDisposable OpenBlockSemicolon()
        {
            return _writer.OpenBlockSemicolon();
        }

        protected IDisposable OpenBlockString(string expression)
        {
            return _writer.OpenBlockString(expression);
        }

        protected IDisposable OpenProjectBlock()
        {
            return _writer.OpenProjectBlock();
        }

        protected IDisposable OpenPropertyGroupBlock()
        {
            return _writer.OpenPropertyGroupBlock();
        }

        protected IDisposable OpenItemGroupBlock()
        {
            return _writer.OpenItemGroupBlock();
        }



        protected virtual void GenerateFileHeader()
        {
            WriteLine("//------------------------------------------------------------------------------");
            WriteLine("// <auto-generated>");
            WriteLine("//     This code was generated by a tool.");
            WriteLine("//     Runtime Version:4.0.30319.42000");
            WriteLine("//");
            WriteLine($"//     Generation Time: {DateTime.Now.ToLongDateString()} @ {DateTime.Now.TimeOfDay}");
            WriteLine("//");
            WriteLine("//     Changes to this file may cause incorrect behavior and will be lost if");
            WriteLine("//     the code is regenerated.");
            WriteLine("// </auto-generated>");
            WriteLine("//------------------------------------------------------------------------------");
            WriteLine("//");
            WriteLine("using System;");
            WriteLine("using System.Linq;");
            WriteLine("using System.Threading;");
            WriteLine("using System.Diagnostics;");
            WriteLine("using System.Threading.Tasks;");
            WriteLine("using System.Collections.Generic;");
        }
    }
}
